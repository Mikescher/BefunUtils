
namespace BefunHighlight
{
	public class HighlightInformation
	{
		public bool outgoing_direction_top = false;
		public bool outgoing_direction_bottom = false;
		public bool outgoing_direction_left = false;
		public bool outgoing_direction_right = false;

		public bool hl_string = false;
		public bool hl_command = false;

		public bool hl_jumpover = false;

		public bool is_set { get { return hl_string || hl_command; } }

		public void setDirection(BeGraphDirection d, bool val)
		{
			switch (d)
			{
				case BeGraphDirection.LeftRight:
				case BeGraphDirection.LeftRight_sm:
					outgoing_direction_right = val;
					return;
				case BeGraphDirection.TopBottom:
				case BeGraphDirection.TopBottom_sm:
					outgoing_direction_bottom = val;
					return;
				case BeGraphDirection.RightLeft:
				case BeGraphDirection.RightLeft_sm:
					outgoing_direction_left = val;
					return;
				case BeGraphDirection.BottomTop:
				case BeGraphDirection.BottomTop_sm:
					outgoing_direction_top = val;
					return;
			}
		}
	}
}
