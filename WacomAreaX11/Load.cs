using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void Load()
			=> ListPicker.Menu("Pick a config to load", Config.GetAll()).Apply();
	}
}