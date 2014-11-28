
namespace BefunRep.Test
{
	public class DummyResultTester : ResultTester
	{
		public override bool test(string code, int result, out string error)
		{
			error = "DUMMY";
			return true;
		}
	}
}
