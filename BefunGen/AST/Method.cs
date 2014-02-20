using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Method : ASTObject
	{
		private static int _M_ID_COUNTER = 100;
		protected static int M_ID_COUNTER { get { return _M_ID_COUNTER++; } }

		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public List<VarDeclaration> Variables; // Includes Parameter
		public Statement Body;

		public readonly int ID;

		public Method(SourceCodePosition pos, Method_Header h, Method_Body b)
			: this(pos, h.ResultType, h.Identifier, h.Parameter, b.Variables, b.Body)
		{
			//--
		}

		public Method(SourceCodePosition pos, BType t, string id, List<VarDeclaration> p, List<VarDeclaration> v, Statement b)
			: base(pos)
		{
			this.ID = M_ID_COUNTER;

			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;

			this.Variables = v;
			this.Body = b;

			Variables.AddRange(Parameter);
		}

		public override string getDebugString()
		{
			return string.Format("#Method ({{{0}}}({1}):{2})\n[\n#Parameter:\n{3}\n#Variables:\n{4}\n#Body:\n{5}\n]",
				ID,
				Identifier,
				ResultType.getDebugString(),
				indent(getDebugStringForList(Parameter)),
				indent(getDebugStringForList(Variables.Where(p => !Parameter.Contains(p)).ToList())),
				indent(Body.getDebugString()));
		}

		public void linkVariables()
		{
			Body.linkVariables(this);
		}

		public void linkMethods(Program owner)
		{
			Body.linkMethods(owner);
		}

		public void linkResultTypes()
		{
			Body.linkResultTypes(this);
		}

		public VarDeclaration findVariableByIdentifier(string ident)
		{
			return Variables.Count(p => p.Identifier.ToLower() == ident.ToLower()) == 1 ? Variables.Single(p => p.Identifier.ToLower() == ident.ToLower()) : null;
		}

		public Statement_Label findLabelByIdentifier(string ident)
		{
			return Body.findLabelByIdentifier(ident);
		}

		public static void resetCounter()
		{
			_M_ID_COUNTER = 1;
		}

		public CodePiece generateCode()
		{
			CodePiece p = new CodePiece();

			//TODO IMPLEMENT NOW; MUDDAFUCKAA

			return p;
		}
	}

	public class Method_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public Method_Header(SourceCodePosition pos, BType t, string id, List<VarDeclaration> p)
			: base(pos)
		{
			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;
		}

		public override string getDebugString()
		{
			throw new InvalidASTStateException(Position);
		}
	}

	public class Method_Body : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public List<VarDeclaration> Variables;
		public Statement Body;

		public Method_Body(SourceCodePosition pos, List<VarDeclaration> v, Statement b)
			: base(pos)
		{
			this.Variables = v;
			this.Body = b;
		}

		public override string getDebugString()
		{
			throw new InvalidASTStateException(Position);
		}
	}
}