#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-01 16:44:55 +0200 (Sal, 01 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Extensions.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-01 16:44:55 +0200 (Sal, 01 Mar 2011) $
// $LastChangedRevision: 128 $
// $Revision: 128 $

#endregion

using System.Text;
using System.Collections.Generic;
using System.Linq;

using Styx;
using Styx.WoWInternals.WoWObjects;
using Styx.Logic.Combat;

namespace Singular
{
    internal static class Extensions
    {
        /// <summary>
        ///   A string extension method that turns a Camel-case string into a spaced string. (Example: SomeCamelString -> Some Camel String)
        /// </summary>
        /// <remarks>
        ///   Created 2/7/2011.
        /// </remarks>
        /// <param name = "str">The string to act on.</param>
        /// <returns>.</returns>
        public static string CamelToSpaced(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsUpper(c))
                {
                    sb.Append(' ');
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static string SafeName(this WoWObject obj)
        {
            if (obj.IsMe)
                return "Myself";

            if (obj is WoWPlayer)
                return "Player";

            if (obj is WoWUnit && obj.ToUnit().IsPet)
                return "a Pet";

            return obj.Name;
        }

        public static bool IsPet(this WoWUnit unit)
        {
            return unit.SummonedByUnitGuid != 0 || unit.CharmedByUnitGuid != 0;
        }
    }
}