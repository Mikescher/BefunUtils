using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System.Linq;
namespace BefunGen.AST
{
	#region Parents

	public abstract class BType : ASTObject
	{
		protected const int PRIORITY_VOID = 0;
		protected const int PRIORITY_BOOL = 1;
		protected const int PRIORITY_DIGIT = 2;
		protected const int PRIORITY_CHAR = 3;
		protected const int PRIORITY_INT = 4;
		protected const int PRIORITY_UNION = 99;

		public BType(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract int GetSize();

		public override bool Equals(System.Object obj)
		{
			if (obj == null)
				return false;

			return this.Equals(obj as BType);
		}

		public virtual bool Equals(BType p)
		{
			if ((object)p == null)
				return false;

			return GetType() == p.GetType();
		}

		public static bool operator ==(BType a, BType b)
		{
			return (object)a != null && a.Equals(b);
		}

		public static bool operator !=(BType a, BType b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return -getPriority();
		}

		public override string ToString()
		{
			return getDebugString();
		}

		public abstract Literal getDefaultValue();
		public abstract bool isImplicitCastableTo(BType other);
		public abstract int getPriority();
	}

	public abstract class BType_Value : BType
	{
		public BType_Value(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override int GetSize()
		{
			return 1;
		}
	}

	public abstract class BType_Array : BType
	{
		public BType_Value InternalType { get { return getInternType(); } }

		public int Size;

		public BType_Array(SourceCodePosition pos, int sz)
			: base(pos)
		{
			Size = sz;
		}

		public override int GetSize()
		{
			return Size;
		}

		public override bool Equals(BType p)
		{
			if ((object)p == null)
				return false;

			return this.GetType() == p.GetType() && (p as BType_Array).Size == Size;
		}

		public override int GetHashCode()
		{
			return getPriority() * (Size + 1);
		}

		protected abstract BType_Value getInternType();
	}

	public class BType_Void : BType // neither Array nor Value ...
	{
		public BType_Void(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override int GetSize()
		{
			return 1;
		}

		public override string getDebugString()
		{
			return "void";
		}

		public override Literal getDefaultValue()
		{
			throw new VoidObjectCallException(Position);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return false;
		}

		public override int getPriority()
		{
			return PRIORITY_VOID;
		}
	}

	public class BType_Union : BType // Only for internal cast - is castable to everything
	{
		public BType_Union(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public override int GetSize()
		{
			throw new InvalidASTStateException(Position);
		}

		public override string getDebugString()
		{
			return "union";
		}

		public override Literal getDefaultValue()
		{
			return new Literal_Int(new SourceCodePosition(), 0);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return true;
		}

		public override int getPriority()
		{
			return PRIORITY_UNION;
		}
	}

	#endregion

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
			return new Literal_Int(new SourceCodePosition(), CodeGenOptions.DefaultNumeralValue);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Int);
		}

		public override int getPriority()
		{
			return PRIORITY_INT;
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
			return new Literal_Digit(new SourceCodePosition(), CodeGenOptions.DefaultNumeralValue);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Digit || other is BType_Int);
		}

		public override int getPriority()
		{
			return PRIORITY_DIGIT;
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
			return new Literal_Char(new SourceCodePosition(), CodeGenOptions.DefaultCharacterValue);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Char);
		}

		public override int getPriority()
		{
			return PRIORITY_CHAR;
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
			return new Literal_Bool(new SourceCodePosition(), CodeGenOptions.DefaultBooleanValue);
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Bool);
		}

		public override int getPriority()
		{
			return PRIORITY_BOOL;
		}
	}

	#endregion Value Types

	#region Array Types

	public class BType_IntArr : BType_Array
	{
		public BType_IntArr(SourceCodePosition pos, int sz)
			: base(pos, sz)
		{
		}

		public override string getDebugString()
		{
			return string.Format("int[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_IntArr(new SourceCodePosition(), Enumerable.Repeat((int)CodeGenOptions.DefaultNumeralValue, Size).ToList());
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Array && (other as BType_Array).Size == Size && (other is BType_IntArr));
		}

		public override int getPriority()
		{
			return PRIORITY_INT;
		}

		protected override BType_Value getInternType()
		{
			return new BType_Int(Position);
		}
	}

	public class BType_CharArr : BType_Array
	{
		public BType_CharArr(SourceCodePosition pos, int sz)
			: base(pos, sz)
		{
		}

		public override string getDebugString()
		{
			return string.Format("char[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_CharArr(new SourceCodePosition(), Enumerable.Repeat(CodeGenOptions.DefaultCharacterValue, Size).ToList());
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Array && (other as BType_Array).Size == Size && (other is BType_CharArr));
		}

		public override int getPriority()
		{
			return PRIORITY_CHAR;
		}

		protected override BType_Value getInternType()
		{
			return new BType_Char(Position);
		}
	}

	public class BType_DigitArr : BType_Array
	{
		public BType_DigitArr(SourceCodePosition pos, int sz)
			: base(pos, sz)
		{
		}

		public override string getDebugString()
		{
			return string.Format("digit[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_DigitArr(new SourceCodePosition(), Enumerable.Repeat(CodeGenOptions.DefaultNumeralValue, Size).ToList());
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Array && (other as BType_Array).Size == Size && (other is BType_DigitArr || other is BType_IntArr));
		}

		public override int getPriority()
		{
			return PRIORITY_DIGIT;
		}

		protected override BType_Value getInternType()
		{
			return new BType_Digit(Position);
		}
	}

	public class BType_BoolArr : BType_Array
	{
		public BType_BoolArr(SourceCodePosition pos, int sz)
			: base(pos, sz)
		{
		}

		public override string getDebugString()
		{
			return string.Format("bool[{0}]", Size);
		}

		public override Literal getDefaultValue()
		{
			return new Literal_BoolArr(new SourceCodePosition(), Enumerable.Repeat(CodeGenOptions.DefaultBooleanValue, Size).ToList());
		}

		public override bool isImplicitCastableTo(BType other)
		{
			return (other is BType_Array && (other as BType_Array).Size == Size && (other is BType_BoolArr));
		}

		public override int getPriority()
		{
			return PRIORITY_BOOL;
		}

		protected override BType_Value getInternType()
		{
			return new BType_Bool(Position);
		}
	}

	#endregion Array Types
}