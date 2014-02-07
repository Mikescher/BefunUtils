using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	abstract class Statement : ASTObject
	{
		public Statement()
		{
			//--
		}
	}

	class Statement_StatementList : Statement
	{
		public List<Statement> List;

		public Statement_StatementList(List<Statement> sl)
		{
			List = sl.ToList();
		}
	}

	class Statement_MethodCall : Statement
	{
		public List<Expression> CallParameter;
		public string Identifier;

		public Statement_MethodCall(string id)
		{
			this.Identifier = id;
			this.CallParameter = new List<Expression>();
		}

		public Statement_MethodCall(string id, List<Expression> cp)
		{
			this.Identifier = id;
			this.CallParameter = cp.ToList();
		}
	}

	#region Keywords

	class Statement_Label : Statement
	{
		public string Identifier;

		public Statement_Label(string id)
		{
			this.Identifier = id;
		}
	}

	class Statement_Goto : Statement
	{
		public string TargetIdentifier;

		public Statement_Goto(string id)
		{
			this.TargetIdentifier = id;
		}
	}

	class Statement_Return : Statement
	{
		public Expression Value;

		public Statement_Return()
		{
			this.Value = null;
		}

		public Statement_Return(Expression v)
		{
			this.Value = v;
		}
	}

	class Statement_Out : Statement
	{
		public Expression Value;

		public Statement_Out(Expression v)
		{
			this.Value = v;
		}
	}

	class Statement_In : Statement
	{
		public Expression_ValuePointer ValueTarget;

		public Statement_In(Expression_ValuePointer vt)
		{
			this.ValueTarget = vt;
		}
	}

	class Statement_Quit : Statement
	{
		public Statement_Quit()
		{
		}
	}

	#endregion

	#region Operations

	class Statement_Inc : Statement
	{
		public Expression_ValuePointer Identifier;

		public Statement_Inc(Expression_ValuePointer id)
		{
			this.Identifier = id;
		}
	}

	class Statement_Dec : Statement
	{
		public Expression_ValuePointer Identifier;

		public Statement_Dec(Expression_ValuePointer id)
		{
			this.Identifier = id;
		}
	}

	class Statement_Assignment : Statement
	{
		public Expression_ValuePointer Target;
		public Expression Expr;

		public Statement_Assignment(Expression_ValuePointer t, Expression e)
		{
			this.Target = t;
			this.Expr = e;
		}
	}

	#endregion

	#region Constructs

	class Statement_If : Statement
	{
		public Expression Condition;
		public Statement Body;
		public Statement Else;

		public Statement_If(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = null;
		}

		public Statement_If(Expression c, Statement b, Statement e)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = e;
		}
	}

	class Statement_While : Statement
	{
		public Expression Condition;
		public Statement Body;

		public Statement_While(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
		}
	}

	class Statement_RepeatUntil : Statement
	{
		public Expression Condition;
		public Statement Body;

		public Statement_RepeatUntil(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
		}
	}

	#endregion
}
