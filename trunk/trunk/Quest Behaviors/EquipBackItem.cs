using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Styx.Database;
using Styx.Logic.Combat;
using Styx.Helpers;
using Styx.Logic.Inventory.Frames.Gossip;
using Styx.Logic.Inventory;
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
    public class EquipBackItem : CustomForcedBehavior
    {

        /// <summary>
        /// EquipBackItem by Natfoth
        /// This is used for a few specific quests for my worgen profile but can be used for anything with a back piece
        /// ##Syntax##
        /// ItemID: The Item ID of the item you wish to equip
        /// QuestId: Id of the quest.
        /// </summary>
        /// 
        

        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {

            {"ItemID",null},
            {"QuestId",null},

        };

        bool success = true;

        public EquipBackItem(Dictionary<string, string> args)
            : base(args)
        {

            CheckForUnrecognizedAttributes(recognizedAttributes);

            int itemID = 0;
            int questId = 0;

            success = success && GetAttributeAsInteger("ItemID", true, "1", 0, int.MaxValue, out itemID);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);

            Counter = 0;
            ItemID = itemID;


        }

        public WoWPoint MovePoint { get; private set; }
        public int Counter { get; set; }
        public int ItemID { get; set; }

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
                TreeRoot.GoalText = "EquipBackItem: Running";
        }

        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ?? (_root =
                new PrioritySelector(

                    new Decorator(ret => Counter > 0,
                                new Sequence(
                                    new Action(ret => TreeRoot.StatusText = "Finished!"),
                                    new WaitContinue(120,
                                        new Action(delegate
                                        {
                                            _isDone = true;
                                            return RunStatus.Success;
                                        }))
                                    )),

                           new Decorator(ret => Counter == 0,
                                new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "Equiping Item - " + wowItem.Name),
                                        new Action(ret => Lua.DoString("EquipItemByName({0}, {1})", ItemID, InventorySlot.BackSlot)),
                                        new Action(ret => Thread.Sleep(300)),
                                        new Action(ret => Counter++)
                                    ))
                    ));
        }

        private bool _isDone;
        public override bool IsDone
        {
            get { return _isDone; }
        }

        #endregion
    }
}

