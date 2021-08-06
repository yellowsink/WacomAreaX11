using System;
using System.Linq;

namespace WacomAreaX11.Input
{
	public static class ListPicker
	{
		public static T Pick<T>(string message, bool cleanup, T[] options, string[] display, int defaultSelection = 0)
		{
			var selected = defaultSelection;
			var con  = new CountingConsole();

			while (true)
			{
				con.ClearLine();
				con.Write($"{Tools.ClearLineAndToStart}{message}    ");
				for (var i = 0; i < options.Length; i++)
				{
					// if selected draw some "arrows" around it
					con.Write(i == selected
									  ? $">>> {display[i]} <<<"
									  : display[i]);

					// If not last item in options write a separator
					if (i != options.Length - 1) con.Write(" | ");
				}

				var key = con.ReadKey();
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
						con.WriteLine();
						if (cleanup) con.ClearAllLinesWritten();
						return options[selected];
					
					default: continue;
				}
			}
		}
		
		public static T Pick<T>(string message, bool cleanup, T[] options, int defaultSelection = 0)
			=> Pick(message, cleanup, options, options.Select(item => item.ToString()).ToArray(), defaultSelection);

		public static T Pick<T>(string message, bool cleanup, T[] options, string[] display, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return Pick(message, cleanup, options, display, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}
		
		public static T Pick<T>(string message, bool cleanup, T[] options, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return Pick(message, cleanup, options, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}


		public static T Menu<T>(string message, T[] options, string[] display, int defaultSelection = 0)
		{
			var selected = defaultSelection;

			while (true)
			{
				Console.WriteLine(message);
				for (var i = 0; i < options.Length; i++)
				{
					// if selected draw some "arrows" before it
					Console.Write(i == selected
									  ? $">>> {display[i]}"
									  : $"    {display[i]}");

					// If not last item in options write a separator
					if (i != options.Length - 1) Console.Write("\n");
				}

				var key = Console.ReadKey();
				// ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
				// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
				switch (key.Key)
				{
					case ConsoleKey.UpArrow:
						if (selected != 0)
							selected--;
						break;
					
					case ConsoleKey.DownArrow:
						if (selected < options.Length - 1) // not last item
							selected++;
						break;
					
					case ConsoleKey.Enter:
						ClearMenu();
						return options[selected];
				}
				
				ClearMenu();
			}

			void ClearMenu()
			{
				for (var i = 0; i < options.Length + message.Split('\n').Length - 1; i++)
				{
					Console.Write(Tools.ClearLineAndToStart);
					Console.CursorTop--;
				}
				Console.Write(Tools.ClearLineAndToStart);
			}
		}

		public static T Menu<T>(string message, T[] options, int defaultSelection = 0)
			=> Menu(message, options, options.Select(item => item.ToString()).ToArray(), defaultSelection);
		
		public static T Menu<T>(string message, T[] options, string[] display, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return Menu(message, options, display, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}
		
		public static T Menu<T>(string message, T[] options, T defaultSelection)
		{
			for (var i = 0; i < options.Length; i++)
				if (Equals(defaultSelection, options[i]))
					return Menu(message, options, i);

			throw new ArgumentException($"{nameof(defaultSelection)} was not in {nameof(options)}", nameof(defaultSelection));
		}
	}
}