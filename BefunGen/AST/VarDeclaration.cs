using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using BefunGen.AST.Exceptions;
namespace BefunGen.AST
{
	public abstract class VarDeclaration : ASTObject
	{
		private static int _V_ID_COUNTER = 100;
		protected static int V_ID_COUNTER { get { return _V_ID_COUNTER++; } }

		public readonly BType Type;
		public readonly string Identifier;
		public readonly Literal Initial;
		public readonly int ID;

		private int _CodePositionX = -1;
		public int CodePositionX
		{
			get
			{
				if (_CodePositionX < 0)
					throw new InternalCodeGenException();
				else
					return _CodePositionX;
			}

			set { _CodePositionX = value; }
		}

		private int _CodePositionY = -1;
		public int CodePositionY
		{
			get
			{
				if (_CodePositionY < 0)
					throw new InternalCodeGenException();
				else
					return _CodePositionY;
			}

			set { _CodePositionY = value; }
		}

		public VarDeclaration(SourceCodePosition pos, BType t, string ident, Literal init)
			: base(pos)
		{
			this.Type = t;
			this.Identifier = ident;
			this.ID = V_ID_COUNTER;

			if (init == null)
			{
				this.Initial = t.getDefaultValue();
			}
			else
			{
				this.Initial = init;
			}
		}

		public override string getDebugString()
		{
			return string.Format("{0} {{{1}}} ::= {2}", Type.getDebugString(), ID, Initial == null ? "NULL" : Initial.getDebugString());
		}

		public string getShortDebugString()
		{
			return string.Format("{{{0}}}", ID);
		}

		public static void resetCounter()
		{
			_V_ID_COUNTER = 1;
		}

		// Code for Variable Initialization
		public abstract CodePiece generateCode(bool reversed);
		public abstract CodePiece generateCode_SetToStackVal(bool reversed);
	}

	#region Children

	public class VarDeclaration_Value : VarDeclaration
	{
		public VarDeclaration_Value(SourceCodePosition pos, BType_Value t, string id)
			: base(pos, t, id, null)
		{
		}

		public VarDeclaration_Value(SourceCodePosition pos, BType_Value t, string id, Literal_Value v)
			: base(pos, t, id, v)
		{
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX;
			int varY = CodePositionY;

			if (reversed)
			{
				p.AppendLeft((Initial as Literal_Value).generateCode(reversed));
				p.AppendLeft(NumberCodeHelper.generateCode(varX, reversed));
				p.AppendLeft(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight((Initial as Literal_Value).generateCode(reversed));
				p.AppendRight(NumberCodeHelper.generateCode(varX, reversed));
				p.AppendRight(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendRight(BCHelper.Reflect_Set);
			}

			p.normalizeX();

			return p;
		}

		public override CodePiece generateCode_SetToStackVal(bool reversed)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX;
			int varY = CodePositionY;

			if (reversed)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(varX, reversed));
				p.AppendLeft(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(varX, reversed));
				p.AppendRight(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendRight(BCHelper.Reflect_Set);
			}

			p.normalizeX();

			return p;
		}
	}

	public class VarDeclaration_Array : VarDeclaration
	{
		public BType_Value InternalType { get { return (Type as BType_Array).InternalType; } }

		public int Size { get { return (Type as BType_Array).Size; } }

		public VarDeclaration_Array(SourceCodePosition pos, BType_Array t, string id)
			: base(pos, t, id, null)
		{
			if (Size < 2)
			{
				throw new ArrayTooSmallException(Position);
			}
		}

		public VarDeclaration_Array(SourceCodePosition pos, BType_Array t, string id, Literal_Array v)
			: base(pos, t, id, v)
		{
			int LiteralSize = ((Literal_Array)Initial).Count;

			if (LiteralSize > t.Size)
			{
				throw new ArrayLiteralTooBigException(pos);
			}
			else if (LiteralSize < t.Size)
			{
				((Literal_Array)Initial).AppendDefaultValues(t.Size - LiteralSize);
			}
		}

		public override CodePiece generateCode(bool reversed)
		{
			Literal_Array value = Initial as Literal_Array;

			if (value.isUniform())
			{
				return generateCode_Uniform(reversed, value);
			}
			else
			{
				return generateCode_Universal(reversed, value);
			}
		}

		private CodePiece generateCode_Uniform(bool reversed, Literal_Array value)
		{
			int varX_start = CodePositionX;
			int varX_end = CodePositionX + Size - 1;
			int varY = CodePositionY;

			if (reversed)
			{
				// $_v#!`\{X2}:p{Y}\{V}:<{X1}
				//   >1+                ^
				CodePiece p = new CodePiece();

				p.AppendRight(BCHelper.Stack_Pop);
				p.AppendRight(BCHelper.If_Horizontal);

				int bot_end = p.MaxX;

				p.AppendRight(BCHelper.PC_Down);
				p.AppendRight(BCHelper.PC_Jump);
				p.AppendRight(BCHelper.Not);
				p.AppendRight(BCHelper.GreaterThan);
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(NumberCodeHelper.generateCode(varX_end, reversed));
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(BCHelper.Reflect_Set);
				p.AppendRight(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(value.generateCode(0, reversed));
				p.AppendRight(BCHelper.Stack_Dup);

				int bot_start = p.MaxX;

				p.AppendRight(BCHelper.PC_Right);
				p.AppendRight(NumberCodeHelper.generateCode(varX_start, reversed));

				p[bot_start + 0, 1] = BCHelper.PC_Right;
				p[bot_start + 1, 1] = BCHelper.Digit_1;
				p[bot_start + 2, 1] = BCHelper.Add;

				p.FillRowWW(1, bot_start + 3, bot_end);

				p[bot_end, 1] = BCHelper.PC_Up;

				return p;
			}
			else
			{
				// {X1}>:{V}\{Y}p:{X2}\`#v_$
				//     ^+1               < 
				CodePiece p = new CodePiece();

				p.AppendRight(NumberCodeHelper.generateCode(varX_start, reversed));

				int bot_start = p.MaxX;

				p.AppendRight(BCHelper.PC_Right);
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(value.generateCode(0, reversed));
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(NumberCodeHelper.generateCode(varY, reversed));
				p.AppendRight(BCHelper.Reflect_Set);
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(NumberCodeHelper.generateCode(varX_end, reversed));
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(BCHelper.GreaterThan);
				p.AppendRight(BCHelper.PC_Jump);

				int bot_end = p.MaxX;

				p.AppendRight(BCHelper.PC_Down);
				p.AppendRight(BCHelper.If_Horizontal);
				p.AppendRight(BCHelper.Stack_Pop);

				p[bot_start + 0, 1] = BCHelper.PC_Up;
				p[bot_start + 1, 1] = BCHelper.Add;
				p[bot_start + 2, 1] = BCHelper.Digit_1;

				p.FillRowWW(1, bot_start + 3, bot_end);

				p[bot_end, 1] = BCHelper.PC_Left;

				return p;
			}
		}

		private CodePiece generateCode_Universal(bool reversed, Literal_Array value)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX - 1;
			int varY = CodePositionY;

			if (reversed)
			{
				p.AppendLeft(BCHelper.Digit_0);

				for (int i = 0; i < Size; i++)
				{
					p.AppendLeft(value.generateCode(i, reversed));
					p.AppendLeft(NumberCodeHelper.generateCode(i + 1, reversed));
				}

				// ################################

				//   >       v
				// $_^#!:pY+X<
				CodePiece op = new CodePiece();

				op.AppendLeft(BCHelper.PC_Left);
				op.AppendLeft(NumberCodeHelper.generateCode(varX, reversed));
				op.AppendLeft(BCHelper.Add);
				op.AppendLeft(NumberCodeHelper.generateCode(varY, reversed));
				op.AppendLeft(BCHelper.Reflect_Set);
				op.AppendLeft(BCHelper.Stack_Dup);
				op.AppendLeft(BCHelper.Not);
				op.AppendLeft(BCHelper.PC_Jump);
				op.AppendLeft(BCHelper.PC_Up);

				op[-1, -1] = BCHelper.PC_Down;
				op.FillRowWW(-1, op.MinX + 1, -1);
				op[op.MinX, -1] = BCHelper.PC_Right;

				op.AppendLeft(BCHelper.If_Horizontal);
				op.AppendLeft(BCHelper.Stack_Pop);

				// ################################

				p.AppendLeft(op);
			}
			else
			{
				p.AppendRight(BCHelper.Digit_0);

				for (int i = 0; i < Size; i++)
				{
					p.AppendRight(value.generateCode(i, reversed));
					p.AppendRight(NumberCodeHelper.generateCode(i + 1, reversed));
				}

				// ################################

				// >X+Yp:#v_$
				// ^      <
				CodePiece op = new CodePiece();

				op.AppendRight(BCHelper.PC_Right);
				op.AppendRight(NumberCodeHelper.generateCode(varX, reversed));
				op.AppendRight(BCHelper.Add);
				op.AppendRight(NumberCodeHelper.generateCode(varY, reversed));
				op.AppendRight(BCHelper.Reflect_Set);
				op.AppendRight(BCHelper.Stack_Dup);
				op.AppendRight(BCHelper.PC_Jump);
				op.AppendRight(BCHelper.PC_Down);

				op[0, 1] = BCHelper.PC_Up;
				op.FillRowWW(1, 1, op.MaxX - 1);
				op[op.MaxX - 1, 1] = BCHelper.PC_Left;

				op.AppendRight(BCHelper.If_Horizontal);
				op.AppendRight(BCHelper.Stack_Pop);

				// ################################

				p.AppendRight(op);
			}

			return p;
		}

		public override CodePiece generateCode_SetToStackVal(bool reversed)
		{
			return CodePieceStore.WriteArrayFromStack(this, reversed);
		}
	}

	#endregion
}