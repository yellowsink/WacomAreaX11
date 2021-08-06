using WacomAreaX11.Input;
using XSetWacom;

namespace WacomAreaX11
{
	internal static partial class Program
	{
		private static void EnterRotation(Tablet tablet)
			=> tablet.BoundArea.Rotation = ListPicker.Pick("Pick your tablet area rotation",
														   true,
														   new[]
														   {
															   Rotation.None, Rotation.Cw, Rotation.Half, Rotation.Ccw
														   },
														   tablet.BoundArea.Rotation);
	}
}