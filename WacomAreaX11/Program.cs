using System;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void Main()
		{
			// We require xsetwacom to work, so throw an error if not available
			if (!TabletDriver.IsDriverAccessible())
			{
				Console.WriteLine(@"This tool requires xsetwacom to work. Some things to check:
	- Are you running macOS or Windows? This tool is for linux.
	- Are you using Wayland? This tool is for the xf86 driver on X11.
	- Are you using OpenTabletDriver? See above.
	- Is xsetwacom in $PATH? If it isnt this tool won't work.");
				Environment.Exit(0);
			}

			var tablet = TabletDriver.GetTablet();
			MainMenu(tablet);
			
			Console.WriteLine($"-- WacomAreaX11 by Cain Atkinson --\n\n{TabletInfo(tablet)}");
		}
	}
}