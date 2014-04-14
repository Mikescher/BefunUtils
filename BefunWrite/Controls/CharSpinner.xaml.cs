using BefunWrite.Helper;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace BefunWrite.Controls
{
	/// <summary>
	/// Interaction logic for CharSpinner.xaml
	/// </summary>
	public partial class CharSpinner : UserControl
	{
		internal const int MAX_VALUE = 256;

		public char CValue
		{
			get { return (char)Value; }
			set { Value = value; }
		}

		#region Value Dependency Property

		public uint Value
		{
			get { return (uint)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(uint), typeof(CharSpinner), new PropertyMetadata());

		#endregion

		public CharSpinner()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void ButtonSpinner_Spin(object sender, SpinEventArgs e)
		{
			if (e.Direction == SpinDirection.Increase)
			{
				Value = (Value + 1) % MAX_VALUE;
			}
			else
			{
				Value = (Value - 1) % MAX_VALUE;
			}
		}
	}

	public class UIntToDispCharConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint v = (value as uint?).Value % CharSpinner.MAX_VALUE;

			if (v == ' ')
				return "'SPACE' (#" + v + ")";
			else if (v > ' ' && v <= '~')
				return "'" + (char)v + "' (#" + v + ")";
			else
				return "(#" + v + ")";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return MathHelper.ParseFormattedCharString(value as string);
		}
	}
}
