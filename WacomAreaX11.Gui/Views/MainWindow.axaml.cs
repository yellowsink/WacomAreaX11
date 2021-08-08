using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DynamicData;
using JetBrains.Annotations;
using WacomAreaX11.Gui.ViewModels;
using XSetWacom;

namespace WacomAreaX11.Gui.Views
{
	public class MainWindow : Window
	{
		public MainWindow()
		{
			DataContext = new MainWindowViewModel();

			AvaloniaXamlLoader.Load(this);
#if DEBUG
			this.AttachDevTools();
#endif
		}

		[UsedImplicitly]
		private void ApplyConfig(object? sender = null, RoutedEventArgs routedEventArgs = null!)
		{
			if (((MainWindowViewModel) DataContext!).Tablet == null) return;

			var width     = ((MainWindowViewModel) DataContext!).Width;
			var height    = ((MainWindowViewModel) DataContext!).Height;
			var left      = ((MainWindowViewModel) DataContext!).OffsetX;
			var top       = ((MainWindowViewModel) DataContext!).OffsetY;
			var rotation  = ((MainWindowViewModel) DataContext!).Rotation;
			var smoothing = ((MainWindowViewModel) DataContext!).Smoothing;

			// ignore the 0,0,0,0 and shit i just wrote the constructors in a weird way and cba to fix it
			var area = new TabletArea(0, 0, 0, 0, ((MainWindowViewModel) DataContext!).Tablet!.FullArea, rotation)
			{
				ScaleFactor = 0.01m,
				Left   = left,
				Top    = top,
				Right  = left + width,
				Bottom = top  + height
			};
			
			((MainWindowViewModel) DataContext!).Tablet!.Area                = area;
			((MainWindowViewModel) DataContext!).Tablet!.BoundArea.Smoothing = smoothing;
		}
		
		[UsedImplicitly]
		private void ResetToCurrent(object? sender = null, RoutedEventArgs routedEventArgs = null!)
		{
			if (((MainWindowViewModel) DataContext!).Tablet == null) return;
			
			var area = ((MainWindowViewModel) DataContext!).Tablet!.Area;
			area.ScaleFactor = 0.01m;
			
			((MainWindowViewModel) DataContext!).Width     = area.Width;
			((MainWindowViewModel) DataContext!).Height    = area.Height;
			((MainWindowViewModel) DataContext!).OffsetX   = area.Left;
			((MainWindowViewModel) DataContext!).OffsetY   = area.Top;
			((MainWindowViewModel) DataContext!).Rotation  = area.Rotation;
			((MainWindowViewModel) DataContext!).Smoothing = ((MainWindowViewModel) DataContext!).Tablet!.BoundArea.Smoothing;
		}
		
		[UsedImplicitly]
		private void ResetToDefault(object? sender = null, RoutedEventArgs routedEventArgs = null!)
		{
			((MainWindowViewModel) DataContext!).Tablet?.ResetArea();
			ResetToCurrent();
		}

		[UsedImplicitly]
		private void TabSelectedChanged(object? sender, SelectionChangedEventArgs e)
		{
			switch (((TabControl) sender!).SelectedIndex)
			{
				case 0:
					try
					{
						((MainWindowViewModel) DataContext!).Tablets.Clear();
						((MainWindowViewModel) DataContext!).Tablets.AddRange(TabletDriver.GetTablets());
						((MainWindowViewModel) DataContext!).Tablet
							??= ((MainWindowViewModel) DataContext!).Tablets.Items.First();
					}
					catch (Exception)
					{
						// Oh no! Anyway...
					}
					break;
				case 1:
					ResetToCurrent();
					break;
				case 2:
					break;
			}
		}
	}
}