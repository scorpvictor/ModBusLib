using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Butek.ModBus
{
	public class CustomSettings
	{
		public StopBits StopBits { get; set; }
		public Parity Parity { get; set; }
		public int BaudRate { get; set; }
		public int DataBits { get; set; }
		public int TimeOut { get; set; }
		public int NumberRepeat { get; set; }
		public string ComPort { get; set; }
		public Handshake FlowControl { get; set; }
		public bool EndingSymbolEnable { get; set; }
		public byte EndingSymbol { get; set; }
		public bool EndingSymbolEnableTx { get; set; }
		public byte EndingSymbolTx { get; set; }
	}
}
