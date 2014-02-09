using BefunGen.AST.CodeGen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BefunGenTest
{
	[TestClass]
	public class CodePieceTest
	{
		[TestMethod]
		public void setTest()
		{
			CodePiece cp = new CodePiece();

			cp.set(0, 0, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(1, cp.Width);
			Assert.AreEqual(1, cp.Height);

			cp.set(0, 2, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(1, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp.set(2, 0, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp.set(2, 2, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp.set(0, 0, new BefungeCommand(BefungeCommandType.Mult));
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(3, cp.Height);
			Assert.AreEqual('*', cp[0, 0].getCommandCode());

			cp.set(0, -2, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(5, cp.Height);

			cp.set(-2, 0, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(5, cp.Width);
			Assert.AreEqual(5, cp.Height);

			cp.set(-2, -2, new BefungeCommand(BefungeCommandType.Add));
			Assert.AreEqual(5, cp.Width);
			Assert.AreEqual(5, cp.Height);
		}
	}
}
