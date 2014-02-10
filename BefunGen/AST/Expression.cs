﻿using BefunGen.AST.Exceptions;
namespace BefunGen.AST
{
	public abstract class Expression : ASTObject
	{
		public Expression()
		{
			//--
		}

		public abstract void linkVariables(Method owner);
	}

	public abstract class Expression_Binary : Expression
	{
		public Expression Left;
		public Expression Right;

		public Expression_Binary(Expression l, Expression r)
		{
			this.Left = l;
			this.Right = r;
		}

		public override void linkVariables(Method owner)
		{
			Left.linkVariables(owner);
			Right.linkVariables(owner);
		}
	}

	public abstract class Expression_Compare : Expression_Binary
	{
		public Expression_Compare(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}
	}

	public abstract class Expression_Unary : Expression
	{
		public Expression Expr;

		public Expression_Unary(Expression e)
		{
			this.Expr = e;
		}

		public override void linkVariables(Method owner)
		{
			Expr.linkVariables(owner);
		}
	}

	public abstract class Expression_ValuePointer : Expression
	{
		public Expression_ValuePointer()
		{
			//--
		}
	}

	#region ValuePointer

	public class Expression_DirectValuePointer : Expression_ValuePointer
	{
		public string Identifier; // Temporary -- before linking;
		public VarDeclaration_Value Target;

		public Expression_DirectValuePointer(string id)
		{
			this.Identifier = id;
		}

		public override string getDebugString()
		{
			return Target.getDebugString();
		}

		public override void linkVariables(Method owner)
		{
			Target = owner.findVariableByIdentifier(Identifier) as VarDeclaration_Value;

			if (Target == null)
				throw new UnresolvableReferenceException(Identifier);

			Identifier = null;
		}
	}

	public class Expression_ArrayValuePointer : Expression_ValuePointer
	{
		public string Identifier;
		public VarDeclaration_Array Target;

		public Expression Index;

		public Expression_ArrayValuePointer(string id, Expression idx)
		{
			this.Identifier = id;
			this.Index = idx;
		}

		public override string getDebugString()
		{
			return Target.getDebugString();
		}

		public override void linkVariables(Method owner)
		{
			Target = owner.findVariableByIdentifier(Identifier) as VarDeclaration_Array;

			if (Target == null)
				throw new UnresolvableReferenceException(Identifier);
			if (!typeof(BType_Array).IsAssignableFrom(Target.Type.GetType()))
				throw new IndexOperatorNotDefiniedException();

			Identifier = null;
		}
	}

	#endregion ValuePointer

	#region Binary

	public class Expression_Mult : Expression_Binary
	{
		public Expression_Mult(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} * {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Div : Expression_Binary
	{
		public Expression_Div(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} / {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Mod : Expression_Binary
	{
		public Expression_Mod(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} % {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Add : Expression_Binary
	{
		public Expression_Add(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} + {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Sub : Expression_Binary
	{
		public Expression_Sub(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} - {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	#endregion Binary

	#region Compare

	public class Expression_Equals : Expression_Compare
	{
		public Expression_Equals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} == {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Unequals : Expression_Compare
	{
		public Expression_Unequals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} != {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Greater : Expression_Compare
	{
		public Expression_Greater(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} > {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_Lesser : Expression_Compare
	{
		public Expression_Lesser(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} < {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_GreaterEquals : Expression_Compare
	{
		public Expression_GreaterEquals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} >= {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	public class Expression_LesserEquals : Expression_Compare
	{
		public Expression_LesserEquals(Expression l, Expression r)
			: base(l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} <= {1})", Left.getDebugString(), Right.getDebugString());
		}
	}

	#endregion Compare

	#region Unary

	public class Expression_Not : Expression_Unary
	{
		public Expression_Not(Expression e)
			: base(e)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("(! {1})", Expr.getDebugString());
		}
	}

	public class Expression_Negate : Expression_Unary
	{
		public Expression_Negate(Expression e)
			: base(e)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("(- {1})", Expr.getDebugString());
		}
	}

	#endregion Unary

	#region Other

	public class Expression_Literal : Expression
	{
		public Literal Value;

		public Expression_Literal(Literal l)
		{
			this.Value = l;
		}

		public override string getDebugString()
		{
			return string.Format("{0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	public class Expression_Rand : Expression
	{
		public Expression_Rand()
		{
			//--
		}

		public override string getDebugString()
		{
			return "#RAND#";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}
	}

	public class Expression_Cast : Expression
	{
		private BType Type;
		private Expression Expr;

		public Expression_Cast(BType t, Expression e)
		{
			this.Type = t;
			this.Expr = e;
		}

		public override string getDebugString()
		{
			return string.Format("(({0}){1})", Type.getDebugString(), Expr.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Expr.linkVariables(owner);
		}
	}

	public class Expression_FunctionCall : Expression
	{
		public Statement_MethodCall Method;

		public Expression_FunctionCall(Statement_MethodCall mc)
		{
			this.Method = mc;
		}

		public override string getDebugString()
		{
			return Method.getDebugString();
		}

		public override void linkVariables(Method owner)
		{
			Method.linkVariables(owner);
		}
	}

	#endregion Other
}