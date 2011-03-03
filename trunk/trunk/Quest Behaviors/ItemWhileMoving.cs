using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Styx.Database;
using Styx.Helpers;
using Styx.Logic.Inventory.Frames.Gossip;
using Styx.Logic.Pathing;
using Styx.Logic.Questing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using TreeSharp;
using Styx.Logic.BehaviorTree;
using Action = TreeSharp.Action;

namespace Styx.Bot.Quest_Behaviors
{
    public class ItemWhileMoving : CustomForcedBehavior
    {

        /// <summary>
        /// ItemWhileMoving by Natfoth
        /// Will use the same item over and over until a location is reached.
        /// ##Syntax##
        /// QuestId: Id of the quest.
        /// ItemId: Item to use Over and Over until Location is reached.
        /// X,Y,Z: The general location where theese objects can be found
        /// </summary>
        /// 

        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {

            {"ItemId",null},
            {"X",null},
            {"Y",null},
            {"Z",null},
            {"QuestId",null},

        };

        bool success = true;


        public ItemWhileMoving(Dictionary<string, string> args)
            : base(args)
        {

            CheckForUnrecognizedAttributes(recognizedAttributes);

            int itemId = 0;
            int questId = 0;
            WoWPoint location = new WoWPoint(0, 0, 0);

            success = success && GetAttributeAsInteger("ItemId", true, "1", 0, int.MaxValue, out itemId);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);
            success = success && GetXYZAttributeAsWoWPoint("X", "Y", "Z", true, new WoWPoint(0, 0, 0), out location);

            TreeRoot.GoalText = "ItemWhileMoving: Running";

            QuestId = (uint)questId;
            ItemID = itemId;
            Counter = 0;
            MovedToTarget = false;
            Location = location;
        }

        public WoWPoint Location { get; private set; }
        public int Counter { get; set; }
        public int ItemID { get; set; }
        public int NPCID { get; set; }
        public bool MovedToTarget;
        public int NumberOfTimes { get; set; }
        public uint QuestId { get; set; }

        public WoWPoint[] Path { get; private set; }
        int pathIndex = 0;


        public static LocalPlayer me = ObjectManager.Me;

        public WoWItem wowItem
        {
            get
            {
                List<WoWItem> inventory = ObjectManager.GetObjectsOfType<WoWItem>(false);

                foreach (WoWItem item in inventory)
                {
                    if (item.Entry == ItemID)
                        return item;
                }

                return inventory[0];
            }
        }

        #region Overrides of CustomForcedBehavior

        public override void OnStart()
        {
            PlayerQuest quest = StyxWoW.Me.QuestLog.GetQuestById(QuestId);

            if (quest != null)
            {
                TreeRoot.GoalText = "ItemWhileMoving - " + quest.Name;
            }
            else
            {
                TreeRoot.GoalText = "ItemWhileMoving: Running";
            }
        }

        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ?? (_root =
                new PrioritySelector(

                            new Decorator(ret => (Counter > NumberOfTimes) || (me.QuestLog.GetQuestById(QuestId) != null && me.QuestLog.GetQuestById(QuestId).IsCompleted),
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
                                    new Action(ret => TreeRoot.StatusText = "Moving To Location: Using Item - " + wowItem.Name),
                                    new Action(ret => WoWMovement.ClickToMove(moveToLocation)),
                                    new Action(ret => wowItem.Interact()),
                                    new Action(ret => Thread.Sleep(300))
                                    ))
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


        private bool _isDone;
        public override bool IsDone
        {
            get { return _isDone; }
        }

        #endregion
    }
}
