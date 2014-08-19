using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Butek.ModBus.Properties;
using Timer = System.Timers.Timer;


namespace Butek.ModBus
{
	/// <summary>
	///     Класс реализует протокол обмена ModBus посредством последовательного порта
	/// </summary>
	public class ModBus
	{
		#region Fields
		readonly PacketDetectedEventArg _packetDetectedArgument = new PacketDetectedEventArg();
		/// <summary>
		///     Файл хранит настройки порта
		/// </summary>
		private readonly string _fileName;

		private readonly SerialPort _serialPort = new SerialPort();
		private readonly Timer _timerCheck = new Timer();
		private readonly Timer _timerOut = new Timer();
		private ModBusEventArg _eventArgument = new ModBusEventArg();
		private bool _endingSymbolEnable = true;
		private byte _endingSymbol = 0;
		/// <summary>
		///     Адрес подчиненого
		/// </summary>
		private int _addressSlave;

		/// <summary>
		/// прочитанное кол-во байт
		/// </summary>
		private int _bytesRead;

		/// <summary>
		/// Запрошенная функция
		/// </summary>
		private int _requestFunction;

		private int _counterRepeat;

		/// <summary>
		///     Максимальное число повторов запросов
		/// </summary>
		private int _numberRepeatMax = 5;

		private FormOptions _setupForm;

		/// <summary>
		///     TimeOut в милисе6кундах
		/// </summary>
		private int _timeOut = 300;

		/// <summary>
		///     Ожидание ответа от подчиненого
		/// </summary>
		private bool _waitResponse;

		/// <summary>
		///     Буфер для хранения предыдущей отправленой посылки
		///     (в случае если посылка не прошла по таймауту то повторяем запрос с содержимым данного буфера)
		/// </summary>
		private byte[] _bufferTransmit;

		/// <summary>
		/// Счетчик посланых пакетов
		/// </summary>
		private int _sendCounter;
		/// <summary>
		/// Счетчик полученых пакетов
		/// </summary>
		private int _recieveCounter;
		/// <summary>
		/// Счетчик ошибочных пакетов
		/// </summary>
		private int _errorCounter;

		#endregion


		#region Constructors

		public ModBus()
		{
			Init();
		}

		/// <summary>
		/// </summary>
		/// <param name="fileName">Файл которых хранит настройки порта</param>
		public ModBus(string fileName)
		{
			_fileName = fileName;
			Init();
		}

		#endregion

		#region Properties

		public string PortName
		{
			get { return _serialPort.PortName; }
		}
		public int PortBaudrate
		{
			get { return _serialPort.BaudRate; }
		}
		public int PortDataBits
		{
			get { return _serialPort.DataBits; }
		}
		public StopBits PortStopBits
		{
			get { return _serialPort.StopBits; }
		}
		public Parity PortParity
		{
			get { return _serialPort.Parity; }
		}

		/// <summary>
		/// Счетчик посланых пакетов
		/// </summary>
		public int SendCounter
		{
			get { return _sendCounter; }
			private set { _sendCounter = value; }
		}

		/// <summary>
		/// Счетчик полученых пакетов
		/// </summary>
		public int RecieveCounter
		{
			get { return _recieveCounter; }
			private set { _recieveCounter = value; }
		}

		/// <summary>
		/// Счетчик ошибочных пакетов
		/// </summary>
		public int ErrorCounter
		{
			get { return _errorCounter; }
			private set { _errorCounter = value; }
		}

		/// <summary>
		///     Максимальное число повторов запросов
		/// </summary>
		public int NumberRepeatMax
		{
			get { return _numberRepeatMax; }
			set { _numberRepeatMax = value; }
		}

		public byte EndingSymbol
		{
			get { return _endingSymbol; }
			set { _endingSymbol = value; }
		}

		public bool EndingSymbolEnable
		{
			get { return _endingSymbolEnable; }
			set { _endingSymbolEnable = value; }
		}

		#endregion

		void Init()
		{
			_timerOut.AutoReset = false;
			_timerCheck.AutoReset = false;
			_timerOut.Interval = _timeOut;
			_timerOut.Elapsed += timerOut_Tick;
			_timerCheck.Interval = 10;
			_timerCheck.Elapsed += timerCheck_Tick;
			_serialPort.ReadTimeout = SerialPort.InfiniteTimeout;
			_serialPort.WriteTimeout = SerialPort.InfiniteTimeout;

		}
		private byte[] _bytesReadArray;

		private void timerCheck_Tick(object sender, EventArgs e)
		{
			try
			{
				var count = _serialPort.BytesToRead;
				if (count != 0)
				{
					if (_bytesReadArray == null || _bytesReadArray.Length == 0)
					{
						_bytesReadArray = new byte[count];
						_serialPort.Read(_bytesReadArray, 0, count);
					}
					else
					{
						var bytesArray = new byte[count];
						_serialPort.Read(bytesArray, 0, count);
						var oldLength = _bytesReadArray.Length;
						Array.Resize(ref _bytesReadArray, _bytesReadArray.Length + count);
						Array.Copy(bytesArray, 0, _bytesReadArray, oldLength, count);
					}
					var result = CheckData(_bytesReadArray);
					_timerOut.Stop();
					switch (result.Status)
					{
						case ModBusStatus.PresetMultipleRegistersOK:
						case ModBusStatus.ReadHoldingRegistersOK:
						case ModBusStatus.CustomFunctionOk:
							_waitResponse = false;
							RecieveCounter++;
							RaisePacketDetected(_bytesReadArray);
							_eventArgument = result;
							OnExchangeEnd();
							break;
						case ModBusStatus.CRCError:
						case ModBusStatus.UnknowPacket:
						case ModBusStatus.None:
						case ModBusStatus.NoEndingSymbol:
						case ModBusStatus.TimeOutError:
							_timerCheck.Start();
							_timerOut.Reset();
							break;
						default:
							ErrorCounter++;
							RaisePacketDetected(_bytesReadArray);
							if (!CheckRepeat())
							{
								_waitResponse = false;
								_eventArgument = result;
								OnExchangeEnd();
							}
							break;
					}
					return;
				}
			}
			catch (InvalidOperationException exc)
			{
				if (CatchSerialException(exc))
					return;
			}
			_timerCheck.Start();
		}

		/// <summary>
		/// Проверка нужно ли повторить запрос
		/// </summary>
		/// <returns></returns>
		bool CheckRepeat()
		{
			_counterRepeat++;
			if (_counterRepeat <= NumberRepeatMax)
			{
				RepeatRequest();
				return true;
			}
			_waitResponse = false;
			return false;
		}

		private void timerOut_Tick(object sender, EventArgs e)
		{
			var result = CheckData(_bytesReadArray);
			switch (result.Status)
			{
				case ModBusStatus.PresetMultipleRegistersOK:
				case ModBusStatus.ReadHoldingRegistersOK:
				case ModBusStatus.CustomFunctionOk:
					_waitResponse = false;
					RecieveCounter++;
					_timerCheck.Stop();
					RaisePacketDetected(_bytesReadArray);
					_eventArgument = result;
					OnExchangeEnd();
					break;

				default:
					ErrorCounter++;
					if (!CheckRepeat())
					{
						_eventArgument = new ModBusEventArg() { Status = result.Status };
						OnExchangeEnd();
					}
					break;
			}
		}

		/// <summary>
		///     Обработка исключений порта
		/// </summary>
		/// <param name="exc"></param>
		private bool CatchSerialException(Exception exc)
		{
			switch (exc.TargetSite.DeclaringType.Name)
			{
				default:
				case "SerialPort":
					_timerCheck.Stop();
					_timerOut.Stop();
					_waitResponse = false;
					_eventArgument.Status = ModBusStatus.PortIsClosed;
					OnExchangeEnd();
					return true;
					break;
			}
		}

		void RaisePacketDetected(byte[] buffer)
		{
#if DEBUG
			Console.Write("{0} Receive:", _serialPort.PortName);
			foreach (byte b in buffer)
			{
				Console.Write("{0} ", b.ToString("X2"));
			}
			Console.WriteLine();
#endif
			_packetDetectedArgument.Data = buffer;
			_packetDetectedArgument.IsRecieved = true;
			OnPacketDetected();
		}

		/// <summary>
		/// проверяем полученные данные
		/// </summary>
		private ModBusEventArg CheckData(byte[] buffer)
		{

			if (buffer == null || buffer.Length == 0)
			{
				return new ModBusEventArg { Status = ModBusStatus.TimeOutError };
			}

			if (buffer.Length <= 2)
			{
				return new ModBusEventArg { Status = ModBusStatus.UnknowPacket };
			}

			if (buffer[0] != _addressSlave)
			{
				return new ModBusEventArg { Status = ModBusStatus.AddressSlaveError };
			}
			var function = buffer[1] & ~0x80;
			var error = (buffer[1] & 0x80) == 0x80;
			if (_requestFunction != function)
			{
				return new ModBusEventArg { Status = ModBusStatus.InvalidFunction };
			}

			if (_endingSymbolEnable && buffer[buffer.Length - 1] != EndingSymbol)
			{
				return new ModBusEventArg { Status = ModBusStatus.NoEndingSymbol };
			}
			if (!CRC.CheckCRC(buffer, 0, buffer.Length - (_endingSymbolEnable ? 3 : 2), buffer, buffer.Length - (_endingSymbolEnable ? 3 : 2)))
			{
				return new ModBusEventArg { Status = ModBusStatus.CRCError };
			}

			if (error)
			{
				return new ModBusEventArg
				{
					Status = ModBusStatus.FunctionError,
					Function = buffer[1],
					data = new short[] { buffer[2] },
				};
			}
			//			RecieveCounter++;
			var ea = new ModBusEventArg
			{
				Function = buffer[1]
			};
			// Определяем тип функции
			switch (function)
			{
				// Read Holding Registers
				case 3:
					ea.data = new short[buffer[2] / 2];
					ea.Status = ModBusStatus.ReadHoldingRegistersOK;

					int i = 0;
					while (i != ea.data.Length)
					{
						ea.data[i] = (short)(buffer[i * 2 + 3] << 8);
						ea.data[i] += buffer[i * 2 + 1 + 3];
						i++;
					}
					break;

				// Preset Multiple Registers
				case 0x10:
					ea.data = new short[0];
					ea.Status = ModBusStatus.PresetMultipleRegistersOK;
					break;
				default:
					ea.data = new short[buffer[2] / 2];
					ea.Status = ModBusStatus.CustomFunctionOk;
					i = 0;
					while (i != ea.data.Length)
					{
						ea.data[i] = (short)(buffer[i * 2 + 3] << 8);
						ea.data[i] += buffer[i * 2 + 1 + 3];
						i++;
					}

					break;
			}
			return ea;
		}


		/// <summary>
		///     Отображает диалог настройки
		/// </summary>
		public DialogResult OptionsShow(string caption)
		{
			if (string.IsNullOrEmpty(_fileName))
			{
				_setupForm = _setupForm ?? (new FormOptions());
			}
			else
			{
				_setupForm = _setupForm ?? (new FormOptions(_fileName));
			}
			_setupForm.Text = caption;
			#region Заполняю форму настройками порта

			if (_setupForm.ShowDialog() == DialogResult.OK)
			{
				try
				{
					LoadSettingsPort();
				}
				catch
				{
					if (_serialPort.IsOpen)
						MessageBox.Show("Порт уже открыт", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return DialogResult.OK;
			}
			else
				return DialogResult.Cancel;

			#endregion
		}
		/// <summary>
		/// Отображает диалог настройки
		/// </summary>
		public DialogResult OptionsShow()
		{
			return OptionsShow("Настройки");
		}
		void LoadSettingsPort()
		{
			if (string.IsNullOrEmpty(_fileName))
			{
				_serialPort.PortName = SettingsOptions.Default.comPort;
				_serialPort.BaudRate = SettingsOptions.Default.baudRate;
				_serialPort.DataBits = SettingsOptions.Default.dataBits;
				_serialPort.Parity = SettingsOptions.Default.parity;
				_serialPort.StopBits = SettingsOptions.Default.stopBits;
				// По умолчанию Handshake = Handshake.None
				// Но если его его не переприсвоить заново 
				// Последовательный порт работал не стабильно
				// Наблюдались повторы посылок по ModBus
				// Возможно это связанно с ПО Аркадия (Виртуальный COM порт не принимал правильно настройки от Хоста)
				_serialPort.Handshake = SettingsOptions.Default.flowControl;
				_timeOut = SettingsOptions.Default.timeOut;
				NumberRepeatMax = SettingsOptions.Default.numberRepeat;
				EndingSymbolEnable = SettingsOptions.Default.endingSymbolEnable;
				EndingSymbol = SettingsOptions.Default.endingSymbol;
			}
			else
			{
				CustomSettings settings = FormOptions.LoadSettings(_fileName);
				_serialPort.PortName = settings.ComPort;
				_serialPort.BaudRate = settings.BaudRate;
				_serialPort.DataBits = settings.DataBits;
				_serialPort.Parity = settings.Parity;
				_serialPort.StopBits = settings.StopBits;
				_serialPort.Handshake = settings.FlowControl;
				_timeOut = settings.TimeOut;
				EndingSymbolEnable = settings.EndingSymbolEnable;
				EndingSymbol = settings.EndingSymbol;
				NumberRepeatMax = settings.NumberRepeat;
			}

			_timerOut.Interval = _timeOut;
		}

		/// <summary>
		///     запрос на чтение двоичного содержания регистров подчиненого
		/// </summary>
		/// <param name="address">Адрес подчиненого</param>
		/// <param name="regAddress">начальный адрес регистров</param>
		/// <param name="numberReg">количество регистров </param>
		public void ReadHoldingRegisters(byte address, short regAddress, short numberReg)
		{
			if (!_waitResponse)
			{
				_counterRepeat = 0;
				int i = 0;
				_bufferTransmit = new byte[8];
				_addressSlave = address;
				_bufferTransmit[i++] = address;
				_bufferTransmit[i++] = 0x03;
				_bufferTransmit[i++] = (byte)(regAddress >> 8);
				_bufferTransmit[i++] = (byte)(regAddress);

				_bufferTransmit[i++] = (byte)(numberReg >> 8);
				_bufferTransmit[i++] = (byte)(numberReg);

				_bufferTransmit[i++] = (byte)CRC.GetCRC(_bufferTransmit, 6);
				_bufferTransmit[i] = (byte)(CRC.GetCRC(_bufferTransmit, 6) >> 8);

				_requestFunction = 3;
				RepeatRequest();
			}
			else
				throw new BusyException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="address">адрес подчиненого</param>
		/// <param name="function">запрашиваемая функция</param>
		/// <param name="data">передаваемые данные</param>
		public void RequestCustomFunction(byte address, byte function, byte[] data)
		{
			if (!_waitResponse)
			{
				_counterRepeat = 0;
				int i = 0;
				_bufferTransmit = new byte[(data == null ? 0 : data.Length) + 5];
				_addressSlave = address;
				_bufferTransmit[i++] = address;
				_bufferTransmit[i++] = function;
				_bufferTransmit[i++] = data == null ? (byte)0 : (byte)data.Length;

				if (data != null)
					foreach (byte b in data)
						_bufferTransmit[i++] = b;

				_bufferTransmit[i++] = (byte)CRC.GetCRC(_bufferTransmit, _bufferTransmit.Length - 2);
				_bufferTransmit[i] = (byte)(CRC.GetCRC(_bufferTransmit, _bufferTransmit.Length - 2) >> 8);

				_requestFunction = function;
				RepeatRequest();
			}
			else
				throw new BusyException();
		}
		/// <summary>
		///     Запрос на запись двоичного содержания регистров подчиненого
		/// </summary>
		/// <param name="address">дрес подчиненого</param>
		/// <param name="regAddress">начальный адрес регистров</param>
		/// <param name="data">Массив новых значений регистров</param>
		/// <param name="numberReg">количество регистров</param>
		public void PresetMultipleRegisters(byte address, short regAddress, short[] data, short numberReg)
		{
			if (!_waitResponse)
			{
				_counterRepeat = 0;
				int i = 0;
				_bufferTransmit = new byte[numberReg * 2 + 9];
				_addressSlave = address;
				_bufferTransmit[i++] = address;
				_bufferTransmit[i++] = 0x10;
				_bufferTransmit[i++] = (byte)(regAddress >> 8);
				_bufferTransmit[i++] = (byte)(regAddress);

				_bufferTransmit[i++] = (byte)(numberReg >> 8);
				_bufferTransmit[i++] = (byte)(numberReg);

				_bufferTransmit[i++] = (byte)(numberReg * 2);


				int index = 0;
				while (index != numberReg)
				{
					_bufferTransmit[i++] = (byte)(data[index] >> 8);
					_bufferTransmit[i++] = (byte)(data[index++]);
				}

				_bufferTransmit[i++] = (byte)CRC.GetCRC(_bufferTransmit, _bufferTransmit.Length - 2);
				_bufferTransmit[i] = (byte)(CRC.GetCRC(_bufferTransmit, _bufferTransmit.Length - 2) >> 8);

				_requestFunction = 0x10;
				RepeatRequest();
			}
			else
				throw new BusyException();
		}

		public void ClearCounter()
		{
			ErrorCounter = 0;
			SendCounter = 0;
			RecieveCounter = 0;
		}
		/// <summary>
		///     Повтор посылки последней
		/// </summary>
		public void Repeat()
		{
			if (!_waitResponse)
			{
				_counterRepeat = 0;
				RepeatRequest();
			}
			else
				throw new BusyException();
		}

		void RepeatRequest()
		{
			// очищаем буфер приемный
			try
			{
				Array.Resize(ref _bytesReadArray, 0);
				_bytesRead = int.MaxValue;
				_serialPort.DiscardInBuffer();
				_serialPort.Write(_bufferTransmit, 0, _bufferTransmit.Length);
			}
			catch (InvalidOperationException exc)
			{
				if (CatchSerialException(exc))
					return;
			}
			catch (IOException exc)
			{
				if (CatchSerialException(exc))
					return;
			}
#if DEBUG
			Console.Write("{0} Send:", _serialPort.PortName);
			foreach (byte b in _bufferTransmit)
			{
				Console.Write("{0} ", b.ToString("X2"));
			}
			Console.WriteLine();
#endif
			_packetDetectedArgument.Data = _bufferTransmit;
			_packetDetectedArgument.IsTransmitted = true;

			SendCounter++;
			OnPacketDetected();

			_waitResponse = true;
			_timerOut.Start();
			_timerCheck.Start();
		}
		/// <summary>
		///     Открывает порт
		/// </summary>
		/// <returns>false - порт не открылся</returns>
		public bool OpenPort()
		{
			bool result = true;
			try
			{
				if (!_serialPort.IsOpen)
				{
					LoadSettingsPort();
					_serialPort.Open();
				}
				_eventArgument.Status = ModBusStatus.PortOk;
				OnExchangeEnd();
			}
			catch
			{
				_eventArgument.Status = ModBusStatus.BusyPort;
				OnExchangeEnd();
				result = false;
			}
			return result;
		}

		public void ClosePort()
		{
			_counterRepeat = 0;
			_waitResponse = false;
			_timerOut.Stop();
			_timerCheck.Stop();
			if (_serialPort.IsOpen)
				_serialPort.Close();
		}



		#region Events

		public delegate void PacketDetectedHandle(object sender, PacketDetectedEventArg e);
		public event PacketDetectedHandle PacketDetected;
		private void OnPacketDetected()
		{
			// Если не ссылается на NULL то вызываем событие
			if (PacketDetected != null)
			{
				_packetDetectedArgument.Date = DateTime.Now;
				PacketDetected(this, _packetDetectedArgument);
			}
		}

		public event ExchangeEndHandle ExchangeEnd;
		public delegate void ExchangeEndHandle(object sender, ModBusEventArg e);
		private void OnExchangeEnd()
		{
			// Если не ссылается на NULL то вызываем событие
			if (ExchangeEnd != null)
			{
				ExchangeEnd(this, _eventArgument);
			}
		}

		#endregion

	}


	public enum ModBusStatus
	{
		None,

		/// <summary>
		///     Ошибка провышение времени ожидания ответа(посылки)
		/// </summary>
		TimeOutError,

		/// <summary>
		///     Получены неизвестные данные
		/// </summary>
		UnknowPacket,
		/// <summary>
		/// Принята неверная функция
		/// </summary>
		InvalidFunction,
		/// <summary>
		/// Запрос функции вернул ошибку
		/// </summary>
		FunctionError,
		/// <summary>
		///     Ошибка подчиненого
		/// </summary>
		AddressSlaveError,

		/// <summary>
		///     Ошибка контрольной суммы
		/// </summary>
		CRCError,

		/// <summary>
		///     Данные прочитаны успешно
		/// </summary>
		ReadPacketOK,

		/// <summary>
		///     Данные записаны успешно
		/// </summary>
		WritePacketOK,

		/// <summary>
		///     Занято процессом чтения данных
		/// </summary>
		BusyRead,

		/// <summary>
		///     Занято процессом записи данных
		/// </summary>
		BusyWrite,

		/// <summary>
		///     Запрос регистров успешно завершен (команда 3)
		/// </summary>
		ReadHoldingRegistersOK,

		/// <summary>
		///     Порт занят
		/// </summary>
		BusyPort,

		/// <summary>
		///     Порт закрыт
		/// </summary>
		PortIsClosed,
		PortOk,
		// Запись регистров успешна (команда 0x10)
		PresetMultipleRegistersOK,
		/// <summary>
		/// Пользовательская функция верна
		/// </summary>
		CustomFunctionOk,
		/// <summary>
		/// В полученной посылке нет символа окончания
		/// </summary>
		NoEndingSymbol
	}

	public class PacketDetectedEventArg : EventArgs
	{
		#region Fields

		private bool _flag;
		private byte[] _data;
		private DateTime _date;

		#endregion


		#region Properties

		public bool IsRecieved
		{
			get { return !_flag; }
			internal set { _flag = !value; }
		}

		public bool IsTransmitted
		{
			get { return _flag; }
			internal set { _flag = value; }
		}

		public byte[] Data
		{
			get { return _data; }
			internal set { _data = value; }
		}

		public DateTime Date
		{
			get { return _date; }
			internal set { _date = value; }
		}

		#endregion

	}

	/// <summary>
	///     Аргумент события ModBus
	/// </summary>
	public class ModBusEventArg : EventArgs
	{
		internal short[] data;
		private ModBusStatus status = 0;
		public int Function { get; set; }
		public short[] Data
		{
			get { return data; }
			internal set { data = value; }
		}

		public ModBusStatus Status
		{
			get { return status; }
			internal set { status = value; }
		}
	}

	#region Исключительные ситуации

	/// <summary>
	///     Занят  ожиданием ответа от подчиненого
	/// </summary>
	[Serializable]
	public class BusyException : Exception
	{
		public BusyException()
			: base("Ожидание ответа от подчиненого")
		{
		}
	}

	#endregion

	public class CRC
	{
		public static ushort GetCRC(byte[] buf)
		{
			return GetCRC(buf, buf.Length);
		}

		public static ushort GetCRC(byte[] buf, int buf_size)
		{
			ushort crc16mb_prev, index = 0;
			crc16mb_prev = 0xffff;

			while (buf_size != 0)
			{
				crc16mb_prev ^= buf[index];
				for (byte uc1 = 8; uc1 != 0; uc1--)
				{
					var uc2 = (byte)(crc16mb_prev & 0x0001);
					crc16mb_prev >>= 1;
					if (uc2 == 1)
						crc16mb_prev ^= 0xA001;
				}
				buf_size--;
				index++;
			}
			return (crc16mb_prev);
		}

		public static ushort GetCRC(byte[] buf, int beginIndex, int contByte)
		{
			ushort crc16mb_prev;
			int index = beginIndex;
			crc16mb_prev = 0xffff;

			while (contByte != 0)
			{
				crc16mb_prev ^= buf[index];
				for (byte uc1 = 8; uc1 != 0; uc1--)
				{
					var uc2 = (byte)(crc16mb_prev & 0x0001);
					crc16mb_prev >>= 1;
					if (uc2 == 1)
						crc16mb_prev ^= 0xA001;
				}
				contByte--;
				index++;
			}
			return (crc16mb_prev);
		}

		/// <summary>
		///     Проверяет CRC в посылке
		/// </summary>
		/// <param name="buf">массив данных</param>
		/// <param name="beginIndex">начальный индекс в массиве</param>
		/// <param name="countByte">Количество байт</param>
		/// <param name="bufCRC">массив где находится CRC</param>
		/// <param name="beginIndexCRC">начальный индекс в массиве где находится CRC</param>
		/// <returns>true - CRC верно</returns>
		public static bool CheckCRC(byte[] buf, int beginIndex, int countByte, byte[] bufCRC, int beginIndexCRC)
		{
			ushort crc;
			crc = (ushort)(bufCRC[beginIndexCRC + 1] << 8);
			crc += bufCRC[beginIndexCRC];
			if (GetCRC(buf, beginIndex, countByte) == crc)
				return true;
			else
				return false;
		}
	}

	/// <summary>
	///     Атрибуты регистра
	/// </summary>
	public enum RegisterAtr
	{
		/// <summary>
		///     Доступ на чтение запись
		/// </summary>
		ReadWrite,

		/// <summary>
		///     Доступ на чтение
		/// </summary>
		Read,

		/// <summary>
		///     Доступ на запись
		/// </summary>
		Write
	}

	/// <summary>
	///     Описывает регистр ModBus
	/// </summary>
	[Serializable]
	public class Register
	{
		/// <summary>
		///     Адрес регистра
		/// </summary>
		private ushort _address;

		/// <summary>
		///     Атрибут регистра
		/// </summary>
		private RegisterAtr _attribute = RegisterAtr.Read;

		/// <summary>
		///     Количество регистров
		/// </summary>
		private int _length;

		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}

		/// <summary>
		///     Адрес регистра
		/// </summary>
		public ushort Address
		{
			get { return _address; }
			set { _address = value; }
		}

		/// <summary>
		///     Атрибут регистра
		/// </summary>
		public RegisterAtr Attribute
		{
			get { return _attribute; }
			set { _attribute = value; }
		}
	}
	public static class TimerExtension
	{
		public static void Reset(this Timer timer)
		{
			timer.Stop();
			timer.Start();
		}
	}

}




