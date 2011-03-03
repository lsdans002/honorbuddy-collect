using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Styx.Database;
using Styx.Helpers;
using Styx.Logic;
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
    public class ForceTrain : CustomForcedBehavior
    {
        

        /// <summary>
        /// ForceTrain by Natfoth
        /// Forces the bot to train
        /// ##Syntax##
        /// QuestId: Id of the quest.
        /// ForceTrain: Not needed but may mess up the args.
        /// </summary>
        /// 

        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {

            {"ForceTrain",null},
            {"QuestId",null},

        };

        bool success = true;

        public ForceTrain(Dictionary<string, string> args)
            : base(args)
        {

            CheckForUnrecognizedAttributes(recognizedAttributes);

            int chattype = 0;
            int questId = 0;
            
            success = success && GetAttributeAsInteger("ForceTrain", false, "1", 0, int.MaxValue, out chattype);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);

            QuestId = (uint)questId;
            Counter = 0;
            ChatType = chattype;
        }

        public int Counter { get; set; }
        public int ChatType { get; set; }
        public uint QuestId { get; set; }

        public static LocalPlayer me = ObjectManager.Me;

        #region Overrides of CustomForcedBehavior

        public override void OnStart()
        {
            PlayerQuest quest = StyxWoW.Me.QuestLog.GetQuestById(QuestId);

            if (quest != null)
            {
                TreeRoot.GoalText = "ForceTrain - " + quest.Name;
            }
            else
            {
                TreeRoot.GoalText = "ForceTrain: Running";
            }
        }

        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ?? (_root =
                new PrioritySelector(

                            new Decorator(ret => (Counter > 0) || (me.QuestLog.GetQuestById(QuestId) != null && me.QuestLog.GetQuestById(QuestId).IsCompleted),
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
                                        new Action(ret => TreeRoot.StatusText = "Training"),
                                        new Action(ret => LevelbotSettings.Instance.FindVendorsAutomatically = true),
                                        new Action(ret => Vendors.ForceTrainer = true),
                                        new Action(ret => Counter++)
                                    )
                                )
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
