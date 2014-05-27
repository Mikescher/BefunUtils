
namespace BefunHighlight
{
	class HighlightField
	{
		public HighlightInformation[] information = new HighlightInformation[8]; // One for each BEGraphDirection

		public HighlightField()
		{
			for (int i = 0; i < 8; i++)
			{
				information[i] = new HighlightInformation();
			}
		}
	}
}
