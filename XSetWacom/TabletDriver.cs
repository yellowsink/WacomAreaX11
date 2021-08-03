using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace XSetWacom
{
	public static class TabletDriver
	{
		public static bool IsDriverAccessible()
		{
			try { 
				Process.Start(new ProcessStartInfo("xsetwacom", "--version")
				{
					RedirectStandardOutput = true
				}); 
			}
			catch (Exception) { return false; } // if anything about this fails we wont be able to use xsetwacom

			return true;
		}

		public static TabletArea GetArea(int tabletId)
		{
			var process = Process.Start(new ProcessStartInfo("xsetwacom", $"get {tabletId} Area")
			{
				RedirectStandardOutput = true
			});
			process!.WaitForExit();

			var area = process.StandardOutput.ReadToEnd().Split();

			return new TabletArea((int.Parse(area[0]), int.Parse(area[1]), int.Parse(area[2]), int.Parse(area[3])));
		}

		public static TabletArea GetFullArea(int tabletId)
		{
			var currentArea = GetArea(tabletId);
			ResetArea(tabletId);
			var fullArea = GetArea(tabletId);
			SetArea(tabletId, currentArea);
			return fullArea;
		}
		
		public static void SetArea(int tabletId, TabletArea area)
			=> Process.Start(new ProcessStartInfo("xsetwacom",
												  $"set {tabletId} Area {area.Unscaled.left} {area.Unscaled.top} {area.Unscaled.right} {area.Unscaled.bottom}")
			{
				RedirectStandardOutput = true
			}) !.WaitForExit();

		public static void ResetArea(int tabletId)
			=> Process.Start(new ProcessStartInfo("xsetwacom", $"set {tabletId} ResetArea")
			{
				RedirectStandardOutput = true
			}) !.WaitForExit();

		public static int GetTabletId()
		{
			var process = Process.Start(new ProcessStartInfo("xsetwacom", "list")
			{
				RedirectStandardOutput = true
			});
			process!.WaitForExit();
			var tablets = process.StandardOutput.ReadToEnd().Split('\n');

			var tablet = tablets.FirstOrDefault(s => s.Contains("type: STYLUS"));
			if (tablet == null)
			{
				Console.WriteLine("Could not find a tablet. Is one connected?");
				Environment.Exit(0);
			}

			var id = Regex.Replace(tablet, ".*id: (\\d+).*", "$1");

			return int.Parse(id);
		}

		public static Tablet GetTablet() => (Tablet) GetTabletId();
	}
}