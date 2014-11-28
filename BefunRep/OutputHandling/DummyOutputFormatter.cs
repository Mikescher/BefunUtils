
namespace BefunRep.OutputHandling
{
	public class DummyOutputFormatter : OutputFormatter
	{
		public DummyOutputFormatter()
			: base(string.Empty)
		{
			//
		}

		public override void Output(FileHandling.RepresentationSafe safe)
		{
			// Do nothing
		}
	}
}
