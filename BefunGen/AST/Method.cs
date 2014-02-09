using System;
using System.Collections.Generic;

namespace BefunGen.AST
{
	public class Method : ASTObject
	{
		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public List<VarDeclaration> Variables;
		public Statement Body;

		public Method(Method_Header h, Method_Body b)
			: this(h.ResultType, h.Identifier, h.Parameter, b.Variables, b.Body)
		{
			//--
		}

		public Method(BType t, string id, List<VarDeclaration> p, List<VarDeclaration> v, Statement b)
		{
			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;

			this.Variables = v;
			this.Body = b;
		}

		public override string getDebugString()
		{
			return string.Format("#Method ({0}:{1})\n[\n#Parameter:\n{2}\n#Variables:\n{3}\n#Body:\n{4}\n]",
				Identifier,
				ResultType.getDebugString(),
				indent(getDebugStringForList(Parameter)),
				indent(getDebugStringForList(Variables)),
				indent(Body.getDebugString()));
		}
	}

	public class Method_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public Method_Header(BType t, string id, List<VarDeclaration> p)
		{
			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class Method_Body : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public List<VarDeclaration> Variables;
		public Statement Body;

		public Method_Body(List<VarDeclaration> v, Statement b)
		{
			this.Variables = v;
			this.Body = b;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}
}
