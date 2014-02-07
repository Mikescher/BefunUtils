
namespace BefunGen.AST
{
	abstract class VarDeclaration : ASTObject
	{
		public VarDeclaration()
		{
			//--
		}
	}

	class VarDeclaration_Value : VarDeclaration
	{
		public BType_Value Type;
		public string Identifier;
		public Literal_Value Initial;

		public VarDeclaration_Value(BType_Value t, string id)
		{
			this.Type = t;
			this.Identifier = id;
			this.Initial = null;
		}

		public VarDeclaration_Value(BType_Value t, string id, Literal_Value v)
		{
			this.Type = t;
			this.Identifier = id;
			this.Initial = v;
		}
	}

	class VarDeclaration_Array : VarDeclaration
	{
		public BType_Array Type;
		public string Identifier;
		public Literal_Array Initial;

		public VarDeclaration_Array(BType_Array t, string id)
		{
			this.Type = t;
			this.Identifier = id;
			this.Initial = null;
		}

		public VarDeclaration_Array(BType_Array t, string id, Literal_Array v)
		{
			this.Type = t;
			this.Identifier = id;
			this.Initial = v;
		}
	}
}
