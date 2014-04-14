using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BefunWrite.Controls
{
	/// <summary>
	/// Interaction logic for SimpleToolbarButton.xaml
	/// </summary>
	public partial class SimpleToolbarButton : UserControl
	{
		private string _Text = "";
		public string BText { get { return _Text; } set { _Text = value; txt.Text = value; } }

		public ImageSource _Source;
		public ImageSource BSource { get { return _Source; } set { _Source = value; img.Source = value; } }

		public ICommand _BCommand;
		public ICommand BCommand { get { return _BCommand; } set { _BCommand = value; btn.Command = value; } }

		public SimpleToolbarButton()
		{
			InitializeComponent();
		}
	}

}
