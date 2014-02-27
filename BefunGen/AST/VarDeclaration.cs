using BefunGen.AST.CodeGen;
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

		public int _CodePositionX = -1;
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

		public int _CodePositionY = -1;
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
		public abstract CodePiece generateCode(bool reverse);
		public abstract CodePiece generateCode_Parameter(bool reverse);
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

		public override CodePiece generateCode(bool reverse)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX;
			int varY = CodePositionY;

			if (reverse)
			{
				p.AppendLeft((Initial as Literal_Value).generateCode(reverse));
				p.AppendLeft(NumberCodeHelper.generateCode(varX, reverse));
				p.AppendLeft(NumberCodeHelper.generateCode(varY, reverse));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight((Initial as Literal_Value).generateCode(reverse));
				p.AppendRight(NumberCodeHelper.generateCode(varX, reverse));
				p.AppendRight(NumberCodeHelper.generateCode(varY, reverse));
				p.AppendRight(BCHelper.Reflect_Set);
			}

			p.normalizeX();

			return p;
		}

		public override CodePiece generateCode_Parameter(bool reverse)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX;
			int varY = CodePositionY;

			if (reverse)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(varX, reverse));
				p.AppendLeft(NumberCodeHelper.generateCode(varY, reverse));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(varX, reverse));
				p.AppendRight(NumberCodeHelper.generateCode(varY, reverse));
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

		public override CodePiece generateCode(bool reverse)
		{
			CodePiece p = new CodePiece();

			Literal_Array value = Initial as Literal_Array;

			int varX = CodePositionX - 1;
			int varY = CodePositionY;

			if (reverse)
			{
				p.AppendLeft(BCHelper.Digit_0);

				for (int i = 0; i < Size; i++)
				{
					p.AppendLeft(value.generateCode(i, true));
					p.AppendLeft(NumberCodeHelper.generateCode(i + 1, true));
				}

				// ################################

				//   >       v
				// $_^#!:pY+X<
				CodePiece op = new CodePiece();

				op.AppendLeft(BCHelper.PC_Left);
				op.AppendLeft(NumberCodeHelper.generateCode(varX, reverse));
				op.AppendLeft(BCHelper.Add);
				op.AppendLeft(NumberCodeHelper.generateCode(varY, reverse));
				op.AppendLeft(BCHelper.Reflect_Set);
				op.AppendLeft(BCHelper.Stack_Dup);
				op.AppendLeft(BCHelper.Not);
				op.AppendLeft(BCHelper.PC_Jump);
				op.AppendLeft(BCHelper.PC_Up);

				op[-1, -1] = BCHelper.PC_Down;
				op.Fill(op.MinX + 1, -1, -1, 0, BCHelper.Walkway);
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
					p.AppendRight(value.generateCode(i));
					p.AppendRight(NumberCodeHelper.generateCode(i + 1));
				}

				// ################################

				// >X+Yp:#v_$
				// ^      <
				CodePiece op = new CodePiece();

				op.AppendRight(BCHelper.PC_Right);
				op.AppendRight(NumberCodeHelper.generateCode(varX, reverse));
				op.AppendRight(BCHelper.Add);
				op.AppendRight(NumberCodeHelper.generateCode(varY, reverse));
				op.AppendRight(BCHelper.Reflect_Set);
				op.AppendRight(BCHelper.Stack_Dup);
				op.AppendRight(BCHelper.PC_Jump);
				op.AppendRight(BCHelper.PC_Down);

				op[0, 1] = BCHelper.PC_Up;
				op.Fill(1, 1, op.MaxX - 1, 2, BCHelper.Walkway);
				op[op.MaxX - 1, 1] = BCHelper.PC_Left;

				op.AppendRight(BCHelper.If_Horizontal);
				op.AppendRight(BCHelper.Stack_Pop);

				// ################################

				p.AppendRight(op);
			}

			return p;
		}

		public override CodePiece generateCode_Parameter(bool reverse)
		{
			CodePiece p = new CodePiece();

			int varX = CodePositionX;
			int varY = CodePositionY;

			for (int pos = 0; pos < Size; pos++)
			{
				if (reverse)
				{
					p.AppendLeft(NumberCodeHelper.generateCode(varX+pos, reverse));
					p.AppendLeft(NumberCodeHelper.generateCode(varY, reverse));
					p.AppendLeft(BCHelper.Reflect_Set);
				}
				else
				{
					p.AppendRight(NumberCodeHelper.generateCode(varX+pos, reverse));
					p.AppendRight(NumberCodeHelper.generateCode(varY, reverse));
					p.AppendRight(BCHelper.Reflect_Set);
				}
			}

			p.normalizeX();

			return p;
		}
	}

	#endregion
}