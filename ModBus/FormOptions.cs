using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ModBus
{
	internal partial class FormOptions : Form
	{
		public FormOptions()
		{
			InitializeComponent();
			
			SelectStopBits.Items.Add(StopBits.None);
			SelectStopBits.Items.Add(StopBits.One);
			SelectStopBits.Items.Add(StopBits.OnePointFive);
			SelectStopBits.Items.Add(StopBits.Two);

			SelectParity.Items.Add(Parity.Even);
			SelectParity.Items.Add(Parity.Mark);
			SelectParity.Items.Add(Parity.None);
			SelectParity.Items.Add(Parity.Odd);
			SelectParity.Items.Add(Parity.Space);

			SelectBaudRate.Items.Add((int)300);
			SelectBaudRate.Items.Add((int)600);
			SelectBaudRate.Items.Add((int)1200);
			SelectBaudRate.Items.Add((int)2400);
			SelectBaudRate.Items.Add((int)4800);
			SelectBaudRate.Items.Add((int)9600);
			SelectBaudRate.Items.Add((int)14400);
			SelectBaudRate.Items.Add((int)19200);
			SelectBaudRate.Items.Add((int)38400);
			SelectBaudRate.Items.Add((int)56000);
			SelectBaudRate.Items.Add((int)57600);
			SelectBaudRate.Items.Add((int)115200);

			SelectBits.Items.Add((int)7);
			SelectBits.Items.Add((int)8);

			comboBoxFlowControl.Items.Add(Handshake.None);
			comboBoxFlowControl.Items.Add(Handshake.RequestToSend);
			comboBoxFlowControl.Items.Add(Handshake.RequestToSendXOnXOff);
			comboBoxFlowControl.Items.Add(Handshake.XOnXOff);

			SelectStopBits.SelectedItem = Properties.SettingsOptions.Default.stopBits;
			SelectParity.SelectedItem = Properties.SettingsOptions.Default.parity;
			comboBoxFlowControl.SelectedItem = Properties.SettingsOptions.Default.flowControl;
			SelectBaudRate.SelectedItem = Properties.SettingsOptions.Default.baudRate;
			SelectBits.SelectedItem = Properties.SettingsOptions.Default.dataBits;
			RefreshButton_Click(this, null);
			SelectPort.SelectedItem = Properties.SettingsOptions.Default.comPort;
			SelectTimeout.Text = Properties.SettingsOptions.Default.timeOut.ToString();
			SelectNumberRepeat.Text = Properties.SettingsOptions.Default.numberRepeat.ToString();
			
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			//обновить названия портов
			SelectPort.Items.Clear();
			SelectPort.Items.AddRange(SerialPort.GetPortNames());
			
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Properties.SettingsOptions.Default.stopBits = (StopBits)SelectStopBits.SelectedItem;
			Properties.SettingsOptions.Default.parity = (Parity)SelectParity.SelectedItem;
			Properties.SettingsOptions.Default.flowControl = (Handshake)comboBoxFlowControl.SelectedItem;
			Properties.SettingsOptions.Default.baudRate = (int)SelectBaudRate.SelectedItem;
			Properties.SettingsOptions.Default.dataBits = (int)SelectBits.SelectedItem;
			Properties.SettingsOptions.Default.comPort = (string)SelectPort.SelectedItem;
			Properties.SettingsOptions.Default.timeOut = int.Parse(SelectTimeout.Text);
			Properties.SettingsOptions.Default.numberRepeat = int.Parse(SelectNumberRepeat.Text);
			Properties.SettingsOptions.Default.Save();
		}
	}
}