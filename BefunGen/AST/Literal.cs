using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
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

		protected string EscapeChar(char input)
		{
			using (var writer = new StringWriter())
			{
				using (var provider = CodeDomProvider.CreateProvider("CSharp"))
				{
					provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
					return writer.ToString();
				}
			}
		}

		protected string EscapeString(string input)
		{
			using (var writer = new StringWriter())
			{
				using (var provider = CodeDomProvider.CreateProvider("CSharp"))
				{
					provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
					return writer.ToString();
				}
			}
		}

		public abstract BType getBType();

		public abstract CodePiece generateCode(bool reversed);

	}

	#region Parents

	public abstract class Literal_Value : Literal
	{
		public Literal_Value(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract bool ValueEquals(Literal_Value o);
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

		public abstract CodePiece generateCode(int pos, bool reversed);

		public abstract bool isUniform();
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

		public override CodePiece generateCode(bool reversed)
		{
			return NumberCodeHelper.generateCode(Value, reversed);
		}

		public override bool ValueEquals(Literal_Value o)
		{
			return (o is Literal_Int) && (o as Literal_Int).Value == this.Value;
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
			return EscapeChar(Value);
		}

		public override BType getBType()
		{
			return new BType_Char(new SourceCodePosition());
		}

		public override CodePiece generateCode(bool reversed)
		{
			return NumberCodeFactory_StringmodeChar.generateCode(Value, reversed) ?? NumberCodeHelper.generateCode(Value, reversed);
		}

		public override bool ValueEquals(Literal_Value o)
		{
			return (o is Literal_Char) && (o as Literal_Char).Value == this.Value;
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

		public override CodePiece generateCode(bool reversed)
		{
			return NumberCodeFactory_Boolean.generateCode(Value);
		}

		public override bool ValueEquals(Literal_Value o)
		{
			return (o is Literal_Bool) && (o as Literal_Bool).Value == this.Value;
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

		public override CodePiece generateCode(bool reversed)
		{
			return NumberCodeFactory_Digit.generateCode(Value);
		}

		public override bool ValueEquals(Literal_Value o)
		{
			return (o is Literal_Digit) && (o as Literal_Digit).Value == this.Value;
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

		public override bool isUniform()
		{
			return Value.All(p => p == Value[0]);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(CGO.DefaultNumeralValue);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			foreach (int val in Value)
			{
				if (reversed)
					p.AppendRight(NumberCodeHelper.generateCode(val, reversed));
				else
					p.AppendLeft(NumberCodeHelper.generateCode(val, reversed));
			}

			p.normalizeX();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed)
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
			return EscapeString(string.Join("", Value));
		}

		protected override int getCount()
		{
			return Value.Count;
		}

		public override bool isUniform()
		{
			return Value.All(p => p == Value[0]);
		}

		public override BType getBType()
		{
			return new BType_CharArr(new SourceCodePosition(), Count);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(CGO.DefaultCharacterValue);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				foreach (char val in Value.Reverse<char>()) // Reverse Value -> correct stack order
				{
					p.AppendLeft(NumberCodeFactory_StringmodeChar.generateCode(val, reversed) ?? NumberCodeHelper.generateCode(val, reversed));
				}
			}
			else
			{
				foreach (char val in Value.Reverse<char>())// Reverse Value -> correct stack order
				{
					p.AppendRight(NumberCodeFactory_StringmodeChar.generateCode(val, reversed) ?? NumberCodeHelper.generateCode(val, reversed));
				}
			}

			p.normalizeX();

			p.TrimDoubleStringMode();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed)
		{
			return NumberCodeFactory_StringmodeChar.generateCode(Value[pos], reversed) ?? NumberCodeHelper.generateCode(pos, reversed);
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

		public override bool isUniform()
		{
			return Value.All(p => p == Value[0]);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(CGO.DefaultBooleanValue);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();
			int i = 0;

			if (reversed)
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

			p.normalizeX();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed)
		{
			return NumberCodeFactory_Boolean.generateCode(Value[pos]);
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

		public override bool isUniform()
		{
			return Value.All(p => p == Value[0]);
		}

		protected override void AppendDefaultValue()
		{
			Value.Add(CGO.DefaultNumeralValue);
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();
			int i = 0;

			if (reversed)
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

			p.normalizeX();

			return p;
		}

		public override CodePiece generateCode(int pos, bool reversed)
		{
			return NumberCodeFactory_Digit.generateCode(Value[pos]);
		}
	}

	#endregion Array Literals
}