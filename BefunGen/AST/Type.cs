
namespace BefunGen.AST
{
	public abstract class BType : ASTObject
	{
		public BType()
		{
			//--
		}
	}

	public abstract class BType_Value : BType
	{
		public BType_Value()
		{
			//--
		}
	}

	public abstract class BType_Array : BType
	{
		public BType_Array()
		{
			//--
		}
	}

	public class BType_Void : BType // neither Array nor Value ...
	{
		public BType_Void()
		{
			//--
		}

		public override string getDebugString()
		{
			return "void";
		}
	}

	#region Value Types

	public class BType_Int : BType_Value
	{
		public BType_Int()
		{
			//--
		}

		public override string getDebugString()
		{
			return "int";
		}
	}

	public class BType_Digit : BType_Value
	{
		public BType_Digit()
		{
			//--
		}

		public override string getDebugString()
		{
			return "digit";
		}
	}

	public class BType_Char : BType_Value
	{
		public BType_Char()
		{
			//--
		}

		public override string getDebugString()
		{
			return "char";
		}
	}

	public class BType_Bool : BType_Value
	{
		public BType_Bool()
		{
			//--
		}

		public override string getDebugString()
		{
			return "bool";
		}
	}

	#endregion

	#region Array Types

	public class BType_IntArr : BType_Array
	{
		public int Size;

		public BType_IntArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("int[{0}]", Size);
		}
	}

	public class BType_CharArr : BType_Array
	{
		public int Size;

		public BType_CharArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("char[{0}]", Size);
		}
	}

	public class BType_DigitArr : BType_Array
	{
		public int Size;

		public BType_DigitArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("digit[{0}]", Size);
		}
	}

	public class BType_BoolArr : BType_Array
	{
		public int Size;

		public BType_BoolArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("bool[{0}]", Size);
		}
	}

	#endregion
}
