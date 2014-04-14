using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


	/// <summary>
	/// Class used to have an image that is able to be gray when the control is not enabled.
	/// Author: Thomas LEBRUN (http://blogs.developpeur.org/tom)
	/// </summary>
	public class AutoGreyableImage : Image
	{
		static AutoGreyableImage()
		{
			IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnAutoGreyScaleImageIsEnabledPropertyChanged)));
		}

		private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
		{
			var autoGreyScaleImg = source as AutoGreyableImage;
			var isEnable = Convert.ToBoolean(args.NewValue);
			if (autoGreyScaleImg != null && autoGreyScaleImg.Source != null)
			{
				if (!isEnable)
				{
					var bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));
					autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);
					autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);
				}
				else
				{
					autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;
					autoGreyScaleImg.OpacityMask = null;
				}
			}
		}
	}
}
