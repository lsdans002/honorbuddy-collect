﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-01 02:00:32 +0200 (Sal, 01 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/ClassSpecific/Priest/Common.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-01 02:00:32 +0200 (Sal, 01 Mar 2011) $
// $LastChangedRevision: 125 $
// $Revision: 125 $

#endregion

using System.Linq;

using Styx.Combat.CombatRoutine;
using Styx.WoWInternals.WoWObjects;

using TreeSharp;

namespace Singular
{
    partial class SingularRoutine
    {
        [Class(WoWClass.Priest)]
        [Spec(TalentSpec.DisciplineHealingPriest)]
        [Spec(TalentSpec.DisciplinePriest)]
        [Spec(TalentSpec.HolyPriest)]
        [Spec(TalentSpec.ShadowPriest)]
        [Behavior(BehaviorType.PreCombatBuffs)]
        [Context(WoWContext.All)]
        public Composite CreatePriestCombatBuffs()
        {
            return new PrioritySelector(
                CreateSpellBuffOnSelf("Power Word: Fortitude", ret => CanCastFortitudeOn(Me)),
                CreateSpellBuffOnSelf("Inner Fire"),
                CreateSpellBuffOnSelf("Fear Ward"),
                CreateSpellCast(
                    "Power Word: Fortitude",
                    ret => NearbyFriendlyPlayers.Any(u => !u.Dead && !u.IsGhost && u.IsInMyPartyOrRaid && CanCastFortitudeOn(u)),
					ret => Me),
                CreateSpellBuffOnSelf("Shadowform"),
                CreateSpellBuffOnSelf("Vampiric Embrace")
                );
        }

        public bool CanCastFortitudeOn(WoWUnit unit)
        {
            return !unit.HasAura("Blood Pact") &&
                   !unit.HasAura("Power Word: Fortitude") &&
                   !unit.HasAura("Qiraji Fortitude") &&
                   !unit.HasAura("Commanding Shout");
        }
    }
}