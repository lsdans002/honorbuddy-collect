using System.Collections.Generic;
using System.Linq;
using Styx.Helpers;
using Styx.Logic.BehaviorTree;
using Styx.Logic.Pathing;
using Styx.Logic.Questing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using TreeSharp;
using Styx.Logic;

using Action = TreeSharp.Action;

namespace Styx.Bot.Quest_Behaviors
{
    /// <summary>
    /// WaitForPatrol by HighVoltz
    /// Waits at a safe location until an NPC is X distance way from you.. Useful for the quest in dk starter area where you have to ninja a horse but have to stay away from the stable master
    /// ##Syntax##
    /// MobID: This is the ID of the bad boy you want to stay clear of 
    /// QuestId: (Optional) The Quest to perform this behavior on
    /// Distance: The Distance to stay away from 
    /// X,Y,Z: The Safe Location location where you want wait at.
    /// </summary>
    /// 
    public class WaitForPatrol : CustomForcedBehavior
    {
        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {
            {"MobId",null},
            {"Distance",null},
            {"QuestId",null},
            {"X",null},
            {"Y",null},
            {"Z",null},
        };
        bool success = true;
        public WaitForPatrol(Dictionary<string, string> args)
            : base(args)
        {
            // tba. dictionary format is not documented.
            CheckForUnrecognizedAttributes(recognizedAttributes);
            int mobID = 0;
            int distance = 0;
            int questId= 0;
            WoWPoint point = WoWPoint.Empty;

            success = success && GetAttributeAsInteger("MobId", true, "0", 0, int.MaxValue, out mobID);
            success = success && GetAttributeAsInteger("Distance", true, "0", 0, int.MaxValue, out distance);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);
            success = success && GetXYZAttributeAsWoWPoint("X", "Y", "Z", true, WoWPoint.Empty, out point);

            if (!success)
                Err("Error loading Profile\nStoping HB");
            MobID = mobID;
            QuestId = questId;
            Distance = distance;
            Location = point;
        }

        public int MobID { get; private set; }
        public int Distance { get; private set; }
        public int QuestId { get; private set; }
        public WoWPoint Location { get; private set; }

        public WoWObject Npc
        {
            get
            {
                return ObjectManager.GetObjectsOfType<WoWUnit>(true).Where(o => o.Entry == MobID).
                    OrderBy(o => o.Distance).FirstOrDefault();
            }
        }

        LocalPlayer Me { get { return StyxWoW.Me; } }

            #region Overrides of CustomForcedBehavior
        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ??(_root = 
                new PrioritySelector(
                    new Decorator(c => ObjectManager.Me.Combat || ObjectManager.Me.Dead,
                        new Action(c => RunStatus.Failure)),

                    new Decorator(c => Me.Location.Distance(Location) > 4,
                        new Action(c =>
                        {
                            if (!Me.Mounted && Mount.CanMount() && LevelbotSettings.Instance.UseMount && 
                                Me.Location.Distance(Location) > 35)
                            {
                                if (ObjectManager.Me.IsMoving)
                                {
                                    WoWMovement.MoveStop();
                                }
                                Mount.MountUp();
                            }
                            else
                            {
                                Navigator.MoveTo(Location);
                            }
                        })),
                    new Decorator(c => Npc != null && Npc.Distance <= Distance,
                        new Action(c =>
                        {
                            Log("Waiting on {0} to move {1} distance away", Npc, Distance);
                        })),
                    new Decorator(c => Npc == null|| Npc.Distance > Distance,
                        new Action(c =>
                        {
                            _isDone = true;
                        }))
                ));
        }


        void Err(string format, params object[] args)
        {
            Logging.Write(System.Drawing.Color.Red, "WaitForPatrol: " + format, args);
            TreeRoot.Stop();
        }

        void Log(string format, params object[] args)
        {
            Logging.Write("WaitForPatrol: " + format, args);
        }

        private bool _isDone;
        public override bool IsDone
        {
            get
            {
                var quest = ObjectManager.Me.QuestLog.GetQuestById((uint)QuestId);
                return _isDone || (QuestId > 0 && ((quest != null && quest.IsCompleted) || quest == null));
            }
        }

        public override void OnStart()
        {
            TreeRoot.GoalText = string.Format("Moving to safepoint {0} then waiting there until Npc {1} moves {2} distance away",Location,MobID,Distance);
        }

        #endregion
    }
}
