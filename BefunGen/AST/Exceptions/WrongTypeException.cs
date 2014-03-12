using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST.Exceptions
{
	public class WrongTypeException : ASTException
	{
		public WrongTypeException(SourceCodePosition pos, BType found, BType expec)
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, expec), pos)
		{
		}

		public WrongTypeException(SourceCodePosition pos, BType found, params BType[] expec)
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, string.Join(" or ", expec.ToList())), pos)
		{
		}
	}
}
