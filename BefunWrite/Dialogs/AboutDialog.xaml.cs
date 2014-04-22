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
	}
}
