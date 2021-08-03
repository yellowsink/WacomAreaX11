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
				Rotation.Half => RawRight,
				Rotation.Ccw  => RawBottom,
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
						RawRight = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawBottom = (int) (value / ScaleFactor);
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
				Rotation.Half => RawLeft,
				Rotation.Ccw  => RawTop,
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
						RawLeft = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawTop = (int) (value / ScaleFactor);
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
				Rotation.Cw   => RawLeft,
				Rotation.Half => RawBottom,
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
						RawLeft = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						RawBottom = (int) (value / ScaleFactor);
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
				Rotation.Cw   => RawRight,
				Rotation.Half => RawTop,
				Rotation.Ccw  => RawLeft,
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
						RawRight = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						RawTop = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						RawLeft = (int) (value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}