using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.IO;
using System.Windows;
using System.Xml;

namespace BefunWrite
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Init();
		}

		private void Init()
		{
			using (XmlReader reader = new XmlTextReader(new StringReader(Properties.Resources.TextFunge)))
			{
				IHighlightingDefinition customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
				codeEditor.SyntaxHighlighting = customHighlighting;
			}

			codeEditor.ShowLineNumbers = true;
			codeEditor.Options.CutCopyWholeLine = true;
			codeEditor.Options.ShowTabs = true;

			codeEditor.Text = Properties.Resources.example;
		}
	}
}


//TODO Check Licenses:
// AvalonEdit
// AvalonDock
// GOLDTools
// OpenTK
// QuickFont
// SyntaxBox (?)
// Fugue Icon Set