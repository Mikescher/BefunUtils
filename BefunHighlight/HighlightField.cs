using System.Linq;

namespace BefunHighlight
{
	public class HighlightField
	{
		public HighlightInformation[] information = new HighlightInformation[8]; // One for each _incoming_ BEGraphDirection

		#region Convenient Properties

		public bool incoming_information
		{
			get { return incoming_information_top || incoming_information_bottom || incoming_information_left || incoming_information_right; }
		}

		public bool incoming_information_top
		{
			get { return information[(int)BeGraphDirection.TopBottom].is_set || information[(int)BeGraphDirection.TopBottom_sm].is_set; }
		}

		public bool incoming_information_bottom
		{
			get { return information[(int)BeGraphDirection.BottomTop].is_set || information[(int)BeGraphDirection.BottomTop_sm].is_set; }
		}

		public bool incoming_information_left
		{
			get { return information[(int)BeGraphDirection.LeftRight].is_set || information[(int)BeGraphDirection.LeftRight_sm].is_set; }
		}

		public bool incoming_information_right
		{
			get { return information[(int)BeGraphDirection.RightLeft].is_set || information[(int)BeGraphDirection.RightLeft_sm].is_set; }
		}

		public bool outgoing_information
		{
			get { return outgoing_information_top || outgoing_information_bottom || outgoing_information_left || outgoing_information_right; }
		}

		public bool outgoing_information_top
		{
			get { return information.Any(p => p.outgoing_direction_top); }
		}

		public bool outgoing_information_bottom
		{
			get { return information.Any(p => p.outgoing_direction_bottom); }
		}

		public bool outgoing_information_left
		{
			get { return information.Any(p => p.outgoing_direction_left); }
		}

		public bool outgoing_information_right
		{
			get { return information.Any(p => p.outgoing_direction_right); }
		}

		#endregion

		public BeGraphCommand command;

		public HighlightField(BeGraphCommand cmd)
		{
			command = cmd;

			for (int i = 0; i < 8; i++)
			{
				information[i] = new HighlightInformation();
			}
		}

		public HighlightType getType()
		{
			bool hl_string = false;
			bool hl_command = false;

			for (int i = 0; i < 8; i++)
			{
				hl_string |= information[i].hl_string;
				hl_command |= information[i].hl_command;
			}

			if (hl_string && hl_command)
				return HighlightType.String_and_Command;
			else if (hl_string)
				return HighlightType.String;
			else if (hl_command)
				return HighlightType.Command;
			else
				return HighlightType.NOP;
		}

		public char toDebugString()
		{
			switch (getType())
			{
				case HighlightType.Command:
					return '+';
				case HighlightType.String:
					return ':';
				case HighlightType.String_and_Command:
					return '#';
				case HighlightType.NOP:
					return ' ';
				default:
					return '@';
			}
		}
	}
}
