using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public class CodePieceReverseException : Exception
	{
		public CodePieceReverseException(CodePiece p)
			: base("Cannot reverse Codepiece " + p + ".")
		{

		}
	}
}
