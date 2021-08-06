using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WacomAreaX11.Input;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void Load()
		{
			var configs = new DirectoryInfo(ConfigSavePath).GetFiles()
														   .Where(f => f.Extension == ".sh")
														   .Select(f => (f.Name[Range.EndAt(f.Name.Length - 3)],
																		 f.FullName))
														   .ToArray();

			var (choice, choicePath) = ListPicker.Menu("Pick a config to load",
				configs,
				configs.Select(c => c.Item1).ToArray());

			Process.Start("sh", choicePath)!.WaitForExit();
			
			var con = CountingConsole.WriteLineNew($@"Loaded config {choice} successfully.
Press a key to go back to the main menu.");
			con.ReadKey();
			con.ClearAllLinesWritten();
		}
	}
}