using BefunGen.AST.CodeGen;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenUserException : BefunGenException
	{
		public BefunGenUserException(string msg, SourceCodePosition pos)
			: base("USER", msg, pos)
		{
		}

		public override string ToPopupString()
		{
			return this.GetType().Name + "\r\n    > " + BefMessage;
		}
	}
}
