using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class ASTObject
	{
		public readonly SourceCodePosition Position;

		public ASTObject(SourceCodePosition pos)
		{
			this.Position = pos;
		}

		protected string getDebugCommaStringForList<T>(List<T> ls) where T : ASTObject
		{
			return string.Join(", ", ls.Select(p => p.getDebugString()));
		}

		protected string getDebugStringForList<T>(List<T> ls) where T : ASTObject
		{
			return string.Join("\n", ls.Select(p => p.getDebugString()));
		}

		protected string indent(string s)
		{
			return string.Join("\n", s.Split(new string[] { "\n" }, StringSplitOptions.None).Select(p => "    " + p));
		}

		public abstract string getDebugString();
	}
}