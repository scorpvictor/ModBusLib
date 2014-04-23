using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Management;

namespace Butek.ModBus
{
	public static class WMITools
	{
		public static string GetFullNameComPort(string comPort)
		{
			var mo = new ManagementObjectSearcher(@"root\cimv2", "SELECT Name FROM Win32_PnPEntity WHERE ClassGuid = '{4d36e978-e325-11ce-bfc1-08002be10318}'");
			ManagementObjectCollection moc = mo.Get();
			Regex reg = new Regex("COM(?<number>[0-9]+)");
			foreach (var o in moc)
			{
				MatchCollection mc = reg.Matches(o["Name"].ToString());
				if (mc.Count != 0)
					if (mc[0].Value == comPort)
						return o.GetPropertyValue("Name").ToString();
			}
			return null;
		}
	}
}
