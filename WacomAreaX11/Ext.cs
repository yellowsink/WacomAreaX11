using System.Globalization;

namespace WacomAreaX11
{
	public static class Ext
	{
		public static string NiceFormat(this decimal n) => n.ToString(CultureInfo.CurrentCulture).PadLeft(5);
	}
}