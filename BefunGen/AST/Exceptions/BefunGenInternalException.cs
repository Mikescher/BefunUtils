using BefunGen.AST.CodeGen;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenInternalException : BefunGenException
	{
		public BefunGenInternalException(string msg, SourceCodePosition pos)
			: base("INTERNAL", msg, pos)
		{
		}

		public BefunGenInternalException(string msg)
			: base("INTERNAL", msg)
		{
		}

		public override string ToPopupString()
		{
			return "[Internal Exception] " + this.GetType().Name + "\r\n    > " + BefMessage;
		}
	}
}
