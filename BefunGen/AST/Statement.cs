using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class Statement : ASTObject
	{
		public Statement()
		{
			//--
		}

		public abstract void linkVariables(Method owner);
	}

	public class Statement_StatementList : Statement
	{
		public List<Statement> List;

		public Statement_StatementList(List<Statement> sl)
		{
			List = sl.ToList();
		}

		public override string getDebugString()
		{
			return string.Format("#StatementList\n[\n{0}\n]", indent(getDebugStringForList(List)));
		}

		public override void linkVariables(Method owner)
		{
			foreach (Statement s in List)
				s.linkVariables(owner);
		}
	}

	public class Statement_MethodCall : Statement
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

		public override string getDebugString()
		{
			return string.Format("#MethodCall ({0})\n#Parameter:\n{1}", Identifier, indent(getDebugStringForList(CallParameter)));
		}

		public override void linkVariables(Method owner)
		{
			foreach (Expression e in CallParameter)
				e.linkVariables(owner);
		}
	}

	#region Keywords

	public class Statement_Label : Statement
	{
		public string Identifier;

		public Statement_Label(string id)
		{
			this.Identifier = id;
		}

		public override string getDebugString()
		{
			return string.Format("#LABEL: {0}", Identifier);
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	public class Statement_Goto : Statement
	{
		public string TargetIdentifier;

		public Statement_Goto(string id)
		{
			this.TargetIdentifier = id;
		}

		public override string getDebugString()
		{
			return string.Format("#GOTO: {0}", TargetIdentifier);
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	public class Statement_Return : Statement
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

		public override string getDebugString()
		{
			return string.Format("#RETURN: {0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Value.linkVariables(owner);
		}
	}

	public class Statement_Out : Statement
	{
		public Expression Value;

		public Statement_Out(Expression v)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return string.Format("#OUT: {0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Value.linkVariables(owner);
		}
	}

	public class Statement_In : Statement
	{
		public Expression_ValuePointer ValueTarget;

		public Statement_In(Expression_ValuePointer vt)
		{
			this.ValueTarget = vt;
		}

		public override string getDebugString()
		{
			return string.Format("#OUT: {0}", ValueTarget.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			ValueTarget.linkVariables(owner);
		}
	}

	public class Statement_Quit : Statement
	{
		public Statement_Quit()
		{
		}

		public override string getDebugString()
		{
			return "#QUIT";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	public class Statement_NOP : Statement // Do Nothing
	{
		public Statement_NOP()
		{
		}

		public override string getDebugString()
		{
			return "#NOP";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	#endregion Keywords

	#region Operations

	public class Statement_Inc : Statement
	{
		public Expression_ValuePointer Target;

		public Statement_Inc(Expression_ValuePointer id)
		{
			this.Target = id;
		}

		public override string getDebugString()
		{
			return string.Format("#INC {0}", Target.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
		}
	}

	public class Statement_Dec : Statement
	{
		public Expression_ValuePointer Target;

		public Statement_Dec(Expression_ValuePointer id)
		{
			this.Target = id;
		}

		public override string getDebugString()
		{
			return string.Format("#DEC {0}", Target.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
		}
	}

	public class Statement_Assignment : Statement
	{
		public Expression_ValuePointer Target;
		public Expression Expr;

		public Statement_Assignment(Expression_ValuePointer t, Expression e)
		{
			this.Target = t;
			this.Expr = e;
		}

		public override string getDebugString()
		{
			return string.Format("#ASSIGN {0} = {1}", Target.getDebugString(), Expr.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
			Expr.linkVariables(owner);
		}
	}

	#endregion Operations

	#region Constructs

	public class Statement_If : Statement
	{
		public Expression Condition;
		public Statement Body;
		public Statement Else;

		public Statement_If(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = new Statement_NOP();
		}

		public Statement_If(Expression c, Statement b, Statement e)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = e;
		}

		public override string getDebugString()
		{
			return string.Format("#IF ({0})\n{1}\n#IFELSE\n{2}", Condition.getDebugString(), indent(Body.getDebugString()), Else == null ? "  NULL" : indent(Else.ToString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
			Else.linkVariables(owner);
		}
	}

	public class Statement_While : Statement
	{
		public Expression Condition;
		public Statement Body;

		public Statement_While(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
		}

		public override string getDebugString()
		{
			return string.Format("#WHILE ({0})\n{1}", Condition.getDebugString(), indent(Body.getDebugString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
		}
	}

	public class Statement_RepeatUntil : Statement
	{
		public Expression Condition;
		public Statement Body;

		public Statement_RepeatUntil(Expression c, Statement b)
		{
			this.Condition = c;
			this.Body = b;
		}

		public override string getDebugString()
		{
			return string.Format("#REPEAT-UNTIL ({0})\n{1}", Condition.getDebugString(), indent(Body.getDebugString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
		}
	}

	#endregion Constructs
}