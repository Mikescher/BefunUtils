using BefunGen.AST.CodeGen;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class Literal : ASTObject
	{
		public Literal(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract BType getBType();

		public abstract CodePiece generateCode(bool reverse = false);
	}

	#region Parents

	public abstract class Literal_Value : Literal
	{
		public Literal_Value(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}
	}

	public abstract class Literal_Array : Literal
	{
		public int Count { get { return getCount(); } }

		public Literal_Array(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		protected abstract int getCount();
		protected abstract void AppendDefaultValue();

		public void AppendDefaultValues(int cnt)
		{
			for (int i = 0; i < cnt; i++)
				AppendDefaultValue();
		}

		public abstract CodePiece generateCode(int pos, bool reversed = false);
	}

	#endregion Parents

	#region Value Literals

	public class Literal_Int : Literal_Value
	{
		public readonly int Value;

		public Literal_Int(SourceCodePosition pos, int v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}

		public override BType getBType()
		{
			return new BType_Int(new SourceCodePosition());
		}

		public override CodePiece generateCode(bool reversed = false)
		{
			return NumberCodeHelper.generateCode(Value, reversed);
		}
	}

	public class Literal_Char : Literal_Value
	{
		public readonly char Value;

		public Literal_Char(SourceCodePosition pos, char v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}

		public override BType getBType()
		{
			return new BType_Char(new SourceCodePosition());
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			return NumberCodeHelper.generateCode_Stringmode(Value, reverse);
		}
	}

	public class Literal_Bool : Literal_Value
	{
		public readonly bool Value;

		public Literal_Bool(SourceCodePosition pos, bool v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}

		public override BType getBType()
		{
			return new BType_Bool(new SourceCodePosition());
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			return NumberCodeHelper.generateCode(Value);
		}
	}

	public class Literal_Digit : Literal_Value
	{
		public readonly byte Value;

		public Literal_Digit(SourceCodePosition pos, byte v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}

		public override BType getBType()
		{
			return new BType_Digit(new SourceCodePosition());
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			return NumberCodeHelper.generateCode_Digit(Value);
		}
	}

	#endregion Value Literals

	#region Array Literals

	public class Literal_IntArr : Literal_Array
	{
		public List<int> Value = new List<int>();

		public Literal_IntArr(SourceCodePosition pos, List<int> v)
			: base(pos)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}

		protected override int getCount()
		{
			return Value.Count;
		}

		public override BType getBType()
		{
			return new BType_IntArr(new SourceCodePosition(), Count);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(0);
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			CodePiece p = new CodePiece();

			foreach (int val in Value)
			{
				if (reverse)
					p.AppendRight(NumberCodeHelper.generateCode(val, reverse));
				else
					p.AppendLeft(NumberCodeHelper.generateCode(val, reverse));
			}

			p.normalize();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed = false)
		{
			return NumberCodeHelper.generateCode(Value[pos], reversed);
		}
	}

	public class Literal_CharArr : Literal_Array
	{
		public List<char> Value = new List<char>();

		public Literal_CharArr(SourceCodePosition pos, List<char> v)
			: base(pos)
		{
			this.Value = v.ToList();
		}

		public Literal_CharArr(SourceCodePosition pos, string v)
			: base(pos)
		{
			this.Value = v.ToCharArray().ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}

		protected override int getCount()
		{
			return Value.Count;
		}

		public override BType getBType()
		{
			return new BType_CharArr(new SourceCodePosition(), Count);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add('0');
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			CodePiece p = new CodePiece();
			int i = 0;

			if (reverse)
			{
				p[i--, 0] = BCHelper.Stringmode;
				foreach (char val in Value)
				{
					p[i--, 0] = BCHelper.chr(val);
				}
				p[i--, 0] = BCHelper.Stringmode;
			}
			else
			{
				p[i++, 0] = BCHelper.Stringmode;
				foreach (char val in Value)
				{
					p[i++, 0] = BCHelper.chr(val);
				}
				p[i++, 0] = BCHelper.Stringmode;
			}

			p.normalize();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed = false)
		{
			return NumberCodeHelper.generateCode_Stringmode(Value[pos], reversed);
		}
	}

	public class Literal_BoolArr : Literal_Array
	{
		public List<bool> Value = new List<bool>();

		public Literal_BoolArr(SourceCodePosition pos, List<bool> v)
			: base(pos)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}

		protected override int getCount()
		{
			return Value.Count;
		}

		public override BType getBType()
		{
			return new BType_BoolArr(new SourceCodePosition(), Count);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(false);
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			CodePiece p = new CodePiece();
			int i = 0;

			if (reverse)
			{
				foreach (bool val in Value)
				{
					p[i--, 0] = BCHelper.dig(val ? (byte)1 : (byte)0);
				}
			}
			else
			{
				foreach (bool val in Value)
				{
					p[i++, 0] = BCHelper.dig(val ? (byte)1 : (byte)0);
				}
			}

			p.normalize();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed = false)
		{
			return NumberCodeHelper.generateCode(Value[pos]);
		}
	}

	public class Literal_DigitArr : Literal_Array
	{
		public List<byte> Value = new List<byte>();

		public Literal_DigitArr(SourceCodePosition pos, List<byte> v)
			: base(pos)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}

		protected override int getCount()
		{
			return Value.Count;
		}

		public override BType getBType()
		{
			return new BType_DigitArr(new SourceCodePosition(), Count);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(0);
		}

		public override CodePiece generateCode(bool reverse = false)
		{
			CodePiece p = new CodePiece();
			int i = 0;

			if (reverse)
			{
				foreach (byte val in Value)
				{
					p[i--, 0] = BCHelper.dig(val);
				}
			}
			else
			{
				foreach (byte val in Value)
				{
					p[i++, 0] = BCHelper.dig(val);
				}
			}

			p.normalize();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed = false)
		{
			return NumberCodeHelper.generateCode_Digit(Value[pos]);
		}
	}

	#endregion Array Literals
}