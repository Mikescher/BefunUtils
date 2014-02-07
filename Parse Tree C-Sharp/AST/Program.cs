using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	class Program : ASTObject
	{
		public string Identifier;
		public Method MainStatement;
		public List<Method> MethodList;

		public Program(string id, Method m, List<Method> mlst)
		{
			this.Identifier = id;
			this.MainStatement = m;
			this.MethodList = mlst.ToList();
		}
	}

	class Program_Footer : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public Program_Footer()
		{
		}
	}

	class Program_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public string Identifier;

		public Program_Header(string id)
		{
			this.Identifier = id;
		}
	}
}
