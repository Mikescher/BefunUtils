using System.Diagnostics;
using System.Windows;

namespace BefunWrite.Dialogs
{
	/// <summary>
	/// Interaction logic for AboutDialog.xaml
	/// </summary>
	public partial class AboutDialog : Window
	{
		public AboutDialog(Window owner)
		{
			this.Owner = owner;

			InitializeComponent();

			licenseBox.Text = Properties.Resources.license;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"http://www.mikescher.de");
		}

		private void Label_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"http://avalonedit.net/");
		}

		private void Label_MouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"https://wpftoolkit.codeplex.com/");
		}

		private void Label_MouseDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"http://goldparser.org/");
		}

		private void Label_MouseDown_4(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"http://p.yusukekamiyamane.com/");
		}

		private void Label_MouseDown_5(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Process.Start(@"http://www.codeproject.com/Articles/238307/A-Two-Column-Grid-for-WPF");
		}
	}
}
