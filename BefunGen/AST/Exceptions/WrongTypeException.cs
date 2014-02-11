using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;

namespace BefunGen.AST.Exceptions
{
	public class WrongTypeException : ASTException
	{
		public WrongTypeException(BType found, BType expec, SourceCodePosition pos)
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, expec), pos)
		{
		}

		public WrongTypeException(BType found, List<BType> expec, SourceCodePosition pos)
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, string.Join(" or ", expec)), pos)
		{
		}
	}
}
