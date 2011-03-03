﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-01 02:00:32 +0200 (Sal, 01 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/ClassSpecific/Druid/Common.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-01 02:00:32 +0200 (Sal, 01 Mar 2011) $
// $LastChangedRevision: 125 $
// $Revision: 125 $

#endregion

using System.Linq;

using Styx;
using Styx.Combat.CombatRoutine;

using TreeSharp;
using Styx.WoWInternals.WoWObjects;

namespace Singular
{
    partial class SingularRoutine
    {
        public ShapeshiftForm WantedDruidForm { get; set; }

        [Class(WoWClass.Druid)]
        [Behavior(BehaviorType.PreCombatBuffs)]
        [Spec(TalentSpec.BalanceDruid)]
        [Spec(TalentSpec.FeralDruid)]
        [Spec(TalentSpec.FeralTankDruid)]
        [Spec(TalentSpec.RestorationDruid)]
        [Context(WoWContext.All)]
        public Composite CreateDruidBuffComposite()
        {
            return new PrioritySelector(
				CreateSpellCast(
                    "Mark of the Wild",
                    ret => NearbyFriendlyPlayers.Any(u => !u.Dead && !u.IsGhost && u.IsInMyPartyOrRaid && CanCastMotWOn(u)),
					ret => Me)
                // TODO: Have it buff MotW when nearby party/raid members are missing the buff.
                );
        }

		public bool CanCastMotWOn(WoWUnit unit)
		{
			return !unit.HasAura("Mark of the Wild") &&
				   !unit.HasAura("Embrace of the Shale Spider") &&
				   !unit.HasAura("Blessing of Kings");
		}
    }
}