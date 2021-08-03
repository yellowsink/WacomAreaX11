namespace XSetWacom
{
	/// <summary>
	///     Works just like TabletArea, but it will submit changes to xsetwacom instantly
	/// </summary>
	public class BoundTabletArea : TabletArea
	{
		public BoundTabletArea(int tabletId) : base(0, 0, 0, 0) { TabletId = tabletId; }

		public int TabletId { get; }

		internal new int RawBottom
		{
			get => TabletDriver.GetArea(TabletId).RawBottom;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawBottom = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal new int RawLeft
		{
			get => TabletDriver.GetArea(TabletId).RawBottom;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawLeft = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal new int RawRight
		{
			get => TabletDriver.GetArea(TabletId).RawBottom;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawRight = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		internal new int RawTop
		{
			get => TabletDriver.GetArea(TabletId).RawBottom;
			set
			{
				var area = TabletDriver.GetArea(TabletId);
				area.RawTop = value;
				TabletDriver.SetArea(TabletId, area);
			}
		}

		public new Rotation Rotation
		{
			get => TabletDriver.GetRotation(TabletId);
			set => TabletDriver.SetRotation(TabletId, value);
		}
	}
}