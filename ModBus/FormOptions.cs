using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Butek.ModBus.Helpers;
using Butek.ModBus.Properties;

namespace Butek.ModBus
{
	public partial class FormOptions : Form
	{
		private readonly string _fileName;

		public FormOptions()
		{
			Init();

			SelectStopBits.SelectedItem = SettingsOptions.Default.stopBits;
			SelectParity.SelectedItem = SettingsOptions.Default.parity;
			comboBoxFlowControl.SelectedItem = SettingsOptions.Default.flowControl;
			SelectBits.SelectedItem = SettingsOptions.Default.dataBits;
			RefreshButton_Click(this, null);
			foreach (object item in SelectPort.Items)
			{
				if (((COMPortItem)item).Name == SettingsOptions.Default.comPort)
					SelectPort.SelectedItem = item;
			}
			SelectBaudRate.SelectedItem = SettingsOptions.Default.baudRate;

			SelectTimeout.Text = SettingsOptions.Default.timeOut.ToString(CultureInfo.InvariantCulture);
			SelectNumberRepeat.Text = SettingsOptions.Default.numberRepeat.ToString(CultureInfo.InvariantCulture);
			checkBoxEndingSymbolEnabled.Checked = SettingsOptions.Default.endingSymbolEnable;
			textBoxEndingSymbol.Text = SettingsOptions.Default.endingSymbol.ToString(CultureInfo.InvariantCulture);
			Binding();

		}

		public FormOptions(string fileName)
		{
			Init();
			_fileName = fileName;
			CustomSettings settings = LoadSettings(_fileName);
			Settings = settings;
			SelectStopBits.SelectedItem = settings.StopBits;
			SelectParity.SelectedItem = settings.Parity;
			comboBoxFlowControl.SelectedItem = settings.FlowControl;
			SelectBits.SelectedItem = settings.DataBits;
			RefreshButton_Click(this, null);
			foreach (object item in SelectPort.Items)
			{
				if (((COMPortItem)item).Name == settings.ComPort)
					SelectPort.SelectedItem = item;
			}
			SelectBaudRate.SelectedItem = settings.BaudRate;
			SelectTimeout.Text = settings.TimeOut.ToString(CultureInfo.InvariantCulture);
			SelectNumberRepeat.Text = settings.NumberRepeat.ToString(CultureInfo.InvariantCulture);
			checkBoxEndingSymbolEnabled.Checked = settings.EndingSymbolEnable;
			textBoxEndingSymbol.Text = settings.EndingSymbol.ToString(CultureInfo.InvariantCulture);
			Binding();

		}

		void Binding()
		{
			var binding = new Binding("Enabled", checkBoxEndingSymbolEnabled, "Checked");
			textBoxEndingSymbol.DataBindings.Add(binding);
		}
		public CustomSettings Settings { get; private set; }

		private void Init()
		{
			InitializeComponent();
			SelectStopBits.DataSource = Enum.GetValues(typeof(StopBits));

			SelectParity.DataSource = Enum.GetValues(typeof(Parity));

			comboBoxFlowControl.DataSource = Enum.GetValues(typeof(Handshake));

			SelectBits.Items.Add(7);
			SelectBits.Items.Add(8);
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			//обновить названия портов
			SelectPort.Items.Clear();
			foreach (string portName in SerialPort.GetPortNames())
			{
				string fullName = WMITools.GetFullNameComPort(portName);
				var ci = new COMPortItem { FullName = fullName, Name = portName };
				if (fullName != null)
					SelectPort.Items.Add(ci);
			}
		}

		public static void SaveSettings(string fileName, CustomSettings settings)
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

		public static CustomSettings LoadSettings(string fileName)
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
					settings = new CustomSettings
						{
							BaudRate = SettingsOptions.Default.baudRate,
							ComPort = SettingsOptions.Default.comPort,
							DataBits = SettingsOptions.Default.dataBits,
							FlowControl = SettingsOptions.Default.flowControl,
							NumberRepeat = SettingsOptions.Default.numberRepeat,
							Parity = SettingsOptions.Default.parity,
							StopBits = SettingsOptions.Default.stopBits,
							TimeOut = SettingsOptions.Default.timeOut,
							EndingSymbol = SettingsOptions.Default.endingSymbol,
							EndingSymbolEnable = SettingsOptions.Default.endingSymbolEnable
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
				SettingsOptions.Default.stopBits = (StopBits)SelectStopBits.SelectedItem;
				SettingsOptions.Default.parity = (Parity)SelectParity.SelectedItem;
				SettingsOptions.Default.flowControl = (Handshake)comboBoxFlowControl.SelectedItem;
				SettingsOptions.Default.baudRate = (int)SelectBaudRate.SelectedItem;
				SettingsOptions.Default.dataBits = (int)SelectBits.SelectedItem;
				SettingsOptions.Default.comPort = ((COMPortItem)SelectPort.SelectedItem).Name;
				SettingsOptions.Default.timeOut = int.Parse(SelectTimeout.Text);
				SettingsOptions.Default.numberRepeat = int.Parse(SelectNumberRepeat.Text);
				SettingsOptions.Default.endingSymbol = byte.Parse(textBoxEndingSymbol.Text);
				SettingsOptions.Default.endingSymbolEnable = checkBoxEndingSymbolEnabled.Checked;
				SettingsOptions.Default.Save();
			}
			else
			{
				var settings = new CustomSettings
					{
						BaudRate = (int)SelectBaudRate.SelectedItem,
						ComPort = ((COMPortItem)SelectPort.SelectedItem).Name,
						DataBits = (int)SelectBits.SelectedItem,
						FlowControl = (Handshake)comboBoxFlowControl.SelectedItem,
						NumberRepeat = int.Parse(SelectNumberRepeat.Text),
						Parity = (Parity)SelectParity.SelectedItem,
						StopBits = (StopBits)SelectStopBits.SelectedItem,
						TimeOut = int.Parse(SelectTimeout.Text),
						EndingSymbol = byte.Parse(textBoxEndingSymbol.Text),
						EndingSymbolEnable = checkBoxEndingSymbolEnabled.Checked
					};
				Settings = settings;
				SaveSettings(_fileName, settings);
			}
		}

		private void SelectPort_SelectedIndexChanged(object sender, EventArgs e)
		{
			var prop = SerialPortHelper.GetComPortProperties(((COMPortItem)SelectPort.SelectedItem).Name);
			var result = (SerialPortHelper.Baud)prop.dwSettableBaud;
			var tmp = Enum.GetValues((typeof(SerialPortHelper.Baud)));
			var selectedItem = SelectBaudRate.SelectedItem;
			SelectBaudRate.Items.Clear();
			var listBaud = new List<object>();
			foreach (Enum br in tmp)
			{
				var item = GetEnumDescription(br);
				try
				{
					listBaud.Add(int.Parse(item));
				}
				catch (FormatException)
				{ continue; }
			}
			SelectBaudRate.Items.AddRange(listBaud.OrderBy(o => o).ToArray());
			if (listBaud.Contains(selectedItem))
				SelectBaudRate.SelectedItem = selectedItem;
			else
				SelectBaudRate.SelectedItem = SelectBaudRate.Items[0];
		}
		public static string GetEnumDescription(Enum value)
		{
			var fi = value.GetType().GetField(value.ToString());
			var attributes =
			  (DescriptionAttribute[])fi.GetCustomAttributes
			  (typeof(DescriptionAttribute), false);
			return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
		}

	}
}