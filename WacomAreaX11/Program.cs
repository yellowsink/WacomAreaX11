using System;
using System.Globalization;
using XSetWacom;

namespace WacomAreaX11
{
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("Wacom area set helper for Linux (xf86 driver)\n"
							+ "Because Wacom coordinates are confusing!\n" + "By Cain Atkinson\n");

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

			// do this first in case it fails
			var tablet = TabletDriver.GetTablet();

			// get area and scale the coordinates to centimetres
			var fullArea = tablet.FullArea;
			fullArea.ScaleToCentimetres();
			var originalArea = tablet.Area;
			originalArea.ScaleToCentimetres();

			var fWidth  = Math.Round(fullArea.Width,      2).NiceFormat();
			var fHeight = Math.Round(fullArea.Height,     2).NiceFormat();
			var cWidth  = Math.Round(originalArea.Width,  2).NiceFormat();
			var cHeight = Math.Round(originalArea.Height, 2).NiceFormat();
			var xOffset = Math.Round(originalArea.Left,   2).NiceFormat();
			var yOffset = Math.Round(originalArea.Top,    2).NiceFormat();
			Console.WriteLine($"Your tablet is         {fWidth}cm wide and and {fHeight}cm high");
			Console.WriteLine($"Your current area is   {cWidth}cm wide and     {cHeight}cm high");
			Console.WriteLine($"Your current offset is {xOffset}cm (from left)  {yOffset}cm (from top)");
			Console.WriteLine($"Your current tablet rotation is: {tablet.Rotation}\n");

			var newWidth   = EnterNumber("Please enter the desired new tablet area width");
			var newHeight  = EnterNumber("Please enter the desired new tablet area height");
			var newXOffset = EnterNumber("Please enter the desired new tablet area left offset");
			var newYOffset = EnterNumber("Please enter the desired new tablet area top offset");

			var newArea = new TabletArea((int) newXOffset,
										 (int) newYOffset,
										 (int) (newXOffset + newWidth),
										 (int) (newYOffset + newHeight),
										 originalArea.Rotation,
										 true);

			tablet.Area = newArea;

			Console.WriteLine("Your area has been set!!!");
		}

		private static decimal EnterNumber(string message)
		{
			while (true)
			{
				Console.Write(message + "\n>> ");

				if (decimal.TryParse(Console.ReadLine(), out var num)) return num;

				Console.CursorTop -= 1;
				Console.Write("\u001b[2K");
				Console.CursorTop -= message.Split('\n').Length;
				Console.Write("Not a number - ");
			}
		}
	}

	public static class Ext
	{
		public static string NiceFormat(this decimal n) => n.ToString(CultureInfo.CurrentCulture).PadLeft(5);
	}
}