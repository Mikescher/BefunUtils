using System.Windows.Input;

namespace BefunWrite
{
	class CustomCMDs
	{
		public static RoutedUICommand Build = new RoutedUICommand("Build", "Build", typeof(CustomCMDs));
		public static RoutedUICommand Start = new RoutedUICommand("Start", "Start", typeof(CustomCMDs));
		public static RoutedUICommand Stop = new RoutedUICommand("Stop", "Stop", typeof(CustomCMDs));
	}
}
