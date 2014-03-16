using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using System;

namespace BefunGen.AST.CodeGen
{
	public enum BefungeCommandType
	{
		NOP,
		Walkway, // Like NOP - But PC can appear here
		Add,
		Sub,
		Mult,
		Div,
		Modulo,
		Not,
		GreaterThan,
		PC_Right,
		PC_Left,
		PC_Up,
		PC_Down,
		PC_Random,
		If_Horizontal,
		If_Vertical,
		Stringmode,
		Stack_Dup,
		Stack_Swap,
		Stack_Pop,
		Out_Int,
		Out_ASCII,
		PC_Jump,
		Reflect_Set,
		Reflect_Get,
		In_Int,
		In_ASCII,
		Stop,
		Other
	}

	// Immutable Object
	public class BefungeCommand
	{
		public readonly BefungeCommandType Type;

		public readonly int Param;

		public readonly CodeTag Tag;

		public BefungeCommand(BefungeCommandType _t)
			: this(_t, 0)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, CodeTag _p)
			: this(_t, 0, _p)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, int _p)
			: this(_t, _p, null)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, int _p, CodeTag _g)
		{
			Type = _t;
			Param = _p;
			Tag = _g;

			if (Tag != null && Type == BefungeCommandType.NOP)
			{
				throw new ArgumentException(); // NOP's dürfen keine Tags haben ...
			}
		}

		public char getCommandCode()
		{
			switch (Type)
			{
				case BefungeCommandType.NOP:
					return CodeGenOptions.SetNOPCellsToCustom ? CodeGenOptions.CustomNOPSymbol : ' ';

				case BefungeCommandType.Walkway:
					return ' ';

				case BefungeCommandType.Add:
					return '+';

				case BefungeCommandType.Sub:
					return '-';

				case BefungeCommandType.Mult:
					return '*';

				case BefungeCommandType.Div:
					return '/';

				case BefungeCommandType.Modulo:
					return '%';

				case BefungeCommandType.Not:
					return '!';

				case BefungeCommandType.GreaterThan:
					return '`';

				case BefungeCommandType.PC_Right:
					return '>';

				case BefungeCommandType.PC_Left:
					return '<';

				case BefungeCommandType.PC_Up:
					return '^';

				case BefungeCommandType.PC_Down:
					return 'v';

				case BefungeCommandType.PC_Random:
					return '?';

				case BefungeCommandType.If_Horizontal:
					return '_';

				case BefungeCommandType.If_Vertical:
					return '|';

				case BefungeCommandType.Stringmode:
					return '"';

				case BefungeCommandType.Stack_Dup:
					return ':';

				case BefungeCommandType.Stack_Swap:
					return '\\';

				case BefungeCommandType.Stack_Pop:
					return '$';

				case BefungeCommandType.Out_Int:
					return '.';

				case BefungeCommandType.Out_ASCII:
					return ',';

				case BefungeCommandType.PC_Jump:
					return '#';

				case BefungeCommandType.Reflect_Set:
					return 'p';

				case BefungeCommandType.Reflect_Get:
					return 'g';

				case BefungeCommandType.In_Int:
					return '&';

				case BefungeCommandType.In_ASCII:
					return '~';

				case BefungeCommandType.Stop:
					return '@';

				case BefungeCommandType.Other:
					return (char)Param;

				default:
					throw new InvalidBefungeCommandTypeException(new SourceCodePosition());
			}
		}

		public BefungeCommand copyWithTag(CodeTag _g)
		{
			return new BefungeCommand(Type, Param, _g);
		}

		public bool IsDeltaIndependent()
		{
			return IsXDeltaIndependent() && IsYDeltaIndependent();
		}

		public bool IsXDeltaIndependent()
		{
			switch (Type)
			{
				case BefungeCommandType.NOP:
				case BefungeCommandType.Walkway:
				case BefungeCommandType.Add:
				case BefungeCommandType.Sub:
				case BefungeCommandType.Mult:
				case BefungeCommandType.Div:
				case BefungeCommandType.Modulo:
				case BefungeCommandType.Not:
				case BefungeCommandType.GreaterThan:
				case BefungeCommandType.PC_Random:
				case BefungeCommandType.Stringmode:
				case BefungeCommandType.Stack_Dup:
				case BefungeCommandType.Stack_Swap:
				case BefungeCommandType.Stack_Pop:
				case BefungeCommandType.Out_Int:
				case BefungeCommandType.Out_ASCII:
				case BefungeCommandType.PC_Jump:
				case BefungeCommandType.PC_Up:
				case BefungeCommandType.PC_Down:
				case BefungeCommandType.If_Vertical:
				case BefungeCommandType.Reflect_Set:
				case BefungeCommandType.Reflect_Get:
				case BefungeCommandType.In_Int:
				case BefungeCommandType.In_ASCII:
				case BefungeCommandType.Stop:
				case BefungeCommandType.Other:
					return true;
				case BefungeCommandType.PC_Right:
				case BefungeCommandType.PC_Left:
				case BefungeCommandType.If_Horizontal:
					return false;
				default:
					throw new InvalidBefungeCommandTypeException(new SourceCodePosition());
			}
		}

		public bool IsYDeltaIndependent()
		{
			switch (Type)
			{
				case BefungeCommandType.NOP:
				case BefungeCommandType.Walkway:
				case BefungeCommandType.Add:
				case BefungeCommandType.Sub:
				case BefungeCommandType.Mult:
				case BefungeCommandType.Div:
				case BefungeCommandType.Modulo:
				case BefungeCommandType.Not:
				case BefungeCommandType.GreaterThan:
				case BefungeCommandType.PC_Random:
				case BefungeCommandType.Stringmode:
				case BefungeCommandType.Stack_Dup:
				case BefungeCommandType.Stack_Swap:
				case BefungeCommandType.Stack_Pop:
				case BefungeCommandType.Out_Int:
				case BefungeCommandType.Out_ASCII:
				case BefungeCommandType.PC_Jump:
				case BefungeCommandType.PC_Left:
				case BefungeCommandType.PC_Right:
				case BefungeCommandType.If_Horizontal:
				case BefungeCommandType.Reflect_Set:
				case BefungeCommandType.Reflect_Get:
				case BefungeCommandType.In_Int:
				case BefungeCommandType.In_ASCII:
				case BefungeCommandType.Stop:
				case BefungeCommandType.Other:
					return true;
				case BefungeCommandType.PC_Up:
				case BefungeCommandType.PC_Down:
				case BefungeCommandType.If_Vertical:
					return false;
				default:
					throw new InvalidBefungeCommandTypeException(new SourceCodePosition());
			}
		}

		public bool IsCompressable()
		{
			switch (Type)
			{
				case BefungeCommandType.NOP:
				case BefungeCommandType.Walkway:
				case BefungeCommandType.PC_Up:
				case BefungeCommandType.PC_Down:
				case BefungeCommandType.PC_Right:
				case BefungeCommandType.PC_Left:
				case BefungeCommandType.Stop:
					return true;
				case BefungeCommandType.Add:
				case BefungeCommandType.Sub:
				case BefungeCommandType.Mult:
				case BefungeCommandType.Div:
				case BefungeCommandType.Modulo:
				case BefungeCommandType.Not:
				case BefungeCommandType.GreaterThan:
				case BefungeCommandType.PC_Random:
				case BefungeCommandType.Stringmode:
				case BefungeCommandType.Stack_Dup:
				case BefungeCommandType.Stack_Swap:
				case BefungeCommandType.Stack_Pop:
				case BefungeCommandType.Out_Int:
				case BefungeCommandType.Out_ASCII:
				case BefungeCommandType.PC_Jump:
				case BefungeCommandType.If_Vertical:
				case BefungeCommandType.Reflect_Set:
				case BefungeCommandType.Reflect_Get:
				case BefungeCommandType.In_Int:
				case BefungeCommandType.In_ASCII:
				case BefungeCommandType.Other:
				case BefungeCommandType.If_Horizontal:
					return false;
				default:
					throw new InvalidBefungeCommandTypeException(new SourceCodePosition());
			}
		}

		public bool EqualsTagLess(BefungeCommand c)
		{
			return !hasTag() && !c.hasTag() && this.Type == c.Type && this.Param == c.Param;
		}

		public bool hasTag()
		{
			return Tag != null;
		}
	}
}