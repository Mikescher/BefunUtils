
namespace BefunRep.Test
{
	public abstract class ResultTester
	{
		public abstract bool test(string code, long result, out string error);
	}
}
