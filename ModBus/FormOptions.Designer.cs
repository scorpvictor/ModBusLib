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
			this.buttonÑancel = new System.Windows.Forms.Button();
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxEndingSymbol = new System.Windows.Forms.TextBox();
			this.checkBoxEndingSymbolEnabled = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkBoxEndingSymbolEnabledTx = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxEndingSymbolTx = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(198, 201);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(1);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "Ñîõðàíèòü";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonÑancel
			// 
			this.buttonÑancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonÑancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonÑancel.Location = new System.Drawing.Point(279, 201);
			this.buttonÑancel.Margin = new System.Windows.Forms.Padding(1);
			this.buttonÑancel.Name = "buttonÑancel";
			this.buttonÑancel.Size = new System.Drawing.Size(75, 23);
			this.buttonÑancel.TabIndex = 0;
			this.buttonÑancel.Text = "Îòìåíà";
			this.buttonÑancel.UseVisualStyleBackColor = true;
			// 
			// SelectTimeout
			// 
			this.SelectTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SelectTimeout.Location = new System.Drawing.Point(274, 3);
			this.SelectTimeout.Name = "SelectTimeout";
			this.SelectTimeout.Size = new System.Drawing.Size(58, 20);
			this.SelectTimeout.TabIndex = 19;
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(200, 5);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 13);
			this.label8.TabIndex = 18;
			this.label8.Text = "Timeout (ìñ)";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(82, 116);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(85, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Ñòîïîâûå áèòû";
			// 
			// SelectPort
			// 
			this.SelectPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.SelectPort, 2);
			this.SelectPort.FormattingEnabled = true;
			this.SelectPort.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectPort.Location = new System.Drawing.Point(3, 3);
			this.SelectPort.Name = "SelectPort";
			this.SelectPort.Size = new System.Drawing.Size(248, 21);
			this.SelectPort.TabIndex = 8;
			this.SelectPort.SelectedIndexChanged += new System.EventHandler(this.SelectPort_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(112, 89);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "×åòíîñòü";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(112, 35);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Ñêîðîñòü";
			// 
			// SelectStopBits
			// 
			this.SelectStopBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.SelectStopBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectStopBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectStopBits.FormattingEnabled = true;
			this.SelectStopBits.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectStopBits.Location = new System.Drawing.Point(176, 112);
			this.SelectStopBits.Name = "SelectStopBits";
			this.SelectStopBits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SelectStopBits.Size = new System.Drawing.Size(75, 21);
			this.SelectStopBits.TabIndex = 13;
			// 
			// SelectBaudRate
			// 
			this.SelectBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.SelectBaudRate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectBaudRate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectBaudRate.FormattingEnabled = true;
			this.SelectBaudRate.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectBaudRate.Location = new System.Drawing.Point(176, 31);
			this.SelectBaudRate.Name = "SelectBaudRate";
			this.SelectBaudRate.Size = new System.Drawing.Size(75, 21);
			this.SelectBaudRate.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(94, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Áèòû äàííûõ";
			// 
			// SelectBits
			// 
			this.SelectBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.SelectBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectBits.FormattingEnabled = true;
			this.SelectBits.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectBits.Location = new System.Drawing.Point(176, 58);
			this.SelectBits.Name = "SelectBits";
			this.SelectBits.Size = new System.Drawing.Size(75, 21);
			this.SelectBits.TabIndex = 12;
			// 
			// SelectParity
			// 
			this.SelectParity.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.SelectParity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.SelectParity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.SelectParity.FormattingEnabled = true;
			this.SelectParity.ImeMode = System.Windows.Forms.ImeMode.On;
			this.SelectParity.Location = new System.Drawing.Point(176, 85);
			this.SelectParity.Name = "SelectParity";
			this.SelectParity.Size = new System.Drawing.Size(75, 21);
			this.SelectParity.TabIndex = 14;
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.RefreshButton.FlatAppearance.BorderSize = 0;
			this.RefreshButton.Image = global::Butek.ModBus.Properties.Resources.Update;
			this.RefreshButton.Location = new System.Drawing.Point(255, 1);
			this.RefreshButton.Margin = new System.Windows.Forms.Padding(1);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(26, 26);
			this.RefreshButton.TabIndex = 7;
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(152, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "Êîëè÷åñòâî ïîâòîðîâ";
			// 
			// SelectNumberRepeat
			// 
			this.SelectNumberRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SelectNumberRepeat.Location = new System.Drawing.Point(274, 27);
			this.SelectNumberRepeat.Name = "SelectNumberRepeat";
			this.SelectNumberRepeat.Size = new System.Drawing.Size(58, 20);
			this.SelectNumberRepeat.TabIndex = 19;
			// 
			// comboBoxFlowControl
			// 
			this.comboBoxFlowControl.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.comboBoxFlowControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.comboBoxFlowControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxFlowControl.FormattingEnabled = true;
			this.comboBoxFlowControl.ImeMode = System.Windows.Forms.ImeMode.On;
			this.comboBoxFlowControl.Location = new System.Drawing.Point(176, 139);
			this.comboBoxFlowControl.Name = "comboBoxFlowControl";
			this.comboBoxFlowControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.comboBoxFlowControl.Size = new System.Drawing.Size(75, 21);
			this.comboBoxFlowControl.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(103, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Flow control";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(5, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(349, 197);
			this.tabControl1.TabIndex = 20;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(288, 171);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Ïîðò";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.SelectPort, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.SelectStopBits, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxFlowControl, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.RefreshButton, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.SelectBits, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.SelectBaudRate, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.SelectParity, 1, 3);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(282, 164);
			this.tableLayoutPanel2.TabIndex = 18;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(341, 171);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Ïðîòîêîë";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
			this.tableLayoutPanel1.Controls.Add(this.SelectTimeout, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.SelectNumberRepeat, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxEndingSymbol, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBoxEndingSymbolEnabled, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.checkBoxEndingSymbolEnabledTx, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.textBoxEndingSymbolTx, 1, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 141);
			this.tableLayoutPanel1.TabIndex = 20;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(248, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Ðàçðåøèòü ñèìâîë îêîí÷àíèÿ ïîñûëêè (ïðèåì)";
			// 
			// label9
			// 
			this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(125, 77);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(143, 13);
			this.label9.TabIndex = 18;
			this.label9.Text = "Ñèìâîë îêîí÷àíèÿ (ïðèåì)";
			// 
			// textBoxEndingSymbol
			// 
			this.textBoxEndingSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxEndingSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxEndingSymbol.Location = new System.Drawing.Point(274, 75);
			this.textBoxEndingSymbol.Name = "textBoxEndingSymbol";
			this.textBoxEndingSymbol.Size = new System.Drawing.Size(58, 20);
			this.textBoxEndingSymbol.TabIndex = 19;
			// 
			// checkBoxEndingSymbolEnabled
			// 
			this.checkBoxEndingSymbolEnabled.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.checkBoxEndingSymbolEnabled.AutoSize = true;
			this.checkBoxEndingSymbolEnabled.Location = new System.Drawing.Point(295, 53);
			this.checkBoxEndingSymbolEnabled.Name = "checkBoxEndingSymbolEnabled";
			this.checkBoxEndingSymbolEnabled.Size = new System.Drawing.Size(15, 14);
			this.checkBoxEndingSymbolEnabled.TabIndex = 20;
			this.checkBoxEndingSymbolEnabled.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(5, 99);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(263, 13);
			this.label10.TabIndex = 18;
			this.label10.Text = "Ðàçðåøèòü ñèìâîë îêîí÷àíèÿ ïîñûëêè (ïåðåäà÷à)";
			// 
			// checkBoxEndingSymbolEnabledTx
			// 
			this.checkBoxEndingSymbolEnabledTx.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.checkBoxEndingSymbolEnabledTx.AutoSize = true;
			this.checkBoxEndingSymbolEnabledTx.Location = new System.Drawing.Point(295, 99);
			this.checkBoxEndingSymbolEnabledTx.Name = "checkBoxEndingSymbolEnabledTx";
			this.checkBoxEndingSymbolEnabledTx.Size = new System.Drawing.Size(15, 14);
			this.checkBoxEndingSymbolEnabledTx.TabIndex = 20;
			this.checkBoxEndingSymbolEnabledTx.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(110, 122);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(158, 13);
			this.label11.TabIndex = 18;
			this.label11.Text = "Ñèìâîë îêîí÷àíèÿ (ïåðåäà÷à)";
			// 
			// textBoxEndingSymbolTx
			// 
			this.textBoxEndingSymbolTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxEndingSymbolTx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxEndingSymbolTx.Location = new System.Drawing.Point(274, 119);
			this.textBoxEndingSymbolTx.Name = "textBoxEndingSymbolTx";
			this.textBoxEndingSymbolTx.Size = new System.Drawing.Size(58, 20);
			this.textBoxEndingSymbolTx.TabIndex = 19;
			// 
			// FormOptions
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonÑancel;
			this.ClientSize = new System.Drawing.Size(356, 227);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.buttonÑancel);
			this.Controls.Add(this.buttonOK);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(319, 265);
			this.Name = "FormOptions";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Íàñòðîéêè";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonÑancel;
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
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		internal System.Windows.Forms.TextBox textBoxEndingSymbol;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkBoxEndingSymbolEnabled;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkBoxEndingSymbolEnabledTx;
		private System.Windows.Forms.Label label11;
		internal System.Windows.Forms.TextBox textBoxEndingSymbolTx;
	}
}