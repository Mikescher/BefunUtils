using System;
using System.Collections.Generic;

namespace BefunGen.AST
{

	/// <summary>
	/// These Lists are only temporary on AST-Creation - They should NEVER appear in the resulting AST
	/// </summary>
	public abstract class ASTList : ASTObject
	{
		public ASTList()
		{

		}
	}

	#region Lists

	public class List_Expressions : ASTList
	{
		public List<Expression> List = new List<Expression>();

		public List_Expressions()
		{

		}

		public List_Expressions(Expression e)
		{
			List.Add(e);
		}

		public List_Expressions Append(Expression e)
		{
			List.Add(e);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_Statements : ASTList
	{
		public List<Statement> List = new List<Statement>();

		public List_Statements()
		{

		}

		public List_Statements(Statement s)
		{
			List.Add(s);
		}

		public List_Statements Append(Statement s)
		{
			List.Add(s);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_VarDeclarations : ASTList
	{
		public List<VarDeclaration> List = new List<VarDeclaration>();

		public List_VarDeclarations()
		{

		}

		public List_VarDeclarations(VarDeclaration d)
		{
			List.Add(d);
		}

		public List_VarDeclarations Append(VarDeclaration d)
		{
			List.Add(d);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_Methods : ASTList
	{
		public List<Method> List = new List<Method>();

		public List_Methods()
		{

		}

		public List_Methods(Method d)
		{
			List.Add(d);
		}

		public List_Methods Append(Method d)
		{
			List.Add(d);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	#endregion

	#region Literals Lists

	public class List_LiteralDigits : ASTList
	{
		public List<Literal_Digit> List = new List<Literal_Digit>();

		public List_LiteralDigits()
		{

		}

		public List_LiteralDigits(Literal_Digit e)
		{
			List.Add(e);
		}

		public List_LiteralDigits Append(Literal_Digit e)
		{
			List.Add(e);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_LiteralInts : ASTList
	{
		public List<Literal_Int> List = new List<Literal_Int>();

		public List_LiteralInts()
		{

		}

		public List_LiteralInts(Literal_Int e)
		{
			List.Add(e);
		}

		public List_LiteralInts Append(Literal_Int e)
		{
			List.Add(e);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_LiteralChars : ASTList
	{
		public List<Literal_Char> List = new List<Literal_Char>();

		public List_LiteralChars()
		{

		}

		public List_LiteralChars(Literal_Char e)
		{
			List.Add(e);
		}

		public List_LiteralChars Append(Literal_Char e)
		{
			List.Add(e);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class List_LiteralBools : ASTList
	{
		public List<Literal_Bool> List = new List<Literal_Bool>();

		public List_LiteralBools()
		{

		}

		public List_LiteralBools(Literal_Bool e)
		{
			List.Add(e);
		}

		public List_LiteralBools Append(Literal_Bool e)
		{
			List.Add(e);
			return this;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	#endregion

}
