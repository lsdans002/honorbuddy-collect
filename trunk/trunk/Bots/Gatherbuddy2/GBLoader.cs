using System;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Globalization;
using Styx.Helpers;
using Styx.Plugins.PluginClass;
using TreeSharp;

namespace Styx.Bot.Plugins
{
    public class GbLoader : BotBase
    {
        private readonly BotBase _botBase;

        public GbLoader()
        {
			try
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				
				string path = Logging.ApplicationPath + @"\Bots\Gatherbuddy2\Gatherbuddy2.dll";
				Assembly asm = Assembly.LoadFrom(path);

				foreach(Type t in asm.GetTypes())
				{
					if(t.IsSubclassOf(typeof(BotBase)) && t.IsClass)
					{
						var obj = Activator.CreateInstance(t);
						_botBase = (BotBase)obj;
					}
				}
			}
			catch (System.Threading.ThreadAbortException) { throw; }
			catch (Exception e)
			{
				Logging.Write(Color.DarkRed, "An error occured while loading Gatherbuddy2");
				Logging.WriteDebug(e.ToString());
			}
        }

        #region Overrides of BotBase

        public override string Name
        {
            get { return _botBase.Name; }
        }

        public override Composite Root
        {
            get { return _botBase.Root; }
        }

        public override PulseFlags PulseFlags
        {
            get { return _botBase.PulseFlags; }
        }

        public override Form ConfigurationForm { get { return _botBase.ConfigurationForm; } }

        public override void Initialize()
        {
            _botBase.Initialize();
        }
		
		public override bool IsPrimaryType
		{
			get
			{
				return _botBase.IsPrimaryType;
			}
		}
		
		public override bool RequirementsMet
		{
			get
			{
				return _botBase.RequirementsMet;
			}
		}

        public override void Pulse()
        {
            _botBase.Pulse();
        }

        public override void Start()
        {
            _botBase.Start();
        }

        public override void Stop()
        {
            _botBase.Stop();
        }

        #endregion
    }
}
