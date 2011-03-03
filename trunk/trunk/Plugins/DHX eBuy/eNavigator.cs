using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.Logic.Pathing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Threading;
using Styx.Helpers;
namespace DHXeBuy
	{
	static class eNavigator
		{
		public static bool MoveToLocation(WoWPoint location)
			{//returns false if movement interupted true if reached location. false should exit all code
			Navigator.Clear();
			while (true)
				{
				if (!DHXeBuy.OkayToTakeOverThread())
					{
					return false;
					}
				Navigator.MoveTo(location);
				if (location.Distance(ObjectManager.Me.Location) < 5)
					{
					return true;
					}
				Thread.Sleep(100);
				}
			}
		}
	}
