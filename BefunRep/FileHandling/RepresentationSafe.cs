
namespace BefunRep.FileHandling
{
	public abstract class RepresentationSafe
	{
		public abstract string get(long key);
		public abstract void put(long key, string representation);

		public abstract void start();
		public abstract void stop();

		public abstract long getLowestValue();
		public abstract long getHighestValue();
	}
}
