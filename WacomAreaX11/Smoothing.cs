using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void EnterSmoothing(Tablet tablet)
			=> tablet.BoundArea.Smoothing = NumberSpinner.Spin("Choose your amount of tablet smoothing",
															   true,
															   1,
															   20,
															   tablet.BoundArea.Smoothing);
	}
}