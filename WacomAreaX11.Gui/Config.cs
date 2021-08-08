using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WacomAreaX11.Gui
{
	[DebuggerDisplay("{Path}")]
	public class Config
	{
		private static readonly string ConfigSavePath
			= System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "tabletconfigs");
		
		public Config(string path, string name)
		{
			Path = path;
			Name = name;
		}
		
		public string Name;
		public string Path;

		public void Apply() => Process.Start("sh", $"\"{Path}\"").WaitForExit();
		
		public override string ToString() => Name;

		public static Config[] GetAll()
			=> new DirectoryInfo(ConfigSavePath).GetFiles()
												.Where(f => f.Extension == ".sh")
												.Select(f => new Config(f.FullName,
																		f.Name[Range.EndAt(f.Name.Length - 3)]))
												.ToArray();
	}
}