using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.Database;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using Styx;

namespace DHXeBuy
	{
	class DHXnpcFinder
		{
		public static NpcResult ClosestAmmoVendor
			{
			get
				{
				return NpcQueries.GetNearestNpc(StyxWoW.Me.FactionTemplate, ObjectManager.Me.MapId, ObjectManager.Me.Location, UnitNPCFlags.AmmoVendor);
				}
			}
		public static NpcResult ClosestFoodVendor
			{
			get
				{
				return NpcQueries.GetNearestNpc(StyxWoW.Me.FactionTemplate, ObjectManager.Me.MapId, ObjectManager.Me.Location, UnitNPCFlags.FoodVendor);
				}
			}
		public static NpcResult ClosestPoisonVendor
			{
			get
				{
				return NpcQueries.GetNearestNpc(StyxWoW.Me.FactionTemplate, ObjectManager.Me.MapId, ObjectManager.Me.Location, UnitNPCFlags.PoisionVendor);
				}
			}
		public static NpcResult ClosestReagentVendor
			{
			get
				{
				return NpcQueries.GetNearestNpc(StyxWoW.Me.FactionTemplate, ObjectManager.Me.MapId, ObjectManager.Me.Location, UnitNPCFlags.ReagentVendor);
				}
			}
		public static WoWUnit GetUnitByEntry(int entry)
			{//return unit if finds unit null if not
			ObjectManager.Update();//update objectmanager to ensure npc is stored in objectmanager
			foreach (WoWUnit wu in ObjectManager.GetObjectsOfType<WoWUnit>())
				{
				if (wu.Entry == entry)
					{
					return wu;
					}
				}
			return null;
			}
		}
	}
