using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class ASTObject
	{
		public static string[] keywords = 
		{ 
			"begin", "close", "const", "display", "do", 
			"else", "elsif", "end", "false", "for", 
			"global", "goto", "if", "in", "out", 
			"program", "quit", "rand", "repeat", "return", 
			"stop", "then", "true", "until", "var", 
			"while", "switch", "case", "default",
			
			"bool", "boolean", "char", "character", "digit", 
			"int", "integer", "void"
		};

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

		public static bool isKeyword(string s)
		{
			return keywords.Contains(s.ToLower());
		}
	}
}