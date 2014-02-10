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

		public VarDeclaration(BType t, string ident, Literal init)
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
			return string.Format("{0} {1}{{{2}}} ::= {3}", Type.getDebugString(), Identifier, ID, Initial == null ? "NULL" : Initial.getDebugString());
		}
	}

	#region Children

	public class VarDeclaration_Value : VarDeclaration
	{
		public VarDeclaration_Value(BType_Value t, string id)
			: base(t, id, null)
		{
		}

		public VarDeclaration_Value(BType_Value t, string id, Literal_Value v)
			: base(t, id, v)
		{
		}
	}

	public class VarDeclaration_Array : VarDeclaration
	{
		public VarDeclaration_Array(BType_Array t, string id)
			: base(t, id, null)
		{
		}

		public VarDeclaration_Array(BType_Array t, string id, Literal_Array v)
			: base(t, id, v)
		{
		}
	}

	#endregion
}