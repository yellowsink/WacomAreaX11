using System;
using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void MainMenu(Tablet tablet)
		{
			var lastChoice = MenuChoices.Area;

			while (true)
			{
				var menuText = @$"Wacom area set helper for Linux (xf86 driver)
Because Wacom coordinates are confusing!
By Cain Atkinson

{TabletInfo(tablet)}
";

				var choice = lastChoice = ListPicker.Menu(menuText,
														  new[]
														  {
															  MenuChoices.Area, MenuChoices.Rotation,
															  MenuChoices.Smoothing, MenuChoices.Save, MenuChoices.Load,
															  MenuChoices.Quit
														  },
														  new[]
														  {
															  "Set area", "Change rotation", "Set smoothing",
															  "Save config", "Load config", "Exit app"
														  },
														  lastChoice);

				switch (choice)
				{
					case MenuChoices.Area:
						EnterArea(tablet);
						break;
					case MenuChoices.Rotation:
						EnterRotation(tablet);
						break;
					case MenuChoices.Smoothing:
						EnterSmoothing(tablet);
						break;
					case MenuChoices.Save:
						Save(tablet);
						break;
					case MenuChoices.Load:
						Load();
						break;
					case MenuChoices.Quit:
						return;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		
		private static string TabletInfo(Tablet tablet)
		{
			var fullArea  = tablet.FullArea;
			var area      = tablet.Area;
			var smoothing = tablet.BoundArea.Smoothing;
			fullArea.ScaleToCentimetres();
			area.ScaleToCentimetres();
			
			var rFullArea = fullArea.ToTabletArea(area.Rotation);
			rFullArea.ScaleToCentimetres();
			
			var fWidth  = Math.Round(rFullArea.Width,  2).NiceFormat();
			var fHeight = Math.Round(rFullArea.Height, 2).NiceFormat();
			var cWidth  = Math.Round(area.Width,       2).NiceFormat();
			var cHeight = Math.Round(area.Height,      2).NiceFormat();
			var xOffset = Math.Round(area.Left,        2).NiceFormat();
			var yOffset = Math.Round(area.Top,         2).NiceFormat();
			return $@"Your tablet is         {fWidth}cm wide and and {fHeight}cm high
Your current area is   {cWidth}cm wide and     {cHeight}cm high
Your current offset is {xOffset}cm (from left)  {yOffset}cm (from top)
Your current tablet rotation is: {area.Rotation}
Your current input smoothing setting is: {smoothing} samples";
		}
	}

	internal enum MenuChoices
	{
		Area,
		Rotation,
		Smoothing,
		Save,
		Load,
		Quit
	}
}