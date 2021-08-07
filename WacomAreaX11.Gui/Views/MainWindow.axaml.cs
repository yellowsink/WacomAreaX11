using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WacomAreaX11.Gui.ViewModels;
using XSetWacom;

namespace WacomAreaX11.Gui.Views
{
	public class MainWindow : Window
	{
		private readonly Tablet _tablet;
		
		public MainWindow()
		{
			DataContext = new MainWindowViewModel();
			
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			_tablet = TabletDriver.GetTablet();
			
			ResetToCurrent(null, null!);
		}

		private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }

		private void ApplyConfig(object? sender, RoutedEventArgs routedEventArgs)
		{
			var width     = ((MainWindowViewModel) DataContext!).Width;
			var height    = ((MainWindowViewModel) DataContext!).Height;
			var left      = ((MainWindowViewModel) DataContext!).OffsetX;
			var top       = ((MainWindowViewModel) DataContext!).OffsetY;
			var rotation  = ((MainWindowViewModel) DataContext!).Rotation;
			var smoothing = ((MainWindowViewModel) DataContext!).Smoothing;

			// ignore the 0,0,0,0 and shit i just wrote the constructors in a weird way and cba to fix it
			var area = new TabletArea(0, 0, 0, 0, _tablet.FullArea, rotation, false)
			{
				ScaleFactor = 0.01m,
				Left   = left,
				Top    = top,
				Right  = left + width,
				Bottom = top  + height
			};
			
			_tablet.Area                = area;
			_tablet.BoundArea.Smoothing = smoothing;
		}
		
		private void ResetToCurrent(object? sender, RoutedEventArgs routedEventArgs)
		{
			var area = _tablet.Area;
			area.ScaleFactor = 0.01m;
			
			((MainWindowViewModel) DataContext!).Width     = area.Width;
			((MainWindowViewModel) DataContext!).Height    = area.Height;
			((MainWindowViewModel) DataContext!).OffsetX   = area.Left;
			((MainWindowViewModel) DataContext!).OffsetY   = area.Top;
			((MainWindowViewModel) DataContext!).Rotation  = area.Rotation;
			((MainWindowViewModel) DataContext!).Smoothing = _tablet.BoundArea.Smoothing;
		}
		
		private void ResetToDefault(object? sender, RoutedEventArgs routedEventArgs)
		{
			_tablet.ResetArea();
			ResetToCurrent(null, null!);
		}
	}
}