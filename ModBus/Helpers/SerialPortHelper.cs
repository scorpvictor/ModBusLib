using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Butek.ModBus.Helpers
{


	public static class SerialPortHelper
	{
		[Flags]
		public enum Baud : int
		{
			[Description("75")]
			BAUD_075 = 0x00000001,
			[Description("110")]
			BAUD_110 = 0x00000002,
			/// <summary>
			/// 134.5 bps
			/// </summary>
			[Description("134.5")]
			BAUD_134_5 = 0x00000004,
			[Description("150")]
			BAUD_150 = 0x00000008,
			[Description("300")]
			BAUD_300 = 0x00000010,
			[Description("600")]
			BAUD_600 = 0x00000020,
			[Description("1200")]
			BAUD_1200=0x00000040,
			[Description("1800")]
			BAUD_1800=0x00000080,
			[Description("2400")]
			BAUD_2400=0x00000100,
			[Description("4800")]
			BAUD_4800=0x00000200,
			[Description("7200")]
			BAUD_7200=0x00000400,
			[Description("9600")]
			BAUD_9600=0x00000800,
			[Description("14400")]
			BAUD_14400=0x00001000,
			[Description("19200")]
			BAUD_19200=0x00002000,
			[Description("38400")]
			BAUD_38400=0x00004000,
			[Description("56000")]
			BAUD_56K=0x00008000,
			[Description("57600")]
			BAUD_57600=0x00040000,
			[Description("115200")]
			BAUD_115200=0x00020000,
			[Description("128000")]
			BAUD_128K=0x00010000,
			/// <summary>
			/// Programmable baud rate.
			/// </summary>
			[Description("Programmable baud rate")]
			BAUD_USER=0x10000000,
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Commprop
		{
			public short wPacketLength;
			public short wPacketVersion;
			public int dwServiceMask;
			public int dwReserved1;
			public int dwMaxTxQueue;
			public int dwMaxRxQueue;
			public int dwMaxBaud;
			public int dwProvSubType;
			public int dwProvCapabilities;
			public int dwSettableParams;
			public int dwSettableBaud;
			public short wSettableData;
			public short wSettableStopParity;
			public int dwCurrentTxQueue;
			public int dwCurrentRxQueue;
			public int dwProvSpec1;
			public int dwProvSpec2;
			public string wcProvChar;
		}

		[DllImport("kernel32.dll")]
		static extern bool GetCommProperties(IntPtr hFile, ref Commprop lpCommProp);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess,
				   int dwShareMode, IntPtr securityAttrs, int dwCreationDisposition,
				   int dwFlagsAndAttributes, IntPtr hTemplateFile);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr CloseHandle(IntPtr hadle);


		public static Commprop GetComPortProperties(string portName)
		{
			var commProp = new Commprop();
			IntPtr hFile = CreateFile(@"\\.\" + portName, 0, 0, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
			GetCommProperties(hFile, ref commProp);
			CloseHandle(hFile);
			return commProp;
		}
	}
}
