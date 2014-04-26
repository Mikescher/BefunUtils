using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System.Collections.Generic;

namespace BefunGen.AST
{
	/// <summary>
	/// These Lists are only temporary on AST-Creation - They should NEVER appear in the resulting AST
	/// </summary>
	public abstract class ASTList : ASTObject
	{
		public ASTList(SourceCodePosition pos)
			: base(pos)
		{
		}

		public override string getDebugString()
		{
			throw new AccessTemporaryASTObjectException(Position);
		}
	}

	#region Lists

	public class List_Expressions : ASTList
	{
		public List<Expression> List = new List<Expression>();

		public List_Expressions(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_Expressions(SourceCodePosition pos, Expression e)
			: base(pos)
		{
			List.Add(e);
		}

		public List_Expressions Append(Expression e)
		{
			List.Add(e);
			return this;
		}
	}

	public class List_Statements : ASTList
	{
		public List<Statement> List = new List<Statement>();

		public List_Statements(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_Statements(SourceCodePosition pos, Statement s)
			: base(pos)
		{
			List.Add(s);
		}

		public List_Statements Append(Statement s)
		{
			List.Add(s);
			return this;
		}
	}

	public class List_VarDeclarations : ASTList
	{
		public List<VarDeclaration> List = new List<VarDeclaration>();

		public List_VarDeclarations(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_VarDeclarations(SourceCodePosition pos, VarDeclaration d)
			: base(pos)
		{
			List.Add(d);
		}

		public List_VarDeclarations Append(VarDeclaration d)
		{
			List.Add(d);
			return this;
		}
	}

	public class List_Methods : ASTList
	{
		public List<Method> List = new List<Method>();

		public List_Methods(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_Methods(SourceCodePosition pos, Method d)
			: base(pos)
		{
			List.Add(d);
		}

		public List_Methods Append(Method d)
		{
			List.Add(d);
			return this;
		}
	}

	public class List_Switchs : ASTList
	{
		public List<Switch_Case> List = new List<Switch_Case>();

		public List_Switchs(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_Switchs(SourceCodePosition pos, Literal_Value l, Statement s)
			: base(pos)
		{
			List.Add(new Switch_Case(l, s));
		}

		public List_Switchs Append(Literal_Value l, Statement s)
		{
			List.Add(new Switch_Case(l, s));
			return this;
		}

		public List_Switchs Prepend(Literal_Value l, Statement s)
		{
			List.Insert(0, new Switch_Case(l, s));
			return this;
		}
	}

	public class List_OutfElements : ASTList
	{
		public class Outf_Union
		{
			public readonly Literal_CharArr String;
			public readonly Expression Expr;

			public bool IsString { get { return String != null; } }
			public bool IsExpression { get { return Expr != null; } }

			public Outf_Union(Literal_CharArr v)
			{
				String = v;
				Expr = null;
			}

			public Outf_Union(Expression v)
			{
				String = null;
				Expr = v;
			}

			public Statement CreateStatement()
			{
				if (IsString)
				{
					return new Statement_Out_CharArrLiteral(String.Position, String);
				}
				else if (IsExpression)
				{
					return new Statement_Out(Expr.Position, Expr);
				}
				else
				{
					throw new InternalCodeGenException();
				}
			}
		}

		public List<Outf_Union> List = new List<Outf_Union>();

		public List_OutfElements(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_OutfElements(SourceCodePosition pos, Expression v)
			: base(pos)
		{
			List.Add(new Outf_Union(v));
		}

		public List_OutfElements(SourceCodePosition pos, Literal_CharArr v)
			: base(pos)
		{
			List.Add(new Outf_Union(v));
		}

		public List_OutfElements Append(Expression v)
		{
			List.Add(new Outf_Union(v));
			return this;
		}

		public List_OutfElements Append(Literal_CharArr v)
		{
			List.Add(new Outf_Union(v));
			return this;
		}

		public List_OutfElements Prepend(Expression v)
		{
			List.Insert(0, new Outf_Union(v));
			return this;
		}

		public List_OutfElements Prepend(Literal_CharArr v)
		{
			List.Insert(0, new Outf_Union(v));
			return this;
		}
	}

	#endregion Lists

	#region Literals Lists

	public class List_LiteralDigits : ASTList
	{
		public List<Literal_Digit> List = new List<Literal_Digit>();

		public List_LiteralDigits(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_LiteralDigits(SourceCodePosition pos, Literal_Digit e)
			: base(pos)
		{
			List.Add(e);
		}

		public List_LiteralDigits Append(Literal_Digit e)
		{
			List.Add(e);
			return this;
		}
	}

	public class List_LiteralInts : ASTList
	{
		public List<Literal_Int> List = new List<Literal_Int>();

		public List_LiteralInts(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_LiteralInts(SourceCodePosition pos, Literal_Int e)
			: base(pos)
		{
			List.Add(e);
		}

		public List_LiteralInts Append(Literal_Int e)
		{
			List.Add(e);
			return this;
		}
	}

	public class List_LiteralChars : ASTList
	{
		public List<Literal_Char> List = new List<Literal_Char>();

		public List_LiteralChars(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_LiteralChars(SourceCodePosition pos, Literal_Char e)
			: base(pos)
		{
			List.Add(e);
		}

		public List_LiteralChars Append(Literal_Char e)
		{
			List.Add(e);
			return this;
		}
	}

	public class List_LiteralBools : ASTList
	{
		public List<Literal_Bool> List = new List<Literal_Bool>();

		public List_LiteralBools(SourceCodePosition pos)
			: base(pos)
		{
		}

		public List_LiteralBools(SourceCodePosition pos, Literal_Bool e)
			: base(pos)
		{
			List.Add(e);
		}

		public List_LiteralBools Append(Literal_Bool e)
		{
			List.Add(e);
			return this;
		}
	}

	#endregion Literals Lists

	#region Helper

	public class Switch_Case
	{
		public Literal_Value Value;
		public Statement Body;

		public Switch_Case(Literal_Value v, Statement s)
		{
			Value = v;
			Body = s;
		}
	}

	#endregion
}