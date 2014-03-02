using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using BefunGen.MathExtensions;
using System;
namespace BefunGen.AST
{
	public abstract class Expression : ASTObject //TODO perhaps increment / decrement expression (prefix && suffix)
	{
		public Expression(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract void linkVariables(Method owner);
		public abstract void linkResultTypes(Method owner);
		public abstract void linkMethods(Program owner);

		public abstract BType getResultType();

		public abstract CodePiece generateCode(bool reversed);
	}

	#region Parents

	public abstract class Expression_Binary : Expression
	{
		public Expression Left;
		public Expression Right;

		public Expression_Binary(SourceCodePosition pos, Expression l, Expression r)
			: base(pos)
		{
			this.Left = l;
			this.Right = r;
		}

		public override void linkVariables(Method owner)
		{
			Left.linkVariables(owner);
			Right.linkVariables(owner);
		}

		public override void linkMethods(Program owner)
		{
			Left.linkMethods(owner);
			Right.linkMethods(owner);
		}
	}

	public abstract class Expression_BinaryMathOperation : Expression_Binary
	{
		public Expression_BinaryMathOperation(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
		}

		public override void linkResultTypes(Method owner)
		{
			Left.linkResultTypes(owner);
			Right.linkResultTypes(owner);

			BType present_L = Left.getResultType();
			BType wanted_L = new BType_Int(Position);

			BType present_R = Right.getResultType();
			BType wanted_R = new BType_Int(Position);

			if (present_L != wanted_L)
			{
				if (present_L.isImplicitCastableTo(wanted_L))
					Left = new Expression_Cast(Position, wanted_L, Left);
				else
					throw new ImplicitCastException(present_L, wanted_L, Position);
			}

			if (present_R != wanted_R)
			{
				if (present_R.isImplicitCastableTo(wanted_R))
					Right = new Expression_Cast(Position, wanted_R, Right);
				else
					throw new ImplicitCastException(present_R, wanted_R, Position);
			}
		}

		public override BType getResultType()
		{
			if (Left.getResultType() != Right.getResultType())
				throw new InvalidASTStateException(Position);

			return Left.getResultType();
		}

		protected CodePiece generateCode_Operands(bool reversed, BefungeCommand cmd)
		{
			CodePiece cp_l = Left.generateCode(reversed);
			CodePiece cp_r = Right.generateCode(reversed);

			if (reversed)
			{
				MathExt.Swap(ref cp_l, ref cp_r); // In Reverse Mode l & r are reversed and then they are reversed added
			}

			if (CodeGenOptions.StripDoubleStringmodeToogle)
			{
				if (cp_l.lastRowIsSingle() && cp_r.firstRowIsSingle() && cp_l[cp_l.MaxX - 1, 0].Type == BefungeCommandType.Stringmode && cp_r[0, 0].Type == BefungeCommandType.Stringmode)
				{
					cp_l.RemoveColumn(cp_l.MaxX - 1);
					cp_r.RemoveColumn(0);
				}
			}

			CodePiece p = CodePiece.CombineHorizontal(cp_l, cp_r);

			if (reversed)
			{
				p.AppendLeft(cmd);
			}
			else
			{
				p.AppendRight(cmd);
			}

			p.normalizeX();

			return p;
		}
	}

	public abstract class Expression_BinaryBoolOperation : Expression_Binary
	{
		public Expression_BinaryBoolOperation(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
		}

		public override void linkResultTypes(Method owner)
		{
			Left.linkResultTypes(owner);
			Right.linkResultTypes(owner);

			BType present_L = Left.getResultType();
			BType wanted_L = new BType_Bool(Position);

			BType present_R = Right.getResultType();
			BType wanted_R = new BType_Bool(Position);

			if (present_L != wanted_L)
			{
				if (present_L.isImplicitCastableTo(wanted_L))
					Left = new Expression_Cast(Position, wanted_L, Left);
				else
					throw new ImplicitCastException(present_L, wanted_L, Position);
			}

			if (present_R != wanted_R)
			{
				if (present_R.isImplicitCastableTo(wanted_R))
					Right = new Expression_Cast(Position, wanted_R, Right);
				else
					throw new ImplicitCastException(present_R, wanted_R, Position);
			}
		}

		public override BType getResultType()
		{
			if (Left.getResultType() != Right.getResultType())
				throw new InvalidASTStateException(Position);

			return Left.getResultType();
		}
	}

	public abstract class Expression_Compare : Expression_Binary
	{
		public Expression_Compare(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override void linkResultTypes(Method owner)
		{
			Left.linkResultTypes(owner);
			Right.linkResultTypes(owner);

			BType present_L = Left.getResultType();

			BType present_R = Right.getResultType();

			if (present_L is BType_Array || present_R is BType_Array)
				throw new InvalidCompareException(present_L, present_R, Position);

			if (present_L != present_R)
			{
				if (present_R.isImplicitCastableTo(present_L) && present_L.isImplicitCastableTo(present_R))
				{
					if (present_R.getPriority() > present_L.getPriority())
						Right = new Expression_Cast(Position, present_L, Right);
					else
						Left = new Expression_Cast(Position, present_R, Left);
				}
				else if (present_R.isImplicitCastableTo(present_L))
				{
					Right = new Expression_Cast(Position, present_L, Right);
				}
				else if (present_L.isImplicitCastableTo(present_R))
				{
					Left = new Expression_Cast(Position, present_R, Left);
				}
				else
				{
					throw new InvalidCompareException(present_L, present_R, Position);
				}
			}
		}

		public override BType getResultType()
		{
			if (Left.getResultType() != Right.getResultType())
				throw new InvalidASTStateException(Position);

			return new BType_Bool(new SourceCodePosition());
		}
	}

	public abstract class Expression_Unary : Expression
	{
		public Expression Expr;

		public Expression_Unary(SourceCodePosition pos, Expression e)
			: base(pos)
		{
			this.Expr = e;
		}

		public override void linkVariables(Method owner)
		{
			Expr.linkVariables(owner);
		}

		public override void linkMethods(Program owner)
		{
			Expr.linkMethods(owner);
		}
	}

	public abstract class Expression_ValuePointer : Expression
	{
		public Expression_ValuePointer(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		// Puts X and Y on the stack: [X, Y]
		public abstract CodePiece generateCodeSingle(bool reversed);
		// Puts X and Y 1 1/2 -times on the stack: [X, X, Y]
		public abstract CodePiece generateCodeDoubleX(bool reversed);
		// Puts Y on the stack: [Y]
		public abstract CodePiece generateCodeSingleY(bool reversed);
	}

	#endregion

	#region ValuePointer

	public class Expression_DirectValuePointer : Expression_ValuePointer
	{
		public string Identifier; // Temporary -- before linking;
		public VarDeclaration Target; // Could also be an array without index

		public Expression_DirectValuePointer(SourceCodePosition pos, string id)
			: base(pos)
		{
			this.Identifier = id;
		}

		public override string getDebugString()
		{
			return Target.getShortDebugString();
		}

		public override void linkVariables(Method owner)
		{
			Target = owner.findVariableByIdentifier(Identifier) as VarDeclaration;

			if (Target == null)
				throw new UnresolvableReferenceException(Identifier, Position);

			Identifier = null;
		}

		public override void linkResultTypes(Method owner)
		{
			// NOP
		}

		public override BType getResultType()
		{
			return Target.Type;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
				p.AppendLeft(BCHelper.Reflect_Get);
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
				p.AppendRight(BCHelper.Reflect_Get);
			}

			p.normalizeX();

			return p;
		}

		// Puts X and Y on the stack: [X, Y]
		public override CodePiece generateCodeSingle(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}

			p.normalizeX();

			return p;
		}

		// Puts X and Y 1 1/2 -times on the stack: [X, X, Y]
		public override CodePiece generateCodeDoubleX(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(BCHelper.Stack_Dup);
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}

			p.normalizeX();

			return p;
		}

		/// Puts Y on the stack: [Y]
		public override CodePiece generateCodeSingleY(bool reversed)
		{
			return NumberCodeHelper.generateCode(Target.CodePositionY, reversed);
		}
	}

	public class Expression_ArrayValuePointer : Expression_ValuePointer
	{
		public string Identifier;
		public VarDeclaration_Array Target;

		public Expression Index;

		public Expression_ArrayValuePointer(SourceCodePosition pos, string id, Expression idx)
			: base(pos)
		{
			this.Identifier = id;
			this.Index = idx;
		}

		public override string getDebugString()
		{
			return Target.getShortDebugString();
		}

		public override void linkVariables(Method owner)
		{
			Index.linkVariables(owner);

			Target = owner.findVariableByIdentifier(Identifier) as VarDeclaration_Array;

			if (Target == null)
				throw new UnresolvableReferenceException(Identifier, Position);
			if (!typeof(BType_Array).IsAssignableFrom(Target.Type.GetType()))
				throw new IndexOperatorNotDefiniedException(Position);

			Identifier = null;
		}

		public override void linkResultTypes(Method owner)
		{
			Index.linkResultTypes(owner);

			BType present = Index.getResultType();
			BType wanted = new BType_Int(Position);

			if (present != wanted)
			{
				if (present.isImplicitCastableTo(wanted))
					Index = new Expression_Cast(Position, wanted, Index);
				else
					throw new ImplicitCastException(present, wanted, Position);
			}
		}

		public override BType getResultType()
		{
			return Target.InternalType;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Index.generateCode(reversed));

				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(BCHelper.Add);
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));

				p.AppendLeft(BCHelper.Reflect_Get);
			}
			else
			{
				p.AppendRight(Index.generateCode(reversed));

				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(BCHelper.Add);
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));

				p.AppendRight(BCHelper.Reflect_Get);
			}

			p.normalizeX();

			return p;
		}

		// This puts X and Y of the var on the stack
		public override CodePiece generateCodeSingle(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Index.generateCode(reversed));

				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(BCHelper.Add);
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}
			else
			{
				p.AppendRight(Index.generateCode(reversed));

				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(BCHelper.Add);
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}

			p.normalizeX();

			return p;
		}

		// Puts X and Y 1 1/2 -times on the stack: [X, X, Y]
		public override CodePiece generateCodeDoubleX(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Index.generateCode(reversed));

				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendLeft(BCHelper.Add);
				p.AppendLeft(BCHelper.Stack_Dup);

				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}
			else
			{
				p.AppendRight(Index.generateCode(reversed));

				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionX, reversed));
				p.AppendRight(BCHelper.Add);
				p.AppendRight(BCHelper.Stack_Dup);

				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePositionY, reversed));
			}

			p.normalizeX();

			return p;
		}

		/// Puts Y on the stack: [Y]
		public override CodePiece generateCodeSingleY(bool reversed)
		{
			return NumberCodeHelper.generateCode(Target.CodePositionY, reversed);
		}
	}

	public class Expression_VoidValuePointer : Expression_ValuePointer
	{
		public Expression_VoidValuePointer(SourceCodePosition pos)
			: base(pos)
		{
		}

		public override string getDebugString()
		{
			return "void";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override BType getResultType()
		{
			return new BType_Void(Position);
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new InvalidASTStateException(Position);
		}

		public override CodePiece generateCodeSingle(bool reversed)
		{
			throw new InvalidASTStateException(Position);
		}

		public override CodePiece generateCodeDoubleX(bool reversed)
		{
			throw new InvalidASTStateException(Position);
		}

		public override CodePiece generateCodeSingleY(bool reversed)
		{
			throw new InvalidASTStateException(Position);
		}
	}

	#endregion ValuePointer

	#region BinaryMathOperation

	public class Expression_Mult : Expression_BinaryMathOperation
	{
		public Expression_Mult(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} * {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = generateCode_Operands(reversed, BCHelper.Mult);

			return p;
		}
	}

	public class Expression_Div : Expression_BinaryMathOperation
	{
		public Expression_Div(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} / {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = generateCode_Operands(reversed, BCHelper.Div);

			return p;
		}
	}

	public class Expression_Mod : Expression_BinaryMathOperation
	{
		public Expression_Mod(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} % {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = generateCode_Operands(reversed, BCHelper.Modulo);

			return p;
		}
	}

	public class Expression_Add : Expression_BinaryMathOperation
	{
		public Expression_Add(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} + {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = generateCode_Operands(reversed, BCHelper.Add);

			return p;
		}
	}

	public class Expression_Sub : Expression_BinaryMathOperation
	{
		public Expression_Sub(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} - {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = generateCode_Operands(reversed, BCHelper.Sub);

			return p;
		}
	}

	#endregion Binary

	#region BinaryBoolOperation

	public class Expression_And : Expression_BinaryBoolOperation
	{
		public Expression_And(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} AND {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				// v  0<
				// v 1_^
				// <  | 
				// ^0$< 
				CodePiece p = new CodePiece();

				p[0, -2] = BCHelper.PC_Down;
				p[1, -2] = BCHelper.Walkway;
				p[2, -2] = BCHelper.Walkway;
				p[3, -2] = BCHelper.Digit_0;
				p[4, -2] = BCHelper.PC_Left;

				p[0, -1] = BCHelper.PC_Down;
				p[1, -1] = BCHelper.Walkway;
				p[2, -1] = BCHelper.Digit_1;
				p[3, -1] = BCHelper.If_Horizontal;
				p[4, -1] = BCHelper.PC_Up;

				p[0, 0] = BCHelper.PC_Left;
				p[1, 0] = BCHelper.Unused;
				p[2, 0] = BCHelper.Unused;
				p[3, 0] = BCHelper.If_Vertical;
				p[4, 0] = BCHelper.Walkway;

				p[0, 1] = BCHelper.PC_Up;
				p[1, 1] = BCHelper.Digit_0;
				p[2, 1] = BCHelper.Stack_Pop;
				p[3, 1] = BCHelper.PC_Left;
				p[4, 1] = BCHelper.Unused;

				p.AppendRight(Right.generateCode(reversed));
				p.AppendRight(Left.generateCode(reversed));

				p.normalizeX();

				return p;
			}
			else
			{
				// >1  v
				// ^_0 v
				//  |  >
				//  >$0^
				CodePiece p = new CodePiece();

				p[0, -2] = BCHelper.PC_Right;
				p[1, -2] = BCHelper.Digit_1;
				p[2, -2] = BCHelper.Walkway;
				p[3, -2] = BCHelper.Walkway;
				p[4, -2] = BCHelper.PC_Down;

				p[0, -1] = BCHelper.PC_Up;
				p[1, -1] = BCHelper.If_Horizontal;
				p[2, -1] = BCHelper.Digit_0;
				p[3, -1] = BCHelper.Walkway;
				p[4, -1] = BCHelper.PC_Down;

				p[0, 0] = BCHelper.Walkway;
				p[1, 0] = BCHelper.If_Vertical;
				p[2, 0] = BCHelper.Unused;
				p[3, 0] = BCHelper.Unused;
				p[4, 0] = BCHelper.PC_Right;

				p[0, 1] = BCHelper.Unused;
				p[1, 1] = BCHelper.PC_Right;
				p[2, 1] = BCHelper.Stack_Pop;
				p[3, 1] = BCHelper.Digit_0;
				p[4, 1] = BCHelper.PC_Up;

				p.AppendLeft(Right.generateCode(reversed));
				p.AppendLeft(Left.generateCode(reversed));

				p.normalizeX();

				return p;
			}
		}
	}

	public class Expression_Or : Expression_BinaryBoolOperation
	{
		public Expression_Or(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} OR {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				// v1$<
				// <  |
				// ^ 1_v
				// ^  0<
				CodePiece p = new CodePiece();

				p[0, -1] = BCHelper.PC_Down;
				p[1, -1] = BCHelper.Digit_1;
				p[2, -1] = BCHelper.Stack_Pop;
				p[3, -1] = BCHelper.PC_Left;
				p[4, -1] = BCHelper.Unused;

				p[0, 0] = BCHelper.PC_Left;
				p[1, 0] = BCHelper.Unused;
				p[2, 0] = BCHelper.Unused;
				p[3, 0] = BCHelper.If_Vertical;
				p[4, 0] = BCHelper.Walkway;

				p[0, 1] = BCHelper.PC_Up;
				p[1, 1] = BCHelper.Walkway;
				p[2, 1] = BCHelper.Digit_1;
				p[3, 1] = BCHelper.If_Horizontal;
				p[4, 1] = BCHelper.PC_Down;

				p[0, 2] = BCHelper.PC_Up;
				p[1, 2] = BCHelper.Walkway;
				p[2, 2] = BCHelper.Walkway;
				p[3, 2] = BCHelper.Digit_0;
				p[4, 2] = BCHelper.PC_Left;

				p.AppendRight(Right.generateCode(reversed));
				p.AppendRight(Left.generateCode(reversed));
				p.normalizeX();

				return p;

			}
			else
			{
				//  >$1v
				//  |  >
				// v_0 ^
				// >1  ^
				CodePiece p = new CodePiece();

				p[0, -1] = BCHelper.Unused;
				p[1, -1] = BCHelper.PC_Right;
				p[2, -1] = BCHelper.Stack_Pop;
				p[3, -1] = BCHelper.Digit_1;
				p[4, -1] = BCHelper.PC_Down;

				p[0, 0] = BCHelper.Walkway;
				p[1, 0] = BCHelper.If_Vertical;
				p[2, 0] = BCHelper.Unused;
				p[3, 0] = BCHelper.Unused;
				p[4, 0] = BCHelper.PC_Right;

				p[0, 1] = BCHelper.PC_Down;
				p[1, 1] = BCHelper.If_Horizontal;
				p[2, 1] = BCHelper.Digit_0;
				p[3, 1] = BCHelper.Walkway;
				p[4, 1] = BCHelper.PC_Up;

				p[0, 2] = BCHelper.PC_Left;
				p[1, 2] = BCHelper.Digit_1;
				p[2, 2] = BCHelper.Walkway;
				p[3, 2] = BCHelper.Walkway;
				p[4, 2] = BCHelper.PC_Up;

				p.AppendLeft(Right.generateCode(reversed));
				p.AppendLeft(Left.generateCode(reversed));
				p.normalizeX();

				return p;
			}
		}
	}

	public class Expression_Xor : Expression_BinaryBoolOperation
	{
		public Expression_Xor(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} XOR {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				// v < 
				//v0_v 
				//<<|##
				//^1_^ 
				// ^ < 
				CodePiece p = new CodePiece();

				p[0, -2] = BCHelper.Unused;
				p[1, -2] = BCHelper.PC_Down;
				p[2, -2] = BCHelper.Walkway;
				p[3, -2] = BCHelper.PC_Left;
				p[4, -2] = BCHelper.Unused;

				p[0, -1] = BCHelper.PC_Down;
				p[1, -1] = BCHelper.Digit_0;
				p[2, -1] = BCHelper.If_Horizontal;
				p[3, -1] = BCHelper.PC_Down;
				p[4, -1] = BCHelper.Unused;

				p[0, 0] = BCHelper.PC_Left;
				p[1, 0] = BCHelper.PC_Left;
				p[2, 0] = BCHelper.If_Vertical;
				p[3, 0] = BCHelper.PC_Jump;
				p[4, 0] = BCHelper.PC_Jump;

				p[0, 1] = BCHelper.PC_Up;
				p[1, 1] = BCHelper.Digit_1;
				p[2, 1] = BCHelper.If_Horizontal;
				p[3, 1] = BCHelper.PC_Up;
				p[4, 1] = BCHelper.Unused;

				p[0, 2] = BCHelper.Unused;
				p[1, 2] = BCHelper.PC_Up;
				p[2, 2] = BCHelper.Walkway;
				p[3, 2] = BCHelper.PC_Left;
				p[4, 2] = BCHelper.Unused;

				p.AppendRight(Right.generateCode(reversed));
				p.AppendRight(Left.generateCode(reversed));
				p.normalizeX();

				return p;
			}
			else
			{
				//  > v 
				//  v_1v
				// ##|>>
				//  ^_0^
				//  > ^
				CodePiece p = new CodePiece();

				p[0, -2] = BCHelper.Unused;
				p[1, -2] = BCHelper.PC_Right;
				p[2, -2] = BCHelper.Walkway;
				p[3, -2] = BCHelper.PC_Down;
				p[4, -2] = BCHelper.Unused;

				p[0, -1] = BCHelper.Unused;
				p[1, -1] = BCHelper.PC_Down;
				p[2, -1] = BCHelper.If_Horizontal;
				p[3, -1] = BCHelper.Digit_1;
				p[4, -1] = BCHelper.PC_Down;

				p[0, 0] = BCHelper.PC_Jump;
				p[1, 0] = BCHelper.PC_Jump;
				p[2, 0] = BCHelper.If_Vertical;
				p[3, 0] = BCHelper.PC_Right;
				p[4, 0] = BCHelper.PC_Right;

				p[0, 1] = BCHelper.Unused;
				p[1, 1] = BCHelper.PC_Up;
				p[2, 1] = BCHelper.If_Horizontal;
				p[3, 1] = BCHelper.Digit_0;
				p[4, 1] = BCHelper.PC_Up;

				p[0, 2] = BCHelper.Unused;
				p[1, 2] = BCHelper.PC_Right;
				p[2, 2] = BCHelper.Walkway;
				p[3, 2] = BCHelper.PC_Up;
				p[4, 2] = BCHelper.Unused;

				p.AppendLeft(Right.generateCode(reversed));
				p.AppendLeft(Left.generateCode(reversed));
				p.normalizeX();

				return p;
			}
		}
	}

	#endregion

	#region Compare

	public class Expression_Equals : Expression_Compare
	{
		public Expression_Equals(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} == {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			//  >0v
			// -| >
			//  >1^
			CodePiece p = new CodePiece();

			p[0, -1] = BCHelper.Unused;
			p[1, -1] = BCHelper.PC_Right;
			p[2, -1] = BCHelper.Digit_0;
			p[3, -1] = BCHelper.PC_Down;

			p[0, 0] = BCHelper.Sub;
			p[1, 0] = BCHelper.If_Vertical;
			p[2, 0] = BCHelper.Unused;
			p[3, 0] = BCHelper.PC_Right;

			p[0, 1] = BCHelper.Unused;
			p[1, 1] = BCHelper.PC_Right;
			p[2, 1] = BCHelper.Digit_1;
			p[3, 1] = BCHelper.PC_Up;

			if (reversed)
			{
				p.reverseX(true);
			}

			if (reversed)
			{
				p.AppendRight(Right.generateCode(reversed));
				p.AppendRight(Left.generateCode(reversed));
			}
			else
			{
				p.AppendLeft(Right.generateCode(reversed));
				p.AppendLeft(Left.generateCode(reversed));
			}

			p.normalizeX();

			return p;
		}
	}

	public class Expression_Unequals : Expression_Compare
	{
		public Expression_Unequals(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} != {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			//  >1v
			// -| >
			//  >0^
			CodePiece p = new CodePiece();

			p[0, -1] = BCHelper.Unused;
			p[1, -1] = BCHelper.PC_Right;
			p[2, -1] = BCHelper.Digit_1;
			p[3, -1] = BCHelper.PC_Down;

			p[0, 0] = BCHelper.Sub;
			p[1, 0] = BCHelper.If_Vertical;
			p[2, 0] = BCHelper.Unused;
			p[3, 0] = BCHelper.PC_Right;

			p[0, 1] = BCHelper.Unused;
			p[1, 1] = BCHelper.PC_Right;
			p[2, 1] = BCHelper.Digit_0;
			p[3, 1] = BCHelper.PC_Up;

			if (reversed)
			{
				p.reverseX(true);
			}

			if (reversed)
			{
				p.AppendRight(Right.generateCode(reversed));
				p.AppendRight(Left.generateCode(reversed));
			}
			else
			{
				p.AppendLeft(Right.generateCode(reversed));
				p.AppendLeft(Left.generateCode(reversed));
			}

			p.normalizeX();

			return p;
		}
	}

	public class Expression_Greater : Expression_Compare
	{
		public Expression_Greater(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} > {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p;

			if (reversed)
			{
				//First Left than Right -->  RIGHT < LEFT
				p = Left.generateCode(reversed);
				p.AppendLeft(Right.generateCode(reversed));

				p.AppendLeft(BCHelper.GreaterThan);
			}
			else
			{
				//First Left than Right -->  RIGHT < LEFT
				p = Left.generateCode(reversed);
				p.AppendRight(Right.generateCode(reversed));

				p.AppendRight(BCHelper.GreaterThan);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Expression_Lesser : Expression_Compare
	{
		public Expression_Lesser(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} < {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p;

			if (reversed)
			{
				//First Right than Left -->  LEFT < RIGHT
				p = Right.generateCode(reversed);
				p.AppendLeft(Left.generateCode(reversed));

				p.AppendLeft(BCHelper.GreaterThan);
			}
			else
			{
				//First Right than Left -->  LEFT < RIGHT
				p = Right.generateCode(reversed);
				p.AppendRight(Left.generateCode(reversed));

				p.AppendRight(BCHelper.GreaterThan);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Expression_GreaterEquals : Expression_Compare
	{
		public Expression_GreaterEquals(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} >= {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				// <1$_v#!:-
				// ^`\0<

				//First Right than Left -->  RIGHT <= LEFT
				CodePiece ep = Right.generateCode(reversed);
				ep.AppendLeft(Left.generateCode(reversed));

				CodePiece p = new CodePiece();
				p[0, 0] = BCHelper.PC_Left;
				p[1, 0] = BCHelper.Digit_1;
				p[2, 0] = BCHelper.Stack_Pop;
				p[3, 0] = BCHelper.If_Horizontal;
				p[4, 0] = BCHelper.PC_Down;
				p[5, 0] = BCHelper.PC_Jump;
				p[6, 0] = BCHelper.Not;
				p[7, 0] = BCHelper.Stack_Dup;
				p[8, 0] = BCHelper.Sub;

				p[0, 1] = BCHelper.PC_Up;
				p[1, 1] = BCHelper.GreaterThan;
				p[2, 1] = BCHelper.Stack_Swap;
				p[3, 1] = BCHelper.Digit_0;
				p[4, 1] = BCHelper.PC_Left;
				p[5, 1] = BCHelper.Unused;
				p[6, 1] = BCHelper.Unused;
				p[7, 1] = BCHelper.Unused;
				p[8, 1] = BCHelper.Unused;

				ep.AppendLeft(p);

				p.normalizeX();

				return ep;
			}
			else
			{
				// -:#v_$1>
				//    >0\`^

				//First Right than Left -->  RIGHT <= LEFT
				CodePiece ep = Right.generateCode(reversed);
				ep.AppendRight(Left.generateCode(reversed));

				CodePiece p = new CodePiece();
				p[0, 0] = BCHelper.Sub;
				p[1, 0] = BCHelper.Stack_Dup;
				p[2, 0] = BCHelper.PC_Jump;
				p[3, 0] = BCHelper.PC_Down;
				p[4, 0] = BCHelper.If_Horizontal;
				p[5, 0] = BCHelper.Stack_Pop;
				p[6, 0] = BCHelper.Digit_1;
				p[7, 0] = BCHelper.PC_Right;

				p[0, 1] = BCHelper.Unused;
				p[1, 1] = BCHelper.Unused;
				p[2, 1] = BCHelper.Unused;
				p[3, 1] = BCHelper.PC_Right;
				p[4, 1] = BCHelper.Digit_0;
				p[5, 1] = BCHelper.Stack_Swap;
				p[6, 1] = BCHelper.GreaterThan;
				p[7, 1] = BCHelper.PC_Up;

				ep.AppendRight(p);

				p.normalizeX();

				return ep;
			}
		}
	}

	public class Expression_LesserEquals : Expression_Compare
	{
		public Expression_LesserEquals(SourceCodePosition pos, Expression l, Expression r)
			: base(pos, l, r)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("({0} <= {1})", Left.getDebugString(), Right.getDebugString());
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				// <1$_v#!:-
				// ^`\0<

				//First Left than Right -->  LEFT <= RIGHT
				CodePiece ep = Left.generateCode(reversed);
				ep.AppendLeft(Right.generateCode(reversed));

				CodePiece p = new CodePiece();
				p[0, 0] = BCHelper.PC_Left;
				p[1, 0] = BCHelper.Digit_1;
				p[2, 0] = BCHelper.Stack_Pop;
				p[3, 0] = BCHelper.If_Horizontal;
				p[4, 0] = BCHelper.PC_Down;
				p[5, 0] = BCHelper.PC_Jump;
				p[6, 0] = BCHelper.Not;
				p[7, 0] = BCHelper.Stack_Dup;
				p[8, 0] = BCHelper.Sub;

				p[0, 1] = BCHelper.PC_Up;
				p[1, 1] = BCHelper.GreaterThan;
				p[2, 1] = BCHelper.Stack_Swap;
				p[3, 1] = BCHelper.Digit_0;
				p[4, 1] = BCHelper.PC_Left;
				p[5, 1] = BCHelper.Unused;
				p[6, 1] = BCHelper.Unused;
				p[7, 1] = BCHelper.Unused;
				p[8, 1] = BCHelper.Unused;

				ep.AppendLeft(p);

				p.normalizeX();

				return ep;
			}
			else
			{
				// -:#v_$1>
				//    >0\`^

				//First Left than Right -->  LEFT <= RIGHT
				CodePiece ep = Left.generateCode(reversed);
				ep.AppendRight(Right.generateCode(reversed));

				CodePiece p = new CodePiece();
				p[0, 0] = BCHelper.Sub;
				p[1, 0] = BCHelper.Stack_Dup;
				p[2, 0] = BCHelper.PC_Jump;
				p[3, 0] = BCHelper.PC_Down;
				p[4, 0] = BCHelper.If_Horizontal;
				p[5, 0] = BCHelper.Stack_Pop;
				p[6, 0] = BCHelper.Digit_1;
				p[7, 0] = BCHelper.PC_Right;

				p[0, 1] = BCHelper.Unused;
				p[1, 1] = BCHelper.Unused;
				p[2, 1] = BCHelper.Unused;
				p[3, 1] = BCHelper.PC_Right;
				p[4, 1] = BCHelper.Digit_0;
				p[5, 1] = BCHelper.Stack_Swap;
				p[6, 1] = BCHelper.GreaterThan;
				p[7, 1] = BCHelper.PC_Up;

				ep.AppendRight(p);

				p.normalizeX();

				return ep;
			}
		}
	}

	#endregion Compare

	#region Unary

	public class Expression_Not : Expression_Unary
	{
		public Expression_Not(SourceCodePosition pos, Expression e)
			: base(pos, e)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("(! {1})", Expr.getDebugString());
		}

		public override void linkResultTypes(Method owner)
		{
			Expr.linkResultTypes(owner);

			if (!(Expr.getResultType() is BType_Bool))
				throw new ImplicitCastException(Expr.getResultType(), new BType_Bool(Position), Position);
		}

		public override BType getResultType()
		{
			return new BType_Bool(Position);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = Expr.generateCode(reversed);

			if (reversed)
			{
				p.AppendLeft(BCHelper.Not);
			}
			else
			{
				p.AppendRight(BCHelper.Not);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Expression_Negate : Expression_Unary
	{
		public Expression_Negate(SourceCodePosition pos, Expression e)
			: base(pos, e)
		{
			//--
		}

		public override string getDebugString()
		{
			return string.Format("(- {1})", Expr.getDebugString());
		}

		public override void linkResultTypes(Method owner)
		{
			Expr.linkResultTypes(owner);

			if (!(Expr.getResultType() is BType_Int))
				throw new ImplicitCastException(Expr.getResultType(), new BType_Bool(Position), Position);
		}

		public override BType getResultType()
		{
			return new BType_Int(Position);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = Expr.generateCode(reversed);

			if (reversed)
			{
				p.AppendRight(BCHelper.Digit_0);
				p.AppendLeft(BCHelper.Sub);
			}
			else
			{
				p.AppendLeft(BCHelper.Digit_0);
				p.AppendRight(BCHelper.Sub);
			}

			p.normalizeX();

			return p;
		}
	}

	#endregion Unary

	#region Other

	public class Expression_Literal : Expression
	{
		public Literal Value;

		public Expression_Literal(SourceCodePosition pos, Literal l)
			: base(pos)
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

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override BType getResultType()
		{
			return Value.getBType();
		}

		public override CodePiece generateCode(bool reversed)
		{
			return Value.generateCode(reversed);
		}
	}

	public class Expression_Rand : Expression
	{
		public Expression_Rand(SourceCodePosition pos)
			: base(pos)
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

		public override void linkResultTypes(Method owner)
		{
			// NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override BType getResultType()
		{
			return new BType_Bool(Position);
		}

		public override CodePiece generateCode(bool reversed)
		{
			//  >>1v
			// #^?0>>
			//   > 0^

			CodePiece p = new CodePiece();

			p[0, -1] = BCHelper.Unused;
			p[1, -1] = BCHelper.PC_Right;
			p[2, -1] = BCHelper.PC_Right;
			p[3, -1] = BCHelper.Digit_1;
			p[4, -1] = BCHelper.PC_Down;
			p[5, -1] = BCHelper.Unused;

			p[0, 0] = BCHelper.PC_Jump;
			p[1, 0] = BCHelper.PC_Up;
			p[2, 0] = BCHelper.PC_Random;
			p[3, 0] = BCHelper.Digit_0;
			p[4, 0] = BCHelper.PC_Right;
			p[5, 0] = BCHelper.PC_Right;

			p[0, 1] = BCHelper.Unused;
			p[1, 1] = BCHelper.Unused;
			p[2, 1] = BCHelper.PC_Right;
			p[3, 1] = BCHelper.Walkway;
			p[4, 1] = BCHelper.Digit_0;
			p[5, 1] = BCHelper.PC_Up;

			if (reversed)
			{
				p.reverseX(true);
			}

			return p;
		}
	}

	public class Expression_Cast : Expression
	{
		public BType Type;
		public Expression Expr;

		public Expression_Cast(SourceCodePosition pos, BType t, Expression e)
			: base(pos)
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

		public override void linkResultTypes(Method owner)
		{
			Expr.linkResultTypes(owner);

		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override BType getResultType()
		{
			return Type;
		}

		public override CodePiece generateCode(bool reversed)
		{
			return Expr.generateCode(reversed); //TODO Perhaps Cast bools to real 0|1 bools (perhaps CodeGenOption)
		}
	}

	public class Expression_FunctionCall : Expression
	{
		public Statement_MethodCall Method;

		public Expression_FunctionCall(SourceCodePosition pos, Statement_MethodCall mc)
			: base(pos)
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

		public override void linkResultTypes(Method owner)
		{
			Method.linkResultTypes(owner);
		}

		public override void linkMethods(Program owner)
		{
			Method.linkMethods(owner);
		}

		public override BType getResultType()
		{
			return Method.Target.ResultType;
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new NotImplementedException(); //TODO Implement
		}
	}

	#endregion Other
}