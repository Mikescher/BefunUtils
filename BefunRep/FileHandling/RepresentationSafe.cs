
namespace BefunRep.FileHandling
{
	public abstract class RepresentationSafe
	{
		public abstract string get(int key);
		public abstract void put(int key, string representation);
	}
}
