using System;

namespace XSetWacom
{
	public partial class TabletArea
	{
		// TODO: Find a way to make this cleaner, more compact, and not switch statement hell

		public decimal Left
		{
			get => Rotation switch
			{
				Rotation.None => RawLeft,
				Rotation.Cw   => RawTop,
				Rotation.Half => FullArea.Width - RawRight,
				Rotation.Ccw  => FullArea.Height - RawBottom,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						RawLeft = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						RawTop = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						RawRight = (int) (FullArea.Width - value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawBottom = (int) (FullArea.Height - value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public decimal Right
		{
			get => Rotation switch
			{
				Rotation.None => RawRight,
				Rotation.Cw   => RawBottom,
				Rotation.Half => FullArea.Width - RawLeft,
				Rotation.Ccw  => FullArea.Height - RawTop,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						RawRight = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						RawBottom = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						RawLeft = (int) (FullArea.Width - value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawTop = (int) (FullArea.Height - value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public decimal Top
		{
			get => Rotation switch
			{
				Rotation.None => RawTop,
				Rotation.Cw   => FullArea.Width - RawRight,
				Rotation.Half => FullArea.Height - RawTop,
				Rotation.Ccw  => RawRight,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						RawTop = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						RawRight = (int) (FullArea.Width - value / ScaleFactor);
						break;
					case Rotation.Half:
						RawTop = (int) (FullArea.Height - value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawRight = (int) (value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public decimal Bottom
		{
			get => Rotation switch
			{
				Rotation.None => RawBottom,
				Rotation.Cw   => FullArea.Width - RawLeft,
				Rotation.Half => FullArea.Height - RawTop,
				Rotation.Ccw  => RawRight,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						RawBottom = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						RawLeft = (int) (FullArea.Width - value / ScaleFactor);
						break;
					case Rotation.Half:
						RawTop = (int) (FullArea.Height - value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawRight = (int) (value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}