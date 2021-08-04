using System.Diagnostics;

namespace XSetWacom
{
	/// <summary>
	///     Works just like TabletArea, but it will submit changes to xsetwacom instantly
	/// </summary>
	[DebuggerDisplay("Bound to tablet ID {TabletId}")]
	public class BoundTabletArea : TabletArea
	{
		public BoundTabletArea(int tabletId) { TabletId = tabletId; }

		public int TabletId { get; }

		internal override int RawBottom
		{
			get => TabletDriver.GetArea(TabletId).RawBottom;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawBottom = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal override int RawLeft
		{
			get => TabletDriver.GetArea(TabletId).RawLeft;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawLeft = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal override int RawRight
		{
			get => TabletDriver.GetArea(TabletId).RawRight;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawRight = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal override int RawTop
		{
			get => TabletDriver.GetArea(TabletId).RawTop;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawTop = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal override FullArea FullArea => TabletDriver.GetFullArea(TabletId);

		public override Rotation Rotation
		{
			get => TabletDriver.GetRotation(TabletId);
			set => TabletDriver.SetRotation(TabletId, value);
		}
	}
}