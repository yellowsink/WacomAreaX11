namespace XSetWacom
{
	public class Tablet
	{
		public Tablet(int id) { Id = id; }

		public int Id { get; }

		public TabletArea Area
		{
			get => TabletDriver.GetArea(Id);
			set => TabletDriver.SetArea(Id, value);
		}

		public BoundTabletArea BoundArea => new(Id);

		public Rotation Rotation
		{
			get => TabletDriver.GetRotation(Id);
			set => TabletDriver.SetRotation(Id, value);
		}

		public FullArea FullArea => TabletDriver.GetFullArea(Id);

		// auto convert to ID if needed (passing to Wacom class methods etc)
		public static implicit operator int(Tablet tab) => tab.Id;
		public static explicit operator Tablet(int id)  => new(id);
	}
}