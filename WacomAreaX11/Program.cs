using System;
using XSetWacom;

namespace WacomAreaX11
{
	internal static class Program
	{
		private static void Main()
		{
			Console.WriteLine(@"Wacom area set helper for Linux (xf86 driver)
Because Wacom coordinates are confusing!
By Cain Atkinson
");

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

			
			var (fullArea, area) = PrepareAreas();
			PrintTabletInfo(area, fullArea);
			
			var (newRotation, newWidth, newHeight, newXOffset, newYOffset) = AskForNewArea(area, fullArea);

			area.Rotation = newRotation; // set this first!!!
			area.Left     = newXOffset;
			area.Top      = newYOffset;
			area.Right    = newXOffset + newWidth;
			area.Bottom   = newYOffset + newHeight;
			
			Console.WriteLine("Your area has been set!!!");
		}

		private static (Rotation newRotation, decimal newWidth, decimal newHeight, decimal newXOffset, decimal newYOffset)
			AskForNewArea(TabletArea area, FullArea fullArea)
		{
			var newRotation = InputHelper.PickFromList("Do you want to change your tablet's rotation?",
													   new[] { Rotation.None, Rotation.Cw, Rotation.Half, Rotation.Ccw },
													   area.Rotation);

			var aspectRatio = InputHelper.PickFromList("Do you want to snap to a ratio?",
													   new[] { AspectRatio.Free, AspectRatio.Square, AspectRatio.WidescreenWidth, AspectRatio.WideScreenHeight },
													   new[] { "No", "Square", "16:9 - set width", "16:9 - set height" });
			decimal newWidth, newHeight;
			switch (aspectRatio)
			{
				case AspectRatio.Free:
					newWidth  = InputHelper.EnterNumber("Please enter the desired new tablet area width");
					newHeight = InputHelper.EnterNumber("Please enter the desired new tablet area height");
					break;
				case AspectRatio.Square:
					newHeight = newWidth = InputHelper.EnterNumber("Please enter the desired new tablet area width");
					break;
				case AspectRatio.WidescreenWidth:
					newWidth  = InputHelper.EnterNumber("Please enter the desired new tablet area width");
					newHeight = newWidth * 9 / 16;
					break;
				case AspectRatio.WideScreenHeight:
					newHeight  = InputHelper.EnterNumber("Please enter the desired new tablet area height");
					newWidth = newHeight * 16 / 9;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var centerX = InputHelper.YesNo("Do you want to center your area horizontally?", true);
			var centerY = InputHelper.YesNo("Do you want to center your area vertically?",   true);

			var (centeredX, centeredY) = GetCenteredOffset(new TabletArea(0, 0, newWidth, newHeight, fullArea, newRotation, true), fullArea);

			var newXOffset = centerX
								 ? centeredX
								 : InputHelper.EnterNumber("Please enter the desired new tablet area left offset");
			var newYOffset = centerY
								 ? centeredY
								 : InputHelper.EnterNumber("Please enter the desired new tablet area top offset");
			return (newRotation, newWidth, newHeight, newXOffset, newYOffset);
		}

		private static (FullArea fullArea, BoundTabletArea area) PrepareAreas()
		{ // do this first in case it fails
			var tablet = TabletDriver.GetTablet();

			// get area and scale the coordinates to centimetres
			var fullArea = tablet.FullArea;
			fullArea.ScaleToCentimetres();
			var area = tablet.BoundArea; // this is inefficient - it causes a xsetwacom call for EVERY get and set
			area.ScaleToCentimetres();
			return (fullArea, area);
		}

		private static void PrintTabletInfo(TabletArea area, FullArea fullArea)
		{
			var rFullArea = fullArea.ToTabletArea(area.Rotation);
			rFullArea.ScaleToCentimetres();
			
			var fWidth  = Math.Round(rFullArea.Width,  2).NiceFormat();
			var fHeight = Math.Round(rFullArea.Height, 2).NiceFormat();
			var cWidth  = Math.Round(area.Width,       2).NiceFormat();
			var cHeight = Math.Round(area.Height,      2).NiceFormat();
			var xOffset = Math.Round(area.Left,        2).NiceFormat();
			var yOffset = Math.Round(area.Top,         2).NiceFormat();
			Console.WriteLine($"Your tablet is         {fWidth}cm wide and and {fHeight}cm high");
			Console.WriteLine($"Your current area is   {cWidth}cm wide and     {cHeight}cm high");
			Console.WriteLine($"Your current offset is {xOffset}cm (from left)  {yOffset}cm (from top)");
			Console.WriteLine($"Your current tablet rotation is: {area.Rotation}\n");
		}

		private static (decimal x, decimal y) GetCenteredOffset(TabletArea area, FullArea fullArea)
		{
			var fullAreaRotated = fullArea.ToTabletArea(area.Rotation);
			fullAreaRotated.ScaleToCentimetres();

			return (fullAreaRotated.Width / 2 - area.Width / 2, fullAreaRotated.Height / 2 - area.Height / 2);
		}
	}

	internal enum AspectRatio
	{
		Free, Square, WidescreenWidth, WideScreenHeight
	}
}