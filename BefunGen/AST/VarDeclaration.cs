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
	}

	#endregion
}