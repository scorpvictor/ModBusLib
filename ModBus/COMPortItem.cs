using System;
using System.Collections.Generic;
using System.Text;

namespace ModBus
{
	internal class COMPortItem
	{
		public string Name;
		public string FullName;
		public override string ToString()
		{
			return FullName;
		}
	}
}
