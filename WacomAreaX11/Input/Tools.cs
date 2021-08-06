using System;

namespace WacomAreaX11.Input
{
	public static class Tools
	{
		public const string ClearLine           = "\u001b[2K";
		public const string ClearLineAndToStart = "\u001b[2K\u001b[G";
		
		public static decimal NumPrompt(string message)
		{
			while (true)
			{
				Console.Write(message + "\n>> ");

				if (decimal.TryParse(Console.ReadLine(), out var num)) return num;

				Console.CursorTop -= 1;
				Console.Write("\u001b[2K");
				Console.CursorTop -= message.Split('\n').Length;
				Console.Write("Not a number - ");
			}
		}

		public static bool YesNo(string message, bool defaultSelection)
			=> ListPicker.Pick(message, new[] { "No", "Yes" }, defaultSelection ? 1 : 0) == "Yes";
	}
}