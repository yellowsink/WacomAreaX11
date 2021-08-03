using System.Diagnostics;

namespace XSetWacom
{
	[DebuggerDisplay("{Left} {Top} {Right} {Bottom}, rotate {Rotation}")]
	public partial class TabletArea
	{
		internal int RawBottom;
		internal int RawLeft;
		internal int RawRight;
		internal int RawTop;

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
			if (centimetres) ScaleToCentimetres();

			Left   = left;
			Top    = top;
			Right  = right;
			Bottom = bottom;

			Rotation = rotation;
		}

		public decimal Width  => Right  - Left;
		public decimal Height => Bottom - Top;

		public (int left, int top, int right, int bottom) Unscaled => (RawLeft, RawTop, RawRight, RawBottom);

		public void Scale(decimal scaleFactor) => ScaleFactor *= scaleFactor;

		public void ScaleToCentimetres() => ScaleFactor = 0.001m;

		public void RotateCw()  => Rotation = RotateCw(Rotation);
		public void RotateCcw() => Rotation = RotateCcw(Rotation);

		public static Rotation RotateCw(Rotation  rotation) => rotation == Rotation.Ccw ? Rotation.None : rotation + 1;
		public static Rotation RotateCcw(Rotation rotation) => rotation == Rotation.None ? Rotation.Ccw : rotation - 1;
	}
}