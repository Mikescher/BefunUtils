
namespace BefunHighlight
{
	public class HighlightField
	{
		public HighlightInformation[] information = new HighlightInformation[8]; // One for each BEGraphDirection

		public HighlightField()
		{
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
					return 'X';
				case HighlightType.String:
					return 'O';
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
