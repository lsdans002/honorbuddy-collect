using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Styx.Helpers;
using Styx;
namespace DHXeBuy
	{
	class DHXeBuySettings
		{
		public static readonly string PluginFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Path.Combine("Plugins", "DHX eBuy"));
		public bool EnableBuy { get; private set; }
		public bool BuyPoisons { get; private set; }
		public bool HaveMammoth { get; private set; }
		public int TFTB { get; private set; }
		public int TDTB { get; private set; }
		public int PTB1 { get; private set; }
		public int PTB2 { get; private set; }
		public int PA1 { get; private set; }
		public int PA2 { get; private set; }
		public bool BuyAmmo { get; private set; }
		public int AATB { get; private set; }
		public void LoadSettings()
			{

			string settingsPath = Path.Combine(PluginFolderPath, StyxWoW.Me.AccountName +"-"+ StyxWoW.Me.Name +"-Settings.xml");
			if (File.Exists(settingsPath))
				{
				}
			else
				{
				File.Copy(Path.Combine(PluginFolderPath, "Settings.xml"), settingsPath);
				}
			XElement elm = XElement.Load(settingsPath);
			XElement[] options = elm.Elements("Option").ToArray();
			bool _EnableBuy;
			bool _BuyPoisons;
			bool _HaveMammoth;
			int _TFTB;
			int _TDTB;
			int _PTB1;
			int _PTB2;
			int _PA1;
			int _PA2;
			bool _BuyAmmo;
			int _AATB;
			#region foreach
			foreach (XElement opt in options)
				{
				var value = opt.Attributes("Value").ToList();
				var set = opt.Attributes("Set").ToList();

				if (value[0].Value == "EnableBuy")
					{
					bool.TryParse(set[0].Value, out _EnableBuy);
					EnableBuy = _EnableBuy;
					}
				if (value[0].Value == "TFTB")
					{
					int.TryParse(set[0].Value, out _TFTB);
					TFTB = _TFTB;
					}
				if (value[0].Value == "TDTB")
					{
					int.TryParse(set[0].Value, out _TDTB);
					TDTB = _TDTB;
					}
				if (value[0].Value == "HaveMammoth")
					{
					bool.TryParse(set[0].Value, out _HaveMammoth);
					HaveMammoth = _HaveMammoth;
					}
				if (value[0].Value == "BuyPoisons")
					{
					bool.TryParse(set[0].Value, out _BuyPoisons);
					BuyPoisons = _BuyPoisons;
					}
				if (value[0].Value == "PTB1")
					{
					int.TryParse(set[0].Value, out _PTB1);
					PTB1 = _PTB1;
					}
				if (value[0].Value == "PTB2")
					{
					int.TryParse(set[0].Value, out _PTB2);
					PTB2 = _PTB2;
					}
				if (value[0].Value == "PA1")
					{
					int.TryParse(set[0].Value, out _PA1);
					PA1 = _PA1;
					}
				if (value[0].Value == "PA2")
					{
					int.TryParse(set[0].Value, out _PA2);
					PA2 = _PA2;
					}
				if (value[0].Value == "BuyAmmo")
					{
					bool.TryParse(set[0].Value, out _BuyAmmo);
					BuyAmmo = _BuyAmmo;
					}
				if (value[0].Value == "AATB")
					{
					int.TryParse(set[0].Value, out _AATB);
					AATB = _AATB;
					}
				}
			#endregion
			}
		public void SaveSettings(bool _EnableBuy, bool _BuyPoisons, bool _HaveMammoth, int _TFTB, int _TDTB, int _PTB1, int _PTB2, int _PA1, int _PA2, bool _BuyAmmo, int _AATB)
			{
			string settingsPath = Path.Combine(PluginFolderPath, StyxWoW.Me.AccountName + "-" + StyxWoW.Me.Name + "-Settings.xml");
						if (File.Exists(settingsPath))
				{
				}
			else
				{
				File.Copy(Path.Combine(PluginFolderPath, "Settings.xml"), settingsPath);
				}
			XElement saveElm = File.Exists(settingsPath) ? XElement.Load(settingsPath) : new XElement("Settings");
			XElement[] options = saveElm.Elements("Option").ToArray();
			foreach (XElement opt in options)
				{
				var value = opt.Attributes("Value").ToList();
				if (value[0].Value == "EnableBuy")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "EnableBuy"), new XAttribute("Set", _EnableBuy.ToString())));
					}
				if (value[0].Value == "BuyPoisons")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "BuyPoisons"), new XAttribute("Set", _BuyPoisons.ToString())));
					}
				if (value[0].Value == "HaveMammoth")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "HaveMammoth"), new XAttribute("Set", _HaveMammoth.ToString())));
					}
				if (value[0].Value == "TFTB")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "TFTB"), new XAttribute("Set", _TFTB.ToString())));
					}
				if (value[0].Value == "TDTB")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "TDTB"), new XAttribute("Set", _TDTB.ToString())));
					}
				if (value[0].Value == "PTB1")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "PTB1"), new XAttribute("Set", _PTB1.ToString())));
					}
				if (value[0].Value == "PTB2")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "PTB2"), new XAttribute("Set", _PTB2.ToString())));
					}
				if (value[0].Value == "PA1")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "PA1"), new XAttribute("Set", _PA1.ToString())));
					}
				if (value[0].Value == "PA2")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "PA2"), new XAttribute("Set", _PA2.ToString())));
					}
				if (value[0].Value == "BuyAmmo")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "BuyAmmo"), new XAttribute("Set", _BuyAmmo.ToString())));
					}

				if (value[0].Value == "AATB")
					{
					opt.ReplaceWith(new XElement("Option", new XAttribute("Value", "AATB"), new XAttribute("Set", _AATB.ToString())));
					}
				saveElm.Save(settingsPath);
				}
			}
		}
	}