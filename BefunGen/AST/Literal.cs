using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class Literal : ASTObject
	{
		public Literal()
		{
			//--
		}
	}

	#region Parents

	public abstract class Literal_Value : Literal
	{
		public Literal_Value()
		{
			//--
		}
	}

	public abstract class Literal_Array : Literal
	{
		public Literal_Array()
		{
			//--
		}
	}

	#endregion Parents

	#region Value Literals

	public class Literal_Int : Literal_Value
	{
		public int Value;

		public Literal_Int(int v)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}
	}

	public class Literal_Char : Literal_Value
	{
		public char Value;

		public Literal_Char(char v)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}
	}

	public class Literal_Bool : Literal_Value
	{
		public bool Value;

		public Literal_Bool(bool v)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}
	}

	public class Literal_Digit : Literal_Value
	{
		public byte Value;

		public Literal_Digit(byte v)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return Value.ToString();
		}
	}

	#endregion Value Literals

	#region Array Literals

	public class Literal_IntArr : Literal_Array
	{
		public List<int> Value = new List<int>();

		public Literal_IntArr(List<int> v)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}
	}

	public class Literal_CharArr : Literal_Array
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

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}
	}

	public class Literal_BoolArr : Literal_Array
	{
		public List<bool> Value = new List<bool>();

		public Literal_BoolArr(List<bool> v)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}
	}

	public class Literal_DigitArr : Literal_Array
	{
		public List<byte> Value = new List<byte>();

		public Literal_DigitArr(List<byte> v)
		{
			this.Value = v.ToList();
		}

		public override string getDebugString()
		{
			return "{" + string.Join(",", Value.Select(p => p.ToString())) + "}";
		}
	}

	#endregion Array Literals
}