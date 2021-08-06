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
			var filesInConfigDir = new DirectoryInfo(ConfigSavePath).GetFiles();
			
			var configs = (from file in filesInConfigDir
						   where file.Extension == ".sh"
						   select (file.Name[Range.EndAt(file.Name.Length - 3)], file.FullName))
			   .ToArray();

			var choicePath = ListPicker.Menu("Pick a config to load",
											 configs.Select(c => c.FullName).ToArray(),
											 configs.Select(c => c.Item1).ToArray());

			Process.Start("sh", choicePath)!.WaitForExit();
		}
	}
}