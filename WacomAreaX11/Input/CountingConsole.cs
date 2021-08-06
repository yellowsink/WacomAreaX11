using System;
using System.Diagnostics;

namespace WacomAreaX11.Input
{
	[DebuggerDisplay("{Count} Lines")]
	public class CountingConsole
	{
		public int Count;

		public void Write(string item)
		{
			Count += item.Split('\n').Length - 1;
			Console.Write(item);
		}
		
		public void WriteLine(string item = "")
		{
			Count += item.Split('\n').Length;
			Console.WriteLine(item);
		}

		public ConsoleKeyInfo ReadKey() => Console.ReadKey();

		public int Read() => Console.Read();

		public string ReadLine()
		{
			Count++;
			return Console.ReadLine();
		}

		public void ClearLine()
		{
			Console.Write(Tools.ClearLineAndToStart);
		}
		
		public void ClearLineAndUp()
		{
			Count--;
			ClearLine();
			Console.CursorTop--;
		}

		public void ClearAllLinesWritten()
		{
			while (Count > 0) ClearLineAndUp();
			ClearLine();
		}

		public static CountingConsole WriteNew(string item)
		{
			var con = new CountingConsole();
			con.Write(item);
			return con;
		}
		
		public static CountingConsole WriteLineNew(string item)
		{
			var con = new CountingConsole();
			con.WriteLine(item);
			return con;
		}
	}
}