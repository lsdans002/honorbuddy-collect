using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Styx.Database;
using Styx.Logic.Combat;
using Styx.Helpers;
using Styx.Logic.Inventory.Frames.Gossip;
using Styx.Logic.Pathing;
using Styx.Logic.Profiles.Quest.Order;
using Styx.Logic.Questing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using TreeSharp;
using Styx.Logic.BehaviorTree;
using Action = TreeSharp.Action;

namespace Styx.Bot.Quest_Behaviors
{
    public class NoCombatMoveTo : CustomForcedBehavior
    {

        /// <summary>
        /// NoCombatMoveTo by Natfoth
        /// Allows you to move to a specific target with engaging in Combat, to avoid endless combat loops.
        /// ##Syntax##
        /// QuestId: Id of the quest.
        /// X,Y,Z: Where you want to go to.
        /// </summary>
        /// 

        #region Overrides of CustomForcedBehavior

        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {

            {"X",null},
            {"Y",null},
            {"Z",null},
            {"QuestId",null},

        };

        bool success = true;

        public NoCombatMoveTo(Dictionary<string, string> args)
            : base(args)
        {
            CheckForUnrecognizedAttributes(recognizedAttributes);

            WoWPoint location = new WoWPoint(0, 0, 0);
            int questId = 0;

            success = success && GetXYZAttributeAsWoWPoint("X", "Y", "Z", true, new WoWPoint(0, 0, 0), out location);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);

            QuestId = (uint)questId;
            Location = location;

            Counter = 0;
        }

        public WoWPoint Location { get; private set; }
        public int Counter { get; set; }
        public uint QuestId { get; set; }

        public WoWPoint[] Path { get; private set; }
        int pathIndex = 0;

        public static LocalPlayer me = ObjectManager.Me;

        public override void OnStart()
        {
            PlayerQuest quest = StyxWoW.Me.QuestLog.GetQuestById(QuestId);

            if (quest != null)
            {
                TreeRoot.GoalText = "NoCombatMoveTo - " + quest.Name;
            }
            else
            {
                TreeRoot.GoalText = "NoCombatMoveTo: Running";
            }
        }

        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ?? (_root =
                new PrioritySelector(

                            new Decorator(ret => Location.Distance(me.Location) <= 3,
                                new Sequence(
                                    new Action(ret => TreeRoot.StatusText = "Finished!"),
                                    new WaitContinue(120,
                                        new Action(delegate
                                        {
                                            _isDone = true;
                                            
                                            return RunStatus.Success;
                                        }))
                                    )),

                            new Decorator(ret => Location.Distance(me.Location) > 3,
                                new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "Moving To Location - X: " + Location.X + " Y: " + Location.Y + " Z: " + Location.Z),
                                        new Decorator(c => !me.Dead, MoveBehavior)
                                    )
                                )
                    ));
        }

        WoWPoint moveToLocation
        {
            get
            {

                Path = Navigator.GeneratePath(me.Location, Location);
                pathIndex = 0;

                while (Path[pathIndex].Distance(me.Location) <= 3 && pathIndex < Path.Length - 1)
                    pathIndex++;
                return Path[pathIndex];

            }
        }

        Composite MoveBehavior
        {
            get
            {
                return new Action(c =>
                {
                    Path = Navigator.GeneratePath(me.Location, Location);

                    while (Path[pathIndex].Distance(me.Location) <= 1 && pathIndex < Path.Length - 1)
                        pathIndex++;

                    if (Location.Distance(me.Location) <= 3)
                    {
                        WoWMovement.ClickToMove(Path[pathIndex]);
                        return RunStatus.Success;
                    }
                    else
                    {
                        WoWMovement.ClickToMove(Path[pathIndex]);
                        return RunStatus.Running;
                    }

                });
            }
        }

        private bool _isDone;
        public override bool IsDone
        {
            get { return _isDone; }
        }

        #endregion
    }
}

