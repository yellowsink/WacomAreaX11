using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace XSetWacom
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

		public void Apply() => Process.Start("sh", $"\"{Path}\"")!.WaitForExit();

		public static void Save(string name, Tablet tablet)
		{
			var area      = tablet.Area.Unscaled;
			var rotation  = tablet.BoundArea.Rotation;
			var smoothing = tablet.BoundArea.Smoothing;

			var tabletName = tablet.Name;
			
			var configFileText = $@"#!/bin/sh
# This file was generated by the WacomAreaX11
# https://github.com/yellowsink/WacomAreaX11
xsetwacom set ""{tabletName}"" Area {area.left} {area.top} {area.right} {area.bottom}
xsetwacom set ""{tabletName}"" Rotate {rotation}
xsetwacom set ""{tabletName}"" RawSample {smoothing}";
			
			var filePath = System.IO.Path.Combine(ConfigSavePath, name + ".sh");

			Directory.CreateDirectory(ConfigSavePath);
			File.WriteAllText(filePath, configFileText);
			Process.Start("chmod", new[] { "+x", filePath }).WaitForExit();
		}
		
		public override string ToString() => Name;

		public static Config[] GetAll()
			=> new DirectoryInfo(ConfigSavePath).GetFiles()
												.Where(f => f.Extension == ".sh")
												.Select(f => new Config(f.FullName,
																		f.Name[Range.EndAt(f.Name.Length - 3)]))
												.OrderBy(f => f.Name)
												.ToArray();
	}
}