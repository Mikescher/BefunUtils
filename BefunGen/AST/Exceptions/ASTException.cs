using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
