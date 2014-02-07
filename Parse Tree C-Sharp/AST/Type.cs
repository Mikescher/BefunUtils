
namespace BefunGen.AST
{
	abstract class BType : ASTObject
	{
		public BType()
		{
			//--
		}
	}

	abstract class BType_Value : BType
	{
		public BType_Value()
		{
			//--
		}
	}

	abstract class BType_Array : BType
	{
		public BType_Array()
		{
			//--
		}
	}

	class BType_Void : BType // neither Array nor Value ...
	{
		public BType_Void()
		{
			//--
		}
	}

	#region Value Types

	class BType_Int : BType_Value
	{
		public BType_Int()
		{
			//--
		}
	}

	class BType_Digit : BType_Value
	{
		public BType_Digit()
		{
			//--
		}
	}

	class BType_Char : BType_Value
	{
		public BType_Char()
		{
			//--
		}
	}

	class BType_Bool : BType_Value
	{
		public BType_Bool()
		{
			//--
		}
	}

	#endregion

	#region Array Types

	class BType_IntArr : BType_Array
	{
		public int Size;

		public BType_IntArr(int sz)
		{
			this.Size = sz;
		}
	}

	class BType_CharArr : BType_Array
	{
		public int Size;

		public BType_CharArr(int sz)
		{
			this.Size = sz;
		}
	}

	class BType_DigitArr : BType_Array
	{
		public int Size;

		public BType_DigitArr(int sz)
		{
			this.Size = sz;
		}
	}

	class BType_BoolArr : BType_Array
	{
		public int Size;

		public BType_BoolArr(int sz)
		{
			this.Size = sz;
		}
	}

	#endregion
}
