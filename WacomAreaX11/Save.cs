using System;
using System.Diagnostics;
using System.IO;
using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void Save(Tablet tablet)
		{
			var saveFileName = Tools.Prompt("What should the save be called?", true);

			var con = CountingConsole.WriteLineNew($@"Saving to ~/tabletconfigs/{saveFileName}.sh
To apply the config either use this tool or run the file directly from a terminal.");

			var area      = tablet.Area.Unscaled;
			var rotation  = tablet.BoundArea.Rotation;
			var smoothing = tablet.BoundArea.Smoothing;

			var tabletName = tablet.Name;
			
			var configFileText = $@"#!/bin/sh
xsetwacom set ""{tabletName}"" Area {area.left} {area.top} {area.right} {area.bottom}
xsetwacom set ""{tabletName}"" Rotate {rotation}
xsetwacom set ""{tabletName}"" RawSample {smoothing}
# This file was generated by WacomAreaX11
# https://github.com/yellowsink/WacomAreaX11";

			var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
										"tabletconfigs",
										saveFileName + ".sh");

			File.WriteAllText(filePath, configFileText);
			var proc = Process.Start("chmod", new[] { "+x", filePath });
			proc.WaitForExit();
		}
	}
}