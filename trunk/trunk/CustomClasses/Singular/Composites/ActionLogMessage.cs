#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: apoc $
// $Date: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Composites/ActionLogMessage.cs $
// $LastChangedBy: apoc $
// $LastChangedDate: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $LastChangedRevision: 72 $
// $Revision: 72 $

#endregion

using TreeSharp;

namespace Singular.Composites
{
    internal class ActionLogMessage : Action
    {
        private readonly bool _debug;

        private readonly string _message;

        public ActionLogMessage(bool debug, string message)
        {
            _message = message;
            _debug = debug;
        }

        protected override RunStatus Run(object context)
        {
            if (_debug)
            {
                Logger.WriteDebug(_message);
            }
            else
            {
                Logger.Write(_message);
            }

            if (Parent is Selector)
            {
                return RunStatus.Failure;
            }
            return RunStatus.Success;
        }
    }
}