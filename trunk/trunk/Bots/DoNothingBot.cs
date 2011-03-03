using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Styx;

using TreeSharp;

namespace DoNothingBot
{
    public class DoNothingBot : BotBase
    {
        public override string Name { get { return "DoNothing"; } }

        private Composite _root = new PrioritySelector();
        public override Composite Root { get { return _root; } }

        public override PulseFlags PulseFlags { get { return Styx.PulseFlags.All; } }
    }
}
