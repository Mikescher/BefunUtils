
namespace BefunRep.Test
{
	public abstract class ResultTester
	{
		public abstract bool test(string code, int result, out string error);
	}
}
