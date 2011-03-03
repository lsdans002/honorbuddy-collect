#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-02-18 14:28:58 +0200 (Cum, 18 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Settings/DeathKnightSettings.cs $
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
    internal class DeathKnightSettings : Styx.Helpers.Settings
    {
        public DeathKnightSettings()
            : base(SingularSettings.SettingsPath + "_DeathKnight.xml")
        {
        }
    }
}