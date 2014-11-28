using BefunRep.FileHandling;

namespace BefunRep.OutputHandling
{
	public abstract class OutputFormatter
	{
		protected readonly string filepath;

		public OutputFormatter(string path)
		{
			this.filepath = path;
		}

		public abstract void Output(RepresentationSafe safe);
	}
}
