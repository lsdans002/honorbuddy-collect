using Styx;
using Styx.Combat.CombatRoutine;
using Styx.Database;
using Styx.Helpers;
using Styx.Logic.Inventory;
using Styx.Plugins.PluginClass;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace DHXeBuy
	{
	public class DHXeBuy : HBPlugin
		{
		DHXeBuyConfigForm Form = new DHXeBuyConfigForm();
		Stopwatch sw = new Stopwatch();
		public bool FirstRun = true;
		public bool settingsLoaded = false;
		public static bool HaveGun = false;
		public static bool HaveBow = false;
		public static bool RationsRun = false;
		public static bool AmmoRun = false;
		public static bool PoisonRun = false;
		public static bool MeRogue = false;
		DHXeBuySettings settings = new DHXeBuySettings();
		public static Dictionary<string, string> FactionsRep = new Dictionary<string, string>();
		public static Dictionary<uint, string> FriendlyFactions = new Dictionary<uint, string>();
		List<Styx.WoWInternals.WoWObjects.WoWObject> UnitList = new List<Styx.WoWInternals.WoWObjects.WoWObject>();
		public static WoWFactionTemplate ifact = null;
		static DateTime blacklistfood = DateTime.Now;
		static DateTime blacklistdrink = DateTime.Now;
		static DateTime blacklistammo = DateTime.Now;
		static DateTime blacklistpoison1 = DateTime.Now;
		static DateTime blacklistpoison2 = DateTime.Now;
		public override string Author { get { return "DHX: Protopally"; } }
		public override string Name { get { return "DHX eBuy"; } }
		public override Version Version { get { return new Version(3, 2, 3); } }
		public override bool WantButton { get { return true; } }
		public override void Pulse()
			{
			if (!sw.IsRunning)
				sw.Start();
			if (FirstRun)
				{
				if (StyxWoW.Me.Class == WoWClass.Rogue)
					MeRogue = true;
				FactionRep();
				UsableFactions();
				FirstRun = false;
				}
			#region eRefershment
			if (!Styx.WoWInternals.ObjectManager.Me.Combat && sw.Elapsed.TotalSeconds > 60)
				{
				string usefoodName = DHXItemChecker.refdetectFood();
				string usedrinkName = DHXItemChecker.refdetectDrink();
				if (usefoodName != "")
					{
					LevelbotSettings.Instance.FoodName = usefoodName;
					}
				if (usedrinkName != "")
					{
					LevelbotSettings.Instance.DrinkName = usedrinkName;
					}
				sw.Reset();
				sw.Start();
				}
			#endregion
			settings.LoadSettings();
			if (!OkayToTakeOverThread())
				return;
			if (settings.EnableBuy)
				{
				#region Food & Drink
				if (NeedDrink() || NeedFood())
					{
					RationsRun = true;
					NpcResult vendor = DHXnpcFinder.ClosestFoodVendor;
					#region manmmoth
					if (settings.HaveMammoth)
						{
						Logging.Write(System.Drawing.Color.Purple, "Using Mammoth to buy Rations");
						#region Horde
						if (StyxWoW.Me.IsHorde)
							{
							if (!AuraChkByID(61446) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61447);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61446))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						#region Alliance
						if (StyxWoW.Me.IsAlliance)
							{
							if (!AuraChkByID(61424) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61425);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61424))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						}
					#endregion
					#region not good vendor
					if (!GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Closest Vendor is Hostile..Aborting eBuy Run.");
						}
					#endregion
					#region good vendor
					if (GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Moving to buy Rations!");
						if (!eNavigator.MoveToLocation(vendor.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Entered combat or something similar! Stopping food+drink run!");
							return;
							}
						WoWUnit vendorUnit = DHXnpcFinder.GetUnitByEntry(vendor.Entry);
						if (vendorUnit == null)
							{//failed to find selected vendor - blacklist buying food for 1 hour
							BlacklistRationsBuying(60, "buying Rations", "selected vendor could not be located");
							return;
							}
						if (!eNavigator.MoveToLocation(vendorUnit.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Failed to walk there?");
							return;
							}
						Thread.Sleep(3000);
						vendorUnit.Interact();
						BuyRoutine();
						}
					#endregion
					}
				#endregion
				#region Poisons
				ObjectManager.Update();
				if (NeedPoison() && MeRogue)
					{
					PoisonRun = true;
					NpcResult vendor = DHXnpcFinder.ClosestPoisonVendor;
					#region mammoth
					if (settings.HaveMammoth)
						{
						Logging.Write(System.Drawing.Color.Purple, "Using Mammoth to buy Poison");
						#region Horde
						if (StyxWoW.Me.IsHorde)
							{
							if (!AuraChkByID(61446) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61447);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61446))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32639)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						#region Alliance
						if (StyxWoW.Me.IsAlliance)
							{
							if (!AuraChkByID(61424) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61425);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61424))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32639)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						}
					#endregion
					#region not good vendor
					if (!GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Closest Vendor is Hostile..Aborting eBuy Run.");
						}
					#endregion
					#region good vendor
					if (GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Moving to buy Poison");
						if (!eNavigator.MoveToLocation(vendor.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Entered combat or something similar! Stopping Poison run!");
							return;
							}
						WoWUnit vendorUnit = DHXnpcFinder.GetUnitByEntry(vendor.Entry);
						if (vendorUnit == null)
							{//failed to find selected vendor - blacklist buying food for 1 hour
							BlacklistPoisonBuying(60, "buying Poison", "selected vendor could not be located");
							return;
							}
						if (!eNavigator.MoveToLocation(vendorUnit.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Failed to walk there?");
							return;
							}
						Thread.Sleep(3000);
						vendorUnit.Interact();
						BuyRoutine();
						return;
						}
					#endregion
					}
				#endregion
				#region Ammo
				#region Arrows
				if (NeedArrow() && settings.BuyAmmo && HaveBow)
					{
					AmmoRun = true;
					NpcResult vendor = DHXnpcFinder.ClosestAmmoVendor;
					#region mammoth
					if (settings.HaveMammoth)
						{
						Logging.Write(System.Drawing.Color.Purple, "Using Mammoth to buy Arrows!!");
						#region Horde
						if (StyxWoW.Me.IsHorde)
							{
							if (!AuraChkByID(61446) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61447);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61446))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						#region Alliance
						if (StyxWoW.Me.IsAlliance)
							{
							if (!AuraChkByID(61424) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61425);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61424))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
						#endregion
							}
					#endregion
						#region not good vendor
						if (!GoodVendor(vendor))
							{
							Logging.Write(System.Drawing.Color.Purple, "Closest Vendor is Hostile..Aborting eBuy Run.");
							}
						#endregion
						#region good vendor
						if (GoodVendor(vendor))
							{
							Logging.Write(System.Drawing.Color.Purple, "Moving to buy Arrows!");
							if (!eNavigator.MoveToLocation(vendor.Location))
								{//if interupted while moving to vendor release thread
								Logging.Write(System.Drawing.Color.Purple, "Entered combat or something similar! Stopping food+drink run!");
								return;
								}
							WoWUnit vendorUnit = DHXnpcFinder.GetUnitByEntry(vendor.Entry);
							if (vendorUnit == null)
								{//failed to find selected vendor - blacklist buying food for 1 hour
								BlacklistAmmoBuying(60, "buying Arrows", "selected vendor could not be located");
								return;
								}
							if (!eNavigator.MoveToLocation(vendorUnit.Location))
								{//if interupted while moving to vendor release thread
								Logging.Write(System.Drawing.Color.Purple, "Failed to walk there?");
								return;
								}
							Thread.Sleep(3000);
							vendorUnit.Interact();
							BuyRoutine();
							}
						}
						#endregion
					}
				#endregion
				#region Bullets
				if (NeedBullet() && settings.BuyAmmo && HaveGun)
					{
					AmmoRun = true;
					NpcResult vendor = DHXnpcFinder.ClosestAmmoVendor;
					#region mammoth
					if (settings.HaveMammoth)
						{
						Logging.Write(System.Drawing.Color.Purple, "Using Mammoth to buy Bullets");
						#region Horde
						if (StyxWoW.Me.IsHorde)
							{
							if (!AuraChkByID(61446) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61447);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61446))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						#region Alliance
						if (StyxWoW.Me.IsAlliance)
							{
							if (!AuraChkByID(61424) && StyxWoW.Me.Mounted)
								{
								Styx.Logic.Mount.Dismount();
								}
							if (!StyxWoW.Me.Mounted)
								{
								Styx.Logic.Combat.SpellManager.CastSpellById(61425);
								Thread.Sleep(5000);
								}
							if (AuraChkByID(61424))
								{
								ObjectManager.Update();
								UnitList = ObjectManager.ObjectList;
								foreach (WoWObject obj in UnitList)
									{
									if (obj.Entry == 32638)
										{
										obj.Interact();
										BuyRoutine();
										return;
										}
									}
								}
							}
						#endregion
						}
					#endregion
					#region not good vendor
					if (!GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Closest Vendor is Hostile..Aborting eBuy Run.");
						}
					#endregion
					#region good vendor
					if (GoodVendor(vendor))
						{
						Logging.Write(System.Drawing.Color.Purple, "Moving to buy Bullets!");
						if (!eNavigator.MoveToLocation(vendor.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Entered combat or something similar! Stopping food+drink run!");
							return;
							}
						WoWUnit vendorUnit = DHXnpcFinder.GetUnitByEntry(vendor.Entry);
						if (vendorUnit == null)
							{//failed to find selected vendor - blacklist buying food for 1 hour
							BlacklistAmmoBuying(60, "buying Bullets", "selected vendor could not be located");
							return;
							}
						if (!eNavigator.MoveToLocation(vendorUnit.Location))
							{//if interupted while moving to vendor release thread
							Logging.Write(System.Drawing.Color.Purple, "Failed to walk there?");
							return;
							}
						Thread.Sleep(3000);
						vendorUnit.Interact();
						BuyRoutine();
						}
					#endregion
					}
				#endregion
				#endregion
				}
			}
		public override void OnButtonPress()
			{
			Form.ShowDialog();
			}
		public static bool GoodVendor(NpcResult vendor)
			{
			foreach (KeyValuePair<uint, string> KVP in FriendlyFactions)
				{
				if (vendor.Faction == KVP.Key)
					{
					return true;
					}
				}
			return false;
			}
		public static bool NeedFood()
			{
			if (blacklistfood > DateTime.Now)
				return false;
			if (DHXItemChecker.detectFood() == null)
				{
				return true;
				}
			else return false;
			}
		public static bool NeedDrink()
			{
			if (blacklistdrink > DateTime.Now)
				return false;
			List<WoWClass> ClassesNotNeedingDrink = new List<WoWClass>(new WoWClass[] { WoWClass.DeathKnight, WoWClass.Rogue, WoWClass.Warrior });
			if (ClassesNotNeedingDrink.Contains(StyxWoW.Me.Class))
				{
				return false;
				}
			if (DHXItemChecker.detectDrink() == null)
				{
				return true;
				}
			else return false;
			}
		public static void BlacklistRationsBuying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistfood = DateTime.Now.AddMinutes(timeToBlacklistFor);
			blacklistdrink = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistPoisonBuying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistpoison1 = DateTime.Now.AddMinutes(timeToBlacklistFor);
			blacklistpoison2 = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistFoodBuying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistfood = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistDrinkBuying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistdrink = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistAmmoBuying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistammo = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistPoison1Buying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistpoison1 = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static void BlacklistPoison2Buying(int timeToBlacklistFor, string typeOfBuying, string reason)
			{
			blacklistpoison2 = DateTime.Now.AddMinutes(timeToBlacklistFor);
			Logging.Write(System.Drawing.Color.Purple, "Blacklisting " + typeOfBuying + " for " + timeToBlacklistFor.ToString() + " because " + reason + ".");
			}
		public static bool OkayToTakeOverThread()
			{
			bool BuyRun = false;
			if (NeedDrink()) BuyRun = true;
			if (NeedFood()) BuyRun = true;
			if (NeedPoison()) BuyRun = true;
			if (NeedArrow()) BuyRun = true;
			if (NeedBullet()) BuyRun = true;
			if (!NeedDrink() && !NeedFood() && !NeedPoison() && !NeedArrow() && !NeedBullet())
				{
				BuyRun = false;
				}
			if (StyxWoW.Me.Combat)
				{
				return false;
				}
			if (StyxWoW.Me.IsResting && !BuyRun)
				{
				return false;
				}
			if (StyxWoW.Me.IsInInstance)
				{
				return false;
				}
			if (StyxWoW.Me.HealthPercent < 50)
				{
				return false;
				}
			if (StyxWoW.Me.IsFlying)
				{
				return false;
				}
			if (StyxWoW.Me.IsFalling)
				{
				return false;
				}
			if (StyxWoW.Me.IsCasting)
				{
				return false;
				}
			return true;
			}
		public static WoWFactionTemplate GetFaction(uint ID)
			{
			return Styx.WoWInternals.WoWFactionTemplate.FromId(ID);
			}
		private static void FactionRep()
			{
			FactionsRep.Clear();
			Lua.DoString("ExpandAllFactionHeaders(0)");
			int FactionCount = Lua.GetReturnVal<int>("return select(1, GetNumFactions())", 0);
			for (int i = 1; i <= FactionCount; i++)
				{
				List<string> FactionInfo = Lua.GetReturnValues("return GetFactionInfo(" + i + ")", "Reputation.lua");
				if ((FactionInfo[8] == "1" || FactionInfo[9] == "1") && FactionInfo[10] != "1")
					continue;

				string FactionStatus = null;
				if (FactionInfo[2] == "8")
					FactionStatus = "Friendly";
				else if (FactionInfo[2] == "7")
					FactionStatus = "Friendly";
				else if (FactionInfo[2] == "6")
					FactionStatus = "Friendly";
				else if (FactionInfo[2] == "5")
					FactionStatus = "Friendly";
				else if (FactionInfo[2] == "4")
					FactionStatus = "Neutral";
				else if (FactionInfo[2] == "3")
					FactionStatus = "Hostile";
				else if (FactionInfo[2] == "2")
					FactionStatus = "Hostile";
				else if (FactionInfo[2] == "1")
					FactionStatus = "Hostile";
				else
					FactionStatus = "Hostile";
				FactionsRep.Add(FactionInfo[0], FactionStatus);
				}
			}
		public static void UsableFactions()
			{
			for (uint i = 0; i < 10001; i++)
				{
				try
					{
					ifact = GetFaction(i);
					string CompName = ifact.Faction.Name;
					string CompStatus;
					if (FactionsRep.ContainsKey(CompName))
						{
						if (FactionsRep.TryGetValue(CompName, out CompStatus))
							{
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Friendly") && (CompStatus == "Friendly"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Friendly") && (CompStatus == "Neutral"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Neutral") && (CompStatus == "Friendly"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Neutral") && (CompStatus == "Neutral"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Hostile") && (CompStatus == "Friendly"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							if ((ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Hostile") && (CompStatus == "Neutral"))
								{
								FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
								}
							}
						}
					else
						{
						if (ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Friendly")
							{
							FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
							}
						if (ifact.GetReactionTowards(StyxWoW.Me.FactionTemplate).ToString() == "Neutral")
							{
							FriendlyFactions.Add(ifact.Id, ifact.Faction.Name);
							}
						}
					}
				catch { continue; }
				finally { }
				}
			}
		public static void BuyRoutine()
			{
			DHXeBuySettings settings = new DHXeBuySettings();
			settings.LoadSettings();
			Thread.Sleep(5000);
			try
				{
				Styx.Logic.Inventory.Frames.Gossip.GossipFrame frame = new Styx.Logic.Inventory.Frames.Gossip.GossipFrame();
				if (frame.IsVisible)
					{
					List<Styx.Logic.Inventory.Frames.Gossip.GossipEntry> gossips = frame.GossipOptionEntries;
					foreach (Styx.Logic.Inventory.Frames.Gossip.GossipEntry goss in gossips)
						{
						if (goss.Type == Styx.Logic.Inventory.Frames.Gossip.GossipEntry.GossipEntryType.Vendor)
							{
							frame.SelectGossipOption(goss.Index);
							Thread.Sleep(5000);
							}
						}
					}
				}
			catch { }
			try
				{
				Styx.Logic.Vendors.SellAllItems();
				}
			catch { }
			try
				{
				Styx.Logic.Vendors.RepairAllItems();
				}
			catch { }
			#region Food & Drink
			if (NeedDrink() && RationsRun)
				{
				if (!DHXVendorAssist.BuyItem(DHXItemType.Drink, 5, settings.TDTB))
					{
					BlacklistDrinkBuying(60, "buying drink", "could not find an item to buy at selected vendor");
					}
				ObjectManager.Update();
				}
			if (NeedFood() && RationsRun)
				{
				if (!DHXVendorAssist.BuyItem(DHXItemType.Food, 5, settings.TFTB))
					{
					BlacklistFoodBuying(60, "buying food", "could not find an item to buy at selected vendor");
					}
				ObjectManager.Update();
				}
			#endregion
			#region Poisons
			#region Instant
			if (DHXItemChecker.detectInstPoison() == null && (settings.PTB1 == 1 || settings.PTB2 == 1) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 1)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Instant Poison", "could not find an item to buy at selected vendor");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 1)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Instant Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#region Deadly
			if (DHXItemChecker.detectDeadPoison() == null && (settings.PTB1 == 2 || settings.PTB2 == 2) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 2)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Deadly Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 2)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Deadly Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#region Wound
			if (DHXItemChecker.detectWoundPoison() == null && (settings.PTB1 == 3 || settings.PTB2 == 3) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 3)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Wound Poison", "was not for sale at selected vendorr");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 3)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Wound Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#region Anestetic
			if (DHXItemChecker.detectAnePoison() == null && (settings.PTB1 == 4 || settings.PTB2 == 4) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 4)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Anestetic Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 4)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Anestetic Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#region Crippling
			if (DHXItemChecker.detectCripPoison() == null && (settings.PTB1 == 5 || settings.PTB2 == 5) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 5)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Crippling Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 5)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Crippling Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#region Mindnumbing
			if (DHXItemChecker.detectNumbPoison() == null && (settings.PTB1 == 6 || settings.PTB2 == 6) && settings.BuyPoisons && PoisonRun)
				{
				if (settings.PTB1 == 6)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison1, 1, settings.PA1))
						{
						BlacklistPoison1Buying(60, "buying Mind-numbing Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				if (settings.PTB2 == 6)
					{
					if (!DHXVendorAssist.BuyItem(DHXItemType.Poison2, 1, settings.PA2))
						{
						BlacklistPoison2Buying(60, "buying Mind-numbing Poison", "was not for sale at selected vendor");
						}
					ObjectManager.Update();
					}
				}
			#endregion
			#endregion
			#region Ammo
			if (DHXItemChecker.detectArrows() == null && settings.BuyAmmo && HaveBow && AmmoRun)
				{
				if (!DHXVendorAssist.BuyItem(DHXItemType.Ammo, 200, settings.AATB))
					{
					BlacklistAmmoBuying(60, "buying Arrows", "was not for sale at selected vendor");
					}
				ObjectManager.Update();
				}
			if (DHXItemChecker.detectBullets() == null && settings.BuyAmmo && HaveGun && AmmoRun)
				{
				if (!DHXVendorAssist.BuyItem(DHXItemType.Ammo, 200, settings.AATB))
					{
					BlacklistAmmoBuying(60, "buying Bullets", "was not for sale at selected vendor");
					}
				ObjectManager.Update();
				}
			#endregion
			Thread.Sleep(5000);
			Logging.Write(System.Drawing.Color.Purple, "Done buying!");
			AmmoRun = false;
			PoisonRun = false;
			RationsRun = false;
			}
		public static bool AuraChkByID(uint ID) ///returns true if you have aura with WowHead id ID.
			{
			ObjectManager.Update();
			List<Styx.Logic.Combat.WoWAura> GetBuffs = StyxWoW.Me.GetAllAuras();
			foreach (Styx.Logic.Combat.WoWAura Buff in GetBuffs)
				{
				if (Buff.SpellId == ID)
					return true;
				}
			return false;
			}
		public static bool NeedPoison()
			{
			DHXeBuySettings settings = new DHXeBuySettings();
			ObjectManager.Update();
			settings.LoadSettings();
			if ((blacklistpoison1 > DateTime.Now) && (blacklistpoison2 > DateTime.Now))
				return false;
			if (settings.BuyPoisons)
				{
				if (DHXItemChecker.detectInstPoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 1) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 1))) { return true; }
				if (DHXItemChecker.detectDeadPoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 2) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 2))) { return true; }
				if (DHXItemChecker.detectWoundPoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 3) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 3))) { return true; }
				if (DHXItemChecker.detectAnePoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 4) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 4))) { return true; }
				if (DHXItemChecker.detectCripPoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 5) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 5))) { return true; }
				if (DHXItemChecker.detectNumbPoison() == null && ((!(blacklistpoison1 > DateTime.Now) && settings.PTB1 == 6) || (!(blacklistpoison2 > DateTime.Now) && settings.PTB2 == 6))) { return true; }
				}
			return false;
			}
		public static bool NeedArrow()
			{
			if (blacklistammo > DateTime.Now)
				return false;
			WoWItem rangedwepslot = StyxWoW.Me.Inventory.GetItemBySlot(17);
			if (rangedwepslot != null)
				{
				if (rangedwepslot.ItemInfo.WeaponClass == Styx.WoWItemWeaponClass.Bow)
					{
					HaveBow = true;
					}
				if (rangedwepslot.ItemInfo.WeaponClass == Styx.WoWItemWeaponClass.Crossbow)
					{
					HaveBow = true;
					}
				else
					{
					HaveBow = false;
					}
				if (DHXItemChecker.detectArrows() == null && HaveBow)
					{
					return true;
					}
				else return false;
				}
			else return false;
			}
		public static bool NeedBullet()
			{
			if (blacklistammo > DateTime.Now)
				return false;
			WoWItem rangedwepslot = StyxWoW.Me.Inventory.GetItemBySlot(17); if (rangedwepslot != null)
				{
				if (rangedwepslot.ItemInfo.WeaponClass == Styx.WoWItemWeaponClass.Gun)
					{
					HaveGun = true;
					}
				else
					{
					HaveGun = false;
					}
				if (DHXItemChecker.detectBullets() == null && HaveGun)
					{
					return true;
					}
				else return false;
				}
			return false;
			}
		}
	}
