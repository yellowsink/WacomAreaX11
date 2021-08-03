using System.Diagnostics;

namespace XSetWacom
{
	[DebuggerDisplay("{Left} {Top} {Right} {Bottom}")]
	public class TabletArea
	{
		public TabletArea((int left, int top, int right, int bottom) area)
			: this(area.left, area.top, area.right, area.bottom)
		{
		}
		
		public TabletArea(int left, int top, int right, int bottom, bool centimetres = false)
		{
			if (centimetres) ScaleToCentimetres();
			
			Left   = left;
			Top    = top;
			Right  = right;
			Bottom = bottom;
		}

		public decimal XScaleFactor = 1;
		public void    ScaleX(decimal scaleFactor) => XScaleFactor *= scaleFactor;
		public decimal YScaleFactor = 1;
		public void    ScaleY(decimal scaleFactor) => YScaleFactor *= scaleFactor;

		public void ScaleToCentimetres() => XScaleFactor = YScaleFactor = 0.001m;

		private int _left;
		private int _right;
		private int _top;
		private int _bottom;

		public decimal Left
		{
			get => _left * XScaleFactor;
			set => _left = (int) (value / XScaleFactor);
		}
		public decimal Right
		{
			get => _right * XScaleFactor;
			set => _right = (int) (value / XScaleFactor);
		}
		public decimal Top
		{
			get => _top * YScaleFactor;
			set => _top = (int) (value / YScaleFactor);
		}
		public decimal Bottom
		{
			get => _bottom * YScaleFactor;
			set => _bottom = (int) (value / YScaleFactor);
		}

		public decimal Width  => Right  - Left;
		public decimal Height => Bottom - Top;

		public (int left, int top, int right, int bottom) Unscaled => (_left, _top, _right, _bottom);
	}
}