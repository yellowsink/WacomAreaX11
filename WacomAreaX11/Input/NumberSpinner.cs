using System;

namespace WacomAreaX11.Input
{
	public static class NumberSpinner
	{
		public static decimal Spin(string message, bool cleanup, bool decimalPlaces = false, decimal min = 0, decimal max = 10, decimal start = 0)
		{
			var con   = new CountingConsole();
			
			var selection = start;
			while (true)
			{
				con.Write($"{Tools.ClearLineAndToStart}{message}    {selection}");
				var key = con.ReadKey().Key;
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
						con.WriteLine();
						if (cleanup) con.ClearAllLinesWritten();
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

		public static int Spin(string message, bool cleanup, int min = 0, int max = 10, int start = 0)
			=> (int) Spin(message, cleanup, false, min, max, start);
	}
}