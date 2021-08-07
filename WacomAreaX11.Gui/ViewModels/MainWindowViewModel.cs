﻿using Avalonia.Controls;
using ReactiveUI;
using XSetWacom;

namespace WacomAreaX11.Gui.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private decimal  _width;
		private decimal  _height;
		private int      _smoothing;
		private decimal  _offsetX;
		private decimal  _offsetY;
		private Rotation _rotation;

		public ComboBoxItem[] RotationItems => new[]
		{
			new ComboBoxItem { Content = Rotation.None },
			new ComboBoxItem { Content = Rotation.Cw },
			new ComboBoxItem { Content = Rotation.Half },
			new ComboBoxItem { Content = Rotation.Ccw }
		};

		public decimal Width
		{
			get => _width;
			set => this.RaiseAndSetIfChanged(ref _width, value);
		}

		public decimal Height
		{
			get => _height;
			set => this.RaiseAndSetIfChanged(ref _height, value);
		}

		public int Smoothing
		{
			get => _smoothing;
			set => this.RaiseAndSetIfChanged(ref _smoothing, value);
		}

		public decimal OffsetX
		{
			get => _offsetX;
			set => this.RaiseAndSetIfChanged(ref _offsetX, value);
		}
		
		public decimal OffsetY
		{
			get => _offsetY;
			set => this.RaiseAndSetIfChanged(ref _offsetY, value);
		}

		public Rotation Rotation
		{
			get => _rotation;
			set => this.RaiseAndSetIfChanged(ref _rotation, value);
		}
	}
}