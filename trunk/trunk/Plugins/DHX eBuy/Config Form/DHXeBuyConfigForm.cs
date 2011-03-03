using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Styx.Helpers;

namespace DHXeBuy
	{
	public partial class DHXeBuyConfigForm : Form
		{
		public DHXeBuyConfigForm()
			{
			InitializeComponent();
			this.Activate();
			Logging.Write(System.Drawing.Color.Purple, "DHX eBuy: 3.2.3 By DHX: Protopally Loaded.");
			}
		DHXeBuySettings settings;
		private void Form1_Shown(object sender, EventArgs e)
			{
			this.Activate();
			settings = new DHXeBuySettings();
			settings.LoadSettings();
			EnableBuy.Checked = settings.EnableBuy;
			BuyPoisons.Checked = settings.BuyPoisons;
			FoodBox.Text = settings.TFTB.ToString();
			DrinkBox.Text = settings.TDTB.ToString();
			PoisonToBuy1.SelectedIndex = settings.PTB1;
			PoisonToBuy2.SelectedIndex = settings.PTB2;
			PoisonAmnt1.Text = settings.PA1.ToString();
			PoisonAmnt2.Text = settings.PA2.ToString();
			Mammoth.Checked = settings.HaveMammoth;
			BuyAmmo.Checked = settings.BuyAmmo;
			AmmoBox.Text = settings.AATB.ToString();
			if (BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = true;
				PoisonToBuy2.Enabled = true;
				PoisonAmnt1.Enabled = true;
				PoisonAmnt2.Enabled = true;
				}
			if (!BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = false;
				PoisonToBuy2.Enabled = false;
				PoisonAmnt1.Enabled = false;
				PoisonAmnt2.Enabled = false;
				}
			if (EnableBuy.Checked)
				{
				DrinkBox.Enabled = true;
				FoodBox.Enabled = true;
				}
			if (!EnableBuy.Checked)
				{
				DrinkBox.Enabled = false;
				FoodBox.Enabled = false;
				}
			if (BuyAmmo.Checked)
				{
				AmmoBox.Enabled = true;
				}
			if (!BuyAmmo.Checked)
				{
				AmmoBox.Enabled = false;
				}
			}
		private void Form1_Activated(object sender, EventArgs e)
			{
			this.Activate();
			settings = new DHXeBuySettings();
			settings.LoadSettings();
			EnableBuy.Checked = settings.EnableBuy;
			BuyPoisons.Checked = settings.BuyPoisons;
			FoodBox.Text = settings.TFTB.ToString();
			DrinkBox.Text = settings.TDTB.ToString();
			PoisonToBuy1.SelectedIndex = settings.PTB1;
			PoisonToBuy2.SelectedIndex = settings.PTB2;
			PoisonAmnt1.Text = settings.PA1.ToString();
			PoisonAmnt2.Text = settings.PA2.ToString();
			Mammoth.Checked = settings.HaveMammoth;
			BuyAmmo.Checked = settings.BuyAmmo;
			AmmoBox.Text = settings.AATB.ToString();
			if (BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = true;
				PoisonToBuy2.Enabled = true;
				PoisonAmnt1.Enabled = true;
				PoisonAmnt2.Enabled = true;
				}
			if (!BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = false;
				PoisonToBuy2.Enabled = false;
				PoisonAmnt1.Enabled = false;
				PoisonAmnt2.Enabled = false;
				}
			if (EnableBuy.Checked)
				{
				DrinkBox.Enabled = true;
				FoodBox.Enabled = true;
				}
			if (!EnableBuy.Checked)
				{
				DrinkBox.Enabled = false;
				FoodBox.Enabled = false;
				}
			if (BuyAmmo.Checked)
				{
				AmmoBox.Enabled = true;
				}
			if (!BuyAmmo.Checked)
				{
				AmmoBox.Enabled = false;
				}
			}
		private void buttonClose_Click(object sender, EventArgs e)
			{
			this.Close();
			}
		bool NumberEntered = false;
		private bool CheckIfNumericKey(Keys K, bool isDecimalPoint)
			{
			if (K == Keys.Back) //backspace?
				return true;
			else if (K == Keys.OemPeriod || K == Keys.Decimal)  //decimal point?
				return isDecimalPoint ? false : true;       //or: return !isDecimalPoint
			else if ((K >= Keys.D0) && (K <= Keys.D9))      //digit from top of keyboard?
				return true;
			else if ((K >= Keys.NumPad0) && (K <= Keys.NumPad9))    //digit from keypad?
				return true;
			else
				return false;   //no "numeric" key
			}
		private void MyTextbox_KeyDown(object sender, KeyEventArgs e)
			{
			//Get our textbox.
			TextBox Tbx = (TextBox)sender;
			// Initialize the flag.
			NumberEntered = CheckIfNumericKey(e.KeyCode, Tbx.Text.Contains("."));
			}
		private void MyTextbox_KeyPress(object sender, KeyPressEventArgs e)
			{
			// Check for the flag being set in the KeyDown event.
			if (NumberEntered == false)
				{
				// Stop the character from being entered into the control since it is non-numerical.
				e.Handled = true;
				}

			}
		private void EnableBuy_CheckedChanged(object sender, EventArgs e)
			{
			if (EnableBuy.Checked)
				{
				DrinkBox.Enabled = true;
				FoodBox.Enabled = true;
				}
			if (!EnableBuy.Checked)
				{
				DrinkBox.Enabled = false;
				FoodBox.Enabled = false;
				}
			}
		private void buttonSave_Click(object sender, EventArgs e)
			{
			settings = new DHXeBuySettings();
			settings.SaveSettings(EnableBuy.Checked, BuyPoisons.Checked, Mammoth.Checked, int.Parse(FoodBox.Text), int.Parse(DrinkBox.Text), PoisonToBuy1.SelectedIndex, PoisonToBuy2.SelectedIndex, int.Parse(PoisonAmnt1.Text), int.Parse(PoisonAmnt2.Text), BuyAmmo.Checked, int.Parse(AmmoBox.Text));
			EnableBuy.Checked = settings.EnableBuy;
			BuyPoisons.Checked = settings.BuyPoisons;
			FoodBox.Text = settings.TFTB.ToString();
			DrinkBox.Text = settings.TDTB.ToString();
			PoisonToBuy1.SelectedIndex = settings.PTB1;
			PoisonToBuy2.SelectedIndex = settings.PTB2;
			PoisonAmnt1.Text = settings.PA1.ToString();
			PoisonAmnt2.Text = settings.PA2.ToString();
			Mammoth.Checked = settings.HaveMammoth;
			BuyAmmo.Checked = settings.BuyAmmo;
			AmmoBox.Text = settings.AATB.ToString();
			if (BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = true;
				PoisonToBuy2.Enabled = true;
				PoisonAmnt1.Enabled = true;
				PoisonAmnt2.Enabled = true;
				}
			if (!BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = false;
				PoisonToBuy2.Enabled = false;
				PoisonAmnt1.Enabled = false;
				PoisonAmnt2.Enabled = false;
				}
			if (EnableBuy.Checked)
				{
				DrinkBox.Enabled = true;
				FoodBox.Enabled = true;
				}
			if (!EnableBuy.Checked)
				{
				DrinkBox.Enabled = false;
				FoodBox.Enabled = false;
				}
			if (BuyAmmo.Checked)
				{
				AmmoBox.Enabled = true;
				}
			if (!BuyAmmo.Checked)
				{
				AmmoBox.Enabled = false;
				}
			Logging.Write(System.Drawing.Color.Purple, "eBuy: Settings Saved.");
			this.Close();
			}
		private void BuyPoisons_CheckedChanged(object sender, EventArgs e)
			{
			if (BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = true;
				PoisonToBuy2.Enabled = true;
				PoisonAmnt1.Enabled = true;
				PoisonAmnt2.Enabled = true;
				}
			if (!BuyPoisons.Checked)
				{
				PoisonToBuy1.Enabled = false;
				PoisonToBuy2.Enabled = false;
				PoisonAmnt1.Enabled = false;
				PoisonAmnt2.Enabled = false;
				}
			}
		private void BuyAmmo_CheckedChanged(object sender, EventArgs e)
			{
			if (BuyAmmo.Checked)
				{
				AmmoBox.Enabled = true;
				}
			if (!BuyAmmo.Checked)
				{
				AmmoBox.Enabled = false;
				}
			}
		}
	}
