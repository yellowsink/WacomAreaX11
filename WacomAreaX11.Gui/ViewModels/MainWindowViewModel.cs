using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using XSetWacom;

namespace WacomAreaX11.Gui.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public Thickness AreaDisplayBorderSize => new(3);
		
		public int AreaDisplayCanvasHeight => 200;

		public int AreaDisplayCanvasWidth
			=> (int) (AreaDisplayCanvasHeight / (FullArea?.Height ?? 1) * (FullArea?.Width ?? 2));

		public int AreaDisplayWidth   => (int) (_width   * (AreaDisplayCanvasWidth  / (FullArea?.Width  ?? 1))) - (int) AreaDisplayBorderSize.Top * 4;
		public int AreaDisplayHeight  => (int) (_height  * (AreaDisplayCanvasHeight / (FullArea?.Height ?? 1))) - (int) AreaDisplayBorderSize.Top * 4;
		public int AreaDisplayOffsetX => (int) (_offsetX * (AreaDisplayCanvasWidth  / (FullArea?.Width  ?? 1))) + (int) AreaDisplayBorderSize.Top;
		public int AreaDisplayOffsetY => (int) (_offsetY * (AreaDisplayCanvasHeight / (FullArea?.Height ?? 1))) + (int) AreaDisplayBorderSize.Top;

		private TabletArea? _fullAreaCache;
		
		// utility for above props
		private TabletArea? FullArea
		{
			get
			{
				if (_fullAreaCache != null) return _fullAreaCache;
				
				var fullAreaRotated = Tablet?.FullArea?.ToTabletArea(_rotation);
				
				if (fullAreaRotated != null) fullAreaRotated.ScaleFactor = 0.01m;
				return _fullAreaCache = fullAreaRotated;
			}
		}
		
		
		private decimal  _width;
		private decimal  _height;
		private int      _smoothing;
		private decimal  _offsetX;
		private decimal  _offsetY;
		private Rotation _rotation;
		private bool     _configSaveButtonActive;
		private Tablet?  _tablet;

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
			set
			{
				this.RaiseAndSetIfChanged(ref _width, value);
				this.RaisePropertyChanged(nameof(AreaDisplayWidth));
			}
		}

		public decimal Height
		{
			get => _height;
			set
			{
				this.RaiseAndSetIfChanged(ref _height, value);
				this.RaisePropertyChanged(nameof(AreaDisplayHeight));
			}
		}

		public int Smoothing
		{
			get => _smoothing;
			set => this.RaiseAndSetIfChanged(ref _smoothing, value);
		}

		public decimal OffsetX
		{
			get => _offsetX;
			set
			{
				this.RaiseAndSetIfChanged(ref _offsetX, value);
				this.RaisePropertyChanged(nameof(AreaDisplayOffsetX));
			}
		}
		
		public decimal OffsetY
		{
			get => _offsetY;
			set
			{
				this.RaiseAndSetIfChanged(ref _offsetY, value);
				this.RaisePropertyChanged(nameof(AreaDisplayOffsetY));
			}
		}

		public Rotation Rotation
		{
			get => _rotation;
			set
			{
				this.RaiseAndSetIfChanged(ref _rotation, value);
				_fullAreaCache = null;
				this.RaisePropertyChanged(nameof(AreaDisplayCanvasWidth));
				this.RaisePropertyChanged(nameof(AreaDisplayWidth));
				this.RaisePropertyChanged(nameof(AreaDisplayHeight));
				this.RaisePropertyChanged(nameof(AreaDisplayOffsetX));
				this.RaisePropertyChanged(nameof(AreaDisplayOffsetY));
			}
		}

		public Tablet[] Tablets => TabletDriver.GetTablets();

		public Tablet? Tablet
		{
			get => _tablet;
			set
			{
				_tablet        = value;
				_fullAreaCache = null;
			}
		}

		public Config[] Configs => Config.GetAll();
		
		public Config? Config { get; set; }

		public bool ConfigSaveButtonActive
		{
			get => _configSaveButtonActive;
			set => this.RaiseAndSetIfChanged(ref _configSaveButtonActive, value);
		}
		
		public string ConfigSaveName { get; set; } = string.Empty;
	}
}