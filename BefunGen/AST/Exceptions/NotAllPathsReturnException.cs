using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public class NotAllPathsReturnException : ASTException
	{
		public NotAllPathsReturnException(Method m, SourceCodePosition pos)
			: base("Not all Paths of Method " + m.Identifier + "() return a value.", pos)
		{
		}
	}
}
