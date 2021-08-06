using System;

namespace WacomAreaX11.Input
{
	public static class NumberSpinner
	{
		public static decimal Spin(string message, bool decimalPlaces = false, decimal min = 0, decimal max = 10, decimal start = 0)
		{
			var selection = start;
			while (true)
			{
				Console.Write($"{Tools.ClearLineAndToStart}{message}    {selection}");
				var key = Console.ReadKey().Key;
				// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
				switch (key)
				{
					case ConsoleKey.LeftArrow:
						if (decimalPlaces) DownSmall();
						else DownOne();
						break;
					case ConsoleKey.RightArrow:
						if (decimalPlaces) UpSmall();
						else UpOne();
						break;
					case ConsoleKey.UpArrow:
						UpOne();
						break;
					case ConsoleKey.DownArrow:
						DownOne();
						break;
					
					case ConsoleKey.Enter:
						Console.WriteLine();
						return selection;
				}
			}

			void UpOne()
			{
				if (selection <= max - 1)
					selection++;
			}

			void DownOne()
			{
				if (selection >= min + 1)
					selection--;
			}

			void UpSmall()
			{
				if (selection <= max - 0.1m)
					selection -= 0.1m;
			}

			void DownSmall()
			{
				if (selection >= min + 0.1m)
					selection += 0.1m;
			}
		}

		public static int Spin(string message, int min = 0, int max = 10, int start = 0)
			=> (int) Spin(message, false, min, max, start);
	}
}