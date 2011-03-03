﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-02-26 21:32:24 +0200 (Cmt, 26 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/ClassSpecific/Paladin/Retribution.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-02-26 21:32:24 +0200 (Cmt, 26 Şub 2011) $
// $LastChangedRevision: 121 $
// $Revision: 121 $

#endregion

using System.Linq;

using Styx.Combat.CombatRoutine;

using Singular.Settings;

using TreeSharp;

namespace Singular
{
    partial class SingularRoutine
    {
        [Class(WoWClass.Paladin)]
        [Spec(TalentSpec.RetributionPaladin)]
        [Behavior(BehaviorType.Combat)]
        [Behavior(BehaviorType.Heal)]
        [Context(WoWContext.All)]
        public Composite CreateRetributionPaladinCombat()
        {
            // Divine Purpose - Tab 3, Index 17
            return
                new PrioritySelector(
                    CreateEnsureTarget(),
                    // Make sure we're in range, and facing the damned target. (LOS check as well)
                    CreateRangeAndFace(5f, ret => Me.CurrentTarget),
                    CreateAutoAttack(true),
                    CreateSpellCast("Word of Glory", ret => Me.CurrentHolyPower >= 2 && Me.HealthPercent <= 50),
                    CreateSpellBuffOnSelf("Inquisition", ret => Me.CurrentHolyPower == 3),
                    CreateSpellCast("Hammer of Wrath"),
                    CreateSpellCast("Exorcism", ret => Me.ActiveAuras.ContainsKey("The Art of War")),
                    CreateSpellCast("Templar's Verdict", ret => Me.CurrentHolyPower == 3),
                    CreateSpellCast(
                        "Crusader Strike", ret => NearbyUnfriendlyUnits.Count(u => u.Distance < 8) < 4 || TalentManager.GetCount(3, 17) < 2),
                    CreateSpellCast(
                        "Divine Storm", ret => NearbyUnfriendlyUnits.Count(u => u.Distance < 8) >= 4 && TalentManager.GetCount(3, 17) >= 2),
                    CreateSpellCast("Templar's Verdict", ret => Me.HasAura("Hand of Light")),
                    CreateSpellCast("Judgement"),
                    CreateSpellCast("Holy Wrath"),
                    CreateSpellCast("Consecration")
                    );
        }

        [Class(WoWClass.Paladin)]
        [Spec(TalentSpec.RetributionPaladin)]
        [Behavior(BehaviorType.Pull)]
        [Context(WoWContext.All)]
        public Composite CreateRetributionPaladinPull()
        {
            return
                new PrioritySelector(
					CreateEnsureTarget(),
					CreateAutoAttack(true),
					CreateLosAndFace(ret => Me.CurrentTarget),
                    CreateSpellCast("Judgement"),
					CreateRangeAndFace(5f, ret => Me.CurrentTarget)
                    );
        }

        [Class(WoWClass.Paladin)]
        [Spec(TalentSpec.RetributionPaladin)]
        [Behavior(BehaviorType.CombatBuffs)]
        [Context(WoWContext.All)]
        public Composite CreateRetributionPaladinCombatBuffs()
        {
            return
                new PrioritySelector(
                    CreateSpellBuffOnSelf("Zealotry", ret => Me.CurrentTarget.MaxHealth > Me.MaxHealth * 10),
                    CreateSpellBuffOnSelf("Avenging Wrath", ret => !Me.HasAura("Zealotry") && NearbyUnfriendlyUnits.Count > 0),
                    CreateSpellBuffOnSelf("Lay on Hands", ret => Me.HealthPercent <= SingularSettings.Instance.Paladin.LayOnHandsHealthRet && !Me.HasAura("Forbearance")),
					CreateSpellBuffOnSelf("Divine Protection", ret => Me.HealthPercent <= SingularSettings.Instance.Paladin.DivineProtectionHealthRet)
                    );
        }

        [Class(WoWClass.Paladin)]
        [Spec(TalentSpec.RetributionPaladin)]
        [Behavior(BehaviorType.PreCombatBuffs)]
        [Context(WoWContext.All)]
        public Composite CreateRetributionPaladinPreCombatBuffs()
        {
            return
                new PrioritySelector(
                    CreateSpellBuffOnSelf(
                        "Blessing of Kings",
                        ret => (!Me.HasAura("Blessing of Might") || Me.Auras["Blessing of Might"].CreatorGuid != Me.Guid) &&
                               !Me.HasAura("Embrace of the Shale Spider") &&
                               !Me.HasAura("Mark of the Wild") &&
                               !Me.HasAura("Blessing of Kings")),
                    CreateSpellBuffOnSelf(
                        "Blessing of Might",
                        ret => (!Me.HasAura("Blessing of Kings") || Me.Auras["Blessing of Kings"].CreatorGuid != Me.Guid) &&
                               !Me.HasAura("Blessing of Might")),
                    CreateSpellBuffOnSelf("Seal of Truth", ret => !Me.HasAura("Seal of Truth")),
                    CreateSpellBuffOnSelf("Seal of Righteousness", ret => !Me.HasAura("Seal of Truth") && !Me.HasAura("Seal of Righteousness"))
                    );
        }
    }
}