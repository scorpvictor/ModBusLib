using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ModBus.Properties;

namespace ModBus
{
	internal partial class FormOptions : Form
	{
		private readonly string _fileName;
		//private CustomSettings _settings;
		public CustomSettings Settings
		{
			get; private set; }
		public FormOptions()
		{
			Init();

			SelectStopBits.SelectedItem = Properties.SettingsOptions.Default.stopBits;
			SelectParity.SelectedItem = Properties.SettingsOptions.Default.parity;
			comboBoxFlowControl.SelectedItem = Properties.SettingsOptions.Default.flowControl;
			SelectBaudRate.SelectedItem = Properties.SettingsOptions.Default.baudRate;
			SelectBits.SelectedItem = Properties.SettingsOptions.Default.dataBits;
			RefreshButton_Click(this, null);
			foreach (var item in SelectPort.Items)
			{
				if (((COMPortItem)item).Name == Properties.SettingsOptions.Default.comPort)
					SelectPort.SelectedItem = item;
			}
			SelectTimeout.Text = Properties.SettingsOptions.Default.timeOut.ToString();
			SelectNumberRepeat.Text = Properties.SettingsOptions.Default.numberRepeat.ToString();

		}

		public FormOptions(string fileName)
		{
			Init();
			_fileName = fileName;
			var settings = LoadSettings(_fileName);
			SelectStopBits.SelectedItem = settings.StopBits;
			SelectParity.SelectedItem = settings.Parity;
			comboBoxFlowControl.SelectedItem = settings.FlowControl;
			SelectBaudRate.SelectedItem = settings.BaudRate;
			SelectBits.SelectedItem = settings.DataBits;
			RefreshButton_Click(this, null);
			foreach (var item in SelectPort.Items)
			{
				if (((COMPortItem)item).Name == settings.ComPort)
					SelectPort.SelectedItem = item;
			}
			SelectTimeout.Text = settings.TimeOut.ToString();
			SelectNumberRepeat.Text = settings.NumberRepeat.ToString();
		}

		void Init()
		{
			InitializeComponent();
			SelectStopBits.DataSource = Enum.GetValues(typeof(StopBits));

			SelectParity.DataSource = Enum.GetValues(typeof(Parity));

			comboBoxFlowControl.DataSource = Enum.GetValues(typeof(Handshake));

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
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			//обновить названия портов
			SelectPort.Items.Clear();
			foreach (var portName in SerialPort.GetPortNames())
			{
				var fullName = WMITools.GetFullNameComPort(portName);
				var ci = new COMPortItem { FullName = fullName, Name = portName };
				if (fullName != null)
					SelectPort.Items.Add(ci);
			}
		}

		static public void SaveSettings(string fileName, CustomSettings settings)
		{
			var xmlSer = new XmlSerializer(settings.GetType());
			var xmlSet = new XmlWriterSettings
				{
					NewLineOnAttributes = true,
					Indent = true,
					NewLineChars = "\r\n",
					Encoding = Encoding.UTF8
				};
			XmlWriter xmlWr = XmlWriter.Create(fileName, xmlSet);
			xmlSer.Serialize(xmlWr, settings);
			xmlWr.Close();
		}
		static public CustomSettings LoadSettings(string fileName)
		{
			var settings = new CustomSettings();
			var xmlSer = new XmlSerializer(settings.GetType());
			XmlReader xmlReader = new XmlTextReader(fileName);
			try
			{
				var v = (CustomSettings)xmlSer.Deserialize(xmlReader);
				xmlReader.Close();
				return v;
			}
			catch (Exception exc)
			{
				if (exc.InnerException.GetType() == typeof(FileNotFoundException))
				{
					settings = new CustomSettings()
					{
						BaudRate = Properties.SettingsOptions.Default.baudRate,
						ComPort = Properties.SettingsOptions.Default.comPort,
						DataBits = Properties.SettingsOptions.Default.dataBits,
						FlowControl = Properties.SettingsOptions.Default.flowControl,
						NumberRepeat = Properties.SettingsOptions.Default.numberRepeat,
						Parity = Properties.SettingsOptions.Default.parity,
						StopBits = Properties.SettingsOptions.Default.stopBits,
						TimeOut = Properties.SettingsOptions.Default.timeOut
					};
					return settings;
				}
			}
			return null;
		}
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_fileName))
			{
				Properties.SettingsOptions.Default.stopBits = (StopBits)SelectStopBits.SelectedItem;
				Properties.SettingsOptions.Default.parity = (Parity)SelectParity.SelectedItem;
				Properties.SettingsOptions.Default.flowControl = (Handshake)comboBoxFlowControl.SelectedItem;
				Properties.SettingsOptions.Default.baudRate = (int)SelectBaudRate.SelectedItem;
				Properties.SettingsOptions.Default.dataBits = (int)SelectBits.SelectedItem;
				Properties.SettingsOptions.Default.comPort = ((COMPortItem)SelectPort.SelectedItem).Name;
				Properties.SettingsOptions.Default.timeOut = int.Parse(SelectTimeout.Text);
				Properties.SettingsOptions.Default.numberRepeat = int.Parse(SelectNumberRepeat.Text);
				Properties.SettingsOptions.Default.Save();
			}
			else
			{
				var settings = new CustomSettings()
					{
						BaudRate = (int)SelectBaudRate.SelectedItem,
						ComPort = ((COMPortItem)SelectPort.SelectedItem).Name,
						DataBits = (int)SelectBits.SelectedItem,
						FlowControl = (Handshake)comboBoxFlowControl.SelectedItem,
						NumberRepeat = int.Parse(SelectNumberRepeat.Text),
						Parity = (Parity)SelectParity.SelectedItem,
						StopBits = (StopBits)SelectStopBits.SelectedItem,
						TimeOut = int.Parse(SelectTimeout.Text)
					};
				SaveSettings(_fileName, settings);
			}
		}
	}
}