namespace WacomAreaX11.Input
{
	public static class Tools
	{
		public const string ClearLine           = "\u001b[2K";
		public const string ClearLineAndToStart = "\u001b[2K\u001b[G";
		
		public static decimal NumPrompt(string message, bool cleanup)
		{
			while (true)
				if (decimal.TryParse(Prompt(message, cleanup), out var num))
					return num;
		}

		public static string Prompt(string message, bool cleanup)
		{
			var con     = CountingConsole.WriteNew(message + "\n>> ");
			var entered = con.ReadLine();
			if (cleanup) con.ClearAllLinesWritten();
			return entered;
		}

		public static bool YesNo(string message, bool cleanup, bool defaultSelection)
			=> ListPicker.Pick(message, cleanup, new[] { "No", "Yes" }, defaultSelection ? 1 : 0) == "Yes";
	}
}