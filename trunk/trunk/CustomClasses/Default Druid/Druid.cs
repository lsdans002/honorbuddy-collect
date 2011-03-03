using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Styx;
using Styx.Combat.CombatRoutine;
using Styx.Helpers;
using Styx.Logic;
using Styx.Logic.Combat;
using Styx.Logic.Pathing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

#pragma warning disable

namespace Druid
{
    public class Druid : CombatRoutine
    {
        public override WoWClass Class
        {
            get { return WoWClass.Druid; }
        }

        public override string Name
        {
            get { return "Hawker's Druid 2.5"; }
        }

        private static WoWUnit MyTarget
        {
            get { return ObjectManager.Me.CurrentTarget; }
        }

        private static bool GotTarget
        {
            get { return ObjectManager.Me.CurrentTarget != null; }
        }

        private static void Player_OnMobKilled(BotEvents.Player.MobKilledEventArgs args)
        {
            StyxWoW.Me.ClearTarget();

            if (StyxWoW.Me.IsAutoAttacking)
            {
                StyxWoW.Me.ToggleAttack();
            }
        }

        public void HandleFalling() { }

        private void Slog(string msg)
        {
            if (msg == _logspam)
            {
                return;
            }

            Logging.Write(msg);
            _logspam = msg;
        }

        private void Slog(string msg, params object[] args)
        {
            if (msg != _logspam)
            {
                try
                {
                    Logging.Write(msg, args);
                    _logspam = msg;
                }
                catch (Exception ex)
                {
                    Logging.WriteDebug("Error in Log: ");
                    Logging.WriteDebug(ex.Source);
                }
            }
        }

        private bool SafeCast(int spellId)
        {
            if (!StyxWoW.Me.IsCasting)
            {
                try
                {
                    if (GotTarget && MyTarget.Attackable)
                    {
                        WoWMovement.Face();
                    }
                }
                catch
                {
                    Logging.WriteDebug("No need to face target.");
                }

                while (StyxWoW.GlobalCooldown)
                {
                    Thread.Sleep(100);
                }

                SpellManager.Cast(spellId);
                return true;
            }

            return false;
        }

        private bool SafeCast(string spellName)
        {
            if (Me.IsCasting || !SpellManager.HasSpell(spellName))
            {
                return false;
            }

            if (SpellManager.HasSpell(spellName) && !StyxWoW.Me.IsCasting)
            {
                if (SpellManager.Spells[spellName].CastTime > 1)
                {
                    WoWMovement.MoveStop();
                    WoWMovement.Face();
                }

                try
                {
                    if (GotTarget && MyTarget.Attackable)
                    {
                        WoWMovement.Face();
                    }
                }
                catch
                {
                    Logging.WriteDebug("No need to face target.");
                }

                while (StyxWoW.GlobalCooldown)
                {
                    Thread.Sleep(100);
                }

                SpellManager.Cast(spellName);

                return true;
            }

            Logging.WriteDebug("{0} can't cast {1}.", Name, spellName);

            return false;
        }

        #region Global Variables
        const uint CombatHealNeededPercentage = 35;
        private const uint KillComboCount = 4;
        private static readonly List<ulong> RunnerList = new List<ulong>();
        private static ulong _lastGuid;
        private static readonly Stopwatch FightTimer = new Stopwatch();
        private static readonly Stopwatch PullTimer = new Stopwatch();
        public readonly LocalPlayer Me = ObjectManager.Me;
        private readonly TimeSpan _needBuffTimespan = new TimeSpan(0, 3, 0);
        private string _logspam;

        private static bool HasBearForm
        {
            get
            {
                if (ObjectManager.Me.Shapeshift == ShapeshiftForm.Bear ||
                    ObjectManager.Me.Shapeshift == ShapeshiftForm.DireBear)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private static Stopwatch _addsWarning = new Stopwatch();
        private List<WoWUnit> AddsList
        {
            get
            {
                List<WoWUnit> mobList = ObjectManager.GetObjectsOfType<WoWUnit>(false);
                var enemyMobList = new List<WoWUnit>();

                foreach (WoWUnit thing in mobList)
                {
                    try
                    {
                        if ((thing.Guid != Me.Guid) && (thing.IsTargetingMeOrPet || thing.Fleeing))
                        {
                            enemyMobList.Add(thing);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteException(ex);
                        Slog("Add error.");
                    }
                }

                if (enemyMobList.Count > 1)
                {
                    if (!_addsWarning.IsRunning || _addsWarning.ElapsedMilliseconds > 10)
                    {
                        Slog("Warning: there are " + enemyMobList.Count + " attackers.");
                        _addsWarning.Reset();
                        _addsWarning.Start();
                    }
                }
                return enemyMobList;
            }
        }

        private bool DeadList
        {
            get
            {
                List<WoWUnit> mobList = ObjectManager.GetObjectsOfType<WoWUnit>(false);
                var enemyMobList = new List<WoWUnit>();

                foreach (WoWUnit thing in mobList)
                {
                    try
                    {
                        if ((thing.Dead) && thing.CanLoot)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteException(ex);
                        Slog("Dead thing error.");
                    }
                }

                return false;
            }
        }

        #endregion

        #region Pulse
        public override void Pulse()
        {
            if (Me.IsSwimming && Me.Level > 20 && Me.Shapeshift != ShapeshiftForm.Aqua)
            {
                TakeForm(ShapeshiftForm.Aqua);
            }
        }
        #endregion

        #region Rest

        public override bool NeedRest
        {
            get
            {
                if (ObjectManager.Me.Combat)
                {
                    return false;
                }

                if (ObjectManager.Me.Auras.ContainsKey("Resurrection Sickness"))
                {
                    return true;
                }

                if (ObjectManager.Me.IsSwimming)
                {
                    return false;
                }

                bool ret = false;

                // forms test
                if (!Me.Mounted)
                {

                    if (SpellManager.HasSpell("Mark of the Wild") &&
                        (!Me.Auras.ContainsKey("Mark of the Wild") ||
                         (Me.Auras["Mark of the Wild"].TimeLeft < _needBuffTimespan && Me.Auras["Mark of the Wild"].CreatorGuid == Me.Guid)) &&
                        !Me.Auras.ContainsKey("Gift of the Wild"))
                    {
                        Slog("I need Mark of the Wild");
                        ret = true;
                    }
                }

                // health/mana test
                if (ObjectManager.Me.HealthPercent <= 50)
                {
                    if (HealingTouchTimer.IsRunning && HealingTouchTimer.ElapsedMilliseconds < 5000)
                    {
                        float trash;
                        uint latency;
                        StyxWoW.WoWClient.GetNetStats(out trash, out trash, out latency);

                        if (HealingTouchTimer.ElapsedMilliseconds >=
                            SpellManager.Spells["Healing Touch"].CastTime + (latency * 2 + 50))
                        {
                            Slog("Health: " + Math.Round(ObjectManager.Me.HealthPercent) + "%");
                            ret = true;
                        }
                    }
                }

                if (ObjectManager.Me.ManaPercent <= 40)
                {
                    Slog("Mana: " + Math.Round(ObjectManager.Me.ManaPercent) + "%");
                    ret = true;
                }

                return ret;
            }
        }

        public override void Rest()
        {
            if (ObjectManager.Me.HealthPercent <= 80)
            {
                Heal();
            }

            DruidBuffs();

            if (Me.HealthPercent < 40)
            {
                Styx.Logic.Common.Rest.Feed();
            }

            // reserve Innervate for combat emergencies
            if (Me.ManaPercent < 40)
            {
                Styx.Logic.Common.Rest.Feed();
            }
        }

        #endregion

        #region PreCombatBuffs

        public override bool NeedPreCombatBuffs
        {
            get { return false; }
        }

        public override void PreCombatBuff()
        {
            DruidBuffs();
        }


        private void DruidBuffs()
        {
            if (!Me.Combat && !Me.Mounted)
            {
                if (!Me.Auras.ContainsKey("Mark of the Wild") ||
                   (Me.Auras["Mark of the Wild"].TimeLeft < _needBuffTimespan && Me.Auras["Mark of the Wild"].CreatorGuid == Me.Guid))
                {
                    if (GotTarget && MyTarget.Guid != Me.Guid)
                    {
                        Me.ClearTarget();
                        Thread.Sleep(250);
                    }
                    MarkOfTheWild();
                }
            }
        }

        #endregion

        #region Combat Auras

        public override bool NeedCombatBuffs
        {
            get { return false; }
        }

        public override void CombatBuff()
        {
            DruidBuffs();
        }

        /// <summary>
        /// Has this buff procced?
        /// </summary>
        /// <param name="cSearchBuffName"></param>
        /// <param name="minStackCount"></param>
        /// <returns>True if the buff is present</returns>
        private static bool HasBuffProcced(string cSearchBuffName, int minStackCount)
        {
            int stackCount = 0;

            List<string> myBuffs = Lua.LuaGetReturnValue("return UnitBuff(\"player\",\"" + cSearchBuffName + "\")",
                                                         "hawker.lua");

            if (Equals(null, myBuffs))
            {
                return false;
            }

            string buffName = myBuffs[0];

            if (minStackCount > 0)
            {
                stackCount = Convert.ToInt32(myBuffs[3]);
            }

            if (buffName == cSearchBuffName || stackCount >= minStackCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Heal

        public override bool NeedHeal
        {
            get
            {
                if (Me.Combat && SpellManager.HasSpell("Maul"))
                {
                    return false;
                }

                if (ObjectManager.Me.HealthPercent < 80)
                {
                    if (!Me.Combat)
                    {
                        Me.ClearTarget();
                    }
                    return true;
                }
                return false;
            }
        }

        private void HealInCombat()
        {
            const double HealingTouchPercentage = 60;
            const double RegrowthPercentage = 60;
            const double RejuventationPercentage = 80;

            if (Me.HealthPercent > CombatHealNeededPercentage)
            {
                return;
            }

            // make sure you have mana to get back into combat form
            if (Me.ManaPercent > 50 && SpellManager.HasSpell("Maul"))
            {
                if (SpellManager.HasSpell("War Stomp") && !SpellManager.Spells["War Stomp"].Cooldown)
                {
                    WarStomp();

                    float trash;
                    uint latency = 0;

                    StyxWoW.WoWClient.GetNetStats(out trash, out trash, out latency);

                    Thread.Sleep((int)latency * 2 + 50);
                }

                if (RegrowthTimer.ElapsedMilliseconds < 16)
                {
                    HealingTouch();
                }
                else
                {
                    Regrowth();
                }

                if (Me.ManaPercent > 35)
                {
                    Rejuvenation();
                }

                if (Me.ManaPercent < 36)
                {
                    Innervate();
                }
            }
            else if (Me.HealthPercent < 25)
            {
                Healing.UseHealthPotion();
            }
        }

        public override void Heal()
        {
            if (Me.HealthPercent < 80)
            {
                if (Me.HealthPercent > 65)
                {
                    if (SpellManager.HasSpell("Lifebloom"))
                    {
                        Lifebloom();
                    }
                    else
                    {
                        Rejuvenation();
                    }
                }
                else
                {
                    HealingTouch();
                }
            }

            if (Convert.ToInt32(Lua.LuaGetReturnValue("return GetItemCount(\"" + LevelbotSettings.Instance.DrinkName + "\")", "hawker.lua")[0]) < 1 && Me.ManaPercent < 36)
            {
                Innervate();
            }

        }

        #endregion

        #region Pull

        public override bool NeedPullBuffs
        {
            get { return false; }
        }

        public override void PullBuff()
        {
            DruidBuffs();
        }

        public override void Pull()
        {
            #region checks

            if (!GotTarget)
            {
                return;
            }

            if (MyTarget.Distance > LevelbotSettings.Instance.PullDistance + 5)
            {
                return;
            }

            if (MyTarget.Guid != _lastGuid)
            {
                FightTimer.Reset();
                _lastGuid = MyTarget.Guid;

                if (MyTarget.IsPlayer)
                {
                    Slog("Killing Level " + MyTarget.Level + " " + MyTarget.Race + " who is " +
                         Math.Round(MyTarget.Distance) + " yards away.");
                }
                else
                {
                    Slog("Killing " + MyTarget.Name + " at distance " + Math.Round(MyTarget.Distance) + ".");
                    if (RunnerList.Contains(MyTarget.Entry))
                    {
                        Logging.Write("{0} is likely to try to run away.", MyTarget.Name);
                    }
                }

                PullTimer.Reset();
                PullTimer.Start();
            }
            else
            {
                if (PullTimer.ElapsedMilliseconds > 30 * 1000)
                {
                    if (MyTarget.IsPlayer)
                    {
                        Slog("Cannot approach level " + MyTarget.Level + " " + MyTarget.Class +
                             " now.  Blacklist for 15 seconds.");
                        Blacklist.Add(MyTarget.Guid, TimeSpan.FromSeconds(15));
                    }
                    else
                    {
                        Slog("Cannot pull " + MyTarget.Name + " now.  Blacklist for 3 minutes.");
                        Blacklist.Add(MyTarget.Guid, TimeSpan.FromMinutes(3));
                    }
                    Me.ClearTarget();
                }
            }

            #endregion

            #region select pull method


            if (ObjectManager.Me.Shapeshift == ShapeshiftForm.Cat && SpellManager.HasSpell("Faerie Fire (Feral)"))
            {
                CatFaerieFeralFirePull();
                return;
            }
            else if (HasBearForm && ObjectManager.Me.ManaPercent < 55)
            {
                BearPull();
                return;
            }
            else
            {
                RangedPull();
                return;
            }

            #endregion
        }

        #endregion

        #region Combat

        public override void Combat()
        {
            if (Me.Race == WoWRace.Worgen)
            {
            }
            if (MyTarget == null || StyxWoW.GlobalCooldown || StyxWoW.Me.IsCasting)
                return;

            if (Battlegrounds.IsInsideBattleground)
            {
                _lastGuid = 0;
            }
            else if (!FightTimer.IsRunning || (MyTarget != null && MyTarget.Guid != _lastGuid))
            {
                FightTimer.Reset();
                FightTimer.Start();
                _lastGuid = MyTarget.Guid;
            }

            if (Me.HealthPercent < 25)
            {
                Healing.UseHealthPotion();
                GiftOfTheNaaru();
            }

            if (Me.ManaPercent < 50 && MyTarget.HealthPercent > Me.HealthPercent)
            {
                Healing.UseManaPotion();
            }

            if (Targeting.Instance.FirstUnit != null && Targeting.Instance.FirstUnit.CurrentTargetGuid == Me.Guid &&
                (!GotTarget || !MyTarget.Combat))
            {
                Targeting.Instance.FirstUnit.Target();
                Thread.Sleep(200);
            }

            if (Me.IsMoving)
            {
                WoWMovement.MoveStop();
                Thread.Sleep(100);
            }

            if (Battlegrounds.IsInsideBattleground)
            {
                WoWPlayer healYou = BestHealTarget();
                if (!Equals(null, healYou) && Me.ManaPercent > 50)
                {
                    healYou.Target();
                    Regrowth();
                    return;
                }
            }

            WoWMovement.Face();

            if (!Me.IsAutoAttacking && GotTarget)
            {
                Lua.DoString("StartAttack()");
            }

            if (CombatChecks && Me.Shapeshift == ShapeshiftForm.Normal && Me.ManaPercent > 60 && Me.HealthPercent < 55)
            {
                Regrowth();
            }

            if (AddsList.Count < 2 && SpellManager.HasSpell("Cat Form") && !HasBearForm)
            {
                TakeForm(ShapeshiftForm.Cat);
                Fluffycat();
            }
            else if (AddsList.Count < 2 && SpellManager.HasSpell("Cat Form") && HasBearForm && Me.ManaPercent > 50 &&
                     Me.HealthPercent > 50)
            {
                Logging.Write("Leaving {0} form with {1}% mana and {2}% health.", Me.Shapeshift,
                              (int)Me.ManaPercent,
                              (int)Me.HealthPercent);
                TakeForm(ShapeshiftForm.Cat);
                Fluffycat();
            }
            else if (SpellManager.HasSpell("Bear Form") || SpellManager.HasSpell("Dire Bear Form"))
            {
                if (!HasBearForm)
                {
                    if (AddsList.Count > 1 && SpellManager.HasSpell("Cat Form"))
                    {
                        Logging.Write("Using Bear Form as there is more than one attacker.");
                    }

                    TakeForm(ShapeshiftForm.Bear);
                }

                if (AddsList.Count > 1 && HasBearForm)
                {
                    Barkskin();
                    Berserk();
                }

                HuggyBear();
            }
            else
            {
                SquishyNoob();
            }

            if (!MyTarget.IsPlayer && FightTimer.ElapsedMilliseconds > 40 * 1000 && MyTarget.HealthPercent > 95)
            {
                Slog(" This " + MyTarget.Name + " is a bugged mob.  blacklisting for 1 hour.");

                Blacklist.Add(MyTarget.Guid, TimeSpan.FromHours(1.00));

                KeyboardManager.PressKey('S');
                Thread.Sleep(5 * 1000);
                KeyboardManager.ReleaseKey('S');
                Me.ClearTarget();
                _lastGuid = 0;
            }
        }

        #endregion

        #region Combat Styles

        private static bool CombatChecks
        {
            get
            {
                if (!GotTarget || ObjectManager.Me.Dead || !ObjectManager.Me.Combat)
                {
                    return false;
                }
                else if (!ObjectManager.Me.IsAutoAttacking)
                {
                    Lua.DoString("StartAttack()");
                }

                if (MyTarget.Distance > 50)
                {
                    if (MyTarget.IsPlayer)
                    {
                        Logging.Write("Out of range: Level " + MyTarget.Level + " " + MyTarget.Race + " " +
                                      Math.Round(MyTarget.Distance) + " yards away.");
                    }
                    else
                    {
                        Logging.Write("Out of range: Level " + MyTarget.Name + " is " +
                                      Math.Round(MyTarget.Distance) +
                                      " yards away.");
                    }


                    if (ObjectManager.Me.Combat)
                    {
                        Blacklist.Add(MyTarget.Guid, TimeSpan.FromSeconds(10));
                        Targeting.Instance.Clear();
                    }
                    ObjectManager.Me.ClearTarget();
                    return false;
                }

                const double meleeRange = 5;

                if (!ObjectManager.Me.IsCasting && MyTarget.Distance > meleeRange)
                {
                    int a = 0;

                    while (a < 50 && ObjectManager.Me.IsAlive && GotTarget && MyTarget.Distance > meleeRange)
                    {
                        WoWMovement.Face();
                        Navigator.MoveTo(WoWMovement.CalculatePointFrom(MyTarget.Location, 2.5f));
                        Thread.Sleep(250);
                        ++a;
                    }
                    WoWMovement.MoveStop();
                }

                WoWMovement.Face();

                return true;
            }
        }

        private void TakeForm(ShapeshiftForm wantedForm)
        {

            if (wantedForm == ShapeshiftForm.Cat && ObjectManager.Me.Shapeshift != ShapeshiftForm.Cat &&
                SpellManager.HasSpell("Cat Form"))
            {
                SafeCast("Cat Form");

                int a = 0;

                while (a < 30 && ObjectManager.Me.Shapeshift != ShapeshiftForm.Cat)
                {
                    Thread.Sleep(100);
                    ++a;
                }

                Slog(ObjectManager.Me.Shapeshift == ShapeshiftForm.Cat ? "Enter Cat Form." : "Failed to enter Cat Form.");

                return;
            }

            if (wantedForm == ShapeshiftForm.Bear && !HasBearForm &&
                (SpellManager.HasSpell("Bear Form") || SpellManager.HasSpell("Dire Bear Form")))
            {
                Slog("Enter Bear form.");

                SafeCast(SpellManager.HasSpell("Dire Bear Form") ? "Dire Bear Form" : "Bear Form");

                int a = 0;

                while (a < 30 && !HasBearForm)
                {
                    Thread.Sleep(100);
                    ++a;
                }

                Slog(HasBearForm ? "Enter Bear Form." : "Failed to enter Bear Form.");

                return;
            }
        }

        #region Spells

        private static readonly Stopwatch DemoralizingRoarTimer = new Stopwatch();

        private static readonly Stopwatch MoonfireTimer = new Stopwatch();

        private static readonly Stopwatch EntanglingRootsTimer = new Stopwatch();

        private static readonly Stopwatch HealingTouchTimer = new Stopwatch();

        private static readonly Stopwatch RegrowthTimer = new Stopwatch();

        private static readonly Stopwatch RejuvenationTimer = new Stopwatch();

        private void DemoralizingRoar()
        {
            if (DemoralizingRoarTimer.IsRunning && DemoralizingRoarTimer.ElapsedMilliseconds < 1500)
            {
                return;
            }

            if (SpellManager.HasSpell("Demoralizing Roar") && GotTarget && !MyTarget.Auras.ContainsKey("Demoralizing Roar") && SafeCast("Demoralizing Roar"))
            {
                Slog("Demoralizing Roar.");
                DemoralizingRoarTimer.Reset();
                DemoralizingRoarTimer.Start();
            }
        }

        private void Moonfire()
        {
            if (MoonfireTimer.IsRunning && MoonfireTimer.ElapsedMilliseconds < 1500)
            {
                return;
            }

            WoWMovement.Face();

            if (SpellManager.HasSpell("Moonfire") && GotTarget && !MyTarget.Auras.ContainsKey("Moonfire") && SafeCast("Moonfire"))
            {
                Slog("Moonfire.");
                MoonfireTimer.Reset();
                MoonfireTimer.Start();
            }
        }

        private void EntanglingRoots()
        {
            if (EntanglingRootsTimer.IsRunning && EntanglingRootsTimer.ElapsedMilliseconds < 1500)
            {
                return;
            }

            WoWMovement.Face();

            if (SpellManager.HasSpell("Entangling Roots") && GotTarget && !MyTarget.Auras.ContainsKey("Entangling Roots") && SafeCast("Entangling Roots"))
            {
                Slog("Entangling Roots.");
                EntanglingRootsTimer.Reset();
                EntanglingRootsTimer.Start();
            }
        }

        private void Wrath()
        {
            if (GotTarget && SafeCast("Wrath"))
            {
                WoWMovement.MoveStop();
                Slog("Wrath.");
            }
        }

        private void Starfire()
        {
            if (!SpellManager.HasSpell("Starfire"))
            {
                Wrath();
            }

            if (GotTarget && SafeCast("Starfire"))
            {
                Slog("Starfire.");
            }
        }

        private void HealingTouch()
        {


            if (HealingTouchTimer.IsRunning && HealingTouchTimer.ElapsedMilliseconds < 5000)
            {
                float trash;
                uint latency;

                StyxWoW.WoWClient.GetNetStats(out trash, out trash, out latency);

                if (HealingTouchTimer.ElapsedMilliseconds <
                    SpellManager.Spells["Healing Touch"].CastTime + (latency * 2 + 50))
                {
                    return;
                }
            }

            if ((!HealingTouchTimer.IsRunning || HealingTouchTimer.ElapsedMilliseconds > 3250) && Me.HealthPercent < 75 &&
                           SafeCast("Healing Touch"))
            {
                HealingTouchTimer.Reset();
                HealingTouchTimer.Start();

                Thread.Sleep(1000);

                while (Me.IsCasting)
                {
                    Thread.Sleep(100);
                    if (Me.HealthPercent > 75)
                    {
                        KeyboardManager.KeyUpDown(' ');
                        break;
                    }
                }
            }
        }

        private void Regrowth()
        {
            const long WaitForRegrowth = 10 * 1000;
            if (Me.Auras.ContainsKey("Regrowth"))
            {
                Slog("Already got Regrowth buff");

                if (Me.Auras.ContainsKey("Rejuvenation"))
                    Rejuvenation();
                return;
            }

            if (RegrowthTimer.IsRunning && RegrowthTimer.ElapsedMilliseconds < WaitForRegrowth)
            {
                return;
            }

            if ((!RegrowthTimer.IsRunning || RegrowthTimer.ElapsedMilliseconds >= WaitForRegrowth) && Me.HealthPercent < 75 && SafeCast("Regrowth"))
            {
                RegrowthTimer.Reset();
                RegrowthTimer.Start();

                Thread.Sleep(1000);

                while (Me.IsCasting)
                {
                    Thread.Sleep(100);
                }

                Thread.Sleep(250);
            }
        }

        private void Lifebloom()
        {
            if (SafeCast("Lifebloom"))
            {
                Slog("Lifebloom");
            }
        }

        private void Rejuvenation()
        {
            const long WaitForRejuvenation = 10 * 1000;

            if (RejuvenationTimer.IsRunning && RejuvenationTimer.ElapsedMilliseconds < WaitForRejuvenation)
            {
                return;
            }

            if (!Me.Auras.ContainsKey("Rejuvenation") && SafeCast("Rejuvenation"))
            {
                RejuvenationTimer.Reset();
                RejuvenationTimer.Start();

                Thread.Sleep(250);

                Slog("Rejuvenation.");
            }
        }

        /// <summary>
        /// http://www.wowhead.com/?spell=22812
        /// The druid's skin becomes as tough as bark.  All damage taken is reduced by 20%.  While protected, damaging attacks will not cause spellcasting delays for 12 seconds.
        /// </summary>
        private void Barkskin()
        {
            if (SafeCast("Barkskin"))
            {
                Slog("Barkskin.");
            }
        }

        /// <summary>
        /// http://www.wowhead.com/?spell=29166
        /// Causes the target to regenerate mana equal to 225% of the casting Druid's base mana pool over 10 sec.
        /// </summary>
        private void Innervate()
        {
            if (SafeCast("Innervate"))
            {
                Slog("Innervate.");
            }
        }

        /// <summary>
        /// Renews MotW every 28 minutes or when it has been purged
        /// </summary>
        private void MarkOfTheWild()
        {
            if (SpellManager.HasSpell("Mark of the Wild") && (!Me.Auras.ContainsKey("Mark of the Wild") ||
                (Me.Auras["Mark of the Wild"].TimeLeft < _needBuffTimespan && Me.Auras["Mark of the Wild"].CreatorGuid == Me.Guid)))
            {
                if (SafeCast("Mark of the Wild"))
                {
                    Slog("Mark of the Wild");
                }
            }
        }

        /// <summary>
        /// Renews Thorns every 8 minutes or when it has been purged.
        /// </summary>
        private void Thorns()
        {
            if (SpellManager.HasSpell("Thorns") && (!Me.Auras.ContainsKey("Thorns") ||
                (Me.Auras.ContainsKey("Thorns") && Me.Auras["Thorns"].TimeLeft < _needBuffTimespan && Me.Auras["Thorns"].CreatorGuid == Me.Guid)))
            {
                if (SafeCast("Thorns"))
                {
                    Slog("Thorns");
                }
            }
        }

        private void Rip()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && SpellManager.HasSpell("Rip") && Me.CurrentEnergy > SpellManager.Spells["Rip"].PowerCost)
            {
                if (GotTarget && !MyTarget.Auras.ContainsKey("Rip") && SafeCast("Rip"))
                {
                    Slog("Rip");
                }
            }
        }

        private void FaerieFireFeral()
        {
            if (Me.Shapeshift != ShapeshiftForm.Cat && !HasBearForm)
            {
                return;
            }

            if (MyTarget.Auras.ContainsKey("Faerie Fire"))
            {
                return;
            }

            if (GotTarget && SafeCast("Faerie Fire (Feral)"))
            {
                Slog("Faerie Fire (Feral).");
            }
        }


        private void Prowl()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && !Me.Auras.ContainsKey("Prowl") && SafeCast("Prowl"))
            {
                Slog("Prowl.");
            }
        }

        private void Pounce()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && Me.Auras.ContainsKey("Prowl") && GotTarget && SafeCast("Pounce"))
            {
                Slog("Pounce.");
            }
        }

        private void Rake()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && SpellManager.HasSpell("Rake") && Me.CurrentEnergy > SpellManager.Spells["Rake"].PowerCost)
            {
                if (GotTarget && !MyTarget.Auras.ContainsKey("Rake") && SafeCast("Rake"))
                {
                    Slog("Rake.");
                }
            }
        }

        private void Bash()
        {
            if (HasBearForm && SpellManager.HasSpell("Bash") && Me.CurrentRage > SpellManager.Spells["Bash"].PowerCost)
            {
                if (GotTarget && SafeCast("Bash"))
                {
                    Slog("Bash.");
                }
            }
        }

        private void TigersFury()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && Me.GetCurrentPower(WoWPowerType.Energy) < 50 && SafeCast("Tiger's Fury"))
            {
                Slog("Tiger's Fury.");
            }
        }

        private void FerociousBite()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && GotTarget && Me.CurrentEnergy >= SpellManager.Spells["Ferocious Bite"].PowerCost && SafeCast("Ferocious Bite"))
            {
                Slog("Ferocious Bite.");
            }
        }

        private void Shred()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && GotTarget && Me.CurrentEnergy >= SpellManager.Spells["Shred"].PowerCost && SafeCast("Shred"))
            {
                Slog("Shred.");
            }
        }

        private void Ravage()
        {
            if (Me.Shapeshift == ShapeshiftForm.Cat && GotTarget && SpellManager.HasSpell("Ravage") &&
                Me.CurrentEnergy >= SpellManager.Spells["Ravage"].PowerCost && SafeCast("Ravage"))
            {
                Slog("Ravage.");
            }
        }

        private void MangleBear()
        {
            if (HasBearForm && SpellManager.HasSpell("Mangle (Bear)") && Me.CurrentRage > SpellManager.Spells["Mangle (Bear)"].PowerCost)
            {
                if (GotTarget && SafeCast(33878))
                {
                    Slog("Mangle (Bear).");
                }
            }
        }

        private void MangleCat()
        {
            if (Me.Shapeshift != ShapeshiftForm.Cat)
            {
                return;
            }

            if (MyTarget.Fleeing)
            {
                Shred();
            }

            if (SpellManager.HasSpell("Mangle (Cat)") && Me.CurrentEnergy > SpellManager.Spells["Mangle (Cat)"].PowerCost)
            {
                SafeCast(33876);
                Slog("Mangle.");
            }
            else if (Me.CurrentEnergy > SpellManager.Spells["Claw"].PowerCost)
            {
                SafeCast("Claw");
            }
            else
            {
                // Slog("My energy: {0}", Me.CurrentEnergy);
            }
        }

        private void Maul()
        {
            if (HasBearForm && SpellManager.HasSpell("Maul") && Me.CurrentRage > SpellManager.Spells["Maul"].PowerCost)
            {
                if (GotTarget && SafeCast("Maul"))
                {
                    Slog("Maul.");
                }
            }
        }

        private void Swipe()
        {
            if (HasBearForm && SpellManager.HasSpell("Swipe (Bear)") && Me.CurrentRage > SpellManager.Spells["Swipe (Bear)"].PowerCost)
            {
                if (GotTarget && SafeCast("Swipe (Bear)"))
                {
                    Slog("Swipe (Bear).");
                }
            }
        }

        private void Enrage()
        {
            if (HasBearForm && SpellManager.HasSpell("Enrage") && !SpellManager.Spells["Enrage"].Cooldown && GotTarget && SafeCast("Enrage"))
            {
                Slog("Enrage.");
            }
        }

        /// <summary>
        /// When activated, this ability causes your Mangle (Bear) ability to hit up to 3 targets 
        /// and have no cooldown, 
        /// and reduces the energy cost of all your Cat Form abilities by 50%.
        /// </summary>
        private void Berserk()
        {
            if (SpellManager.HasSpell("Berserk") && (HasBearForm || Me.Shapeshift == ShapeshiftForm.Cat) && GotTarget && SafeCast("Berserk"))
            {
                Slog("Berserk.");
            }
        }

        private void SurvivalInstincts()
        {
            if (SpellManager.HasSpell("Survival Instincts") && !SpellManager.Spells["Survival Instincts"].Cooldown &&
                (HasBearForm || Me.Shapeshift == ShapeshiftForm.Cat) && GotTarget && SafeCast("Survival Instincts"))
            {
                Slog("Survival Instincts.");
            }
        }

        #endregion

        #endregion

        #region PVP

        private static List<WoWPlayer> HealTargets
        {
            get
            {
                return ObjectManager.GetObjectsOfType<WoWPlayer>().Where(player => player.IsAlliance == ObjectManager.Me.IsAlliance && player.HealthPercent < 100).ToList();
            }
        }

        private static WoWPlayer BestHealTarget()
        {
            for (int a = HealTargets.Count - 1; a >= 0; --a)
            {
                if (HealTargets[a].Distance > 25 ||
                    HealTargets[a].HealthPercent > 90)
                {
                    HealTargets.RemoveAt(a);
                }
                else
                {
                    return HealTargets[a];
                }
            }

            return null;
        }

        #endregion

        #region movement logic

        private static WoWPoint AttackPoint
        {
            get
            {
                if (GotTarget)
                {
                    // return MyTarget.Location;
                    return WoWMovement.CalculatePointFrom(MyTarget.Location, 3.5f);
                }
                else
                {
                    var noSpot = new WoWPoint();
                    return noSpot;
                }
            }
        }

        private WoWPoint GetTraceLinePos()
        {
            return new WoWPoint(Me.X, Me.Y, Me.Z + 2.132f);
        }

        #endregion

        #region horde racials

        // Berserking
        private void Berserking()
        {
            if (ObjectManager.Me.Race == WoWRace.Troll &&
                SafeCast("Berserking"))
            {
                Logging.Write("Berserking.");
            }
        }

        // Bloodrage
        private void Bloodrage()
        {
            if (ObjectManager.Me.Race == WoWRace.Orc &&
                SafeCast("Bloodrage"))
            {
                Logging.Write("Bloodrage.");
            }
        }

        // War Stomp
        private void WarStomp()
        {
            if (ObjectManager.Me.Race == WoWRace.Tauren &&
                SafeCast("War Stomp"))
            {
                Logging.Write("War Stomp.");
            }
        }

        // Arcane Torrent
        private void ArcaneTorrent()
        {
            if (ObjectManager.Me.Race == WoWRace.BloodElf &&
                SafeCast("Arcane Torrent"))
            {
                Logging.Write("Arcane Torrent.");
            }
        }

        // Will of the Forsaken 
        private void WillOfTheForsaken()
        {
            if (ObjectManager.Me.Race == WoWRace.Undead &&
                SafeCast("Will of the Forsaken"))
            {
                Logging.Write("Will of the Forsaken.");
            }
        }

        #endregion

        #region alliance racials

        // Stoneform
        private void Stoneform()
        {
            if (ObjectManager.Me.Race == WoWRace.Dwarf &&
                SafeCast("Stoneform"))
            {
                Logging.Write("Stoneform.");
            }
        }

        // Gift of the Naaru
        private void GiftOfTheNaaru()
        {
            if (ObjectManager.Me.Race == WoWRace.Draenei &&
                SafeCast("Gift of the Naaru"))
            {
                Logging.Write("Gift of the Naaru.");
            }
        }

        // Every Man for Himself
        private void EveryManForHimself()
        {
            if (ObjectManager.Me.Race == WoWRace.Human &&
                SafeCast("Every Man for Himself"))
            {
                Logging.Write("Every Man for Himself.");
            }
        }

        // Shadowmeld
        private void Shadowmeld()
        {
            if (ObjectManager.Me.Race == WoWRace.NightElf &&
                SafeCast("Shadowmeld"))
            {
                Logging.Write("Shadowmeld.");
            }
        }

        // Escape Artist
        private void EscapeArtist()
        {
            if (ObjectManager.Me.Race == WoWRace.Gnome &&
                SafeCast("Escape Artist"))
            {
                Logging.Write("Escape Artist.");
            }
        }

        #endregion

        #region bear

        private void BearPull()
        {
            if (Me.Combat || !GotTarget || !SpellManager.HasSpell("Bear Form"))
            {
                return;
            }

            if (!HasBearForm)
            {
                if (GotTarget && MyTarget.Distance > SpellManager.Spells["Moonfire"].MaxRange)
                {

                    int a = 0;
                    while (a < 50 && ObjectManager.Me.IsAlive && GotTarget &&
                           MyTarget.Distance > SpellManager.Spells["Moonfire"].MaxRange - 3)
                    {
                        if (Me.Combat)
                        {
                            return;
                        }

                        WoWMovement.Face();
                        Navigator.MoveTo(WoWMovement.CalculatePointFrom(MyTarget.Location, 2.5f));
                        Thread.Sleep(250);
                        ++a;
                    }
                }

                Moonfire();

                if (!HasBearForm)
                {
                    Slog("Pull: Enter Bear Form.");
                    SafeCast(SpellManager.HasSpell("Dire Bear Form") ? "Dire Bear Form" : "Bear Form");
                }
            }
            else if (SpellManager.HasSpell("Feral Charge)"))
            {
                double fcDistance = SpellManager.Spells["Feral Charge)"].MaxRange - 1;

                if (MyTarget.Distance > fcDistance)
                {

                    if (MyTarget.Distance > fcDistance)
                    {
                        int a = 0;
                        while (a < 50 && Me.IsAlive && GotTarget && MyTarget.Distance > fcDistance)
                        {
                            if (ObjectManager.Me.Combat)
                            {
                                Logging.Write("Combat has started.  Abandon pull.");
                                break;
                            }

                            WoWMovement.Face();
                            Navigator.MoveTo(AttackPoint);
                            Thread.Sleep(250);
                            ++a;
                        }
                    }
                }
                else
                {
                    SafeCast("Feral Charge)");
                    Slog("Pulled Feral Charge.");
                }
            }
            else if (SpellManager.HasSpell("Faerie Fire (Feral)"))
            {
                double fffDistance = SpellManager.Spells["Faerie Fire (Feral)"].MaxRange - 3;

                if (MyTarget.Distance > fffDistance)
                {
                    int a = 0;
                    while (a < 50 && Me.IsAlive && GotTarget && MyTarget.Distance > fffDistance)
                    {
                        if (ObjectManager.Me.Combat)
                        {
                            Logging.Write("Combat has started.  Abandon pull.");
                            break;
                        }

                        WoWMovement.Face();
                        Navigator.MoveTo(AttackPoint);
                        Thread.Sleep(250);
                        ++a;
                    }
                }
                else
                {
                    SafeCast("Faerie Fire (Feral)");
                }
            }

            uint iter = 0;
            while (iter < 50 && ObjectManager.Me.IsAlive && GotTarget && MyTarget.Distance > 3)
            {
                if (Me.Combat)
                {
                    return;
                }

                WoWMovement.Face();
                Navigator.MoveTo(WoWMovement.CalculatePointFrom(MyTarget.Location, 4.5f));
                Thread.Sleep(250);
                ++iter;
            }

            if (CombatChecks)
            {
                Maul();
            }
        }

        private void HuggyBear()
        {
            if (!CombatChecks)
            {
                return;
            }

            #region survival

            // predator's swiftness
            if (Me.HealthPercent < 70 && Me.ManaPercent > 55 && HasBuffProcced("Predator's Swiftness", 0))
            {
                Slog("Predator's Swiftness.");
                HealingTouch();
                return;
            }

            HealInCombat();

            #endregion

            #region runners

            uint targetEntry = MyTarget.Entry;

            if (GotTarget && RunnerList.Contains(targetEntry) && !MyTarget.Fleeing)
            {
                if (MyTarget.HealthPercent < 30 && SpellManager.CanCast("Bash") && !MyTarget.Auras.ContainsKey("Entangling Roots"))
                {
                    Bash();
                }
                else if (MyTarget.HealthPercent < 35 && SpellManager.CanCast("Nature's Grasp"))
                {
                    SafeCast("Nature's Grasp");
                }
            }

            // try to execute runners

            if (AddsList.Count < 2 && GotTarget && MyTarget.Fleeing)
            {
                if (!RunnerList.Contains(targetEntry))
                {
                    Logging.Write("Adding {0} to the list of running mobs.", MyTarget.Name);
                    RunnerList.Add(targetEntry);
                }

                Slog("Cast Entangling Roots on runner.");
                EntanglingRoots();
                Moonfire();
            }
            // use Spam it on adds over level 24 as Glyph of Rake prevents runners.
            else if (Me.Level > 24 && AddsList.Count > 1 && GotTarget && SpellManager.CanCast("Nature's Grasp"))
            {
                SafeCast("Nature's Grasp");
            }

            #endregion

            #region combat casting

            if (!HasBearForm)
            {
                // don't go into Bear Form unless you have to
                if (SpellManager.HasSpell("Cat Form") && AddsList.Count < 2 && GotTarget && Me.HealthPercent > MyTarget.HealthPercent)
                {
                    string targetName = MyTarget.Name;

                    if (MyTarget.IsPlayer)
                    {
                        targetName = "Level " + MyTarget.Level + " " + MyTarget.Race + " " + MyTarget.Class;
                    }

                    Logging.Write("Use Cat form to kill {0}.", targetName);

                    Fluffycat();
                    return;
                }

                Logging.Write("Combat: Need Bear form");
                TakeForm(ShapeshiftForm.Bear);
            }

            if (!CombatChecks)
            {
                Slog("Combat checks failed");
            }
            else
            {
                if (!HasBearForm)
                {
                    Logging.Write("Combat: Really need Bear form");
                }
                Enrage();

                MyTarget.Interact();

                DemoralizingRoar();

                Maul();
                MangleBear();

                if (AddsList.Count > 1 && SpellManager.HasSpell("Swipe (Bear)"))
                {
                    Swipe();
                    SurvivalInstincts();
                }
            }
        }

            #endregion

        #region caster

        private void RangedPull()
        {
            int a = 0;

            float distance = SpellManager.HasSpell("Moonfire") ? SpellManager.Spells["Moonfire"].MaxRange - 1 : SpellManager.Spells["Wrath"].MaxRange - 1;


            if (MyTarget.Distance > distance)
            {
                while (a < 50 && Me.IsAlive && GotTarget &&
                       MyTarget.Distance > distance && !Me.Combat)
                {
                    if (ObjectManager.Me.Combat)
                    {
                        Logging.Write("Combat has started.  Abandon pull.");
                        break;
                    }

                    WoWMovement.Face();
                    Navigator.MoveTo(WoWMovement.CalculatePointFrom(MyTarget.Location, SpellManager.Spells["Moonfire"].MaxRange - 5));
                    Thread.Sleep(250);
                    ++a;
                }
            }

            if (!MyTarget.InLineOfSight)
            {
                while (a < 50 && Me.IsAlive && GotTarget && !MyTarget.InLineOfSight && !Me.Combat)
                {
                    if (ObjectManager.Me.Combat)
                    {
                        Logging.Write("Combat has started.  Abandon pull.");
                        break;
                    }

                    WoWMovement.Face();
                    Navigator.MoveTo(MyTarget.Location);
                    Thread.Sleep(250);
                    ++a;
                }
            }
            WoWMovement.MoveStop();

            if (Me.CurrentTarget.Distance > distance)
            {
                Logging.Write("Too far for ranged pull - try again.");
                return;
            }

            if (HasBuffProcced("Predator's Swiftness", 0))
            {
                Wrath();
            }

            int wait = 0;
            while (a < 3 && !Me.Combat)
            {
                if (SpellManager.Spells.ContainsKey("Moonfire"))
                {
                    Moonfire();
                }
                else
                {
                    Wrath();
                }
                while (StyxWoW.GlobalCooldown)
                {
                    Thread.Sleep(100);
                }

                ++a;
            }

            if (SpellManager.HasSpell("Thorns") && (!Me.Auras.ContainsKey("Thorns")))
            {
                Thorns();
            }

        }

        private void SquishyNoob()
        {
            if (NeedHeal)
            {
                if (SpellManager.HasSpell("War Stomp") && !SpellManager.Spells["War Stomp"].Cooldown)
                {
                    WarStomp();

                    float trash;
                    uint latency = 0;

                    StyxWoW.WoWClient.GetNetStats(out trash, out trash, out latency);

                    Thread.Sleep((int)latency * 2 + 50);
                }

                HealingTouch();
            }

            if (Me.HealthPercent < 25)
            {
                Healing.UseHealthPotion();
            }

            if (MyTarget.Fleeing)
            {
                Wrath();
            }

            if (CombatChecks &&
                Me.ManaPercent > 30)
            {
                Moonfire();
            }

            if (CombatChecks && Me.ManaPercent > 30)
            {
                Wrath();
            }

            if (CombatChecks)
            {
                WarStomp();
            }
        }

        #endregion

        # region cat

        private void CatFaerieFeralFirePull()
        {
            if (SpellManager.HasSpell("Faerie Fire (Feral)"))
            {
                if (ObjectManager.Me.Shapeshift != ShapeshiftForm.Cat)
                {
                    Logging.Write("Pull with Faerie Fire (Feral): Need Cat form");
                    TakeForm(ShapeshiftForm.Cat);
                }

                double fffDistance = SpellManager.Spells["Faerie Fire (Feral)"].MaxRange - 3;

                if (MyTarget.Distance > fffDistance || !MyTarget.InLineOfSight)
                {
                    int a = 0;
                    while (a < 50 && Me.IsAlive && GotTarget && MyTarget.Distance > fffDistance)
                    {
                        if (ObjectManager.Me.Combat)
                        {
                            Logging.Write("Combat has started.  Abandon pull.");
                            break;
                        }

                        WoWMovement.Face();
                        Navigator.MoveTo(AttackPoint);
                        Thread.Sleep(250);
                        ++a;
                    }

                    if (GotTarget && !MyTarget.InLineOfSight)
                    {
                        a = 0;
                        while (a < 50 && Me.IsAlive && GotTarget && !MyTarget.InLineOfSight)
                        {
                            if (ObjectManager.Me.Combat)
                            {
                                Logging.Write("Combat has started.  Abandon pull.");
                                break;
                            }

                            WoWMovement.Face();
                            Navigator.MoveTo(AttackPoint);
                            Thread.Sleep(250);
                            ++a;
                        }
                    }
                }
                else
                {
                    int wait = 0;
                    while (wait < 3 && !Me.Combat)
                    {
                        FaerieFireFeral();
                        while (StyxWoW.GlobalCooldown)
                        {
                            Thread.Sleep(100);
                        }

                        ++wait;
                    }
                }

                if (SpellManager.HasSpell("Dash") && !SpellManager.Spells["Dash"].Cooldown && SafeCast("Dash"))
                {
                    Slog("Dash to the target.");
                }
            }
            else
            {
                Logging.Write("I don't know Faerie Fire (Feral)");
            }
        }

        private void Fluffycat()
        {
            #region survival

            // predator's swiftness
            if (Me.HealthPercent < 70 && Me.ManaPercent > 55 && HasBuffProcced("Predator's Swiftness", 0))
            {
                Slog("Predator's Swiftness.");
                HealingTouch();
                return;
            }

            HealInCombat();

            #endregion

            #region runners

            if (MyTarget == null)
            {
                Slog("NO TARGET");
                return;
            }
            uint targetEntry = MyTarget.Entry;

            if (GotTarget && RunnerList.Contains(targetEntry) && !MyTarget.Fleeing && MyTarget.HealthPercent < 30 && SpellManager.CanCast("Nature's Grasp"))
            {
                SafeCast("Nature's Grasp");
            }

            // try to execute runners

            if (AddsList.Count < 2 && GotTarget && MyTarget.Fleeing)
            {
                if (!RunnerList.Contains(targetEntry))
                {
                    Logging.Write("Adding {0} to the list of running mobs.", MyTarget.Name);
                    RunnerList.Add(targetEntry);
                }

                Slog("Cast Entangling Roots on runner.");
                EntanglingRoots();
                Moonfire();
            }

            #endregion

            #region combat casting

            if (AddsList.Count > 0)
            {
                // combat
                if (CombatChecks)
                {
                    if (ObjectManager.Me.Shapeshift != ShapeshiftForm.Cat)
                    {
                        Logging.Write("Combat: Need Cat form");
                        TakeForm(ShapeshiftForm.Cat);
                    }

                    if (SpellManager.HasSpell("Tiger's Fury") && !SpellManager.Spells["Tiger's Fury"].Cooldown)
                    {
                        TigersFury();
                    }

                    const uint bleedOnRunnersPercent = 55;

                    if (!GotTarget)
                    {
                        return;
                    }
                    else if (MyTarget.HealthPercent > 50 && SpellManager.HasSpell("Faerie Fire (Feral)") &&
                             !MyTarget.Auras.ContainsKey("Faerie Fire"))
                    {
                        FaerieFireFeral();
                    }
                    else if (RunnerList.Contains(MyTarget.Entry) && MyTarget.HealthPercent < bleedOnRunnersPercent && SpellManager.HasSpell("Rake") &&
                             !MyTarget.Auras.ContainsKey("Rake"))
                    {
                        Slog("Rake on {0}.", MyTarget.Name);
                        Rake();
                    }
                    else if (!RunnerList.Contains(MyTarget.Entry) && MyTarget.HealthPercent > 50 && SpellManager.HasSpell("Rake") &&
                             !MyTarget.Auras.ContainsKey("Rake"))
                    {
                        Rake();
                    }
                    else if (RunnerList.Contains(MyTarget.Entry) && MyTarget.HealthPercent < bleedOnRunnersPercent && SpellManager.HasSpell("Rip") &&
                             !MyTarget.Auras.ContainsKey("Rip"))
                    {
                        Slog("Rip on {0}.", MyTarget.Name);
                        Rip();
                    }
                    else if (!RunnerList.Contains(MyTarget.Entry) && Me.ComboPoints > 0 && Me.ComboPoints < 4 && MyTarget.HealthPercent > 50 && SpellManager.HasSpell("Rip") &&
                             !MyTarget.Auras.ContainsKey("Rip"))
                    {
                        Rip();
                    }
                    else if (SpellManager.HasSpell("Ferocious Bite") && (Me.ComboPoints >= KillComboCount || (MyTarget.HealthPercent < 30 && Me.ComboPoints > 2)))
                    {
                        FerociousBite();
                    }
                    else
                    {
                        MangleCat();
                    }
                }
            }

            #endregion
        }

        #endregion

        #endregion
    }
}

#pragma warning restore