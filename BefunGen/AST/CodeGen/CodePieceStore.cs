using BefunGen.AST.CodeGen.NumberCode;
using BefunGen.MathExtensions;
using System;

namespace BefunGen.AST.CodeGen
{
	public static class CodePieceStore
	{
		#region ReadArrayToStack

		public static CodePiece ReadArrayToStack(VarDeclaration_Array v, bool reversed)
		{
			return ReadArrayToStack(v.Size, v.CodePositionX, v.CodePositionY, reversed);
		}

		public static CodePiece ReadArrayToStack(int arrLen, MathExt.Point arr, bool reversed)
		{
			return ReadArrayToStack(arrLen, arr.X, arr.Y, reversed);
		}

		public static CodePiece ReadArrayToStack(int arrLen, int arrX, int arrY, bool reversed)
		{
			// Result: Horizontal     [LEFT, 0] IN ... [RIGHT, 0] OUT (or the other way when reversed)

			// Array will land reversed on Stack
			// [A, B, C, D] ->
			//
			// _____
			// | A |
			// | B |
			// | C |
			// | D |
			// ¯¯¯¯¯
			//

			CodePiece p_len = NumberCodeHelper.generateCode(arrLen - 1, reversed);
			CodePiece p_arx = NumberCodeHelper.generateCode(arrX, reversed);
			CodePiece p_ary = NumberCodeHelper.generateCode(arrY, reversed);

			if (reversed)
			{
				// $_v#!:\g{Y}+{X}:<{M}
				//   >1-           ^
				CodePiece p = new CodePiece();

				#region Reversed
				int bot_start;
				int bot_end;

				bot_start = 0;

				p[-2, 0] = BCHelper.Stack_Pop;
				p[-1, 0] = BCHelper.If_Horizontal;
				p[0, 0] = BCHelper.PC_Down;
				p[1, 0] = BCHelper.PC_Jump;
				p[2, 0] = BCHelper.Not;
				p[3, 0] = BCHelper.Stack_Dup;
				p[4, 0] = BCHelper.Stack_Swap;
				p[5, 0] = BCHelper.Reflect_Get;

				p.AppendRight(p_ary);
				p.AppendRight(BCHelper.Add);
				p.AppendRight(p_arx);
				p.AppendRight(BCHelper.Stack_Dup);
				bot_end = p.MaxX;
				p.AppendRight(BCHelper.PC_Left);
				p.AppendRight(p_len);

				p[bot_start, 1] = BCHelper.PC_Right;
				p[bot_start + 1, 1] = BCHelper.Digit_1;
				p[bot_start + 2, 1] = BCHelper.Sub;

				p[bot_end, 1] = BCHelper.PC_Up;

				p.FillRowWW(1, bot_start + 3, bot_end);

				p.normalizeX();
				#endregion

				return p;
			}
			else
			{
				// {LEN}>:{X}+{Y}g\:#v_$
				//      ^-1          <
				CodePiece p = new CodePiece();

				#region Normal
				int bot_start;
				int bot_end;

				bot_start = 0;

				p[0, 0] = BCHelper.PC_Right;
				p[1, 0] = BCHelper.Stack_Dup;

				p.AppendRight(p_arx);
				p.AppendRight(BCHelper.Add);
				p.AppendRight(p_ary);
				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(BCHelper.PC_Jump);

				bot_end = p.MaxX;

				p.AppendRight(BCHelper.PC_Down);
				p.AppendRight(BCHelper.If_Horizontal);
				p.AppendRight(BCHelper.Stack_Pop);

				p[bot_start, 1] = BCHelper.PC_Up;
				p[bot_start + 1, 1] = BCHelper.Sub;
				p[bot_start + 2, 1] = BCHelper.Digit_1;
				p[bot_end, 1] = BCHelper.PC_Left;

				p.AppendLeft(p_len);

				p.FillRowWW(1, bot_start + 3, bot_end);

				p.normalizeX();
				#endregion

				return p;
			}
		}

		#endregion

		#region WriteArrayFromStack

		public static CodePiece WriteArrayFromStack(VarDeclaration_Array v, bool reversed)
		{
			return WriteArrayFromStack(v.Size, v.CodePositionX, v.CodePositionY, reversed);
		}

		public static CodePiece WriteArrayFromStack(int arrLen, MathExt.Point arr, bool reversed)
		{
			return WriteArrayFromStack(arrLen, arr.X, arr.Y, reversed);
		}

		public static CodePiece WriteArrayFromStack(int arrLen, int arrX, int arrY, bool reversed)
		{
			// Result: Horizontal     [LEFT, 0] IN ... [RIGHT, 0] OUT (or the other way when reversed)

			// Array is reversed in Stack --> will land normal on Field
			// _____
			// | A |
			// | B |
			// | C | -->
			// | D |
			// ¯¯¯¯¯
			//			
			// [A, B, C, D]
			//

			CodePiece p_tpx = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.X, reversed);
			CodePiece p_tpy = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.Y, reversed);

			CodePiece p_tpx_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.X, !reversed);
			CodePiece p_tpy_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.Y, !reversed);

			CodePiece p_len = NumberCodeHelper.generateCode(arrLen - 1, reversed);
			CodePiece p_arx = NumberCodeHelper.generateCode(arrX, reversed);
			CodePiece p_ary = NumberCodeHelper.generateCode(arrY, reversed);

			if (reversed)
			{
				// _v#!-{M}p{Y}+g{TY}{TX}{X}\g{TY}{TX}<p{TY}{TX}0
				//  >{TX}:{TY}g1+\{TY}p               ^          
				CodePiece p = new CodePiece();

				#region Reversed

				int bot_start;
				int bot_end;

				bot_start = 0;

				p[-1, 0] = BCHelper.If_Horizontal;
				p[0, 0] = BCHelper.PC_Down;
				p[1, 0] = BCHelper.PC_Jump;
				p[2, 0] = BCHelper.Not;
				p[3, 0] = BCHelper.Sub;

				p.AppendRight(p_len);

				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(p_ary);

				p.AppendRight(BCHelper.Add);
				p.AppendRight(BCHelper.Reflect_Get);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);
				p.AppendRight(p_arx);

				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(BCHelper.Reflect_Get);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);

				bot_end = p.MaxX;

				p.AppendRight(BCHelper.PC_Left);
				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);

				p.AppendRight(BCHelper.Digit_0);

				CodePiece p_bottom = new CodePiece();
				{
					#region Generate_Bottom

					p_bottom.AppendRight(p_tpx_r);

					p_bottom.AppendRight(BCHelper.Stack_Dup);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Reflect_Get);
					p_bottom.AppendRight(BCHelper.Digit_1);
					p_bottom.AppendRight(BCHelper.Add);
					p_bottom.AppendRight(BCHelper.Stack_Swap);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Reflect_Set);

					p_bottom.normalizeX();

					#endregion
				}

				p[bot_start, 1] = BCHelper.PC_Right;
				p[bot_end, 1] = BCHelper.PC_Up;

				p.SetAt(bot_start + 1, 1, p_bottom);

				p.FillRowWW(1, bot_start + 1 + p_bottom.Width, bot_end);

				p.normalizeX();

				#endregion

				return p;
			}
			else
			{
				// 0{TX}{TY}p>{TX}{TY}g\{X}{TX}{TY}g+{Y}p{M}-#v_
				//           ^p{TY}\+1g{TY}:{TX}              < 
				CodePiece p = new CodePiece();

				#region Normal

				int bot_start;
				int bot_end;

				p.AppendRight(BCHelper.Digit_0);

				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Set);
				bot_start = p.MaxX;
				p.AppendRight(BCHelper.PC_Right);

				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Stack_Swap);

				p.AppendRight(p_arx);
				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Add);

				p.AppendRight(p_ary);

				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(p_len);

				p.AppendRight(BCHelper.Sub);
				p.AppendRight(BCHelper.PC_Jump);
				bot_end = p.MaxX;
				p.AppendRight(BCHelper.PC_Down);
				p.AppendRight(BCHelper.If_Horizontal);

				CodePiece p_bottom = new CodePiece();
				{
					#region Generate_Bottom

					p_bottom[0, 0] = BCHelper.Reflect_Set;

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Stack_Swap);
					p_bottom.AppendRight(BCHelper.Add);
					p_bottom.AppendRight(BCHelper.Digit_1);
					p_bottom.AppendRight(BCHelper.Reflect_Get);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Stack_Dup);

					p_bottom.AppendRight(p_tpx_r);

					p_bottom.normalizeX();

					#endregion
				}

				p[bot_start, 1] = BCHelper.PC_Up;
				p[bot_end, 1] = BCHelper.PC_Left;

				p.SetAt(bot_start + 1, 1, p_bottom);

				p.FillRowWW(1, bot_start + 1 + p_bottom.Width, bot_end);

				p.normalizeX();

				#endregion

				return p;
			}
		}

		public static CodePiece WriteArrayFromReversedStack(int arrLen, int arrX, int arrY, bool reversed)
		{
			// Normally Arrays are reversed on Stack -> this Method is for the reversed case --> Stack is normal on stack

			// Result: Horizontal     [LEFT, 0] IN ... [RIGHT, 0] OUT (or the other way when reversed)

			// Array is !! NOT !! reversed in Stack --> will land normal on Field
			// _____
			// | D |
			// | C |
			// | B | -->
			// | A |
			// ¯¯¯¯¯
			//			
			// [A, B, C, D]
			//

			CodePiece p_tpx = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.X, reversed);
			CodePiece p_tpy = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.Y, reversed);

			CodePiece p_tpx_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.X, !reversed);
			CodePiece p_tpy_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_IO_ARR.Y, !reversed);

			CodePiece p_len = NumberCodeHelper.generateCode(arrLen - 1, reversed);
			CodePiece p_arx = NumberCodeHelper.generateCode(arrX, reversed);
			CodePiece p_ary = NumberCodeHelper.generateCode(arrY, reversed);

			if (reversed)
			{
				// _v#!p{Y}+g{TY}{TX}{X}\g{TY}{TX}<p{TY}{TX}{M}
				//  >{TX}:{TY}g1-\{TY}p           ^        
				CodePiece p = new CodePiece();

				#region Reversed

				int bot_start;
				int bot_end;

				bot_start = 0;

				p[-1, 0] = BCHelper.If_Horizontal;
				p[0, 0] = BCHelper.PC_Down;
				p[1, 0] = BCHelper.PC_Jump;
				p[2, 0] = BCHelper.Not;

				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(p_ary);

				p.AppendRight(BCHelper.Add);
				p.AppendRight(BCHelper.Reflect_Get);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);
				p.AppendRight(p_arx);

				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(BCHelper.Reflect_Get);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);

				bot_end = p.MaxX;

				p.AppendRight(BCHelper.PC_Left);
				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);

				p.AppendRight(p_len);

				CodePiece p_bottom = new CodePiece();
				{
					#region Generate_Bottom

					p_bottom.AppendRight(p_tpx_r);

					p_bottom.AppendRight(BCHelper.Stack_Dup);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Reflect_Get);
					p_bottom.AppendRight(BCHelper.Digit_1);
					p_bottom.AppendRight(BCHelper.Sub);
					p_bottom.AppendRight(BCHelper.Stack_Swap);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Reflect_Set);

					p_bottom.normalizeX();

					#endregion
				}

				p[bot_start, 1] = BCHelper.PC_Right;
				p[bot_end, 1] = BCHelper.PC_Up;

				p.SetAt(bot_start + 1, 1, p_bottom);

				p.FillRowWW(1, bot_start + 1 + p_bottom.Width, bot_end);

				p.normalizeX();

				#endregion

				return p;
			}
			else
			{
				// {M}{TX}{TY}p>{TX}{TY}g\{X}{TX}{TY}g+{Y}p#v_
				//           ^p{TY}\-1g{TY}:{TX}            < 
				CodePiece p = new CodePiece();

				#region Normal

				int bot_start;
				int bot_end;

				p.AppendRight(p_len);

				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Set);
				bot_start = p.MaxX;
				p.AppendRight(BCHelper.PC_Right);

				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Stack_Swap);

				p.AppendRight(p_arx);
				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);

				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Add);

				p.AppendRight(p_ary);

				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(BCHelper.PC_Jump);
				bot_end = p.MaxX;
				p.AppendRight(BCHelper.PC_Down);
				p.AppendRight(BCHelper.If_Horizontal);

				CodePiece p_bottom = new CodePiece();
				{
					#region Generate_Bottom

					p_bottom[0, 0] = BCHelper.Reflect_Set;

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Stack_Swap);
					p_bottom.AppendRight(BCHelper.Sub);
					p_bottom.AppendRight(BCHelper.Digit_1);
					p_bottom.AppendRight(BCHelper.Reflect_Get);

					p_bottom.AppendRight(p_tpy_r);

					p_bottom.AppendRight(BCHelper.Stack_Dup);

					p_bottom.AppendRight(p_tpx_r);

					p_bottom.normalizeX();

					#endregion
				}

				p[bot_start, 1] = BCHelper.PC_Up;
				p[bot_end, 1] = BCHelper.PC_Left;

				p.SetAt(bot_start + 1, 1, p_bottom);

				p.FillRowWW(1, bot_start + 1 + p_bottom.Width, bot_end);

				p.normalizeX();

				#endregion

				return p;
			}
		}

		#endregion

		#region VerticalLaneTurnout

		public static CodePiece VerticalLaneTurnout_Test()
		{
			// #
			// >
			// |
			CodePiece p = new CodePiece();

			p[0, -1] = BCHelper.PC_Jump;
			p[0, +0] = BCHelper.PC_Right;
			p[0, +1] = BCHelper.If_Vertical;

			return p;
		}

		public static CodePiece VerticalLaneTurnout_Dec(bool stripped)
		{
			if (stripped)
			{
				// :
				// !
				// #
				// >
				// |
				CodePiece p = new CodePiece();

				p[0, -3] = BCHelper.Stack_Dup;
				p[0, -2] = BCHelper.Not;
				p[0, -1] = BCHelper.PC_Jump;
				p[0, +0] = BCHelper.PC_Right;
				p[0, +1] = BCHelper.If_Vertical;

				return p;
			}
			else
			{
				// 1
				// -
				// :
				// !
				// #
				// >
				// |
				CodePiece p = new CodePiece();

				p[0, -5] = BCHelper.Digit_1;
				p[0, -4] = BCHelper.Sub;
				p[0, -3] = BCHelper.Stack_Dup;
				p[0, -2] = BCHelper.Not;
				p[0, -1] = BCHelper.PC_Jump;
				p[0, +0] = BCHelper.PC_Right;
				p[0, +1] = BCHelper.If_Vertical;

				return p;
			}
		}

		#endregion

		public static CodePiece BooleanStackFlooder()
		{
			//Stack Flooder ALWAYS reversed (right -> left)

			// $_v#!:-1<\1+1:
			//   >0\   ^
			CodePiece p = new CodePiece();

			p.SetAt(0, 0, CodePiece.ParseFromLine(@"$_v#!:-1<\1+1:"));
			p.SetAt(2, 1, CodePiece.ParseFromLine(@">0\   ^", true));

			return p;
		}

		public static CodePiece PopMultipleStackValues(int count, bool reversed)
		{
			CodePiece p_count = NumberCodeHelper.generateCode(count, reversed);

			CodePiece p = new CodePiece();

			if (reversed)
			{
				//   >\$1-v
				// $_^# !:<{C}

				p.SetAt(2, -1, CodePiece.ParseFromLine(@">\$1-v"));
				p.SetAt(0, +0, CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@"$_^# !:<"), p_count));
			}
			else
			{
				// {C}0>-:#v_$
				//     ^1$\<

				p.SetAt(0, 0, CodePiece.ParseFromLine(@"0>-:#v_$"));
				p.SetAt(1, 1, CodePiece.ParseFromLine(@"^1$\<"));

				p.AppendLeft(p_count);
			}

			p.normalizeX();

			return p;
		}

		public static CodePiece ReadValueFromField(MathExt.Point pos, bool reversed)
		{
			CodePiece p = CodePiece.CombineHorizontal(NumberCodeHelper.generateCode(pos.X), NumberCodeHelper.generateCode(pos.Y), new CodePiece(BCHelper.Reflect_Get));

			if (reversed)
				p.reverseX(false);

			return p;
		}

		public static CodePiece WriteValueToField(MathExt.Point pos, bool reversed)
		{
			CodePiece p = CodePiece.CombineHorizontal(NumberCodeHelper.generateCode(pos.X), NumberCodeHelper.generateCode(pos.Y), new CodePiece(BCHelper.Reflect_Set));

			if (reversed)
				p.reverseX(false);

			return p;
		}

		public static CodePiece ModuloRangeLimiter(int range, bool reversed)
		{
			CodePiece p = new CodePiece();

			CodePiece p_r = NumberCodeHelper.generateCode(range, reversed);
			CodePiece p_r_rev = NumberCodeHelper.generateCode(range, !reversed);

			if (reversed)
			{
				#region Reversed
				// v\{R}:*-10:   <
				// >#<{R}%-++1#v_^#`0:
				//   ^%{R}     <

				CodePiece p_top = CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@"v\"), p_r, CodePiece.ParseFromLine(@":*-10:"));
				CodePiece p_mid = CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@">#<"), p_r_rev, CodePiece.ParseFromLine(@"%-++1#"));
				CodePiece p_bot = CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@"^%"), p_r);

				p_top.AddXOffset(0);
				p_mid.AddXOffset(0);
				p_bot.AddXOffset(2);

				int bot_w = Math.Max(p_mid.MaxX, p_bot.MaxX);

				p_mid.FillRowWW(0, p_mid.MaxX, bot_w);
				p_mid[bot_w + 0, 0] = BCHelper.PC_Down;
				p_mid[bot_w + 1, 0] = BCHelper.If_Horizontal;

				p_bot.FillRowWW(0, p_bot.MaxX, bot_w);
				p_bot[bot_w, 0] = BCHelper.PC_Left;

				int top_w = Math.Max(p_top.MaxX, p_mid.MaxX);

				p_top.FillRowWW(0, p_top.MaxX, top_w);
				p_top.AppendRight(BCHelper.PC_Left);

				p_mid.FillRowWW(0, p_mid.MaxX, top_w);
				p_mid.AppendRight(CodePiece.ParseFromLine(@"^#`0:"));


				p.SetAt(0, -1, p_top);
				p.SetAt(0, +0, p_mid);
				p.SetAt(0, +1, p_bot);

				#endregion
			}
			else
			{
				#region Normal
				//       >:01-*:{R}\ v
				// :0`#v_^#1++-%{R}>#<
				//     >{R}%       ^

				CodePiece p_top = CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@">:01-*:"), p_r);
				CodePiece p_mid = CodePiece.CombineHorizontal(CodePiece.ParseFromLine(@":0`#v_^#1++-%"), p_r_rev);
				CodePiece p_bot = CodePiece.CombineHorizontal(new CodePiece(BCHelper.PC_Right), p_r, new CodePiece(BCHelper.Modulo));

				p_top.AddXOffset(6);
				p_mid.AddXOffset(0);
				p_bot.AddXOffset(4);

				int max = MathExt.Max(p_top.MaxX, p_mid.MaxX, p_bot.MaxX);

				p_top.FillRowWW(0, p_top.MaxX, max);
				p_mid.FillRowWW(0, p_mid.MaxX, max);
				p_bot.FillRowWW(0, p_bot.MaxX, max);

				p_top.AppendRight(CodePiece.ParseFromLine(@"\ v", true));
				p_mid.AppendRight(CodePiece.ParseFromLine(@">#<", false));
				p_bot.AppendRight(CodePiece.ParseFromLine(@"^", false));

				p.SetAt(0, -1, p_top);
				p.SetAt(0, +0, p_mid);
				p.SetAt(0, +1, p_bot);

				#endregion
			}

			return p;
		}

		#region Base4 Random Generator

		public static CodePiece RandomDigitGenerator(CodePiece len, bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				#region Reversed
				//  >       v<   
				//  1  v0<  -1   
				// <|:\<1?v#<|`0:
				//  |  ^2 < 0$   
				// ^<  ^3<  ^<   
				#endregion

				p.SetAt(0, -2, CodePiece.ParseFromLine(@"@>       v<@@@", true, true));
				p.SetAt(0, -1, CodePiece.ParseFromLine(@"@1@@v0<  -1@@@", true, true));
				p.SetAt(0, 00, CodePiece.ParseFromLine(@"<|:\<1?v#<|`0:", true, true));
				p.SetAt(0, +1, CodePiece.ParseFromLine(@" |@@^2 <@0$@@@", true, true));
				p.SetAt(0, +2, CodePiece.ParseFromLine(@"^<@@^3<@@^<@@@", true, true));

				p.AppendRight(len);

				p.AppendRight(BCHelper.Digit_9);

			}
			else
			{
				#region Normal
				//    >v       < 
				//    1-  >0v  1 
				// :0`|>#v?1>\:|>
				//    $0 > 2^  | 
				//    >^  >3^  >^

				p.SetAt(0, -2, CodePiece.ParseFromLine(@"@@@>v       <@", true, true));
				p.SetAt(0, -1, CodePiece.ParseFromLine(@"@@@1-@@>0v@@1@", true, true));
				p.SetAt(0, 00, CodePiece.ParseFromLine(@":0`|>#v?1>\:|>", true, true));
				p.SetAt(0, +1, CodePiece.ParseFromLine(@"@@@$0@> 2^@@|@", true, true));
				p.SetAt(0, +2, CodePiece.ParseFromLine(@"@@@>^@@>3^@@>^", true, true));

				p.AppendLeft(len);

				p.AppendLeft(BCHelper.Digit_9);

				#endregion
			}

			p.normalizeX();

			return p;
		}

		public static CodePiece Base4DigitJoiner(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				#region Reversed
				//  v<     
				// $<|`4:\<				
				//   >\4*+^

				p.SetAt(0, -1, CodePiece.ParseFromLine(@"@v<@@@@@", true, true));
				p.SetAt(0, 00, CodePiece.ParseFromLine(@"$<|`4:\<", true, true));
				p.SetAt(0, +1, CodePiece.ParseFromLine(@"@@>\4*+^", true, true));

				#endregion
			}
			else
			{
				#region Normal
				//      >v
				// >\:4`|>$
				// ^+*4\<

				p.SetAt(0, -1, CodePiece.ParseFromLine(@"@@@@@>v@", true, true));
				p.SetAt(0, 00, CodePiece.ParseFromLine(@">\:4`|>$", true, true));
				p.SetAt(0, +1, CodePiece.ParseFromLine(@"^+*4\<@@", true, true));

				#endregion
			}

			p.normalizeX();

			return p;
		}

		#endregion
	}
}
