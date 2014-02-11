using BefunGen.AST.Exceptions;
using System.Linq;
namespace BefunGen.AST
{
	public abstract class BType : ASTObject
	{
		public BType()
		{
			//--
		}

		public abstract Literal getDefaultValue();
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
		public int Size;

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

		public override Literal getDefaultValue()
		{
			throw new VoidObjectCallException();
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

		public override Literal getDefaultValue()
		{
			return new Literal_Int(0);
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

		public override Literal getDefaultValue()
		{
			return new Literal_Digit(0);
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

		public override Literal getDefaultValue()
		{
			return new Literal_Char('0');
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

		public override Literal getDefaultValue()
		{
			return new Literal_Bool(false);
		}
	}

	#endregion Value Types

	#region Array Types

	public class BType_IntArr : BType_Array
	{
		public BType_IntArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("int[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_IntArr(Enumerable.Repeat(0, Size).ToList());
		}
	}

	public class BType_CharArr : BType_Array
	{
		public BType_CharArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("char[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_CharArr(Enumerable.Repeat('0', Size).ToList());
		}
	}

	public class BType_DigitArr : BType_Array
	{
		public BType_DigitArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("digit[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_DigitArr(Enumerable.Repeat((byte)0, Size).ToList());
		}
	}

	public class BType_BoolArr : BType_Array
	{
		public BType_BoolArr(int sz)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("bool[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_IntArr(Enumerable.Repeat(0, Size).ToList());
		}
	}

	#endregion Array Types
}