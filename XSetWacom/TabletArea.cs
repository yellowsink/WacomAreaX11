using System.Diagnostics;

namespace XSetWacom
{
	[DebuggerDisplay("{Left} {Top} {Right} {Bottom}, rotate {Rotation}")]
	public partial class TabletArea
	{

		internal virtual int RawBottom { get; set; }
		internal virtual int RawLeft   { get; set; }
		internal virtual int RawRight  { get; set; }
		internal virtual int RawTop    { get; set; }

		public virtual Rotation Rotation { get; set; }

		public decimal ScaleFactor = 1;

		public TabletArea((int left, int top, int right, int bottom) area, Rotation rotation = Rotation.None) :
			this(area.left, area.top, area.right, area.bottom, rotation)
		{
		}

		public TabletArea(int left, int top, int right, int bottom, Rotation rotation, bool centimetres = false)
		{
			if (centimetres) ScaleToCentimetres();

			Left   = left;
			Top    = top;
			Right  = right;
			Bottom = bottom;

			// ReSharper disable once VirtualMemberCallInConstructor
			Rotation = rotation;
		}

		/// <summary>
		///		PLEASE IGNORE - only to be used implicitly by the BoundTabletArea constructor
		/// </summary>
		internal TabletArea() { }

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