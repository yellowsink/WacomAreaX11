using System;

namespace WacomAreaX11
{
	public static class InputHelper
	{
		public const string ClearLine           = "\u001b[2K";
		public const string ClearLineAndToStart = "\u001b[2K\u001b[G";
		
		public static decimal EnterNumber(string message)
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
			=> PickFromList(message, new[] { "No", "Yes" }, defaultSelection ? 1 : 0) == "Yes";

		public static T PickFromList<T>(string message, T[] options, int defaultSelection = 0)
		{
			var selected = defaultSelection;

			while (true)
			{
				Console.Write($"{ClearLineAndToStart}{message}    ");
				for (var i = 0; i < options.Length; i++)
				{
					// if selected draw some "arrows" around it
					Console.Write(i == selected
									  ? $">>> {options[i]} <<<"
									  : options[i]);

					// If not last item in options write a separator
					if (i != options.Length - 1) Console.Write(" | ");
				}

				var key = Console.ReadKey();
				// ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
				switch (key.Key)
				{
					case ConsoleKey.LeftArrow:
						if (selected != 0)
							selected--;
						continue;
					
					case ConsoleKey.RightArrow:
						if (selected < options.Length - 1) // not last item
							selected++;
						continue;
					
					case ConsoleKey.Enter:
						Console.WriteLine();
						return options[selected];
					
					default: continue;
				}
			}
		}

		public static T PickFromList<T>(string message, T[] options, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return PickFromList(message, options, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}
	}
}