namespace Butek.ModBus
{
	partial class FormOptions
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
			this.buttonOK = new System.Windows.Forms.Button();
			this.button—ancel = new System.Windows.Forms.Button();
			this.SelectTimeout = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SelectPort = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SelectStopBits = new System.Windows.Forms.ComboBox();
			this.SelectBaudRate = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SelectBits = new System.Windows.Forms.ComboBox();
			this.SelectParity = new System.Windows.Forms.ComboBox();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SelectNumberRepeat = new System.Windows.Forms.TextBox();
			this.comboBoxFlowControl = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(47, 292);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "—Óı‡ÌËÚ¸";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// button—ancel
			// 
			this.button—ancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button—ancel.Location = new System.Drawing.Point(128, 292);
			this.button—ancel.Name = "button—ancel";
			this.button—ancel.Size = new System.Drawing.Size(75, 23);
			this.button—ancel.TabIndex = 0;
			this.button—ancel.Text = "ŒÚÏÂÌ‡";
			this.button—ancel.UseVisualStyleBackColor = true;
			// 
			// SelectTimeout
			// 
			this.SelectTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SelectTimeout.Location = new System.Drawing.Point(128, 225);
			this.SelectTimeout.Name = "SelectTimeout";
			this.SelectTimeout.Size = new System.Drawing.Size(75, 20);
			this.SelectTimeout.TabIndex = 19;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(54, 227);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 13);
			this.label8.TabIndex = 18;
			this.label8.Text = "Timeout (ÏÒ)";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(37, 159);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(85, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "—ÚÓÔÓ‚˚Â ·ËÚ˚";
			// 
			// SelectPort
			// 
			this.SelectPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectPort.FormattingEnabled = true;
			this.SelectPort.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectPort.Location = new System.Drawing.Point(12, 12);
			this.SelectPort.Name = "SelectPort";
			this.SelectPort.Size = new System.Drawing.Size(191, 21);
			this.SelectPort.TabIndex = 8;
			this.SelectPort.SelectedIndexChanged += new System.EventHandler(this.SelectPort_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(67, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "◊ÂÚÌÓÒÚ¸";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(67, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "—ÍÓÓÒÚ¸";
			// 
			// SelectStopBits
			// 
			this.SelectStopBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectStopBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectStopBits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectStopBits.FormattingEnabled = true;
			this.SelectStopBits.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectStopBits.Location = new System.Drawing.Point(128, 156);
			this.SelectStopBits.Name = "SelectStopBits";
			this.SelectStopBits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SelectStopBits.Size = new System.Drawing.Size(75, 21);
			this.SelectStopBits.TabIndex = 13;
			// 
			// SelectBaudRate
			// 
			this.SelectBaudRate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectBaudRate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectBaudRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectBaudRate.FormattingEnabled = true;
			this.SelectBaudRate.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectBaudRate.Location = new System.Drawing.Point(128, 48);
			this.SelectBaudRate.Name = "SelectBaudRate";
			this.SelectBaudRate.Size = new System.Drawing.Size(75, 21);
			this.SelectBaudRate.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(49, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "¡ËÚ˚ ‰‡ÌÌ˚ı";
			// 
			// SelectBits
			// 
			this.SelectBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectBits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectBits.FormattingEnabled = true;
			this.SelectBits.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectBits.Location = new System.Drawing.Point(128, 88);
			this.SelectBits.Name = "SelectBits";
			this.SelectBits.Size = new System.Drawing.Size(75, 21);
			this.SelectBits.TabIndex = 12;
			// 
			// SelectParity
			// 
			this.SelectParity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectParity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectParity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectParity.FormattingEnabled = true;
			this.SelectParity.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectParity.Location = new System.Drawing.Point(128, 122);
			this.SelectParity.Name = "SelectParity";
			this.SelectParity.Size = new System.Drawing.Size(75, 21);
			this.SelectParity.TabIndex = 14;
			// 
			// RefreshButton
			// 
			this.RefreshButton.FlatAppearance.BorderSize = 0;
			this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RefreshButton.Image = global::Butek.ModBus.Properties.Resources.Update;
			this.RefreshButton.Location = new System.Drawing.Point(207, 11);
			this.RefreshButton.Margin = new System.Windows.Forms.Padding(1);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(26, 23);
			this.RefreshButton.TabIndex = 7;
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 259);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = " ÓÎË˜ÂÒÚ‚Ó ÔÓ‚ÚÓÓ‚";
			// 
			// SelectNumberRepeat
			// 
			this.SelectNumberRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SelectNumberRepeat.Location = new System.Drawing.Point(128, 257);
			this.SelectNumberRepeat.Name = "SelectNumberRepeat";
			this.SelectNumberRepeat.Size = new System.Drawing.Size(75, 20);
			this.SelectNumberRepeat.TabIndex = 19;
			// 
			// comboBoxFlowControl
			// 
			this.comboBoxFlowControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.comboBoxFlowControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxFlowControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBoxFlowControl.FormattingEnabled = true;
			this.comboBoxFlowControl.ImeMode = System.Windows.Forms.ImeMode.On;
			this.comboBoxFlowControl.Location = new System.Drawing.Point(128, 191);
			this.comboBoxFlowControl.Name = "comboBoxFlowControl";
			this.comboBoxFlowControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.comboBoxFlowControl.Size = new System.Drawing.Size(75, 21);
			this.comboBoxFlowControl.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(58, 194);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Flow control";
			// 
			// FormOptions
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button—ancel;
			this.ClientSize = new System.Drawing.Size(238, 326);
			this.Controls.Add(this.SelectNumberRepeat);
			this.Controls.Add(this.SelectTimeout);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.SelectPort);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboBoxFlowControl);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.SelectStopBits);
			this.Controls.Add(this.SelectBaudRate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.SelectBits);
			this.Controls.Add(this.SelectParity);
			this.Controls.Add(this.button—ancel);
			this.Controls.Add(this.buttonOK);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOptions";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Õ‡ÒÚÓÈÍË";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button button—ancel;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox SelectTimeout;
		internal System.Windows.Forms.ComboBox SelectPort;
		internal System.Windows.Forms.ComboBox SelectStopBits;
		internal System.Windows.Forms.ComboBox SelectBaudRate;
		internal System.Windows.Forms.ComboBox SelectBits;
		internal System.Windows.Forms.ComboBox SelectParity;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox SelectNumberRepeat;
		internal System.Windows.Forms.ComboBox comboBoxFlowControl;
		private System.Windows.Forms.Label label2;
	}
}