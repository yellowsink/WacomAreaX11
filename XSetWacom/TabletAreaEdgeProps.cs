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
				Rotation.None => _left,
				Rotation.Cw   => _top,
				Rotation.Half => _right,
				Rotation.Ccw  => _bottom,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						_left = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						_top = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						_right = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						_bottom = (int) (value / ScaleFactor);
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
				Rotation.None => _right,
				Rotation.Cw   => _bottom,
				Rotation.Half => _left,
				Rotation.Ccw  => _top,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						_right = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						_bottom = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						_left = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						_top = (int) (value / ScaleFactor);
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
				Rotation.None => _top,
				Rotation.Cw   => _left,
				Rotation.Half => _bottom,
				Rotation.Ccw  => _right,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						_top = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						_left = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						_bottom = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						_right = (int) (value / ScaleFactor);
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
				Rotation.None => _bottom,
				Rotation.Cw   => _right,
				Rotation.Half => _top,
				Rotation.Ccw  => _left,
				_             => throw new ArgumentOutOfRangeException()
			} * ScaleFactor;
			set
			{
				switch (Rotation)
				{
					case Rotation.None:
						_bottom = (int) (value / ScaleFactor);
						break;
					case Rotation.Cw:
						_right = (int) (value / ScaleFactor);
						break;
					case Rotation.Half:
						_top = (int) (value / ScaleFactor);
						break;
					case Rotation.Ccw:
						_left = (int) (value / ScaleFactor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}