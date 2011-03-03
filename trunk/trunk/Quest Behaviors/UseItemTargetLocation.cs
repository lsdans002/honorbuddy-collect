using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Styx.Logic.Combat;
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
using Timer = Styx.Helpers.WaitTimer;

namespace Styx.Bot.Quest_Behaviors
{
    public class UseItemTargetLocation : CustomForcedBehavior
    {
        /// <summary>
        /// UseItemTargetLocation by Natfoth
        /// For those Pesky Quests where you have to use an item then target a location using the Targeting Circle.
        /// ##Syntax##
        /// QuestId: If you have a questID and it is > 0 then it will do the behavior over and over until the quest is complete, otherwise it will use NumOfTimes.
        /// ItemId: The Item you wish to use / Throw.
        /// MinRange: The min distance you are allowed to throw the item.
        /// WaitTime: How long to wait after thrown.
        /// NPCID: If you want to use an Mob Location rather then Target Coords.
        /// GameObjectID: If you want to use an Object Location rather then Target Coords.
        /// MoveX,MoveY,MoveZ: Where you will throw the item from. ****Do not use this arg if you have NpcID or GameObjectID > 0****
        /// TargetX,TargetY,TargetZ: Where you will Target the ground to throw. ****Do not use this arg if you have NpcID or GameObjectID > 0****
        /// X,Y,Z: The general location where these objects can be found
        /// </summary>
        /// 

        Dictionary<string, object> recognizedAttributes = new Dictionary<string, object>()
        {

            {"ItemId",null},
            {"MinRange",null},
            {"WaitTime",null},
            {"NpcID",null},
            {"GameObjectID",null},
            {"QuestId",null},
            {"X",null},
            {"Y",null},
            {"Z",null},
            {"MoveX",null},
            {"MoveY",null},
            {"MoveZ",null},
            {"TargetX",null},
            {"TargetY",null},
            {"TargetZ",null},
         
        };

        bool success = true;
        public Timer _timer;


        public UseItemTargetLocation(Dictionary<string, string> args)
            : base(args)
        {
            CheckForUnrecognizedAttributes(recognizedAttributes);

            int itemId = 0;
            int minrange = 0;
            int waittime = 0;
            int useGO = 0;
            int useNPC = 0;
            int npcID = 0;
            int useNpcLocation = 0;
            int goID = 0;
            int questId = 0;
            WoWPoint movelocation = new WoWPoint(0, 0, 0);
            WoWPoint targetlocation = new WoWPoint(0, 0, 0);
            
            success = success && GetAttributeAsInteger("ItemId", true, "1", 0, int.MaxValue, out itemId);
            success = success && GetAttributeAsInteger("MinRange", false, "1", 0, int.MaxValue, out minrange);
            success = success && GetAttributeAsInteger("WaitTime", true, "1", 0, int.MaxValue, out waittime);
            success = success && GetAttributeAsInteger("NpcID", false, "0", 0, int.MaxValue, out npcID);
            success = success && GetAttributeAsInteger("GameObjectID", false, "0", 0, int.MaxValue, out goID);
            success = success && GetAttributeAsInteger("QuestId", false, "0", 0, int.MaxValue, out questId);

            if (npcID > 0 || goID > 0)
            {

                
                success = success && GetXYZAttributeAsWoWPoint("X", "Y", "Z", true, new WoWPoint(0, 0, 0), out movelocation);

                MoveLocation = movelocation;
            }
            else
            {
                success = success && GetXYZAttributeAsWoWPoint("MoveX", "MoveY", "MoveZ", true, new WoWPoint(0, 0, 0), out movelocation);
                success = success && GetXYZAttributeAsWoWPoint("TargetX", "TargetY", "TargetZ", true, new WoWPoint(0, 0, 0), out targetlocation);

                MoveLocation = movelocation;
                TargetLocation = targetlocation;
            }


            QuestId = (uint)questId;
            WaitTime = waittime;
            ItemID = itemId;
            MinRange = minrange;
            UseGO = useGO;
            UseNPC = useNPC;
            NpcID = npcID;
            ObjectID = goID;
            Counter = 0;
            UseNPCLocation = useNpcLocation;
            MovedToTarget = false;
            
        }

        public WoWPoint MoveLocation { get; private set; }
        public WoWPoint TargetLocation { get; private set; }
        public int Counter { get; set; }
        public int UseNPCLocation { get; set; }
        public int ItemID { get; set; }
        public int MinRange { get; set; }
        public int WaitTime { get; set; }
        public int UseGO { get; set; }
        public int UseNPC { get; set; }
        public int NpcID { get; set; }
        public int ObjectID { get; set; }
        public bool MovedToTarget;
        public uint QuestId { get; set; }

        public static LocalPlayer me = ObjectManager.Me;
        public List<ulong> usedGUID = new List<ulong>();

        public List<WoWGameObject> objectList
        {
            get
            {
                return ObjectManager.GetObjectsOfType<WoWGameObject>()
                                       .Where(u => u.Entry == ObjectID && !u.InUse && !u.IsDisabled)
                                       .OrderBy(u => u.Distance).ToList();
            }
        }

        public List<WoWUnit> npcList
        {
            get
            {
                return ObjectManager.GetObjectsOfType<WoWUnit>()
                                            .Where(u => u.Entry == NpcID && !u.Dead)
                                            .OrderBy(u => u.Distance).ToList();
            }
        }

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
            _timer = null;
            PlayerQuest quest = StyxWoW.Me.QuestLog.GetQuestById(QuestId);

            if (quest != null)
            {
                TreeRoot.GoalText = "UseItemTargetLocation - " + quest.Name;
            }
            else
            {
                TreeRoot.GoalText = "UseItemTargetLocation: Running";
            }
        }

        private Composite _root;
        protected override Composite CreateBehavior()
        {
            return _root ?? (_root =
                new PrioritySelector(

                           new Decorator(ret => (Counter > 0 && QuestId == 0) || (me.QuestLog.GetQuestById(QuestId) != null && me.QuestLog.GetQuestById(QuestId).IsCompleted),
                                new Sequence(
                                    new Action(ret => TreeRoot.StatusText = "Finished!"),
                                    new WaitContinue(120,
                                        new Action(delegate
                                        {
                                            _isDone = true;
                                            return RunStatus.Success;
                                        }))
                                    )),

                           new Decorator(ret => _timer != null && !_timer.IsFinished,
                                new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "WaitTimer: " + _timer.TimeLeft.Seconds + " Seconds Left"),
                                        new Action(delegate { return RunStatus.Success; })
                                    )
                                ),

                           new Decorator(ret => _timer != null && _timer.IsFinished,
                                new Sequence(
                                        new Action(ret => _timer.Reset()),
                                        new Action(ret => _timer = null),
                                        new Action(delegate { return RunStatus.Success; })
                                    )
                                ),

                           new Decorator(ret => objectList.Count > 0,
                                new Sequence(
                                    new DecoratorContinue(ret => objectList[0].Location.Distance(me.Location) >= MinRange && objectList[0].Location.Distance(me.Location) <= MinRange + 3,
                                        new Sequence(
                                            new Action(ret => TreeRoot.StatusText = "Using Item - " + wowItem.Name + " On Object: " + objectList[0].Name + " Yards Away " + objectList[0].Location.Distance(me.Location)),
                                            new Action(ret => WoWMovement.MoveStop()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => wowItem.Interact()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => LegacySpellManager.ClickRemoteLocation(objectList[0].Location)),
                                            new Action(ret => usedGUID.Add(objectList[0].Guid)),
                                            new Action(ret => _timer = new Timer(new TimeSpan(0, 0, 0, 0, WaitTime))),
                                            new Action(ret => _timer.Reset())
                                            )
                                    ),
                                    new DecoratorContinue(ret => objectList[0].Location.Distance(me.Location) > MinRange + 3,
                                        new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "Moving To Mob - " + objectList[0].Name + " Yards Away: " + objectList[0].Location.Distance(me.Location)),
                                        new Action(ret => Navigator.MoveTo(objectList[0].Location)),
                                        new Action(ret => Thread.Sleep(300))
                                            )
                                    ),

                                   new DecoratorContinue(ret => objectList[0].Location.Distance(me.Location) < MinRange,
                                        new Sequence(
                                            new Action(ret => TreeRoot.StatusText = "Too Close, Backing Up"),
                                            new Action(ret => Thread.Sleep(100)),
                                            new Action(ret => WoWMovement.Move(WoWMovement.MovementDirection.Backwards)),
                                            new Action(ret => Thread.Sleep(100)),
                                            new Action(ret => WoWMovement.MoveStop())
                                          ))
                                    )),

                             new Decorator(ret => npcList.Count > 0,
                                new Sequence(
                                    new DecoratorContinue(ret => npcList[0].Location.Distance(me.Location) >= MinRange && npcList[0].Location.Distance(me.Location) <= MinRange + 3,
                                        new Sequence(
                                            new Action(ret => TreeRoot.StatusText = "Using Item - " + wowItem.Name + " On Object: " + npcList[0].Name + " Yards Away " + npcList[0].Location.Distance(me.Location)),
                                            new Action(ret => WoWMovement.MoveStop()),
                                            new Action(ret => npcList[0].Target()),
                                            new Action(ret => npcList[0].Face()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => wowItem.Interact()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => LegacySpellManager.ClickRemoteLocation(npcList[0].Location)),
                                            new Action(ret => usedGUID.Add(npcList[0].Guid)),
                                            new Action(ret => _timer = new Timer(new TimeSpan(0, 0, 0, 0, WaitTime))),
                                            new Action(ret => _timer.Reset())
                                            )
                                    ),
                                    new DecoratorContinue(ret => npcList[0].Location.Distance(me.Location) > MinRange + 3,
                                        new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "Moving To Mob - " + npcList[0].Name + " Yards Away: " + npcList[0].Location.Distance(me.Location)),
                                        new Action(ret => Navigator.MoveTo(npcList[0].Location)),
                                        new Action(ret => Thread.Sleep(300))
                                            )
                                    ),

                                   new DecoratorContinue(ret => npcList[0].Location.Distance(me.Location) < MinRange,
                                        new Sequence(
                                            new Action(ret => TreeRoot.StatusText = "Too Close, Backing Up"),
                                            new Action(ret => npcList[0].Face()),
                                            new Action(ret => Thread.Sleep(100)),
                                            new Action(ret => WoWMovement.Move(WoWMovement.MovementDirection.Backwards)),
                                            new Action(ret => Thread.Sleep(100))
                                          ))
                                    )),

                             new Decorator(ret => npcList.Count == 0 && objectList.Count == 0,
                                new Sequence(
                                    new DecoratorContinue(ret => me.Location.Distance(MoveLocation) <= 2,
                                        new Sequence(
                                            new Action(ret => TreeRoot.StatusText = "Using Item - " + wowItem.Name + " At Location - X: " + TargetLocation.X + " Y: " + TargetLocation.Y + " Z: " + TargetLocation.Z),
                                            new Action(ret => WoWMovement.MoveStop()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => wowItem.Interact()),
                                            new Action(ret => Thread.Sleep(300)),
                                            new Action(ret => LegacySpellManager.ClickRemoteLocation(TargetLocation))
                                            )
                                    ),
                                    new DecoratorContinue(ret => me.Location.Distance(MoveLocation) > 2,
                                        new Sequence(
                                        new Action(ret => TreeRoot.StatusText = "Moving To Mob - " + npcList[0].Name + " Yards Away: " + npcList[0].Location.Distance(me.Location)),
                                        new Action(ret => Navigator.MoveTo(npcList[0].Location)),
                                        new Action(ret => Thread.Sleep(300))
                                            ))
                                    ))
                    ));
        }

        public void CastSpell()
        {

            ObjectManager.Update();

            TreeRoot.StatusText = "Using Item: " + ItemID;

            Lua.DoString("UseItemByName(\"" + ItemID + "\")");
            if (QuestId == 0)
            {
                Counter++;
            }
            
            if (UseGO == 0 && UseNPC == 0)
            {
                LegacySpellManager.ClickRemoteLocation(TargetLocation);
            }
            else if (UseGO == 1)
            {
                LegacySpellManager.ClickRemoteLocation(objectList[0].Location);
                usedGUID.Add(objectList[0].Guid);
            }
            else if (UseNPC == 1)
            {
                LegacySpellManager.ClickRemoteLocation(npcList[0].Location);
                usedGUID.Add(npcList[0].Guid);
            }

            Thread.Sleep(WaitTime);
        }



        private bool _isDone;
        public override bool IsDone
        {
            get { return _isDone; }
        }

        #endregion
    }
}
