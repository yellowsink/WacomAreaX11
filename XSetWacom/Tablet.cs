namespace XSetWacom
{
	public class Tablet
	{
		public Tablet(int id) { Id = id; }

		public int Id { get; }

		public string Name => TabletDriver.GetTabletName(Id);
		
		public TabletArea Area
		{
			get => TabletDriver.GetArea(Id);
			set => TabletDriver.SetArea(Id, value);
		}

		public BoundTabletArea BoundArea => new(Id);

		public FullArea FullArea => TabletDriver.GetFullArea(Id);

		public void ResetArea() => TabletDriver.ResetArea(Id);

		// auto convert to ID if needed (passing to Wacom class methods etc)
		public static implicit operator int(Tablet tab) => tab.Id;
		public static explicit operator Tablet(int id)  => new(id);
	}
}