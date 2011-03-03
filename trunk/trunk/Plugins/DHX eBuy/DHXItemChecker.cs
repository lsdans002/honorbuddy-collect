using Styx.Helpers;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;

namespace DHXeBuy
	{
	class DHXItemChecker
		{
		public static string detectArrows()
			{
			List<string> Arrows = new List<string>();
			string Arrow;
			if (ObjectManager.Me.Level > 79)
				{
				Arrows.Add("52021");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}

			if (ObjectManager.Me.Level > 74)
				{
				Arrows.Add("41586");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 71)
				{
				Arrows.Add("41165");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 69)
				{
				Arrows.Add("34581");
				Arrows.Add("32760");
				Arrows.Add("31737");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 67)
				{
				Arrows.Add("31949");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 65)
				{
				Arrows.Add("30611");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 64)
				{
				Arrows.Add("28056");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 61)
				{
				Arrows.Add("33803");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 60)
				{
				Arrows.Add("24417");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 54)
				{
				Arrows.Add("28053");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 53)
				{
				Arrows.Add("12654");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 51)
				{
				Arrows.Add("18042");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 50)
				{
				Arrows.Add("19316");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 39)
				{
				Arrows.Add("11285");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				} if (ObjectManager.Me.Level > 36)
				{
				Arrows.Add("10579");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 34)
				{
				Arrows.Add("9399");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 24)
				{
				Arrows.Add("3030");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > 9)
				{
				Arrows.Add("2515");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			if (ObjectManager.Me.Level > -1)
				{
				Arrows.Add("2512");
				Arrows.Add("3464");
				if ((Arrow = findItems(Arrows)) != "")
					{
					return Arrow;
					}
				Arrows.Clear();
				}
			return null;
			}
		public static string detectBullets()
			{
			List<string> Bullets = new List<string>();
			string Bullet;
			if (ObjectManager.Me.Level > 79)
				{
				Bullets.Add("52020");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}

			if (ObjectManager.Me.Level > 74)
				{
				Bullets.Add("41584");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 71)
				{
				Bullets.Add("41164");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 69)
				{
				Bullets.Add("34582");
				Bullets.Add("32761");
				Bullets.Add("31735");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 67)
				{
				Bullets.Add("32883");
				Bullets.Add("32882");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 65)
				{
				Bullets.Add("30612");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 64)
				{
				Bullets.Add("28061");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 61)
				{
				Bullets.Add("23773");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 56)
				{
				Bullets.Add("23772");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 55)
				{
				Bullets.Add("13377");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 54)
				{
				Bullets.Add("28060");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 51)
				{
				Bullets.Add("15997");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 50)
				{
				Bullets.Add("19317");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 46)
				{
				Bullets.Add("11630");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				} if (ObjectManager.Me.Level > 43)
				{
				Bullets.Add("10513");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 39)
				{
				Bullets.Add("11284");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 36)
				{
				Bullets.Add("10512");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 29)
				{
				Bullets.Add("8069");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 24)
				{
				Bullets.Add("3033");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 14)
				{
				Bullets.Add("8068");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 12)
				{
				Bullets.Add("5568");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 9)
				{
				Bullets.Add("2519");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > 4)
				{
				Bullets.Add("8067");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			if (ObjectManager.Me.Level > -1)
				{
				Bullets.Add("2516");
				Bullets.Add("3465");
				Bullets.Add("4960");
				if ((Bullet = findItems(Bullets)) != "")
					{
					return Bullet;
					}
				Bullets.Clear();
				}
			return null;
			}
		public static string detectInstPoison()
			{
			List<string> InstPoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 78)
				{
				InstPoisons.Add("43231");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}

			if (ObjectManager.Me.Level > 72)
				{
				InstPoisons.Add("43230");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 67)
				{
				InstPoisons.Add("21927");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 59)
				{
				InstPoisons.Add("8928");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 51)
				{
				InstPoisons.Add("8927");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 43)
				{
				InstPoisons.Add("8926");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 35)
				{
				InstPoisons.Add("6950");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 27)
				{
				InstPoisons.Add("8949");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 19)
				{
				InstPoisons.Add("6947");
				if ((Poison = findItems(InstPoisons)) != "")
					{
					return Poison;
					}
				InstPoisons.Clear();
				}
			return null;
			}
		public static string detectDeadPoison()
			{
			List<string> DeadPoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 79)
				{
				DeadPoisons.Add("43233");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}

			if (ObjectManager.Me.Level > 75)
				{
				DeadPoisons.Add("43232");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 69)
				{
				DeadPoisons.Add("22054");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 61)
				{
				DeadPoisons.Add("22053");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 59)
				{
				DeadPoisons.Add("20844");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 53)
				{
				DeadPoisons.Add("8985");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 45)
				{
				DeadPoisons.Add("8984");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 37)
				{
				DeadPoisons.Add("2893");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 29)
				{
				DeadPoisons.Add("2892");
				if ((Poison = findItems(DeadPoisons)) != "")
					{
					return Poison;
					}
				DeadPoisons.Clear();
				}
			return null;
			}
		public static string detectAnePoison()
			{
			List<string> AnePoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 76)
				{
				AnePoisons.Add("43237");
				if ((Poison = findItems(AnePoisons)) != "")
					{
					return Poison;
					}
				AnePoisons.Clear();
				}
			if (ObjectManager.Me.Level > 67)
				{
				AnePoisons.Add("21835");
				if ((Poison = findItems(AnePoisons)) != "")
					{
					return Poison;
					}
				AnePoisons.Clear();
				}
			return null;
			}
		public static string detectNumbPoison()
			{
			List<string> NumbPoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 23)
				{
				NumbPoisons.Add("5237");
				if ((Poison = findItems(NumbPoisons)) != "")
					{
					return Poison;
					}
				NumbPoisons.Clear();
				}
			return null;
			}
		public static string detectCripPoison()
			{
			List<string> CripPoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 23)
				{
				CripPoisons.Add("3775");
				if ((Poison = findItems(CripPoisons)) != "")
					{
					return Poison;
					}
				CripPoisons.Clear();
				}
			return null;
			}
		public static string detectWoundPoison()
			{
			List<string> WoundPoisons = new List<string>();
			string Poison;
			if (ObjectManager.Me.Level > 77)
				{
				WoundPoisons.Add("43235");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}

			if (ObjectManager.Me.Level > 71)
				{
				WoundPoisons.Add("43234");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 63)
				{
				WoundPoisons.Add("22055");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 55)
				{
				WoundPoisons.Add("10922");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 47)
				{
				WoundPoisons.Add("10921");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 39)
				{
				WoundPoisons.Add("10920");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			if (ObjectManager.Me.Level > 31)
				{
				WoundPoisons.Add("10918");
				if ((Poison = findItems(WoundPoisons)) != "")
					{
					return Poison;
					}
				WoundPoisons.Clear();
				}
			return null;
			}
		public static string detectDrink()
			{
			List<string> drinks = new List<string>();
			string drink;
			if (ObjectManager.Me.Level > 79)
				{
				drinks.Add("43523");
				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level > 74)
				{
				drinks.Add("45932");
				drinks.Add("33445");
				drinks.Add("39520");
				drinks.Add("41731");
				drinks.Add("42777");
				drinks.Add("43236");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level > 73)
				{
				drinks.Add("43518");
				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level > 69)
				{
				drinks.Add("33444");
				drinks.Add("38698");
				drinks.Add("43086");
				drinks.Add("44941");
				drinks.Add("34759");
				drinks.Add("34760");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 64)
				{
				drinks.Add("22018");
				drinks.Add("27860");
				drinks.Add("29395");
				drinks.Add("29401");
				drinks.Add("30457");
				drinks.Add("32453");
				drinks.Add("32668");
				drinks.Add("33042");
				drinks.Add("34411");
				drinks.Add("35954");
				drinks.Add("37253");
				drinks.Add("38431");
				drinks.Add("40357");
				drinks.Add("44750");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 59)
				{
				drinks.Add("28399");
				drinks.Add("29454");
				drinks.Add("30703");
				drinks.Add("38430");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 54)
				{
				drinks.Add("8079");
				drinks.Add("18300");
				drinks.Add("32455");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 50)
				{
				drinks.Add("19301");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 44)
				{
				drinks.Add("8078");
				drinks.Add("8766");
				drinks.Add("38429");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 34)
				{
				drinks.Add("1645");
				drinks.Add("8077");
				drinks.Add("19300");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 24)
				{
				drinks.Add("1708");
				drinks.Add("3772");
				drinks.Add("4791");
				drinks.Add("10841");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 14)
				{
				drinks.Add("1205");
				drinks.Add("2136");
				drinks.Add("9451");
				drinks.Add("19299");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level > 4)
				{
				drinks.Add("17404");
				drinks.Add("2288");
				drinks.Add("1179");
				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level > 0)
				{
				drinks.Add("5342");
				drinks.Add("19221");
				drinks.Add("20709");
				drinks.Add("21114");
				drinks.Add("21151");
				drinks.Add("21721");
				drinks.Add("159");
				drinks.Add("5350");
				drinks.Add("3448");

				if ((drink = findItems(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			return null;
			}
		public static string detectFood()
			{
			List<string> foods = new List<string>();
			string food;
			if (ObjectManager.Me.Level > 79)
				{
				foods.Add("43523");
				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 74)
				{
				foods.Add("35947");
				foods.Add("35948");
				foods.Add("35950");
				foods.Add("35951");
				foods.Add("35952");
				foods.Add("35953");
				foods.Add("38706");
				foods.Add("40202");
				foods.Add("41729");
				foods.Add("42429");
				foods.Add("42431");
				foods.Add("42434");
				foods.Add("42778");
				foods.Add("42779");
				foods.Add("43087");
				foods.Add("44049");
				foods.Add("44071");
				foods.Add("44072");
				foods.Add("44607");
				foods.Add("44722");
				foods.Add("44940");
				foods.Add("45932");
				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level > 73)
				{
				foods.Add("43518");
				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level > 69)
				{
				foods.Add("34125");
				foods.Add("34747");
				foods.Add("34748");
				foods.Add("34749");
				foods.Add("34750");
				foods.Add("34751");
				foods.Add("34752");
				foods.Add("34753");
				foods.Add("34754");
				foods.Add("34755");
				foods.Add("34756");
				foods.Add("34757");
				foods.Add("34758");
				foods.Add("34759");
				foods.Add("34760");
				foods.Add("34761");
				foods.Add("34762");
				foods.Add("34763");
				foods.Add("34764");
				foods.Add("34765");
				foods.Add("34766");
				foods.Add("34767");
				foods.Add("34768");
				foods.Add("34769");
				foods.Add("39691");
				foods.Add("42428");
				foods.Add("42430");
				foods.Add("42432");
				foods.Add("42433");
				foods.Add("42942");
				foods.Add("42993");
				foods.Add("42994");
				foods.Add("42995");
				foods.Add("42996");
				foods.Add("42997");
				foods.Add("42998");
				foods.Add("42999");
				foods.Add("43000");
				foods.Add("43001");
				foods.Add("43004");
				foods.Add("43005");
				foods.Add("43015");
				foods.Add("43268");
				foods.Add("43478");
				foods.Add("43480");
				foods.Add("44953");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 64)
				{
				foods.Add("22019");
				foods.Add("29394");
				foods.Add("29448");
				foods.Add("29449");
				foods.Add("29450");
				foods.Add("29451");
				foods.Add("29452");
				foods.Add("29453");
				foods.Add("30355");
				foods.Add("30357");
				foods.Add("30358");
				foods.Add("30359");
				foods.Add("30361");
				foods.Add("32685");
				foods.Add("32686");
				foods.Add("32722");
				foods.Add("33048");
				foods.Add("33052");
				foods.Add("33053");
				foods.Add("33443");
				foods.Add("33449");
				foods.Add("33451");
				foods.Add("33452");
				foods.Add("33454");
				foods.Add("33825");
				foods.Add("33872");
				foods.Add("34062");
				foods.Add("34780");
				foods.Add("35949");
				foods.Add("37252");
				foods.Add("37452");
				foods.Add("38428");
				foods.Add("40356");
				foods.Add("40358");
				foods.Add("40359");
				foods.Add("44608");
				foods.Add("44609");
				foods.Add("44749");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level > 54)
				{
				foods.Add("20031");
				foods.Add("21023");
				foods.Add("22895");
				foods.Add("24008");
				foods.Add("24009");
				foods.Add("24338");
				foods.Add("24539");
				foods.Add("27651");
				foods.Add("27655");
				foods.Add("27656");
				foods.Add("27657");
				foods.Add("27658");
				foods.Add("27659");
				foods.Add("27660");
				foods.Add("27661");
				foods.Add("27662");
				foods.Add("27663");
				foods.Add("27664");
				foods.Add("27665");
				foods.Add("27666");
				foods.Add("27667");
				foods.Add("27854");
				foods.Add("27855");
				foods.Add("27856");
				foods.Add("27857");
				foods.Add("27858");
				foods.Add("27859");
				foods.Add("28486");
				foods.Add("28501");
				foods.Add("29292");
				foods.Add("29393");
				foods.Add("29412");
				foods.Add("30155");
				foods.Add("30458");
				foods.Add("30610");
				foods.Add("31672");
				foods.Add("31673");
				foods.Add("32721");
				foods.Add("33866");
				foods.Add("33867");
				foods.Add("33874");
				foods.Add("38427");
				foods.Add("41751");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 50)
				{
				foods.Add("19301");

				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 44)
				{
				foods.Add("12216");
				foods.Add("12218");
				foods.Add("16971");
				foods.Add("18045");
				foods.Add("21215");
				foods.Add("8076");
				foods.Add("8932");
				foods.Add("8948");
				foods.Add("8950");
				foods.Add("8952");
				foods.Add("8953");
				foods.Add("8957");
				foods.Add("11415");
				foods.Add("11444");
				foods.Add("13724");
				foods.Add("13810");
				foods.Add("13893");
				foods.Add("13933");
				foods.Add("13934");
				foods.Add("13935");
				foods.Add("16171");
				foods.Add("18254");
				foods.Add("18255");
				foods.Add("19225");
				foods.Add("20452");
				foods.Add("21031");
				foods.Add("21033");
				foods.Add("22324");
				foods.Add("23160");
				foods.Add("35563");
				foods.Add("35565");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 34)
				{
				foods.Add("21217");
				foods.Add("3927");
				foods.Add("4599");
				foods.Add("4601");
				foods.Add("4602");
				foods.Add("4608");
				foods.Add("6887");
				foods.Add("8075");
				foods.Add("9681");
				foods.Add("12215");
				foods.Add("12217");
				foods.Add("13755");
				foods.Add("13927");
				foods.Add("13928");
				foods.Add("13929");
				foods.Add("13930");
				foods.Add("13931");
				foods.Add("13932");
				foods.Add("16168");
				foods.Add("16766");
				foods.Add("17222");
				foods.Add("17408");
				foods.Add("18635");
				foods.Add("19306");
				foods.Add("21030");
				foods.Add("21552");
				foods.Add("33004");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 24)
				{
				foods.Add("3728");
				foods.Add("20074");
				foods.Add("1487");
				foods.Add("1707");
				foods.Add("3729");
				foods.Add("3771");
				foods.Add("4457");
				foods.Add("4539");
				foods.Add("4544");
				foods.Add("4594");
				foods.Add("4607");
				foods.Add("6038");
				foods.Add("8364");
				foods.Add("12210");
				foods.Add("12212");
				foods.Add("12213");
				foods.Add("12214");
				foods.Add("13546");
				foods.Add("13851");
				foods.Add("16169");
				foods.Add("17407");
				foods.Add("18632");
				foods.Add("19224");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 14)
				{
				foods.Add("1082");
				foods.Add("2685");
				foods.Add("5478");
				foods.Add("5526");
				foods.Add("21072");
				foods.Add("5479");
				foods.Add("422");
				foods.Add("1017");
				foods.Add("1114");
				foods.Add("3663");
				foods.Add("3664");
				foods.Add("3665");
				foods.Add("3666");
				foods.Add("3726");
				foods.Add("3727");
				foods.Add("3770");
				foods.Add("4538");
				foods.Add("4542");
				foods.Add("4593");
				foods.Add("4606");
				foods.Add("5480");
				foods.Add("5527");
				foods.Add("7228");
				foods.Add("12209");
				foods.Add("16170");
				foods.Add("19305");


				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level > 4)
				{
				foods.Add("1401");
				foods.Add("414");
				foods.Add("724");
				foods.Add("733");
				foods.Add("1113");
				foods.Add("2287");
				foods.Add("2682");
				foods.Add("2683");
				foods.Add("2684");
				foods.Add("2687");
				foods.Add("3220");
				foods.Add("3662");
				foods.Add("4537");
				foods.Add("4541");
				foods.Add("4592");
				foods.Add("4605");
				foods.Add("5066");
				foods.Add("5095");
				foods.Add("5476");
				foods.Add("5477");
				foods.Add("5525");
				foods.Add("6316");
				foods.Add("6890");
				foods.Add("16167");
				foods.Add("17119");
				foods.Add("17406");
				foods.Add("18633");
				foods.Add("19304");
				foods.Add("22645");
				foods.Add("24072");
				foods.Add("27636");
				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level > -1)
				{
				foods.Add("961");
				foods.Add("1326");
				foods.Add("3448");
				foods.Add("6522");
				foods.Add("6657");
				foods.Add("6807");
				foods.Add("11584");
				foods.Add("19696");
				foods.Add("19994");
				foods.Add("19995");
				foods.Add("19996");
				foods.Add("20516");
				foods.Add("21235");
				foods.Add("21254");
				foods.Add("24105");
				foods.Add("27635");
				foods.Add("28112");
				foods.Add("33924");
				foods.Add("43488");
				foods.Add("43490");
				foods.Add("43491");
				foods.Add("43492");
				foods.Add("44114");
				foods.Add("44228");
				foods.Add("44791");
				foods.Add("46691");
				foods.Add("46887");
				foods.Add("117");
				foods.Add("787");
				foods.Add("2070");
				foods.Add("2679");
				foods.Add("2680");
				foods.Add("2681");
				foods.Add("2888");
				foods.Add("4536");
				foods.Add("4540");
				foods.Add("4604");
				foods.Add("4656");
				foods.Add("5057");
				foods.Add("5349");
				foods.Add("5472");
				foods.Add("5473");
				foods.Add("5474");
				foods.Add("6290");
				foods.Add("6299");
				foods.Add("6888");
				foods.Add("7097");
				foods.Add("7806");
				foods.Add("7807");
				foods.Add("7808");
				foods.Add("11109");
				foods.Add("12224");
				foods.Add("16166");
				foods.Add("17197");
				foods.Add("17198");
				foods.Add("17344");
				foods.Add("19223");
				foods.Add("20857");
				foods.Add("23495");
				foods.Add("23756");
				foods.Add("30816");
				foods.Add("44836");
				foods.Add("44837");
				foods.Add("44838");
				foods.Add("44839");
				foods.Add("44840");
				foods.Add("44854");
				foods.Add("44855");
				foods.Add("46690");
				foods.Add("46784");
				foods.Add("46793");
				foods.Add("46796");
				foods.Add("46797");

				if ((food = findItems(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			return null;
			}
		public static string refdetectDrink()
			{
			List<string> drinks = new List<string>();
			string drink;
			if (ObjectManager.Me.Level >= 80)
				{
				drinks.Add("43523");
				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level >= 75)
				{
				drinks.Add("45932");
				drinks.Add("33445");
				drinks.Add("39520");
				drinks.Add("41731");
				drinks.Add("42777");
				drinks.Add("43236");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level >= 74)
				{
				drinks.Add("43518");
				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level >= 70)
				{
				drinks.Add("33444");
				drinks.Add("38698");
				drinks.Add("43086");
				drinks.Add("44941");
				drinks.Add("34759");
				drinks.Add("34760");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 65)
				{
				drinks.Add("22018");
				drinks.Add("27860");
				drinks.Add("29395");
				drinks.Add("29401");
				drinks.Add("30457");
				drinks.Add("32453");
				drinks.Add("32668");
				drinks.Add("33042");
				drinks.Add("34411");
				drinks.Add("35954");
				drinks.Add("37253");
				drinks.Add("38431");
				drinks.Add("40357");
				drinks.Add("44750");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 60)
				{
				drinks.Add("28399");
				drinks.Add("29454");
				drinks.Add("30703");
				drinks.Add("38430");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 55)
				{
				drinks.Add("8079");
				drinks.Add("18300");
				drinks.Add("32455");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 51)
				{
				drinks.Add("19301");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 45)
				{
				drinks.Add("8078");
				drinks.Add("8766");
				drinks.Add("38429");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 35)
				{
				drinks.Add("1645");
				drinks.Add("8077");
				drinks.Add("19300");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 25)
				{
				drinks.Add("1708");
				drinks.Add("3772");
				drinks.Add("4791");
				drinks.Add("10841");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 15)
				{
				drinks.Add("1205");
				drinks.Add("2136");
				drinks.Add("9451");
				drinks.Add("19299");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			if (ObjectManager.Me.Level >= 5)
				{
				drinks.Add("17404");
				drinks.Add("2288");
				drinks.Add("1179");
				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}

			if (ObjectManager.Me.Level >= 0)
				{
				drinks.Add("5342");
				drinks.Add("19221");
				drinks.Add("20709");
				drinks.Add("21114");
				drinks.Add("21151");
				drinks.Add("21721");
				drinks.Add("159");
				drinks.Add("5350");
				drinks.Add("3448");

				if ((drink = findDrink(drinks)) != "")
					{
					return drink;
					}
				drinks.Clear();
				}
			return "";
			}
		public static string refdetectFood()
			{
			List<string> foods = new List<string>();
			string food;
			if (ObjectManager.Me.Level >= 80)
				{
				foods.Add("43523");
				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level >= 75)
				{
				foods.Add("35947");
				foods.Add("35948");
				foods.Add("35950");
				foods.Add("35951");
				foods.Add("35952");
				foods.Add("35953");
				foods.Add("38706");
				foods.Add("40202");
				foods.Add("41729");
				foods.Add("42429");
				foods.Add("42431");
				foods.Add("42434");
				foods.Add("42778");
				foods.Add("42779");
				foods.Add("43087");
				foods.Add("44049");
				foods.Add("44071");
				foods.Add("44072");
				foods.Add("44607");
				foods.Add("44722");
				foods.Add("44940");
				foods.Add("45932");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level >= 74)
				{
				foods.Add("43518");
				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level >= 70)
				{
				foods.Add("34125");
				foods.Add("34747");
				foods.Add("34748");
				foods.Add("34749");
				foods.Add("34750");
				foods.Add("34751");
				foods.Add("34752");
				foods.Add("34753");
				foods.Add("34754");
				foods.Add("34755");
				foods.Add("34756");
				foods.Add("34757");
				foods.Add("34758");
				foods.Add("34759");
				foods.Add("34760");
				foods.Add("34761");
				foods.Add("34762");
				foods.Add("34763");
				foods.Add("34764");
				foods.Add("34765");
				foods.Add("34766");
				foods.Add("34767");
				foods.Add("34768");
				foods.Add("34769");
				foods.Add("39691");
				foods.Add("42428");
				foods.Add("42430");
				foods.Add("42432");
				foods.Add("42433");
				foods.Add("42942");
				foods.Add("42993");
				foods.Add("42994");
				foods.Add("42995");
				foods.Add("42996");
				foods.Add("42997");
				foods.Add("42998");
				foods.Add("42999");
				foods.Add("43000");
				foods.Add("43001");
				foods.Add("43004");
				foods.Add("43005");
				foods.Add("43015");
				foods.Add("43268");
				foods.Add("43478");
				foods.Add("43480");
				foods.Add("44953");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 65)
				{
				foods.Add("22019");
				foods.Add("29394");
				foods.Add("29448");
				foods.Add("29449");
				foods.Add("29450");
				foods.Add("29451");
				foods.Add("29452");
				foods.Add("29453");
				foods.Add("30355");
				foods.Add("30357");
				foods.Add("30358");
				foods.Add("30359");
				foods.Add("30361");
				foods.Add("32685");
				foods.Add("32686");
				foods.Add("32722");
				foods.Add("33048");
				foods.Add("33052");
				foods.Add("33053");
				foods.Add("33443");
				foods.Add("33449");
				foods.Add("33451");
				foods.Add("33452");
				foods.Add("33454");
				foods.Add("33825");
				foods.Add("33872");
				foods.Add("34062");
				foods.Add("34780");
				foods.Add("35949");
				foods.Add("37252");
				foods.Add("37452");
				foods.Add("38428");
				foods.Add("40356");
				foods.Add("40358");
				foods.Add("40359");
				foods.Add("44608");
				foods.Add("44609");
				foods.Add("44749");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level >= 55)
				{
				foods.Add("20031");
				foods.Add("21023");
				foods.Add("22895");
				foods.Add("24008");
				foods.Add("24009");
				foods.Add("24338");
				foods.Add("24539");
				foods.Add("27651");
				foods.Add("27655");
				foods.Add("27656");
				foods.Add("27657");
				foods.Add("27658");
				foods.Add("27659");
				foods.Add("27660");
				foods.Add("27661");
				foods.Add("27662");
				foods.Add("27663");
				foods.Add("27664");
				foods.Add("27665");
				foods.Add("27666");
				foods.Add("27667");
				foods.Add("27854");
				foods.Add("27855");
				foods.Add("27856");
				foods.Add("27857");
				foods.Add("27858");
				foods.Add("27859");
				foods.Add("28486");
				foods.Add("28501");
				foods.Add("29292");
				foods.Add("29393");
				foods.Add("29412");
				foods.Add("30155");
				foods.Add("30458");
				foods.Add("30610");
				foods.Add("31672");
				foods.Add("31673");
				foods.Add("32721");
				foods.Add("33866");
				foods.Add("33867");
				foods.Add("33874");
				foods.Add("38427");
				foods.Add("41751");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 51)
				{
				foods.Add("19301");

				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 45)
				{
				foods.Add("12216");
				foods.Add("12218");
				foods.Add("16971");
				foods.Add("18045");
				foods.Add("21215");
				foods.Add("8076");
				foods.Add("8932");
				foods.Add("8948");
				foods.Add("8950");
				foods.Add("8952");
				foods.Add("8953");
				foods.Add("8957");
				foods.Add("11415");
				foods.Add("11444");
				foods.Add("13724");
				foods.Add("13810");
				foods.Add("13893");
				foods.Add("13933");
				foods.Add("13934");
				foods.Add("13935");
				foods.Add("16171");
				foods.Add("18254");
				foods.Add("18255");
				foods.Add("19225");
				foods.Add("20452");
				foods.Add("21031");
				foods.Add("21033");
				foods.Add("22324");
				foods.Add("23160");
				foods.Add("35563");
				foods.Add("35565");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 35)
				{
				foods.Add("21217");
				foods.Add("3927");
				foods.Add("4599");
				foods.Add("4601");
				foods.Add("4602");
				foods.Add("4608");
				foods.Add("6887");
				foods.Add("8075");
				foods.Add("9681");
				foods.Add("12215");
				foods.Add("12217");
				foods.Add("13755");
				foods.Add("13927");
				foods.Add("13928");
				foods.Add("13929");
				foods.Add("13930");
				foods.Add("13931");
				foods.Add("13932");
				foods.Add("16168");
				foods.Add("16766");
				foods.Add("17222");
				foods.Add("17408");
				foods.Add("18635");
				foods.Add("19306");
				foods.Add("21030");
				foods.Add("21552");
				foods.Add("33004");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 25)
				{
				foods.Add("3728");
				foods.Add("20074");
				foods.Add("1487");
				foods.Add("1707");
				foods.Add("3729");
				foods.Add("3771");
				foods.Add("4457");
				foods.Add("4539");
				foods.Add("4544");
				foods.Add("4594");
				foods.Add("4607");
				foods.Add("6038");
				foods.Add("8364");
				foods.Add("12210");
				foods.Add("12212");
				foods.Add("12213");
				foods.Add("12214");
				foods.Add("13546");
				foods.Add("13851");
				foods.Add("16169");
				foods.Add("17407");
				foods.Add("18632");
				foods.Add("19224");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 15)
				{
				foods.Add("1082");
				foods.Add("2685");
				foods.Add("5478");
				foods.Add("5526");
				foods.Add("21072");
				foods.Add("5479");
				foods.Add("422");
				foods.Add("1017");
				foods.Add("1114");
				foods.Add("3663");
				foods.Add("3664");
				foods.Add("3665");
				foods.Add("3666");
				foods.Add("3726");
				foods.Add("3727");
				foods.Add("3770");
				foods.Add("4538");
				foods.Add("4542");
				foods.Add("4593");
				foods.Add("4606");
				foods.Add("5480");
				foods.Add("5527");
				foods.Add("7228");
				foods.Add("12209");
				foods.Add("16170");
				foods.Add("19305");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			if (ObjectManager.Me.Level >= 5)
				{
				foods.Add("1401");
				foods.Add("414");
				foods.Add("724");
				foods.Add("733");
				foods.Add("1113");
				foods.Add("2287");
				foods.Add("2682");
				foods.Add("2683");
				foods.Add("2684");
				foods.Add("2687");
				foods.Add("3220");
				foods.Add("3662");
				foods.Add("4537");
				foods.Add("4541");
				foods.Add("4592");
				foods.Add("4605");
				foods.Add("5066");
				foods.Add("5095");
				foods.Add("5476");
				foods.Add("5477");
				foods.Add("5525");
				foods.Add("6316");
				foods.Add("6890");
				foods.Add("16167");
				foods.Add("17119");
				foods.Add("17406");
				foods.Add("18633");
				foods.Add("19304");
				foods.Add("22645");
				foods.Add("24072");
				foods.Add("27636");


				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}

			if (ObjectManager.Me.Level >= 0)
				{
				foods.Add("961");
				foods.Add("1326");
				foods.Add("3448");
				foods.Add("6522");
				foods.Add("6657");
				foods.Add("6807");
				foods.Add("11584");
				foods.Add("19696");
				foods.Add("19994");
				foods.Add("19995");
				foods.Add("19996");
				foods.Add("20516");
				foods.Add("21235");
				foods.Add("21254");
				foods.Add("24105");
				foods.Add("27635");
				foods.Add("28112");
				foods.Add("33924");
				foods.Add("43488");
				foods.Add("43490");
				foods.Add("43491");
				foods.Add("43492");
				foods.Add("44114");
				foods.Add("44228");
				foods.Add("44791");
				foods.Add("46691");
				foods.Add("46887");
				foods.Add("117");
				foods.Add("787");
				foods.Add("2070");
				foods.Add("2679");
				foods.Add("2680");
				foods.Add("2681");
				foods.Add("2888");
				foods.Add("4536");
				foods.Add("4540");
				foods.Add("4604");
				foods.Add("4656");
				foods.Add("5057");
				foods.Add("5349");
				foods.Add("5472");
				foods.Add("5473");
				foods.Add("5474");
				foods.Add("6290");
				foods.Add("6299");
				foods.Add("6888");
				foods.Add("7097");
				foods.Add("7806");
				foods.Add("7807");
				foods.Add("7808");
				foods.Add("11109");
				foods.Add("12224");
				foods.Add("16166");
				foods.Add("17197");
				foods.Add("17198");
				foods.Add("17344");
				foods.Add("19223");
				foods.Add("20857");
				foods.Add("23495");
				foods.Add("23756");
				foods.Add("30816");
				foods.Add("44836");
				foods.Add("44837");
				foods.Add("44838");
				foods.Add("44839");
				foods.Add("44840");
				foods.Add("44854");
				foods.Add("44855");
				foods.Add("46690");
				foods.Add("46784");
				foods.Add("46793");
				foods.Add("46796");
				foods.Add("46797");

				if ((food = findFood(foods)) != "")
					{
					return food;
					}
				foods.Clear();
				}
			return "";
			}
		private static string findFood(List<string> foods)
			{
			foreach (WoWItem item in ObjectManager.GetObjectsOfType<WoWItem>())
				{
				foreach (string food in foods)
					{
					if (food == item.Entry.ToString())
						{
						return food;
						}
					}
				}
			return "";
			}
		private static string findDrink(List<string> drinks)
			{
			foreach (WoWItem item in ObjectManager.GetObjectsOfType<WoWItem>())
				{
				foreach (string drink in drinks)
					{
					if (drink == item.Entry.ToString())
						{
						return drink;
						}
					}
				}
			return "";
			}
		private static string findItemsOld(List<string> items)
			{
			foreach (WoWItem item in ObjectManager.GetObjectsOfType<WoWItem>())
				{
				foreach (string titem in items)
					{
					if (titem == item.Entry.ToString())
						{
						return titem;
						}
					}
				}
			return "";
			}
		private static string findItems(List<string> items)
			{
			ObjectManager.Update();
			List<WoWItem[]> bags = new List<WoWItem[]>();
			WoWPlayerInventory inv = ObjectManager.Me.Inventory;
				bags.Add(inv.Backpack.Items);
				if (ObjectManager.Me.GetBagAtIndex(0) != null)
					{
					bags.Add(ObjectManager.Me.GetBagAtIndex(0).Items);
					}
				if (ObjectManager.Me.GetBagAtIndex(1) != null)
					{
					bags.Add(ObjectManager.Me.GetBagAtIndex(1).Items);
					}
				if (ObjectManager.Me.GetBagAtIndex(2) != null)
					{
					bags.Add(ObjectManager.Me.GetBagAtIndex(2).Items);
					}
				if (ObjectManager.Me.GetBagAtIndex(3) != null)
					{
					bags.Add(ObjectManager.Me.GetBagAtIndex(3).Items);
					}
			foreach(string titem in items)
				{
							foreach (WoWItem[] bag in bags)
                {
                    try
                    {
                        foreach (WoWItem item in bag)
                        {
                            try
                            {
                                if (item.Entry.ToString() == titem && titem != "")
                                {
                                    return titem;
                                }
                            }
                            catch
                            {//empty slot
                            }
                        }
                    }
                    catch (Exception e)
                    { Logging.Write(System.Drawing.Color.Purple,e.Message); }
                }
            }
            return "";
			}
		}
	}

