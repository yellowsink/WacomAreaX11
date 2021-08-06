using System;
using System.Linq;

namespace WacomAreaX11.Input
{
	public static class ListPicker
	{
		public static T Pick<T>(string message, T[] options, string[] display, int defaultSelection = 0)
		{
			var selected = defaultSelection;

			while (true)
			{
				Console.Write($"{Tools.ClearLineAndToStart}{message}    ");
				for (var i = 0; i < options.Length; i++)
				{
					// if selected draw some "arrows" around it
					Console.Write(i == selected
									  ? $">>> {display[i]} <<<"
									  : display[i]);

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
		
		public static T Pick<T>(string message, T[] options, int defaultSelection = 0)
			=> Pick(message, options, options.Select(item => item.ToString()).ToArray(), defaultSelection);

		public static T Pick<T>(string message, T[] options, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return Pick(message, options, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}
	}
}