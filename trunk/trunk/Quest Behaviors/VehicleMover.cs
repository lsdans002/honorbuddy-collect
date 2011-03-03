using System.Collections.Generic;
using System.Linq;
using Styx.Helpers;
using Styx.Logic.BehaviorTree;
using Styx.Logic.Pathing;
using Styx.Logic.Questing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using TreeSharp;
using System.Diagnostics;
using Styx.Logic.Combat;

using Action = TreeSharp.Action;

namespace Styx.Bot.Quest_Behaviors
{
    /// <summary>
    /// VehicleMover by HighVoltz
    /// Moves to location while in a vehicle
    /// ##Syntax##
    /// VehicleID: ID of the vehicle
    /// VehicleID2: (optional) ID of  vehicle , usefully if a vehicle has multiple ids
    /// VehicleID3: (optional) ID of  vehicle , usefully if a vehicle has multiple ids
    /// UseNavigator: (optional) true/false. Setting to false will use Click To Move instead of the Navigator. Default true
    /// Precision: (optional) This behavior moves on to the next waypoint when at Precision distance or less to current waypoint. Default 4;
    /// MobID: (optional) NPC ID to cast spell on to cast spell on.. not required even if you specify a spellID
    /// SpellID: (optional) Casts spell after reaching location.
    /// CastTime: (optional) The Spell Cast Time. Default 0;
    /// IgnoreCombat: (optional) true/false. Setting to true will keep running the behavior even if in combat.Default true
    /// Hop : (optional) true/false. Setting this to true will cause the bot to jump while its moving to location.. this serves as an anti-stuck mechanic Default: false
    /// X,Y,Z: The location where you want to move to
    /// </summary>
    public class VehicleMover : CustomForcedBehavior
    {
        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {
            {"VehicleID",null},
            {"VehicleID2",null},
            {"VehicleID3",null},
            {"UseNavigator",null},
            {"Precision",null},
            {"MobID",null},
            {"SpellID",null},
            {"CastTime",null},
            {"Hop",null},
            {"X",null},
            {"Y",null},
            {"Z",null},
        };
        bool success = true;
        public VehicleMover(Dictionary<string, string> args)
            : base(args)
        {
            // tba. dictionary format is not documented.
            // CheckForUnrecognizedAttributes(recognizedAttributes);
            int vehicleID = 0;
            bool useNavigator = true;
            bool hop = false;
            bool ignoreCombat = false;
            int precision = 0;
            int mobID = 0;
            int spellID = 0;
            int castTime = 0;
            WoWPoint point = WoWPoint.Empty;

            success = success && GetAttributeAsInteger("VehicleID", true, "0", 0, int.MaxValue, out vehicleID);
            VehicleID = vehicleID;
            success = success && GetAttributeAsInteger("VehicleID2", false, "0", 0, int.MaxValue, out vehicleID);
            VehicleID2 = vehicleID;
            success = success && GetAttributeAsInteger("VehicleID3", false, "0", 0, int.MaxValue, out vehicleID);
            VehicleID3 = vehicleID;
            success = success && GetAttributeAsBoolean("UseNavigator", false, "true", out useNavigator);
            success = success && GetAttributeAsInteger("Precision", false, "4", 2, int.MaxValue, out precision);
            success = success && GetAttributeAsInteger("MobID", false, "0", 0, int.MaxValue, out mobID);
            success = success && GetAttributeAsInteger("SpellID", false, "0", 0, int.MaxValue, out spellID);
            success = success && GetAttributeAsInteger("CastTime", false, "0", 0, int.MaxValue, out castTime);
            success = success && GetAttributeAsBoolean("IgnoreCombat", false, "true", out ignoreCombat);
            success = success && GetAttributeAsBoolean("Hop", false, "false", out hop);
            success = success && GetXYZAttributeAsWoWPoint("X", "Y", "Z", true, WoWPoint.Empty, out point);

            Precision = precision;
            SpellID = spellID;
            CastTime = castTime;
            UseNavigator = useNavigator;
            IgnoreCombat = ignoreCombat;
            Hop = hop;
            MobID = mobID;
            Location = point;
        }

        public int VehicleID { get; private set; }
        public int VehicleID2 { get; private set; }
        public int VehicleID3 { get; private set; }
        public int Precision { get; private set; }
        public int MobID { get; private set; }
        public int SpellID { get; private set; }
        public int CastTime { get; private set; }
        public bool UseNavigator { get; private set; }
        public bool Hop { get; private set; }
        public bool IgnoreCombat { get; private set; }
        public WoWPoint Location { get; private set; }
        public WoWPoint[] Path { get; private set; }
        Stopwatch pauseSW = new Stopwatch();// add a small pause before casting.. 
        Stopwatch castSW = new Stopwatch();// cast timer.
        bool casted = false;
        int pathIndex = 0;
        bool InVehicle
        {
            get { return Lua.GetReturnVal<bool>("return UnitInVehicle('player')", 0); }
        }
        public WoWObject Vehicle
        {
            get
            {
                return ObjectManager.GetObjectsOfType<WoWObject>(true).Where(o => o.Entry == VehicleID ||
                    (VehicleID2 > 0 && o.Entry == VehicleID2) || (VehicleID3 > 0 && o.Entry == VehicleID3)).
                    OrderBy(o => o.Distance).FirstOrDefault();
            }
        }
        #region Overrides of CustomForcedBehavior
        private Composite root;
        protected override Composite CreateBehavior()
        {
            return root ??
                (root = new PrioritySelector(
                    new Decorator(c => !ObjectManager.Me.IsAlive, // if we ignore combat and die... 
                        new Action(c =>
                        {
                            return RunStatus.Failure;
                        })),
                    new Decorator(c => success == false,
                        new Action(c =>
                        {
                            Err("Invalid or missing Attributes, Stopping HB");
                        })),
                    new Decorator(c => Vehicle == null,
                        new Action(c =>
                        {
                            Err("No Vehicle matching ID was found, Stopping HB");
                            return RunStatus.Failure;
                        })),
                    new Action(c =>
                    {
                        if (!InVehicle)
                        {
                            Log("Moving to Vehicle {0}", Vehicle.Name);
                            if (!Vehicle.WithinInteractRange)
                                Navigator.MoveTo(Vehicle.Location);
                            else
                                Vehicle.Interact();
                            if (IgnoreCombat)
                                return RunStatus.Running;
                            else
                                return RunStatus.Success;
                        }
                        return RunStatus.Failure;
                    }),
                    new Action(c =>
                    {
                        if (UseNavigator && Path == null)
                        {
                            Log("Genoratoring Path", Vehicle.Name);
                            Path = Navigator.GeneratePath(Vehicle.Location, Location);
                            if (Path == null || Path.Length == 0)
                                Err("Unable to genorate path to {0}\nStoping HB.", Location);
                        }
                        if (IgnoreCombat)
                            return RunStatus.Failure; // move to next action 
                        else
                            return RunStatus.Success;

                    }),
                    new Action(c =>
                    {
                        if (Vehicle.Location.Distance(Location) > Precision || ObjectManager.Me.Dead)
                        {
                            WoWMovement.ClickToMove(moveToLocation);
                            if (IgnoreCombat)
                                return RunStatus.Running;
                            else
                                return RunStatus.Success;
                        }
                        return RunStatus.Failure;
                    }),
                    new Decorator(c => Vehicle.Location.Distance(Location) <= Precision,
                        CreateSpellBehavior)
               ));
        }

        Composite CreateSpellBehavior
        {
            get
            {
                return new Action(c =>
                {
                    if (SpellID > 0)
                    {
                        if (!casted)
                        {
                            if (!pauseSW.IsRunning)
                                pauseSW.Start();
                            if (pauseSW.ElapsedMilliseconds >= 1000)
                            {
                                if (ObjectManager.Me.IsMoving)
                                {
                                    WoWMovement.MoveStop();
                                    if (IgnoreCombat)
                                        return RunStatus.Running;
                                    else
                                        return RunStatus.Success;
                                }

                                // getting a "Spell not learned" error if using HB's spell casting api..
                                Lua.DoString("CastSpellByID({0})", SpellID);
                                casted = true;
                                pauseSW.Stop();
                                pauseSW.Reset();
                                if (CastTime == 0)
                                    isDone = true;
                                castSW.Start();
                            }
                            if (IgnoreCombat)
                                return RunStatus.Running;
                            else
                                return RunStatus.Success; ;
                        }
                        else if (castSW.ElapsedMilliseconds < CastTime)
                        {
                            if (IgnoreCombat)
                                return RunStatus.Running;
                            else
                                return RunStatus.Success; ;
                        }
                    }
                    castSW.Stop();
                    castSW.Reset();
                    isDone = true;
                    if (IgnoreCombat)
                        return RunStatus.Running;
                    else
                        return RunStatus.Success;
                });
            }
        }

        WoWPoint moveToLocation
        {
            get
            {
                if (MobID > 0)
                {
                    // target mob and move to it 
                    WoWUnit mob = ObjectManager.GetObjectsOfType<WoWUnit>(true).Where(o => o.Entry == MobID).
                        OrderBy(o => o.Distance).FirstOrDefault();
                    if (mob != null)
                    {
                        if (!ObjectManager.Me.GotTarget || ObjectManager.Me.CurrentTarget != mob)
                            mob.Target();
                        if (mob.Location.Distance(Location) > 4)
                        {
                            Location = mob.Location;
                            Path = Navigator.GeneratePath(Vehicle.Location, Location);
                            pathIndex = 0;
                            if (Path == null || Path.Length == 0)
                                UseNavigator = false;
                        }
                    }
                }
                if (Hop)
                {
                    WoWMovement.Move(WoWMovement.MovementDirection.JumpAscend);
                    WoWMovement.MoveStop(WoWMovement.MovementDirection.JumpAscend);
                }
                if (UseNavigator)
                {
                    if (Vehicle.Location.Distance(Path[pathIndex]) <= Precision && pathIndex < Path.Length - 1)
                        pathIndex++;
                    return Path[pathIndex];
                }
                else
                    return Location;
            }
        }

        void Err(string format, params object[] args)
        {
            Logging.Write(System.Drawing.Color.Red, "VehicleMover: " + format, args);
            TreeRoot.Stop();
        }

        void Log(string format, params object[] args)
        {
            Logging.Write("VehicleMover: " + format, args);
        }

        private bool isDone = false;

        public override bool IsDone { get { return isDone; } }

        public override void OnStart()
        {
            TreeRoot.GoalText = string.Format("Moving to:{0} while in Vehicle with ID {1} using {2}",
                Location, VehicleID, UseNavigator ? "Navigator" : "Click-To-Move");
        }

        #endregion
    }
}