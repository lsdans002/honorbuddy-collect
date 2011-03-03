﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-01 17:20:28 +0200 (Sal, 01 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/ClassSpecific/Warlock/Demonology.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-01 17:20:28 +0200 (Sal, 01 Mar 2011) $
// $LastChangedRevision: 129 $
// $Revision: 129 $

#endregion

using Styx.Combat.CombatRoutine;
using Styx.Logic.Combat;

using TreeSharp;

namespace Singular
{
    partial class SingularRoutine
    {
        [Class(WoWClass.Warlock)]
        [Spec(TalentSpec.DemonologyWarlock)]
		[Spec(TalentSpec.Lowbie)]
        [Context(WoWContext.All)]
        [Behavior(BehaviorType.Combat)]
        [Behavior(BehaviorType.Pull)]
        public Composite CreateDemonologyCombat()
        {
            WantedPet = "Felguard";

            return new PrioritySelector(
                CreateEnsureTarget(),
                CreateRangeAndFace(35f, ret => Me.CurrentTarget),
                CreateWaitForCast(),
                CreateAutoAttack(true),
                CreateSpellBuffOnSelf("Soulburn", ret => SpellManager.HasSpell("Soul Fire") || Me.HealthPercent < 70),
                CreateSpellCast("Life Tap", ret => Me.ManaPercent < 50 && Me.HealthPercent > 70),
                CreateSpellCast("Drain Life", ret => Me.HealthPercent < 70),
                CreateSpellCast("Health Funnel", ret => Me.GotAlivePet && Me.Pet.HealthPercent < 70),
                new Decorator(
                    ret => Me.CurrentTarget.Fleeing,
                    CreateCastPetAction(PetAction.AxeToss, true)),
                new Decorator(
                    ret => CurrentTargetIsElite,
                    new PrioritySelector(
                        CreateSpellBuffOnSelf("Metamorphosis"),
                        CreateSpellBuffOnSelf("Demon Soul"),
                        CreateSpellCast("Immolation Aura", ret => Me.CurrentTarget.Distance < 5f),
                        CreateSpellCast("Shadowflame", ret => Me.CurrentTarget.Distance < 5)
                        )),
                CreateSpellBuff("Immolate", ret => !Me.CurrentTarget.HasAura("Immolate")),
                CreateSpellBuff("Bane of Doom", ret => CurrentTargetIsElite && !Me.CurrentTarget.HasAura("Bane of Doom")),
                CreateSpellBuff("Bane of Agony", ret => !Me.CurrentTarget.HasAura("Bane of Agony") && !Me.CurrentTarget.HasAura("Bane of Doom")),
                CreateSpellBuff("Corruption", ret => !Me.CurrentTarget.HasAura("Corruption")),
                CreateSpellCast("Hand of Gul'dan"),
                // TODO: Make this cast Soulburn if it's available
                CreateSpellCast("Soul Fire", ret => Me.HasAura("Improved Soul Fire") || Me.HasAura("Soulburn")),
                CreateSpellCast("Soul Fire", ret => Me.HasAura("Decimation")),
                CreateSpellCast("Incinerate", ret => Me.HasAura("Molten Core")),
                CreateSpellCast("Shadow Bolt")
                );
        }
    }
}