using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST
{
	abstract class Literal : ASTObject
	{
		public Literal()
		{
			//--
		}
	}

	abstract class Value_Literal : Literal
	{
		public Value_Literal()
		{
			//--
		}
	}

	abstract class Array_Literal : Literal
	{
		public Array_Literal()
		{
			//--
		}
	}

	class Literal_Int : Value_Literal
	{
		public int Value;

		public Literal_Int(int v)
		{
			this.Value = v;
		}
	}

	class Literal_Char : Value_Literal
	{
		public char Value;

		public Literal_Char(char v)
		{
			this.Value = v;
		}
	}

	class Literal_Bool : Value_Literal
	{
		public bool Value;

		public Literal_Bool(bool v)
		{
			this.Value = v;
		}
	}

	class Literal_Digit : Value_Literal
	{
		public byte Value;

		public Literal_Digit(byte v)
		{
			this.Value = v;
		}
	}

	class Literal_IntArr : Array_Literal
	{
		public List<int> Value = new List<int>();

		public Literal_IntArr(List<int> v)
		{
			this.Value = v.ToList();
		}
	}

	class Literal_CharArr : Array_Literal
	{
		public List<char> Value = new List<char>();

		public Literal_CharArr(List<char> v)
		{
			this.Value = v.ToList();
		}

		public Literal_CharArr(string v)
		{
			this.Value = v.ToCharArray().ToList();
		}
	}

	class Literal_BoolArr : Array_Literal
	{
		public List<bool> Value = new List<bool>();

		public Literal_BoolArr(List<bool> v)
		{
			this.Value = v.ToList();
		}
	}

	class Literal_DigitArr : Array_Literal
	{
		public List<byte> Value = new List<byte>();

		public Literal_DigitArr(List<byte> v)
		{
			this.Value = v.ToList();
		}
	}
}
