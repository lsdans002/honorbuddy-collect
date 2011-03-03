﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-02 03:32:55 +0200 (Çar, 02 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/SingularRoutine.CompositeHelpers.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-02 03:32:55 +0200 (Çar, 02 Mar 2011) $
// $LastChangedRevision: 130 $
// $Revision: 130 $

#endregion


using System.Linq;

using CommonBehaviors.Actions;

using Singular.Composites;
using Singular.Settings;

using Styx;
using Styx.Logic;
using Styx.Logic.Combat;
using Styx.Logic.Pathing;
using Styx.WoWInternals.WoWObjects;

using TreeSharp;
using Styx.Helpers;
using System;

using Action = TreeSharp.Action;

namespace Singular
{
    public delegate WoWPoint LocationRetrievalDelegate(object context);

    partial class SingularRoutine
    {
        #region Delegates

        public delegate bool SimpleBoolReturnDelegate(object context);

        public delegate WoWUnit UnitSelectionDelegate(object context);

        #endregion

        protected Composite CreateWaitForCast()
        {
            return new Decorator(
                ret => Me.IsCasting,
                new ActionAlwaysSucceed());
        }

        protected Composite CreateCastPetAction(PetAction action, bool parentIsSelector)
        {
            return CreateCastPetActionOn(action, parentIsSelector, ret => Me.CurrentTarget);
        }

        protected Composite CreateCastPetActionOn(PetAction action, bool parentIsSelector, UnitSelectionDelegate onUnit)
        {
            return new Action(
                delegate(object context)
                    {
                        PetManager.CastPetAction(action, onUnit(context));

                        // Purposely fail here, we want to 'skip' down the tree.
                        if (parentIsSelector)
                        {
                            return RunStatus.Failure;
                        }
                        return RunStatus.Success;
                    });
        }

		private static WaitTimer targetingTimer = new WaitTimer(TimeSpan.FromSeconds(2));

        protected Composite CreateEnsureTarget()
        {
            return
				new PrioritySelector(
					new Decorator(
						ret => NeedTankTargeting && targetingTimer.IsFinished && Me.Combat &&
							   TankTargeting.Instance.FirstUnit != null && Me.CurrentTarget != TankTargeting.Instance.FirstUnit,
						new Action(ret =>
							{
								Logger.Write("Targeting first unit of TankTargeting");
								TankTargeting.Instance.FirstUnit.Target();
								StyxWoW.SleepForLagDuration();
								targetingTimer.Reset();
							})),
					new Decorator(
						ret => Me.CurrentTarget == null || Me.CurrentTarget.Dead,
						new PrioritySelector(
							// Set our context to the RaF leaders target, or the first in the target list.
							ctx => RaFHelper.Leader != null && RaFHelper.Leader.Combat 
										? 
											RaFHelper.Leader.CurrentTarget 
										: 
											Targeting.Instance.FirstUnit != null && Targeting.Instance.FirstUnit.Combat
											?
												Targeting.Instance.FirstUnit
											:
												null,
							// Make sure the target is VALID. If not, then ignore this next part. (Resolves some silly issues!)
							new Decorator(
								ret => ret != null,
								new Sequence(
									new Action(ret => Logger.Write("Target is invalid. Switching to " + ((WoWUnit)ret).Name + "!")),
									new Action(ret => ((WoWUnit)ret).Target()))),
							// In order to resolve getting "stuck" on a target, we'll clear it if there's nothing viable.
							new Action(
								ret =>
									{
										Me.ClearTarget();
										// Force a failure, just so we can move down the branch.
										return RunStatus.Failure;
									}),
							new ActionLogMessage(false, "No viable target! NOT GOOD!"))));
        }

        protected Composite CreateAutoAttack(bool includePet)
        {
			const int SPELL_ID_AUTO_SHOT = 75;

			return new PrioritySelector(
				new Decorator(
					ret => !Me.IsAutoAttacking && Me.AutoRepeatingSpellId != SPELL_ID_AUTO_SHOT,
					new Action(ret => Me.ToggleAttack())),
				new Decorator(
					ret => includePet && Me.GotAlivePet && !Me.Pet.IsAutoAttacking,
					new Action(delegate { PetManager.CastPetAction(PetAction.Attack); return RunStatus.Failure; }))
				);
        }

		protected Composite CreateHunterBackPedal()
		{
			return
				new Decorator(
					ret => Me.CurrentTarget.Distance <= 7 && Me.CurrentTarget.IsAlive &&
						   (Me.CurrentTarget.CurrentTarget == null || Me.CurrentTarget.CurrentTarget != Me),
					new Action(ret =>
						{
							WoWPoint moveTo = WoWMathHelper.CalculatePointFrom(Me.Location, Me.CurrentTarget.Location, 10f);
							
							if (Navigator.CanNavigateFully(Me.Location, moveTo))
								Navigator.MoveTo(moveTo);
						}));
		}

		protected Composite CreateUseWand()
		{
			return CreateUseWand(ret => true);
		}

		protected Composite CreateUseWand(SimpleBoolReturnDelegate extra)
		{
			return new PrioritySelector(
				new Decorator(
					ret => HasWand && !IsWanding && extra(ret),
					new Action(ret => SpellManager.Cast("Shoot")))
				);
		}

        public Composite CreateUseTrinketsBehavior()
        {
            return new PrioritySelector(
                new Decorator(
                    ret => SingularSettings.Instance.UseFirstTrinket,
                    new Decorator(
                        ret => Miscellaneous.UseTrinket(true),
                        new ActionAlwaysSucceed())),

                new Decorator(
                    ret => SingularSettings.Instance.UseSecondTrinket,
                    new Decorator(
                        ret => Miscellaneous.UseTrinket(false),
                        new ActionAlwaysSucceed()))
                );
        }

        private void CastWithLog(string spellName, WoWUnit onTarget)
        {
            Logger.Write(string.Format("Casting {0} on {1}", spellName, onTarget.SafeName()));
            SpellManager.Cast(spellName, onTarget);
        }

        private void CastWithLog(int spellId, WoWUnit onTarget)
        {
            Logger.Write(string.Format("Casting {0} on {1}", WoWSpell.FromId(spellId).Name, onTarget.SafeName()));
            SpellManager.Cast(spellId, onTarget);
        }

        #region Cast By Name

        public Composite CreateSpellCast(string spellName, SimpleBoolReturnDelegate extra, UnitSelectionDelegate unitSelector)
        {
			return CreateSpellCast(spellName, extra, unitSelector, true);
        }

		public Composite CreateSpellCast(string spellName, SimpleBoolReturnDelegate extra, UnitSelectionDelegate unitSelector, bool checkMoving)
		{
			return new Decorator(
				ret => extra(ret) && unitSelector(ret) != null && CanCast(spellName, unitSelector(ret), checkMoving),
				new PrioritySelector(
					CreateApproachToCast(spellName, unitSelector),
					new Decorator(
						ret => !checkMoving && Me.IsMoving && SpellManager.Spells[spellName].CastTime > 0,
						new Sequence(
							new Action(ret => Navigator.PlayerMover.MoveStop()),
							new Action(ret => StyxWoW.SleepForLagDuration()))),
					new Action(ret => CastWithLog(spellName, unitSelector(ret)))));
		}

        public Composite CreateSpellCast(string spellName)
        {
			return CreateSpellCast(spellName, true);
        }

		public Composite CreateSpellCast(string spellName, bool checkMoving)
		{
			return CreateSpellCast(spellName, ret => true, checkMoving);
		}

        public Composite CreateSpellCast(string spellName, SimpleBoolReturnDelegate extra)
        {
			return CreateSpellCast(spellName, extra, true);
        }

		public Composite CreateSpellCast(string spellName, SimpleBoolReturnDelegate extra, bool checkMoving)
		{
			return CreateSpellCast(spellName, extra, ret => Me.CurrentTarget, checkMoving);
		}

        public Composite CreateSpellCastOnSelf(string spellName)
        {
            return CreateSpellCast(spellName, ret => true);
        }

        public Composite CreateSpellCastOnSelf(string spellName, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellCast(spellName, extra, ret => Me);
        }

        #endregion

        #region Cast By ID

        public Composite CreateSpellCast(int spellId, SimpleBoolReturnDelegate extra, UnitSelectionDelegate unitSelector)
        {
            return new Decorator(
                ret => extra(ret) && SpellManager.CanCast(spellId, unitSelector(ret)),
                new Action(ret => CastWithLog(spellId, unitSelector(ret))));
        }

        public Composite CreateSpellCast(int spellId)
        {
            return CreateSpellCast(spellId, ret => true);
        }

        public Composite CreateSpellCast(int spellId, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellCast(spellId, extra, ret => Me.CurrentTarget);
        }

        public Composite CreateSpellCastOnSelf(int spellId)
        {
            return CreateSpellCast(spellId, ret => true);
        }

        public Composite CreateSpellCastOnSelf(int spellId, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellCast(spellId, ret => true, ret => Me);
        }

        #endregion

        #region Buff By Name

        public Composite CreateSpellBuff(string spellName, SimpleBoolReturnDelegate extra, UnitSelectionDelegate unitSelector)
        {
            // BUGFIX: HB currently doesn't check ActiveAuras in the spell manager. So this'll break on new spell procs
            return CreateSpellCast(
                spellName, ret => extra(ret) && unitSelector(ret) != null && !HasAuraStacks(spellName, 0, unitSelector(ret)), unitSelector, false);
        }

        public Composite CreateSpellBuff(string spellName)
        {
            return CreateSpellBuff(spellName, ret => true);
        }

        public Composite CreateSpellBuff(string spellName, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellBuff(spellName, extra, ret => Me.CurrentTarget);
        }

        public Composite CreateSpellBuffOnSelf(string spellName)
        {
            return CreateSpellBuffOnSelf(spellName, ret => true);
        }

        public Composite CreateSpellBuffOnSelf(string spellName, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellBuff(spellName, extra, ret => Me);
        }

        #endregion

        #region Cast By ID

        public Composite CreateSpellBuff(int spellId, SimpleBoolReturnDelegate extra, UnitSelectionDelegate unitSelector)
        {
            return new Decorator(
                ret => extra(ret) && unitSelector(ret) != null && SpellManager.CanBuff(spellId, unitSelector(ret)),
                new Action(ret => CastWithLog(spellId, unitSelector(ret))));
        }

        public Composite CreateSpellBuff(int spellId)
        {
            return CreateSpellCast(spellId, ret => true);
        }

        public Composite CreateSpellBuff(int spellId, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellCast(spellId, extra, ret => Me.CurrentTarget);
        }

        public Composite CreateSpellBuffOnSelf(int spellId)
        {
            return CreateSpellCast(spellId, ret => true);
        }

        public Composite CreateSpellBuffOnSelf(int spellId, SimpleBoolReturnDelegate extra)
        {
            return CreateSpellCast(spellId, extra, ret => Me);
        }

        #endregion

		#region Party Buff By Name

		/// <summary>
		/// To cast buffs on party members like Dampen Magic and such.
		/// </summary>
		/// <param name="spellName">Name of the buff</param>
		/// <returns></returns>
		public Composite CreateSpellPartyBuff(string spellName)
		{
			return
				new PrioritySelector(
					new Decorator(
						ret => Me.IsInParty && Me.PartyMembers.Any(p => p.IsAlive && !p.HasAura(spellName)),
						new PrioritySelector(
							ctx => Me.PartyMembers.First(p => p.IsAlive && !p.HasAura(spellName)),
							CreateRangeAndFace(35, ret => (WoWUnit)ret),
							CreateSpellCast(spellName, ret => true, ret => (WoWUnit)ret)))
				);
		}

		#endregion

		#region ApproachToCast

		public Composite CreateApproachToCast(string spellName, UnitSelectionDelegate unitSelector)
		{
			return
				new Decorator(
					ret => SpellManager.Spells[spellName].MaxRange != 0 &&
						   (unitSelector(ret).Distance > SpellManager.Spells[spellName].MaxRange - 2f ||
							!unitSelector(ret).InLineOfSightOCD),
					new Action(ret => Navigator.MoveTo(unitSelector(ret).Location)));
		}

		#endregion

		#region CanCast

		public bool CanCast(string spellName, WoWUnit onUnit, bool checkMoving)
		{
			// Do we have spell?
			if (!SpellManager.HasSpell(spellName))
				return false;

			WoWSpell spell = SpellManager.Spells[spellName];

			// Use default CanCast if checkmoving is true
			if (checkMoving)
			{
				return SpellManager.CanCast(spellName, onUnit);
			}

			// is spell in CD?
			if (spell.Cooldown)
			{
				return false;
			}

			// are we casting or channeling ?
			if (Me.IsCasting || Me.ChanneledCastingSpellId != 0)
			{
				return false;
			}

			// do we have enough power?
			if (Me.GetCurrentPower(spell.PowerType) < spell.PowerCost)
			{
				return false;
			}

			// GCD check
			if (StyxWoW.GlobalCooldown)
			{
				return false;
			}

			// lua
			if (!spell.CanCast)
			{
				return false;
			}

			return true;
		}

		#endregion
	}
}