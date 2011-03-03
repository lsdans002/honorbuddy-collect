using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using Styx;
using Styx.Helpers;

namespace DHXeBuy
	{
	static class DHXVendorAssist
		{
		private static List<string>[] foodsListByLevel;//null if that level has not been reached (0->level80 etc)
		private static List<string>[] drinksListByLevel;
		private static List<string>[] InstPListByLevel;
		private static List<string>[] DeadPListByLevel;
		private static List<string>[] AnePListByLevel;
		private static List<string>[] CripPListByLevel;
		private static List<string>[] NumbPListByLevel;
		private static List<string>[] WoundPListByLevel;
		private static List<string>[] arrowsListByLevel;
		private static List<string>[] BulletsListByLevel;
		private static bool firstRun = true;
		private static void SetupVendorAssist()
			{
			#region food list
			foodsListByLevel = new List<string>[13];
			if (StyxWoW.Me.Level > 79)
				{
				List<string> foods = new List<string>();
				foods.Add("43523");
				foodsListByLevel[0] = foods;
				}
			if (StyxWoW.Me.Level > 74)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[1] = foods;
				}
			if (StyxWoW.Me.Level > 73)
				{
				List<string> foods = new List<string>();
				foods.Add("43518");
				foodsListByLevel[2] = foods;
				}
			if (StyxWoW.Me.Level > 69)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[3] = foods;
				}
			if (StyxWoW.Me.Level > 64)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[4] = foods;
				}
			if (StyxWoW.Me.Level > 54)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[5] = foods;
				}
			if (StyxWoW.Me.Level > 50)
				{
				List<string> foods = new List<string>();
				foods.Add("19301");
				foodsListByLevel[6] = foods;
				}
			if (StyxWoW.Me.Level > 44)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[7] = foods;
				}
			if (StyxWoW.Me.Level > 34)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[8] = foods;
				}
			if (StyxWoW.Me.Level > 24)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[9] = foods;
				}
			if (StyxWoW.Me.Level > 14)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[10] = foods;
				}
			if (StyxWoW.Me.Level > 4)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[11] = foods;
				}
			if (StyxWoW.Me.Level > -1)
				{
				List<string> foods = new List<string>();
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
				foodsListByLevel[12] = foods;
				}
			#endregion
			#region drink list
			drinksListByLevel = new List<string>[14];
			if (StyxWoW.Me.Level > 79)
				{
				List<string> drinks = new List<string>();
				drinks.Add("43523");
				drinksListByLevel[0] = drinks;
				}
			if (StyxWoW.Me.Level > 74)
				{
				List<string> drinks = new List<string>();
				drinks.Add("45932");
				drinks.Add("33445");
				drinks.Add("39520");
				drinks.Add("41731");
				drinks.Add("42777");
				drinks.Add("43236");
				drinksListByLevel[1] = drinks;
				}
			if (StyxWoW.Me.Level > 73)
				{
				List<string> drinks = new List<string>();
				drinks.Add("43518");
				drinksListByLevel[2] = drinks;
				}
			if (StyxWoW.Me.Level > 69)
				{
				List<string> drinks = new List<string>();
				drinks.Add("33444");
				drinks.Add("38698");
				drinks.Add("43086");
				drinks.Add("44941");
				drinks.Add("34759");
				drinks.Add("34760");
				drinksListByLevel[3] = drinks;
				}
			if (StyxWoW.Me.Level > 64)
				{
				List<string> drinks = new List<string>();
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
				drinksListByLevel[4] = drinks;
				}
			if (StyxWoW.Me.Level > 59)
				{
				List<string> drinks = new List<string>();
				drinks.Add("28399");
				drinks.Add("29454");
				drinks.Add("30703");
				drinks.Add("38430");
				drinksListByLevel[5] = drinks;
				}
			if (StyxWoW.Me.Level > 54)
				{
				List<string> drinks = new List<string>();
				drinks.Add("8079");
				drinks.Add("18300");
				drinks.Add("32455");
				drinksListByLevel[6] = drinks;
				}
			if (StyxWoW.Me.Level > 50)
				{
				List<string> drinks = new List<string>();
				drinks.Add("19301");
				drinksListByLevel[7] = drinks;
				}
			if (StyxWoW.Me.Level > 44)
				{
				List<string> drinks = new List<string>();
				drinks.Add("8078");
				drinks.Add("8766");
				drinks.Add("38429");
				drinksListByLevel[8] = drinks;
				}
			if (StyxWoW.Me.Level > 34)
				{
				List<string> drinks = new List<string>();
				drinks.Add("1645");
				drinks.Add("8077");
				drinks.Add("19300");
				drinksListByLevel[9] = drinks;
				}
			if (StyxWoW.Me.Level > 24)
				{
				List<string> drinks = new List<string>();
				drinks.Add("1708");
				drinks.Add("3772");
				drinks.Add("4791");
				drinks.Add("10841");
				drinksListByLevel[10] = drinks;
				}
			if (StyxWoW.Me.Level > 14)
				{
				List<string> drinks = new List<string>();
				drinks.Add("1205");
				drinks.Add("2136");
				drinks.Add("9451");
				drinks.Add("19299");
				drinksListByLevel[11] = drinks;
				}
			if (StyxWoW.Me.Level > 4)
				{
				List<string> drinks = new List<string>();
				drinks.Add("17404");
				drinks.Add("2288");
				drinks.Add("1179");
				drinksListByLevel[12] = drinks;
				}
			if (StyxWoW.Me.Level > -1)
				{
				List<string> drinks = new List<string>();
				drinks.Add("5342");
				drinks.Add("19221");
				drinks.Add("20709");
				drinks.Add("21114");
				drinks.Add("21151");
				drinks.Add("21721");
				drinks.Add("159");
				drinks.Add("5350");
				drinks.Add("3448");
				drinksListByLevel[13] = drinks;
				}
			#endregion
			#region inst poison list
			InstPListByLevel = new List<string>[9];
			if (StyxWoW.Me.Level > 78)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("43231");
				InstPListByLevel[0] = InstPs;
				}
			if (StyxWoW.Me.Level > 72)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("43230");
				InstPListByLevel[1] = InstPs;
				}
			if (StyxWoW.Me.Level > 67)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("21927");
				InstPListByLevel[2] = InstPs;
				}
			if (StyxWoW.Me.Level > 59)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("8928");
				InstPListByLevel[3] = InstPs;
				}
			if (StyxWoW.Me.Level > 51)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("8927");
				InstPListByLevel[4] = InstPs;
				}
			if (StyxWoW.Me.Level > 43)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("8926");
				InstPListByLevel[5] = InstPs;
				}
			if (StyxWoW.Me.Level > 35)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("6950");
				InstPListByLevel[6] = InstPs;
				}
			if (StyxWoW.Me.Level > 27)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("6949");
				InstPListByLevel[7] = InstPs;
				}
			if (StyxWoW.Me.Level > 19)
				{
				List<string> InstPs = new List<string>();
				InstPs.Add("6947");
				InstPListByLevel[8] = InstPs;
				}
			#endregion
			#region Ane poison list
			AnePListByLevel = new List<string>[2];
			if (StyxWoW.Me.Level > 76)
				{
				List<string> AnePs = new List<string>();
				AnePs.Add("43237");
				AnePListByLevel[0] = AnePs;
				}
			if (StyxWoW.Me.Level > 67)
				{
				List<string> AnePs = new List<string>();
				AnePs.Add("21835");
				AnePListByLevel[1] = AnePs;
				}
			#endregion
			#region Numb poison list
			NumbPListByLevel = new List<string>[1];
			if (StyxWoW.Me.Level > 23)
				{
				List<string> NumbPs = new List<string>();
				NumbPs.Add("5237");
				NumbPListByLevel[0] = NumbPs;
				}
			#endregion
			#region Crip poison list
			CripPListByLevel = new List<string>[1];
			if (StyxWoW.Me.Level > 19)
				{
				List<string> CripPs = new List<string>();
				CripPs.Add("3775");
				CripPListByLevel[0] = CripPs;
				}
			#endregion
			#region dead poison list
			DeadPListByLevel = new List<string>[9];
			if (StyxWoW.Me.Level > 79)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("43233");
				DeadPListByLevel[0] = DeadPs;
				}
			if (StyxWoW.Me.Level > 75)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("43232");
				DeadPListByLevel[1] = DeadPs;
				}
			if (StyxWoW.Me.Level > 69)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("22054");
				DeadPListByLevel[2] = DeadPs;
				}
			if (StyxWoW.Me.Level > 61)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("22053");
				DeadPListByLevel[3] = DeadPs;
				}
			if (StyxWoW.Me.Level > 59)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("20844");
				DeadPListByLevel[4] = DeadPs;
				}
			if (StyxWoW.Me.Level > 53)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("8985");
				DeadPListByLevel[5] = DeadPs;
				}
			if (StyxWoW.Me.Level > 45)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("8984");
				DeadPListByLevel[6] = DeadPs;
				}
			if (StyxWoW.Me.Level > 37)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("2893");
				DeadPListByLevel[7] = DeadPs;
				}
			if (StyxWoW.Me.Level > 29)
				{
				List<string> DeadPs = new List<string>();
				DeadPs.Add("2892");
				DeadPListByLevel[8] = DeadPs;
				}
			#endregion
			#region Wound poison list
			WoundPListByLevel = new List<string>[7];
			if (StyxWoW.Me.Level > 77)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("43235");
				WoundPListByLevel[0] = WoundPs;
				}
			if (StyxWoW.Me.Level > 71)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("43234");
				WoundPListByLevel[1] = WoundPs;
				}
			if (StyxWoW.Me.Level > 63)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("22055");
				WoundPListByLevel[2] = WoundPs;
				}
			if (StyxWoW.Me.Level > 55)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("10922");
				WoundPListByLevel[3] = WoundPs;
				}
			if (StyxWoW.Me.Level > 47)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("10921");
				WoundPListByLevel[4] = WoundPs;
				}
			if (StyxWoW.Me.Level > 39)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("10920");
				WoundPListByLevel[5] = WoundPs;
				}
			if (StyxWoW.Me.Level > 31)
				{
				List<string> WoundPs = new List<string>();
				WoundPs.Add("10918");
				WoundPListByLevel[6] = WoundPs;
				}
			#endregion
			#region Arrow list
			arrowsListByLevel = new List<string>[19];
			if (StyxWoW.Me.Level > 79)
				{
				List<string> arrows = new List<string>();
				arrows.Add("52021");
				arrowsListByLevel[0] = arrows;
				}
			if (StyxWoW.Me.Level > 74)
				{
				List<string> arrows = new List<string>();
				arrows.Add("41586");
				arrowsListByLevel[1] = arrows;
				}
			if (StyxWoW.Me.Level > 71)
				{
				List<string> arrows = new List<string>();
				arrows.Add("41165");
				arrowsListByLevel[2] = arrows;
				}
			if (StyxWoW.Me.Level > 69)
				{
				List<string> arrows = new List<string>();
				arrows.Add("34581");
				arrows.Add("32760");
				arrows.Add("31737");
				arrowsListByLevel[3] = arrows;
				}
			if (StyxWoW.Me.Level > 67)
				{
				List<string> arrows = new List<string>();
				arrows.Add("31949");
				arrowsListByLevel[4] = arrows;
				}
			if (StyxWoW.Me.Level > 65)
				{
				List<string> arrows = new List<string>();
				arrows.Add("30611");
				arrowsListByLevel[5] = arrows;
				}
			if (StyxWoW.Me.Level > 64)
				{
				List<string> arrows = new List<string>();
				arrows.Add("28056");
				arrowsListByLevel[6] = arrows;
				}
			if (StyxWoW.Me.Level > 61)
				{
				List<string> arrows = new List<string>();
				arrows.Add("33803");
				arrowsListByLevel[7] = arrows;
				}
			if (StyxWoW.Me.Level > 60)
				{
				List<string> arrows = new List<string>();
				arrows.Add("24417");
				arrowsListByLevel[8] = arrows;
				}
			if (StyxWoW.Me.Level > 54)
				{
				List<string> arrows = new List<string>();
				arrows.Add("28053");
				arrowsListByLevel[9] = arrows;
				}
			if (StyxWoW.Me.Level > 53)
				{
				List<string> arrows = new List<string>();
				arrows.Add("12654");
				arrowsListByLevel[10] = arrows;
				}
			if (StyxWoW.Me.Level > 51)
				{
				List<string> arrows = new List<string>();
				arrows.Add("18042");
				arrowsListByLevel[11] = arrows;
				}
			if (StyxWoW.Me.Level > 50)
				{
				List<string> arrows = new List<string>();
				arrows.Add("19316");
				arrowsListByLevel[12] = arrows;
				}
			if (StyxWoW.Me.Level > 39)
				{
				List<string> arrows = new List<string>();
				arrows.Add("11285");
				arrowsListByLevel[13] = arrows;
				}
			if (StyxWoW.Me.Level > 36)
				{
				List<string> arrows = new List<string>();
				arrows.Add("10579");
				arrowsListByLevel[14] = arrows;
				}
			if (StyxWoW.Me.Level > 34)
				{
				List<string> arrows = new List<string>();
				arrows.Add("9399");
				arrowsListByLevel[15] = arrows;
				}
			if (StyxWoW.Me.Level > 24)
				{
				List<string> arrows = new List<string>();
				arrows.Add("3030");
				arrowsListByLevel[16] = arrows;
				}
			if (StyxWoW.Me.Level > 9)
				{
				List<string> arrows = new List<string>();
				arrows.Add("2515");
				arrowsListByLevel[17] = arrows;
				}
			if (StyxWoW.Me.Level > -1)
				{
				List<string> arrows = new List<string>();
				arrows.Add("2512");
				arrows.Add("3464");
				arrowsListByLevel[18] = arrows;
				}
			#endregion
			#region Bullet list
			BulletsListByLevel = new List<string>[23];
			if (StyxWoW.Me.Level > 79)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("52020");
				BulletsListByLevel[0] = Bullets;
				}
			if (StyxWoW.Me.Level > 74)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("41584");
				BulletsListByLevel[1] = Bullets;
				}
			if (StyxWoW.Me.Level > 71)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("41164");
				BulletsListByLevel[2] = Bullets;
				}
			if (StyxWoW.Me.Level > 69)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("34582");
				Bullets.Add("32761");
				Bullets.Add("31735");
				BulletsListByLevel[3] = Bullets;
				}
			if (StyxWoW.Me.Level > 67)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("32883");
				Bullets.Add("32882");
				BulletsListByLevel[4] = Bullets;
				}
			if (StyxWoW.Me.Level > 65)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("30612");
				BulletsListByLevel[5] = Bullets;
				}
			if (StyxWoW.Me.Level > 64)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("28061");
				BulletsListByLevel[6] = Bullets;
				}
			if (StyxWoW.Me.Level > 61)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("23773");
				BulletsListByLevel[7] = Bullets;
				}
			if (StyxWoW.Me.Level > 56)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("23772");
				BulletsListByLevel[8] = Bullets;
				}
			if (StyxWoW.Me.Level > 55)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("13377");
				BulletsListByLevel[9] = Bullets;
				}
			if (StyxWoW.Me.Level > 54)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("28060");
				BulletsListByLevel[10] = Bullets;
				}
			if (StyxWoW.Me.Level > 51)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("15997");
				BulletsListByLevel[11] = Bullets;
				}
			if (StyxWoW.Me.Level > 50)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("19317");
				BulletsListByLevel[12] = Bullets;
				}
			if (StyxWoW.Me.Level > 46)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("11630");
				BulletsListByLevel[13] = Bullets;
				}
			if (StyxWoW.Me.Level > 43)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("10513");
				BulletsListByLevel[14] = Bullets;
				}
			if (StyxWoW.Me.Level > 39)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("11284");
				BulletsListByLevel[15] = Bullets;
				}
			if (StyxWoW.Me.Level > 36)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("10512");
				BulletsListByLevel[16] = Bullets;
				}
			if (StyxWoW.Me.Level > 29)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("8069");
				BulletsListByLevel[17] = Bullets;
				}
			if (StyxWoW.Me.Level > 24)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("3033");
				BulletsListByLevel[18] = Bullets;
				}
			if (StyxWoW.Me.Level > 14)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("8068");
				BulletsListByLevel[19] = Bullets;
				}
			if (StyxWoW.Me.Level > 12)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("5568");
				BulletsListByLevel[20] = Bullets;
				}
			if (StyxWoW.Me.Level > 9)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("2519");
				BulletsListByLevel[21] = Bullets;
				}
			if (StyxWoW.Me.Level > 4)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("8067");
				BulletsListByLevel[21] = Bullets;
				}
			if (StyxWoW.Me.Level > -1)
				{
				List<string> Bullets = new List<string>();
				Bullets.Add("2516");
				Bullets.Add("3465");
				Bullets.Add("4960");
				BulletsListByLevel[22] = Bullets;
				}
			#endregion
			}
		public static string GetItemIDVendor(int index)
			{
			try
				{
				string linkstring = Lua.GetReturnValues("return GetMerchantItemLink(" + index + ")", "vendor.lua")[0];
				//Logging.Write(index.ToString() + " " + linkstring);
				string[] ilinkstringlist = linkstring.Split(new Char[] { ':' });
				//Logging.Write(index.ToString() + " " + ilinkstringlist[1]);
				return ilinkstringlist[1];
				}
			catch { return "9999999" + index.ToString(); ; }
			}
		public static bool BuyItem(DHXItemType type, int stackSize, int numToBuy)
			{
			DHXeBuySettings settings = new DHXeBuySettings();
			settings.LoadSettings();
			if (firstRun)
				{
				SetupVendorAssist();
				firstRun = false;
				}
			List<string>[] itemsToBuy = null;
			#region set food buy list
			if (type == DHXItemType.Food)
				{
				itemsToBuy = foodsListByLevel;
				}
			#endregion
			#region set drink buy list
			if (DHXItemType.Drink == type)
				{
				itemsToBuy = drinksListByLevel;
				}
			#endregion
			#region Poison1
			#region set InstP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 1 && DHXItemChecker.detectInstPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = InstPListByLevel;
				}
			#endregion
			#region set DeadP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 2 && DHXItemChecker.detectDeadPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = DeadPListByLevel;
				}
			#endregion
			#region set WoundP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 3 && DHXItemChecker.detectWoundPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = WoundPListByLevel;
				}
			#endregion
			#region set AneP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 4 && DHXItemChecker.detectAnePoison() == null && itemsToBuy == null)
				{
				itemsToBuy = AnePListByLevel;
				}
			#endregion
			#region set NumbP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 6 && DHXItemChecker.detectNumbPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = NumbPListByLevel;
				}
			#endregion
			#region set CripP Buy List
			if (type == DHXItemType.Poison1 && settings.PTB1 == 5 && DHXItemChecker.detectCripPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = CripPListByLevel;
				}
			#endregion
			#endregion
			#region poison2
			#region set InstP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 1 && DHXItemChecker.detectInstPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = InstPListByLevel;
				}
			#endregion
			#region set DeadP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 2 && DHXItemChecker.detectDeadPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = DeadPListByLevel;
				}
			#endregion
			#region set WoundP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 3 && DHXItemChecker.detectWoundPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = WoundPListByLevel;
				}
			#endregion
			#region set AneP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 4 && DHXItemChecker.detectAnePoison() == null && itemsToBuy == null)
				{
				itemsToBuy = AnePListByLevel;
				}
			#endregion
			#region set NumbP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 6 && DHXItemChecker.detectNumbPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = NumbPListByLevel;
				}
			#endregion
			#region set CripP Buy List
			if (type == DHXItemType.Poison2 && settings.PTB2 == 5 && DHXItemChecker.detectCripPoison() == null && itemsToBuy == null)
				{
				itemsToBuy = CripPListByLevel;
				}
			#endregion
			#endregion
			#region set Arrow Buy List
			if (type == DHXItemType.Ammo && DHXeBuy.NeedArrow())
				{
				itemsToBuy = arrowsListByLevel;
				}
			#endregion
			#region set Bullet Buy List
			if (type == DHXItemType.Ammo && DHXeBuy.NeedBullet())
				{
				itemsToBuy = BulletsListByLevel;
				}
			#endregion
			if (itemsToBuy == null)
				{
				return false;
				}
			int numItemsMerchantHas = Convert.ToInt32(Lua.GetReturnValues("return GetMerchantNumItems()", "vendor.lua")[0]);
			Dictionary<string, int> itemsForSale = new Dictionary<string, int>();
			for (int i = 1; i < numItemsMerchantHas +1; i++)
				{
				itemsForSale.Add(GetItemIDVendor(i), i);
				}
			foreach (List<string> list in itemsToBuy)
				{
				if (list != null)
					{
					foreach (string item in list)
						{
						if (itemsForSale.ContainsKey(item))
							{
							for (int i = 0; i < numToBuy; i = i + (stackSize))
								{
								Lua.DoString("BuyMerchantItem(" + itemsForSale[item] + ")");
								}
							ObjectManager.Update();
							return true;
							}
						}
					}
				}
			ObjectManager.Update();
			return false;
			}
		}
	}
