using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ModBus.Properties;

namespace ModBus
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
			SelectBaudRate.SelectedItem = SettingsOptions.Default.baudRate;
			SelectBits.SelectedItem = SettingsOptions.Default.dataBits;
			RefreshButton_Click(this, null);
			foreach (object item in SelectPort.Items)
			{
				if (((COMPortItem) item).Name == SettingsOptions.Default.comPort)
					SelectPort.SelectedItem = item;
			}
			SelectTimeout.Text = SettingsOptions.Default.timeOut.ToString(CultureInfo.InvariantCulture);
			SelectNumberRepeat.Text = SettingsOptions.Default.numberRepeat.ToString(CultureInfo.InvariantCulture);
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
			SelectBaudRate.SelectedItem = settings.BaudRate;
			SelectBits.SelectedItem = settings.DataBits;
			RefreshButton_Click(this, null);
			foreach (object item in SelectPort.Items)
			{
				if (((COMPortItem) item).Name == settings.ComPort)
					SelectPort.SelectedItem = item;
			}
			SelectTimeout.Text = settings.TimeOut.ToString(CultureInfo.InvariantCulture);
			SelectNumberRepeat.Text = settings.NumberRepeat.ToString(CultureInfo.InvariantCulture);
		}

		public CustomSettings Settings { get; private set; }

		private void Init()
		{
			InitializeComponent();
			SelectStopBits.DataSource = Enum.GetValues(typeof (StopBits));

			SelectParity.DataSource = Enum.GetValues(typeof (Parity));

			comboBoxFlowControl.DataSource = Enum.GetValues(typeof (Handshake));

			SelectBaudRate.Items.Add(300);
			SelectBaudRate.Items.Add(600);
			SelectBaudRate.Items.Add(1200);
			SelectBaudRate.Items.Add(2400);
			SelectBaudRate.Items.Add(4800);
			SelectBaudRate.Items.Add(9600);
			SelectBaudRate.Items.Add(14400);
			SelectBaudRate.Items.Add(19200);
			SelectBaudRate.Items.Add(38400);
			SelectBaudRate.Items.Add(56000);
			SelectBaudRate.Items.Add(57600);
			SelectBaudRate.Items.Add(115200);

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
				var ci = new COMPortItem {FullName = fullName, Name = portName};
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
				var v = (CustomSettings) xmlSer.Deserialize(xmlReader);
				xmlReader.Close();
				return v;
			}
			catch (Exception exc)
			{
				if (exc.InnerException.GetType() == typeof (FileNotFoundException))
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
							TimeOut = SettingsOptions.Default.timeOut
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
				SettingsOptions.Default.stopBits = (StopBits) SelectStopBits.SelectedItem;
				SettingsOptions.Default.parity = (Parity) SelectParity.SelectedItem;
				SettingsOptions.Default.flowControl = (Handshake) comboBoxFlowControl.SelectedItem;
				SettingsOptions.Default.baudRate = (int) SelectBaudRate.SelectedItem;
				SettingsOptions.Default.dataBits = (int) SelectBits.SelectedItem;
				SettingsOptions.Default.comPort = ((COMPortItem) SelectPort.SelectedItem).Name;
				SettingsOptions.Default.timeOut = int.Parse(SelectTimeout.Text);
				SettingsOptions.Default.numberRepeat = int.Parse(SelectNumberRepeat.Text);
				SettingsOptions.Default.Save();
			}
			else
			{
				var settings = new CustomSettings
					{
						BaudRate = (int) SelectBaudRate.SelectedItem,
						ComPort = ((COMPortItem) SelectPort.SelectedItem).Name,
						DataBits = (int) SelectBits.SelectedItem,
						FlowControl = (Handshake) comboBoxFlowControl.SelectedItem,
						NumberRepeat = int.Parse(SelectNumberRepeat.Text),
						Parity = (Parity) SelectParity.SelectedItem,
						StopBits = (StopBits) SelectStopBits.SelectedItem,
						TimeOut = int.Parse(SelectTimeout.Text)
					};
				Settings = settings;
				SaveSettings(_fileName, settings);
			}
		}
	}
}