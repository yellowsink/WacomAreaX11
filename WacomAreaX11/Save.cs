using System;
using System.IO;
using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static readonly string ConfigSavePath
			= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "tabletconfigs");
		
		private static void Save(Tablet tablet)
		{
			var saveFileName = Tools.Prompt("What should the save be called?", true);

			Config.Save(saveFileName, tablet);
			
			var con = CountingConsole.WriteLineNew($@"Saved to ~/tabletconfigs/{saveFileName}.sh
To apply the config either use this tool or run the file directly from a terminal.
Press a key to go back to the main menu.");
			con.ReadKey();
			con.ClearAllLinesWritten();
		}
	}
}