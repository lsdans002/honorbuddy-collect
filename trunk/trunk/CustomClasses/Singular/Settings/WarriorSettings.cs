#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-02-18 14:28:58 +0200 (Cum, 18 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Settings/WarriorSettings.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-02-18 14:28:58 +0200 (Cum, 18 Şub 2011) $
// $LastChangedRevision: 74 $
// $Revision: 74 $

#endregion

using System.ComponentModel;

using Styx;
using Styx.Helpers;

using DefaultValue = Styx.Helpers.DefaultValueAttribute;

namespace Singular.Settings
{
    internal class WarriorSettings : Styx.Helpers.Settings
    {
        public WarriorSettings()
            : base(SingularSettings.SettingsPath + "_Warrior.xml")
        {
        }

		[Setting]
		[DefaultValue(50)]
		[Category("Protection")]
		[DisplayName("Enraged Regeneration Health")]
		[Description("Enrage Regeneration will be used when your health drops below this value")]
		public int WarriorEnragedRegenerationHealth { get; set; }

		[Setting]
		[DefaultValue(40)]
		[Category("Protection")]
		[DisplayName("Shield Wall Health")]
		[Description("Shield Wall will be used when your health drops below this value")]
		public int WarriorProtShieldWallHealth { get; set; }
    }
}