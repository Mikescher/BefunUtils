
namespace BefunGen.AST
{
	abstract class Expression : ASTObject
	{
		public Expression()
		{
			//--
		}
	}

	abstract class Expression_Binary : Expression
	{
		public Expression Left;
		public Expression Right;

		public Expression_Binary(Expression l, Expression r)
		{
			this.Left = l;
			this.Right = r;
		}
	}

	abstract class Expression_Compare : Expression_Binary
	{
		public Expression_Compare(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	abstract class Expression_Unary : Expression
	{
		public Expression Expr;

		public Expression_Unary(Expression e)
		{
			this.Expr = e;
		}
	}

	abstract class Expression_ValuePointer : Expression
	{
		public Expression_ValuePointer()
		{
			//--
		}
	}

	#region ValuePointer

	class Expression_DirectValuePointer : Expression_ValuePointer
	{
		public string Identifier;

		public Expression_DirectValuePointer(string id)
		{
			this.Identifier = id;
		}
	}

	class Expression_ArrayValuePointer : Expression_ValuePointer
	{
		public string Identifier;
		public Expression Index;

		public Expression_ArrayValuePointer(string id, Expression idx)
		{
			this.Identifier = id;
			this.Index = idx;
		}
	}

	#endregion

	#region Binary

	class Expression_Mult : Expression_Binary
	{
		public Expression_Mult(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Div : Expression_Binary
	{
		public Expression_Div(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Mod : Expression_Binary
	{
		public Expression_Mod(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Add : Expression_Binary
	{
		public Expression_Add(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Sub : Expression_Binary
	{
		public Expression_Sub(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	#endregion

	#region Compare

	class Expression_Equals : Expression_Compare
	{
		public Expression_Equals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Unequals : Expression_Compare
	{
		public Expression_Unequals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Greater : Expression_Compare
	{
		public Expression_Greater(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_Lesser : Expression_Compare
	{
		public Expression_Lesser(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_GreaterEquals : Expression_Compare
	{
		public Expression_GreaterEquals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	class Expression_LesserEquals : Expression_Compare
	{
		public Expression_LesserEquals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	#endregion

	#region Unary

	class Expression_Not : Expression_Unary
	{
		public Expression_Not(Expression e)
			: base(e)
		{
			//--
		}
	}

	class Expression_Negate : Expression_Unary
	{
		public Expression_Negate(Expression e)
			: base(e)
		{
			//--
		}
	}

	#endregion

	#region Other

	class Expression_Literal : Expression
	{
		public Expression_Literal(Literal l)
		{
			//--
		}
	}

	class Expression_Rand : Expression
	{
		public Expression_Rand()
		{
			//--
		}
	}

	class Expression_Cast : Expression
	{
		BType Type;
		Expression Expr;

		public Expression_Cast(BType t, Expression e)
		{
			this.Type = t;
			this.Expr = e;
		}
	}

	class Expression_FunctionCall : Expression
	{
		public Statement_MethodCall Method;

		public Expression_FunctionCall(Statement_MethodCall mc)
		{
			this.Method = mc;
		}
	}

	#endregion
}
