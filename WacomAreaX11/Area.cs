using System;
using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void EnterArea(Tablet tablet)
		{
			var aspectRatio = ListPicker.Pick("Do you want to snap to a ratio?",
											  true,
											  new[]
											  {
												  AspectRatio.Free, AspectRatio.Square, AspectRatio.WidescreenWidth,
												  AspectRatio.WideScreenHeight
											  },
											  new[] { "No", "Square", "16:9 - set width", "16:9 - set height" });
			decimal newWidth, newHeight;
			switch (aspectRatio)
			{
				case AspectRatio.Free:
					newWidth  = Tools.NumPrompt("Please enter the desired new tablet area width", true);
					newHeight = Tools.NumPrompt("Please enter the desired new tablet area height", true);
					break;
				case AspectRatio.Square:
					newHeight = newWidth = Tools.NumPrompt("Please enter the desired new tablet area width", true);
					break;
				case AspectRatio.WidescreenWidth:
					newWidth  = Tools.NumPrompt("Please enter the desired new tablet area width", true);
					newHeight = newWidth * 9 / 16;
					break;
				case AspectRatio.WideScreenHeight:
					newHeight = Tools.NumPrompt("Please enter the desired new tablet area height", true);
					newWidth  = newHeight * 16 / 9;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var centerX = Tools.YesNo("Do you want to center your area horizontally?", true, true);
			var centerY = Tools.YesNo("Do you want to center your area vertically?",   true, true);

			var (centeredX, centeredY) = GetCenteredOffset(new TabletArea(0, 0, newWidth, newHeight, tablet.FullArea, tablet.BoundArea.Rotation, true), tablet.FullArea);

			var newXOffset = centerX
								 ? centeredX
								 : Tools.NumPrompt("Please enter the desired new tablet area left offset", true);
			var newYOffset = centerY
								 ? centeredY
								 : Tools.NumPrompt("Please enter the desired new tablet area top offset", true);

			var newArea = new TabletArea(0, 0, 0, 0, tablet.FullArea, tablet.BoundArea.Rotation, true)
			{ // sorry about this madness but the constructors are funky
				Left   = newXOffset,
				Top    = newYOffset,
				Right  = newXOffset + newWidth,
				Bottom = newYOffset + newHeight
			};

			tablet.Area = newArea;
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