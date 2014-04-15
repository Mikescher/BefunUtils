using BefunGen.AST.CodeGen;
using System.Windows.Controls;

namespace BefunWrite.Controls
{
	class ClickableTreeViewItem : TreeViewItem
	{
		private SourceCodePosition Position;
		private IconBarMargin Target;

		public ClickableTreeViewItem(IconBarMargin _target, string _header, SourceCodePosition _pos, bool _expanded)
			: base()
		{
			Header = _header;
			Position = _pos;
			IsExpanded = _expanded;
			Target = _target;

			Selected += OnSelected;
		}

		private void OnSelected(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Position != null && Target != null)
			{
				Target.MakePositionVisible(Position);
			}

			e.Handled = false;
		}
	}
}
