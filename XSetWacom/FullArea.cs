namespace XSetWacom
{
	/// <summary>
	/// Simplified version of TabletArea to store a full area
	/// </summary>
	public class FullArea
	{
		private readonly int _rawBottom;
		private readonly int _rawLeft;
		private readonly int _rawRight;
		private readonly int _rawTop;

		public FullArea((int, int, int, int) area) : this(area.Item1, area.Item2, area.Item3, area.Item4)
		{
		}
		
		public FullArea(int left, int top, int right, int bottom)
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

		internal int RawWidth  => _rawRight  - _rawLeft;
		internal int RawHeight => _rawBottom - _rawTop;

		public (int, int, int, int) Unscaled => (_rawLeft, _rawTop, _rawRight, _rawBottom);
		
		public TabletArea ToTabletArea(Rotation rotation) => new(Unscaled, this, rotation);
	}
}