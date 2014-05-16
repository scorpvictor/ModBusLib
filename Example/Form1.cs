using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Butek.ModBus;

namespace Example
{
	public partial class Form1 : Form
	{
		Timer _timer = new Timer(){Interval = 1000};
		ModBus _modBus = new ModBus();

		public Form1()
		{
			InitializeComponent();

			
			_modBus.ExchangeEnd += ModBusOnExchangeEnd;
			if (_modBus.OptionsShow() != DialogResult.OK)
				Close();
			else
				_modBus.OpenPort();
			
			
			_timer.Tick += new EventHandler(_timer_Tick);
			toolStripStatusLabel1.Text = string.Format("{0}: {1}-{2}-{3}-{4}", _modBus.PortName, _modBus.PortBaudrate, _modBus.PortDataBits,
				_modBus.PortParity, _modBus.PortStopBits);
		}


		void _timer_Tick(object sender, EventArgs e)
		{
			_modBus.ReadHoldingRegisters(1, 1, 10);
			UpdateData();
		}

		private  void ModBusOnExchangeEnd(object sender, ModBusEventArg modBusEventArg)
		{
			if (modBusEventArg.Status == ModBusStatus.PortOk)
				_timer.Start();
			Console.WriteLine(@"Function: {0}, Status: {1}", modBusEventArg.Function, modBusEventArg.Status);
			UpdateData();
		}

		void UpdateData()
		{
			label2.Text = string.Format("Recieve: {0}", _modBus.RecieveCounter);
			label3.Text = string.Format("Error: {0}", _modBus.ErrorCounter);
			label1.Text = string.Format("Send: {0}", _modBus.SendCounter);
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			_timer.Enabled = checkBox1.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_modBus.ClearCounter();
		}
	}
}
