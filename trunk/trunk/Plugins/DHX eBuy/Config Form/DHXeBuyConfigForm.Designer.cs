namespace DHXeBuy
	{
	partial class DHXeBuyConfigForm
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.EnableBuy = new System.Windows.Forms.CheckBox();
			this.DrinkBox = new System.Windows.Forms.TextBox();
			this.FoodBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.Mammoth = new System.Windows.Forms.CheckBox();
			this.BuyPoisons = new System.Windows.Forms.CheckBox();
			this.PoisonToBuy1 = new System.Windows.Forms.ComboBox();
			this.PoisonToBuy2 = new System.Windows.Forms.ComboBox();
			this.PoisonAmnt1 = new System.Windows.Forms.TextBox();
			this.PoisonAmnt2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.BuyAmmo = new System.Windows.Forms.CheckBox();
			this.AmmoBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// EnableBuy
			// 
			this.EnableBuy.AutoSize = true;
			this.EnableBuy.Checked = true;
			this.EnableBuy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.EnableBuy.Location = new System.Drawing.Point(13, 13);
			this.EnableBuy.Name = "EnableBuy";
			this.EnableBuy.Size = new System.Drawing.Size(312, 17);
			this.EnableBuy.TabIndex = 0;
			this.EnableBuy.Text = "Enable eBuy - When Disabled Functions Like eRefreshment ";
			this.EnableBuy.UseVisualStyleBackColor = true;
			this.EnableBuy.CheckedChanged += new System.EventHandler(this.EnableBuy_CheckedChanged);
			// 
			// DrinkBox
			// 
			this.DrinkBox.Location = new System.Drawing.Point(274, 56);
			this.DrinkBox.Name = "DrinkBox";
			this.DrinkBox.Size = new System.Drawing.Size(37, 20);
			this.DrinkBox.TabIndex = 1;
			this.DrinkBox.Text = "100";
			this.DrinkBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.DrinkBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextbox_KeyDown);
			this.DrinkBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextbox_KeyPress);
			// 
			// FoodBox
			// 
			this.FoodBox.Location = new System.Drawing.Point(274, 30);
			this.FoodBox.Name = "FoodBox";
			this.FoodBox.Size = new System.Drawing.Size(37, 20);
			this.FoodBox.TabIndex = 2;
			this.FoodBox.Text = "100";
			this.FoodBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.FoodBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextbox_KeyDown);
			this.FoodBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextbox_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(175, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Total Drink To Buy";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(176, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Total Food To Buy";
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(239, 191);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 5;
			this.buttonSave.Text = "Save && Exit";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// Mammoth
			// 
			this.Mammoth.AutoSize = true;
			this.Mammoth.Location = new System.Drawing.Point(13, 195);
			this.Mammoth.Name = "Mammoth";
			this.Mammoth.Size = new System.Drawing.Size(117, 17);
			this.Mammoth.TabIndex = 6;
			this.Mammoth.Text = "I Have A Mammoth";
			this.Mammoth.UseVisualStyleBackColor = true;
			// 
			// BuyPoisons
			// 
			this.BuyPoisons.AutoSize = true;
			this.BuyPoisons.Location = new System.Drawing.Point(13, 85);
			this.BuyPoisons.Name = "BuyPoisons";
			this.BuyPoisons.Size = new System.Drawing.Size(84, 17);
			this.BuyPoisons.TabIndex = 7;
			this.BuyPoisons.Text = "Buy Poisons";
			this.BuyPoisons.UseVisualStyleBackColor = true;
			this.BuyPoisons.CheckedChanged += new System.EventHandler(this.BuyPoisons_CheckedChanged);
			// 
			// PoisonToBuy1
			// 
			this.PoisonToBuy1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PoisonToBuy1.Enabled = false;
			this.PoisonToBuy1.FormattingEnabled = true;
			this.PoisonToBuy1.Items.AddRange(new object[] {
            "None",
            "Instant",
            "Deadly",
            "Wound",
            "Anesthetic",
            "Crippling",
            "Mind-numbing"});
			this.PoisonToBuy1.Location = new System.Drawing.Point(178, 102);
			this.PoisonToBuy1.Name = "PoisonToBuy1";
			this.PoisonToBuy1.Size = new System.Drawing.Size(90, 21);
			this.PoisonToBuy1.TabIndex = 8;
			// 
			// PoisonToBuy2
			// 
			this.PoisonToBuy2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PoisonToBuy2.Enabled = false;
			this.PoisonToBuy2.FormattingEnabled = true;
			this.PoisonToBuy2.Items.AddRange(new object[] {
            "None",
            "Instant",
            "Deadly",
            "Wound",
            "Anesthetic",
            "Crippling",
            "Mind-numbing"});
			this.PoisonToBuy2.Location = new System.Drawing.Point(178, 130);
			this.PoisonToBuy2.Name = "PoisonToBuy2";
			this.PoisonToBuy2.Size = new System.Drawing.Size(90, 21);
			this.PoisonToBuy2.TabIndex = 9;
			// 
			// PoisonAmnt1
			// 
			this.PoisonAmnt1.Enabled = false;
			this.PoisonAmnt1.Location = new System.Drawing.Point(274, 102);
			this.PoisonAmnt1.Name = "PoisonAmnt1";
			this.PoisonAmnt1.Size = new System.Drawing.Size(37, 20);
			this.PoisonAmnt1.TabIndex = 11;
			this.PoisonAmnt1.Text = "10";
			this.PoisonAmnt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.PoisonAmnt1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextbox_KeyDown);
			this.PoisonAmnt1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextbox_KeyPress);
			// 
			// PoisonAmnt2
			// 
			this.PoisonAmnt2.Enabled = false;
			this.PoisonAmnt2.Location = new System.Drawing.Point(274, 130);
			this.PoisonAmnt2.Name = "PoisonAmnt2";
			this.PoisonAmnt2.Size = new System.Drawing.Size(37, 20);
			this.PoisonAmnt2.TabIndex = 10;
			this.PoisonAmnt2.Text = "10";
			this.PoisonAmnt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.PoisonAmnt2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextbox_KeyDown);
			this.PoisonAmnt2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextbox_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(203, 86);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Type";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(271, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Amount";
			// 
			// BuyAmmo
			// 
			this.BuyAmmo.AutoSize = true;
			this.BuyAmmo.Location = new System.Drawing.Point(13, 161);
			this.BuyAmmo.Name = "BuyAmmo";
			this.BuyAmmo.Size = new System.Drawing.Size(76, 17);
			this.BuyAmmo.TabIndex = 14;
			this.BuyAmmo.Text = "Buy Ammo";
			this.BuyAmmo.UseVisualStyleBackColor = true;
			this.BuyAmmo.CheckedChanged += new System.EventHandler(this.BuyAmmo_CheckedChanged);
			// 
			// AmmoBox
			// 
			this.AmmoBox.Location = new System.Drawing.Point(258, 159);
			this.AmmoBox.Name = "AmmoBox";
			this.AmmoBox.Size = new System.Drawing.Size(53, 20);
			this.AmmoBox.TabIndex = 15;
			this.AmmoBox.Text = "1000";
			this.AmmoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.AmmoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextbox_KeyDown);
			this.AmmoBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextbox_KeyPress);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(152, 162);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Total Ammo To Buy";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 222);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.AmmoBox);
			this.Controls.Add(this.BuyAmmo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.PoisonAmnt1);
			this.Controls.Add(this.PoisonAmnt2);
			this.Controls.Add(this.PoisonToBuy2);
			this.Controls.Add(this.PoisonToBuy1);
			this.Controls.Add(this.BuyPoisons);
			this.Controls.Add(this.Mammoth);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FoodBox);
			this.Controls.Add(this.DrinkBox);
			this.Controls.Add(this.EnableBuy);
			this.Name = "Form1";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "eBuy Config";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Form1_Activated);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();
			}
		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.CheckBox EnableBuy;
		public System.Windows.Forms.TextBox DrinkBox;
		public System.Windows.Forms.TextBox FoodBox;
		private System.Windows.Forms.Button buttonSave;
		public System.Windows.Forms.CheckBox Mammoth;
		private System.Windows.Forms.CheckBox BuyPoisons;
		private System.Windows.Forms.ComboBox PoisonToBuy1;
		private System.Windows.Forms.ComboBox PoisonToBuy2;
		public System.Windows.Forms.TextBox PoisonAmnt1;
		public System.Windows.Forms.TextBox PoisonAmnt2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox BuyAmmo;
		private System.Windows.Forms.TextBox AmmoBox;
		private System.Windows.Forms.Label label5;
		}
	}