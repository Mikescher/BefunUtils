using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System.Linq;
namespace BefunGen.AST
{
	public abstract class BType : ASTObject
	{
		public BType(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract Literal getDefaultValue();
	}

	public abstract class BType_Value : BType
	{
		public BType_Value(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}
	}

	public abstract class BType_Array : BType
	{
		public int Size;

		public BType_Array(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}
	}

	public class BType_Void : BType // neither Array nor Value ...
	{
		public BType_Void(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override string getDebugString()
		{
			return "void";
		}

		public override Literal getDefaultValue()
		{
			throw new VoidObjectCallException(Position);
		}
	}

	#region Value Types

	public class BType_Int : BType_Value
	{
		public BType_Int(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override string getDebugString()
		{
			return "int";
		}

		public override Literal getDefaultValue()
		{
			return new Literal_Int(new SourceCodePosition(), 0);
		}
	}

	public class BType_Digit : BType_Value
	{
		public BType_Digit(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override string getDebugString()
		{
			return "digit";
		}

		public override Literal getDefaultValue()
		{
			return new Literal_Digit(new SourceCodePosition(), 0);
		}
	}

	public class BType_Char : BType_Value
	{
		public BType_Char(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override string getDebugString()
		{
			return "char";
		}

		public override Literal getDefaultValue()
		{
			return new Literal_Char(new SourceCodePosition(), '0');
		}
	}

	public class BType_Bool : BType_Value
	{
		public BType_Bool(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override string getDebugString()
		{
			return "bool";
		}

		public override Literal getDefaultValue()
		{
			return new Literal_Bool(new SourceCodePosition(), false);
		}
	}

	#endregion Value Types

	#region Array Types

	public class BType_IntArr : BType_Array
	{
		public BType_IntArr(SourceCodePosition pos, int sz)
			: base(pos)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("int[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_IntArr(new SourceCodePosition(), Enumerable.Repeat(0, Size).ToList());
		}
	}

	public class BType_CharArr : BType_Array
	{
		public BType_CharArr(SourceCodePosition pos, int sz)
			: base(pos)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("char[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_CharArr(new SourceCodePosition(), Enumerable.Repeat('0', Size).ToList());
		}
	}

	public class BType_DigitArr : BType_Array
	{
		public BType_DigitArr(SourceCodePosition pos, int sz)
			: base(pos)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("digit[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_DigitArr(new SourceCodePosition(), Enumerable.Repeat((byte)0, Size).ToList());
		}
	}

	public class BType_BoolArr : BType_Array
	{
		public BType_BoolArr(SourceCodePosition pos, int sz)
			: base(pos)
		{
			this.Size = sz;
		}

		public override string getDebugString()
		{
			return string.Format("bool[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_IntArr(new SourceCodePosition(), Enumerable.Repeat(0, Size).ToList());
		}
	}

	#endregion Array Types
}