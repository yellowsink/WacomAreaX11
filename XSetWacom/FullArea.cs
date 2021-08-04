namespace XSetWacom
{
	/// <summary>
	/// Simplified version of TabletArea to store a full area
	/// </summary>
	public class FullArea
	{
		private int _rawBottom;
		private int _rawLeft;
		private int _rawRight;
		private int _rawTop;

		public FullArea((int, int, int, int) area) : this(area.Item1, area.Item2, area.Item3, area.Item4)
		{
		}
		
		public FullArea(int bottom, int left, int right, int top)
		{
			_rawBottom = bottom;
			_rawLeft   = left;
			_rawRight  = right;
			_rawTop    = top;
		}

		public decimal ScaleFactor = 1;
		public void    ScaleToCentimetres() => ScaleFactor = 0.001m;

		public decimal Bottom => _rawBottom * ScaleFactor;
		public decimal Left => _rawLeft * ScaleFactor;
		public decimal Right => _rawRight * ScaleFactor;
		public decimal Top => _rawTop * ScaleFactor;

		public decimal Width  => Right  - Left;
		public decimal Height => Bottom - Top;
	}
}