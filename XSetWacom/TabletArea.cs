using System.Diagnostics;

namespace XSetWacom
{
	[DebuggerDisplay("{Left} {Top} {Right} {Bottom}, rotate {Rotation}")]
	public partial class TabletArea
	{
		private int _bottom;
		private int _left;
		private int _right;
		private int _top;

		public Rotation Rotation;

		public decimal ScaleFactor = 1;

		public TabletArea((int left, int top, int right, int bottom) area, Rotation rotation = Rotation.None) :
			this(area.left, area.top, area.right, area.bottom, rotation)
		{
		}

		public TabletArea(int left, int top, int right, int bottom, bool centimetres = false) : this(left,
			top,
			right,
			bottom,
			Rotation.None,
			centimetres)
		{
		}

		public TabletArea(int left, int top, int right, int bottom, Rotation rotation, bool centimetres = false)
		{
			Rotation = rotation;
			
			if (centimetres) ScaleToCentimetres();

			Left     = left;
			Top      = top;
			Right    = right;
			Bottom   = bottom;
		}

		public decimal Width  => Right  - Left;
		public decimal Height => Bottom - Top;

		public (int left, int top, int right, int bottom) Unscaled => (_left, _top, _right, _bottom);

		public void Scale(decimal scaleFactor) => ScaleFactor *= scaleFactor;

		public void ScaleToCentimetres() => ScaleFactor = 0.001m;

		public void RotateCw()  => Rotation = RotateCw(Rotation);
		public void RotateCcw() => Rotation = RotateCcw(Rotation);

		public static Rotation RotateCw(Rotation  rotation) => rotation == Rotation.Ccw ? Rotation.None : rotation + 1;
		public static Rotation RotateCcw(Rotation rotation) => rotation == Rotation.None ? Rotation.Ccw : rotation - 1;
	}

}