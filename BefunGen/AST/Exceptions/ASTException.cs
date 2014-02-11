using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class ASTException : Exception
	{
		public ASTException(SourceCodePosition pos)
			: base(String.Format("[{0}] ASTException", pos))
		{

		}

		public ASTException(string msg, SourceCodePosition pos)
			: base(String.Format("[{0}] Exception '{1}'", pos, msg))
		{

		}

		public override string ToString()
		{
			return base.ToString().Replace(" in ", "\r\n      in ").Replace(@"e:\Eigene Dateien\Dropbox\Eigene EDV\Visual Studio\Projects\BefunGen\", "");
		}
	}
}
