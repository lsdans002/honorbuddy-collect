#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: apoc $
// $Date: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Logger.cs $
// $LastChangedBy: apoc $
// $LastChangedDate: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $LastChangedRevision: 72 $
// $Revision: 72 $

#endregion

using System.Drawing;

using Styx.Helpers;

namespace Singular
{
    internal class Logger
    {
        static Logger()
        {
            WriteDebugMessages = true;
        }

        public static bool WriteDebugMessages { get; set; }

        public static void Write(string message)
        {
            Logging.Write(Color.Green, "[Singular] " + message);
        }

        public static void WriteDebug(string message)
        {
            if (!WriteDebugMessages)
            {
                return;
            }

            Logging.WriteDebug(Color.Green, "[Singular-DEBUG] " + message);
        }
    }
}