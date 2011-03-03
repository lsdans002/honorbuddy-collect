﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-02-18 14:28:58 +0200 (Cum, 18 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Settings/DruidSettings.cs $
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
    internal class DruidSettings : Styx.Helpers.Settings
    {
        public DruidSettings()
            : base(SingularSettings.SettingsPath + "_Druid.xml")
        {
        }

		[Setting]
		[DefaultValue(40)]
		[Category("Common")]
		[DisplayName("Innervate Mana")]
		[Description("Innervate will be used when your mana drops below this value")]
		public int InnervateMana { get; set; }

		[Setting]
		[DefaultValue(60)]
		[Category("Restoration")]
		[DisplayName("Tranquility Health")]
		[Description("Tranquility will be used at this value")]
		public int TranquilityHealth { get; set; }

		[Setting]
		[DefaultValue(3)]
		[Category("Restoration")]
		[DisplayName("Tranquility Count")]
		[Description("Tranquility will be used when count of party members whom health is below Tranquility health mets this value ")]
		public int TranquilityCount { get; set; }

		[Setting]
		[DefaultValue(65)]
		[Category("Restoration")]
		[DisplayName("Swiftmend Health")]
		[Description("Swiftmend will be used at this value")]
		public int Swiftmend { get; set; }

		[Setting]
		[DefaultValue(80)]
		[Category("Restoration")]
		[DisplayName("Wild Growth Health")]
		[Description("Wild Growth will be used at this value")]
		public int WildGrowthHealth { get; set; }

		[Setting]
		[DefaultValue(2)]
		[Category("Restoration")]
		[DisplayName("Wild Growth Count")]
		[Description("Wild Growth will be used when count of party members whom health is below Wild Growth health mets this value ")]
		public int WildGrowthCount { get; set; }

		[Setting]
		[DefaultValue(70)]
		[Category("Restoration")]
		[DisplayName("Regrowth Health")]
		[Description("Regrowth will be used at this value")]
		public int Regrowth { get; set; }

		[Setting]
		[DefaultValue(60)]
		[Category("Restoration")]
		[DisplayName("Healing Touch Health")]
		[Description("Healing Touch will be used at this value")]
		public int HealingTouch { get; set; }

		[Setting]
		[DefaultValue(75)]
		[Category("Restoration")]
		[DisplayName("Nourish Health")]
		[Description("Nourish will be used at this value")]
		public int Nourish { get; set; }

		[Setting]
		[DefaultValue(90)]
		[Category("Restoration")]
		[DisplayName("Rejuvenation Health")]
		[Description("Rejuvenation will be used at this value")]
		public int Rejuvenation { get; set; }

		[Setting]
		[DefaultValue(80)]
		[Category("Restoration")]
		[DisplayName("Tree of Life Health")]
		[Description("Tree of Life will be used at this value")]
		public int TreeOfLifeHealth { get; set; }

		[Setting]
		[DefaultValue(3)]
		[Category("Restoration")]
		[DisplayName("Tree of Life Count")]
		[Description("Tree of Life will be used when count of party members whom health is below Tree of Life health mets this value ")]
		public int TreeOfLifeCount { get; set; }

		[Setting]
		[DefaultValue(70)]
		[Category("Restoration")]
		[DisplayName("Barkskin Health")]
		[Description("Barkskin will be used at this value")]
		public int Barkskin { get; set; }


    }
}