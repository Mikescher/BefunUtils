
namespace BefunHighlight
{
	public class HighlightInformation
	{
		public bool hl_string = false;
		public bool hl_command = false;

		public bool is_set { get { return hl_string || hl_command; } }
	}
}
