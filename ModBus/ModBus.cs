using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace ModBus
{
	/// <summary>
	/// Класс реализует протокол обмена ModBus посредством последовательного порта 
	/// </summary>
	public class ModBus
	{
		Timer timerOut = new Timer();
		Timer timerCheck = new Timer();
		SerialPort serialPort = new SerialPort();
		/// <summary>
		/// TimeOut в милисе6кундах
		/// </summary>
		private int timeOut = 300;
		/// <summary>
		/// Ожидание ответа от подчиненого
		/// </summary>
		private bool waitResponse = false;
		/// <summary>
		/// Ожидаемое количество байт на чтение
		/// </summary>
		private int bytesToRead;
		/// <summary>
		/// Адрес подчиненого
		/// </summary>
		private int addressSlave;
		/// <summary>
		/// Максимальное число повторов запросов при таймауте
		/// </summary>
		private int numberRepeatMax = 5;

		private int counterRepeat;

		public ModBus()
		{
			timerOut.Interval = timeOut;
			timerOut.Tick += new EventHandler(timerOut_Tick);
			timerCheck.Interval = 20;
			timerCheck.Tick += new EventHandler(timerCheck_Tick);

		}

		void timerCheck_Tick(object sender, EventArgs e)
		{
			try
			{
				if (bytesToRead <= serialPort.BytesToRead)
				{
					timerCheck.Stop();
					timerOut.Stop();
					CheckData();
				}
			}
			catch (InvalidOperationException exc)
			{
				if (CatchSerialException(exc))
					return;
			}
		}

		void timerOut_Tick(object sender, EventArgs e)
		{
			counterRepeat++;
			if (counterRepeat > numberRepeatMax)
			{
				timerCheck.Stop();
				timerOut.Stop();
				waitResponse = false;
				if (serialPort.BytesToRead != 0)
				{
					eventArgument.Status = ModBusStatus.UnknowPacket;
#if DEBUG
					foreach (byte b in serialPort.ReadExisting())
					{
						Console.Write("{0} ", b.ToString("X2"));
					}
					Console.WriteLine();
#endif
				}
				else
					eventArgument.Status = ModBusStatus.TimeOutError;
				OnExchangeEnd();
			}
			else
			{
				// повторяем запрос
				try
				{
					serialPort.ReadExisting();
					serialPort.Write(bufferTransmit, 0, bufferTransmit.Length);
				}
				catch (InvalidOperationException exc)
				{
					if (CatchSerialException(exc))
						return;
				}

#if DEBUG
				foreach (byte b in bufferTransmit)
				{
					Console.Write("{0} ", b.ToString("X2"));
				}
				Console.WriteLine();
#endif
			}
			//throw new Exception("The method or operation is not implemented.");
		}
		/// <summary>
		/// Обработка исключений порта
		/// </summary>
		/// <param name="exc"></param>
		bool CatchSerialException(Exception exc)
		{
			switch (exc.TargetSite.DeclaringType.Name)
			{
				default:
				case "SerialPort":
					timerCheck.Stop();
					timerOut.Stop();
					waitResponse = false;
					eventArgument.Status = ModBusStatus.PortIsClosed;
					this.OnExchangeEnd();
					return true;
					break;
			}			
		}
		/// <summary>
		/// проверяем полученные данные 
		/// </summary>
		private void CheckData()
		{
			byte[] buffer = null;
			try
			{
				buffer = new byte[serialPort.BytesToRead];
				serialPort.Read(buffer, 0, buffer.Length);
			}
			catch (InvalidOperationException exc)
			{
				if (CatchSerialException(exc))
					return;
			}

#if DEBUG
			foreach (byte b in buffer)
			{
				Console.Write("{0} ", b.ToString("X2"));
			}
			Console.WriteLine();
#endif
			waitResponse = false;
			if (!CRC.CheckCRC(buffer, 0, buffer.Length - 2, buffer, buffer.Length - 2))
			{
				eventArgument.Status = ModBusStatus.CRCError;
				OnExchangeEnd();
				return;
			}
			if (buffer[0] != addressSlave)
			{
				eventArgument.Status = ModBusStatus.AddressSlaveError;
				OnExchangeEnd();
				return;
			}
			// Определяем тип функции
			switch (buffer[1])
			{
				// Read Holding Registers
				case 3:
					eventArgument.data = new short[buffer[2] / 2];
					eventArgument.Status = ModBusStatus.ReadHoldingRegistersOK;

					int i = 0;
					while (i != eventArgument.data.Length)
					{
						eventArgument.data[i] = (short)(buffer[i * 2 + 3] << 8);
						eventArgument.data[i] += buffer[i * 2 + 1 + 3];
						i++;
					}
					OnExchangeEnd();
					break;

				// Preset Multiple Registers
				case 0x10:
					eventArgument.data = new short[0];
					eventArgument.Status = ModBusStatus.PresetMultipleRegistersOK;
					OnExchangeEnd();
					break;
			}
		}

		/// <summary>
		/// Отображает диалог настройки
		/// </summary>
		public DialogResult OptionsShow()
		{
			FormOptions setup = new FormOptions();
			#region Заполняю форму настройками порта

			if (setup.ShowDialog() == DialogResult.OK)
			{
				try
				{
					LoadSettingsPort();
				}
				catch
				{
					if (serialPort.IsOpen)
						MessageBox.Show("Порт уже открыт", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return DialogResult.OK;
			}
			else
				return DialogResult.Cancel;

			#endregion
		}
		void LoadSettingsPort()
		{
			serialPort.PortName = Properties.SettingsOptions.Default.comPort;
			serialPort.BaudRate = Properties.SettingsOptions.Default.baudRate;
			serialPort.DataBits = Properties.SettingsOptions.Default.dataBits;
			serialPort.Parity = Properties.SettingsOptions.Default.parity;
			serialPort.StopBits = Properties.SettingsOptions.Default.stopBits;
			// По умолчанию Handshake = Handshake.None
			// Но если его его не переприсвоить заново 
			// Последовательный порт работал не стабильно
			// Наблюдались повторы посылок по ModBus
			// Возможно это связанно с ПО Аркадия (Виртуальный COM порт не принимал правильно настройки от Хоста)
			serialPort.Handshake = Properties.SettingsOptions.Default.flowControl;
			timeOut = Properties.SettingsOptions.Default.timeOut;
			numberRepeatMax = Properties.SettingsOptions.Default.numberRepeat;

			timerOut.Interval = timeOut;

		}
		/// <summary>
		/// Буфер для хранения предыдущей отправленой посылки
		/// (в случае если посылка не прошла по таймауту то повторяем запрос с содержимым данного буфера)
		/// </summary>
		private byte[] bufferTransmit;

		/// <summary>
		/// запрос на чтение двоичного содержания регистров подчиненого
		/// </summary>
		/// <param name="address">Адрес подчиненого</param>
		/// <param name="regAddress">начальный адрес регистров</param>
		/// <param name="numberReg">количество регистров </param>
		public void ReadHoldingRegisters(byte address, short regAddress, short numberReg)
		{
			if (!waitResponse)
			{
				// очищаем буфер приемный
				//serialPort.Read(new byte[serialPort.BytesToRead], 0, serialPort.BytesToRead);
				int i = 0;
				bufferTransmit = new byte[8];
				addressSlave = address;
				bufferTransmit[i++] = address;
				bufferTransmit[i++] = 0x03;
				bufferTransmit[i++] = (byte)(regAddress >> 8);
				bufferTransmit[i++] = (byte)(regAddress);

				bufferTransmit[i++] = (byte)(numberReg >> 8);
				bufferTransmit[i++] = (byte)(numberReg);

				bufferTransmit[i++] = (byte)CRC.GetCRC(bufferTransmit, 6);
				bufferTransmit[i] = (byte)(CRC.GetCRC(bufferTransmit, 6) >> 8);

				Repeat();
				//counterRepeat = 0;
				//serialPort.Write(bufferTransmit, 0, bufferTransmit.Length);

				//waitResponse = true;
				//timerOut.Start();
				//timerCheck.Start();

				// ожидаем количество байт в ответе 
				bytesToRead = 5 + numberReg * 2;
			}
			else
				throw new BusyException();

		}



		/// <summary>
		/// Запрос на запись двоичного содержания регистров подчиненого
		/// </summary>
		/// <param name="address">дрес подчиненого</param>
		/// <param name="regAddress">начальный адрес регистров</param>
		/// <param name="data">Массив новых значений регистров</param>
		/// <param name="numberReg">количество регистров</param>
		public void PresetMultipleRegisters(byte address, short regAddress, short[] data, short numberReg)
		{
			if (!waitResponse)
			{
				int i = 0;
				bufferTransmit = new byte[numberReg * 2 + 9];
				addressSlave = address;
				bufferTransmit[i++] = address;
				bufferTransmit[i++] = 0x10;
				bufferTransmit[i++] = (byte)(regAddress >> 8);
				bufferTransmit[i++] = (byte)(regAddress);

				bufferTransmit[i++] = (byte)(numberReg >> 8);
				bufferTransmit[i++] = (byte)(numberReg);

				bufferTransmit[i++] = (byte)(numberReg * 2);


				int index = 0;
				while (index != numberReg)
				{
					bufferTransmit[i++] = (byte)(data[index] >> 8);
					bufferTransmit[i++] = (byte)(data[index++]);
				}
				//Array.Copy(data, 0, buffer, i, numberReg * 2);
				//i += numberReg * 2;

				bufferTransmit[i++] = (byte)CRC.GetCRC(bufferTransmit, bufferTransmit.Length - 2);
				bufferTransmit[i] = (byte)(CRC.GetCRC(bufferTransmit, bufferTransmit.Length - 2) >> 8);

				Repeat();
				//counterRepeat = 0;
				//serialPort.Write(bufferTransmit, 0, bufferTransmit.Length);

				//waitResponse = true;
				//timerOut.Start();
				//timerCheck.Start();

				// ожидаем количество байт в ответе 
				bytesToRead = 8;
			}
			else
				throw new BusyException();

			//serialPort.DtrEnable = true;
			//serialPort.RtsEnable = true;
			//serialPort.

		}

		/// <summary>
		/// Повтор посылки последней
		/// </summary>
		public void Repeat()
		{
			if (!waitResponse)
			{
				// очищаем буфер приемный
				try
				{
					serialPort.Read(new byte[serialPort.BytesToRead], 0, serialPort.BytesToRead);
					counterRepeat = 0;
					serialPort.Write(bufferTransmit, 0, bufferTransmit.Length);
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
				foreach (byte b in bufferTransmit)
				{
					Console.Write("{0} ", b.ToString("X2"));
				}
				Console.WriteLine();
#endif
				waitResponse = true;
				timerOut.Start();
				timerCheck.Start();
			}
		}
		/// <summary>
		/// Открывает порт
		/// </summary>
		/// <returns>false - порт не открылся</returns>
		public bool OpenPort()
		{
			bool result = true;
			try
			{
				if (!serialPort.IsOpen)
				{
					LoadSettingsPort();
					serialPort.Open();
					eventArgument.Status = ModBusStatus.PortOk;
					OnExchangeEnd();
				}
			}
			catch
			{
				eventArgument.Status = ModBusStatus.BusyPort;
				OnExchangeEnd();
				result = false;
			}
			return result;
		}

		public void ClosePort()
		{
			counterRepeat = 0;
			waitResponse = false;
			timerOut.Stop();
			timerCheck.Stop();
			if (serialPort.IsOpen)
				serialPort.Close();
		}
		public delegate void ExchangeEndHandle(object sender, ModBusEventArg e);
		public event ExchangeEndHandle ExchangeEnd;
		ModBusEventArg eventArgument = new ModBusEventArg();
		private void OnExchangeEnd()
		{
			// Если не ссылается на NULL то вызываем событие
			if (ExchangeEnd != null)
			{

				ExchangeEnd(this, eventArgument);
			}
		}

	}




	public enum ModBusStatus : int
	{
		/// <summary>
		/// Ошибка провышение времени ожидания ответа(посылки)
		/// </summary>
		TimeOutError,
		/// <summary>
		/// Получены не известные данные
		/// </summary>
		UnknowPacket,
		/// <summary>
		/// Ошибка подчиненого 
		/// </summary>
		AddressSlaveError,
		/// <summary>
		/// Ошибка контрольной суммы
		/// </summary>
		CRCError,
		/// <summary>
		/// Данные прочитаны успешно
		/// </summary>
		ReadPacketOK,
		/// <summary>
		/// Данные записаны успешно
		/// </summary>
		WritePacketOK,
		/// <summary>
		/// Занято процессом чтения данных
		/// </summary>
		BusyRead,
		/// <summary>
		/// Занято процессом записи данных
		/// </summary>
		BusyWrite,
		/// <summary>
		/// Запрос регистров успешно завершен (команда 3) 
		/// </summary>
		ReadHoldingRegistersOK,
		/// <summary>
		/// Порт занят
		/// </summary>
		BusyPort,
		/// <summary>
		/// Порт закрыт
		/// </summary>
		PortIsClosed,
		PortOk,
		// Записьрегистров успешна (команда 0x10)
		PresetMultipleRegistersOK,
	}

	/// <summary>
	/// Аргумент события ModBus
	/// </summary>
	public class ModBusEventArg : EventArgs
	{
		private ModBusStatus status = 0;
		internal short[] data;
		public short[] Data
		{
			get { return data; }
		}
		public ModBusStatus Status
		{
			get
			{
				return status;
			}
			internal set
			{
				status = value;
			}
		}
	}

	#region Исключительные ситуации
	/// <summary>
	/// Занят  ожиданием ответа от подчиненого
	/// </summary>
	[Serializable]
	public class BusyException : Exception
	{
		public BusyException()
			: base("Ожидание ответа от подчиненого") { }

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
					byte uc2 = (byte)(crc16mb_prev & 0x0001);
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
					byte uc2 = (byte)(crc16mb_prev & 0x0001);
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
		/// Проверяет CRC в посылке
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
	/// Атрибуты регистра
	/// </summary>
	public enum RegisterAtr
	{
		/// <summary>
		/// Доступ на чтение запись
		/// </summary>
		ReadWrite,
		/// <summary>
		/// Доступ на чтение
		/// </summary>
		Read,
		/// <summary>
		/// Доступ на запись
		/// </summary>
		Write
	}
	/// <summary>
	/// Описывает регистр ModBus
	/// </summary>
	[Serializable]
	public class Register
	{
		/// <summary>
		/// Адрес регистра
		/// </summary>
		private ushort _address = 0;
		/// <summary>
		/// Количество регистров
		/// </summary>
		private int _length = 0;

		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}
		/// <summary>
		/// Атрибут регистра
		/// </summary>
		private RegisterAtr _attribute = RegisterAtr.Read;

		/// <summary>
		/// Адрес регистра
		/// </summary>
		public ushort Address
		{
			get { return this._address; }
			set { this._address = value; }
		}

		/// <summary>
		/// Атрибут регистра
		/// </summary>
		public RegisterAtr Attribute
		{
			get { return this._attribute; }
			set { this._attribute = value; }
		}
	}

}
