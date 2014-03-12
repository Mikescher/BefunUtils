using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	public static class CodePieceStore
	{
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
				p[bot_start+1, 1] = BCHelper.Sub;
				p[bot_start+2, 1] = BCHelper.Digit_1;
				p[bot_end, 1] = BCHelper.PC_Left;

				p.AppendLeft(p_len);

				p.FillRowWW(1, bot_start + 3, bot_end);

				p.normalizeX();
				#endregion

				return p;
			}
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

			CodePiece p_tpx = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_X, reversed);
			CodePiece p_tpy = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_Y, reversed);

			CodePiece p_tpx_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_X, !reversed);
			CodePiece p_tpy_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_Y, !reversed);

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
	}
}
