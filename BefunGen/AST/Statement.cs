using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using BefunGen.MathExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class Statement : ASTObject //TODO GET/SET/DEFINE DISPLAY
	{
		private static int _CODEPOINT_ADDRESS_COUNTER = 0;
		protected static int CODEPOINT_ADDRESS_COUNTER { get { return _CODEPOINT_ADDRESS_COUNTER++; } }

		public Statement(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public static void resetCounter()
		{
			_CODEPOINT_ADDRESS_COUNTER = 0;
		}

		public CodePiece extendVerticalMCTagsUpwards(CodePiece p)
		{
			List<TagLocation> entries = p.findAllActiveCodeTags(typeof(MethodCall_VerticalReEntry_Tag))
				.OrderBy(tp => (tp.Tag.TagParam as ICodeAddressTarget).CodePointAddr)
				.ToList();
			List<TagLocation> exits = p.findAllActiveCodeTags(typeof(MethodCall_VerticalExit_Tag));

			int pos_y_exitline = p.MinY - 1;

			foreach (TagLocation exit in exits)
			{
				MethodCall_VerticalExit_Tag tag_exit = exit.Tag as MethodCall_VerticalExit_Tag;
				tag_exit.deactivate();

				p[exit.X, pos_y_exitline] = BCHelper.PC_Right_tagged(new MethodCall_HorizontalExit_Tag(tag_exit.TagParam));

				try
				{
					p.CreateColWW(exit.X, pos_y_exitline + 1, exit.Y);
				}
				catch (InvalidCodeManipulationException ce)
				{
					throw new CommandPathFindingFailureException(ce.Message);
				}

				pos_y_exitline--;
			}

			int entrycount = entries.Count;

			int pos_y_entry = pos_y_exitline - entrycount * 3 + 2;

			foreach (TagLocation entry in entries)
			{
				MethodCall_VerticalReEntry_Tag tag_entry = entry.Tag as MethodCall_VerticalReEntry_Tag;
				tag_entry.deactivate();

				p[entry.X, pos_y_entry] = BCHelper.PC_Down_tagged(new MethodCall_HorizontalReEntry_Tag((ICodeAddressTarget)tag_entry.TagParam));

				try
				{
					p.CreateColWW(entry.X, pos_y_entry + 1, entry.Y);
				}
				catch (InvalidCodeManipulationException ce)
				{
					throw new CommandPathFindingFailureException(ce.Message);
				}

				pos_y_entry -= 3;
			}

			return p;
		}

		public abstract void linkVariables(Method owner);
		public abstract void addressCodePoints();
		public abstract void linkResultTypes(Method owner);
		public abstract void linkMethods(Program owner);
		public abstract bool allPathsReturn();

		public abstract Statement_Label findLabelByIdentifier(string ident);

		public abstract CodePiece generateCode(bool reversed);
	}

	#region Interfaces

	public interface ICodeAddressTarget
	{
		int CodePointAddr
		{
			get;
			set;
		}
	}

	#endregion

	#region Other

	public class Statement_StatementList : Statement
	{
		public List<Statement> List;

		public Statement_StatementList(SourceCodePosition pos, List<Statement> sl)
			: base(pos)
		{
			List = sl.ToList();
		}

		public override string getDebugString()
		{
			return string.Format("#StatementList\n[\n{0}\n]", indent(getDebugStringForList(List)));
		}

		public override void linkVariables(Method owner)
		{
			foreach (Statement s in List)
				s.linkVariables(owner);
		}

		public override void addressCodePoints() 
		{
			for (int i = 0; i < List.Count; i++)
			{
				List[i].addressCodePoints();
			}
		}

		public override void linkMethods(Program owner)
		{
			foreach (Statement s in List)
			{
				s.linkMethods(owner);
			}
		}

		public override void linkResultTypes(Method owner)
		{
			foreach (Statement s in List)
				s.linkResultTypes(owner);
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			Statement_Label result = null;

			foreach (Statement s in List)
			{
				Statement_Label found = s.findLabelByIdentifier(ident);
				if (found != null && result != null)
					return null;
				if (found != null && result == null)
					result = found;
			}

			return result;
		}

		public override bool allPathsReturn()
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (List[i].allPathsReturn())
					return true;
			}
			return false;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			#region Special Cases

			if (List.Count == 0)
			{
				return new CodePiece();
			}
			else if (List.Count == 1)
			{
				return extendVerticalMCTagsUpwards(List[0].generateCode(reversed));
			}

			#endregion

			#region Get Statements

			List<Statement> stmts = List.ToList();
			if (stmts.Count % 2 == 0)
				stmts.Add(new Statement_NOP(Position));

			#endregion

			#region Generate Codepieces

			List<CodePiece> cps = new List<CodePiece>();
			for (int i = 0; i < stmts.Count; i++)
			{
				cps.Add(extendVerticalMCTagsUpwards(stmts[i].generateCode(reversed ^ (i % 2 != 0))));
				cps[i].normalizeX();

				if (cps[i].Height == 0) // No total empty statements
					cps[i][0, 0] = BCHelper.Walkway;
			}

			#endregion

			#region Calculate Y-Positions

			List<int> ypos = new List<int>();
			ypos.Add(0);
			for (int i = 1; i < cps.Count; i++)
			{
				ypos.Add(ypos[i - 1] + cps[i - 1].MaxY - cps[i].MinY);
			}

			#endregion

			#region Combine Pieces

			if (reversed)
			{
				#region Reversed

				// ##### WIDTHS ######

				List<int> widths = new List<int>();
				for (int i = 0; i < cps.Count; i += 2)
				{
					int a = i - 1;
					int b = i;

					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					int w_a;
					int w_b;

					if (first)
						w_a = 0;
					else
						w_a = cps[a].Width;

					if (last)
						w_b = cps[b].Width - 1;
					else
						w_b = cps[b].Width;

					int w = Math.Max(w_a, w_b);

					if (!first)
						widths.Add(w);
					widths.Add(w);
				}

				int maxwidth = MathExt.Max(widths[0], widths.ToArray());

				// ##### PC's ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool curr_rev = (i % 2 == 0);
					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					if (first)
					{
						p[-1, ypos[i]] = BCHelper.PC_Down;
					}
					else if (last)
					{
						p[widths[i], ypos[i]] = BCHelper.PC_Left;
					}
					else if (curr_rev) // Reversed
					{
						p[-1, ypos[i]] = BCHelper.PC_Down;
						p[widths[i], ypos[i]] = BCHelper.PC_Left;
					}
					else // Normal
					{
						p[-1, ypos[i]] = BCHelper.PC_Right;
						p[widths[i], ypos[i]] = BCHelper.PC_Down;
					}
				}

				// ##### Walkways ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool curr_rev = (i % 2 == 0);
					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					if (first)
					{
						p.FillRowWW(ypos[i], cps[i].Width, maxwidth + 1);
						p.FillColWW(-1, ypos[i] + 1, ypos[i] + cps[i].MaxY);
					}
					else if (last)
					{
						p.FillRowWW(ypos[i], cps[i].Width - 1, widths[i]);
						p.FillColWW(widths[i], ypos[i] + cps[i].MinY, ypos[i]);
					}
					else
					{
						p.FillRowWW(ypos[i], cps[i].Width, widths[i]);

						if (curr_rev) // Reversed
						{
							p.FillColWW(widths[i], ypos[i] + cps[i].MinY, ypos[i]);
							p.FillColWW(-1, ypos[i] + 1, ypos[i] + cps[i].MaxY);
						}
						else
						{
							p.FillColWW(-1, ypos[i] + cps[i].MinY, ypos[i]);
							p.FillColWW(widths[i], ypos[i] + 1, ypos[i] + cps[i].MaxY);
						}
					}
				}

				// ##### Outer-Walkway ######

				int lastypos = ypos[ypos.Count - 1];
				p[-2, lastypos] = BCHelper.PC_Up;
				p[-2, 0] = BCHelper.PC_Left;

				p.FillColWW(-2, 1, lastypos);

				// ##### Statements ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool last = (i == cps.Count - 1);
					int x = last ? -1 : 0;
					int y = ypos[i];
					CodePiece c = cps[i];

					p.SetAt(x, y, c);
				}

				#endregion
			}
			else
			{
				#region Normal

				// ##### WIDTHS ######

				List<int> widths = new List<int>();
				for (int i = 0; i < cps.Count; i += 2)
				{
					int a = i;
					int b = i + 1;

					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					int w_a;
					int w_b;

					if (first)
						w_a = cps[a].Width - 1;
					else
						w_a = cps[a].Width;

					if (last)
						w_b = 0;
					else
						w_b = cps[b].Width;

					int w = Math.Max(w_a, w_b);

					widths.Add(w);
					if (!last)
						widths.Add(w);
				}

				int right = MathExt.Max(widths[0], widths.ToArray()) + 1;

				// ##### PC's ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool curr_rev = (i % 2 != 0);
					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					if (first)
					{
						p[widths[i], ypos[i]] = BCHelper.PC_Down;
					}
					else if (last)
					{
						p[-1, ypos[i]] = BCHelper.PC_Right;
					}
					else if (curr_rev) // Reversed
					{
						p[-1, ypos[i]] = BCHelper.PC_Down;
						p[widths[i], ypos[i]] = BCHelper.PC_Left;
					}
					else // Normal
					{
						p[-1, ypos[i]] = BCHelper.PC_Right;
						p[widths[i], ypos[i]] = BCHelper.PC_Down;
					}
				}

				// ##### Walkways ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool curr_rev = (i % 2 != 0);
					bool first = (i == 0);
					bool last = (i == cps.Count - 1);

					if (first)
					{
						p.FillRowWW(ypos[i], cps[i].Width - 1, widths[i]);
						p.FillColWW(widths[i], ypos[i] + 1, ypos[i] + cps[i].MaxY);
					}
					else if (last)
					{
						p.FillRowWW(ypos[i], cps[i].Width, right);
						p.FillColWW(-1, ypos[i] + cps[i].MinY, ypos[i]);
					}
					else
					{
						p.FillRowWW(ypos[i], cps[i].Width, widths[i]);

						if (curr_rev) // Reversed
						{
							p.FillColWW(widths[i], ypos[i] + cps[i].MinY, ypos[i]);
							p.FillColWW(-1, ypos[i] + 1, ypos[i] + cps[i].MaxY);
						}
						else
						{
							p.FillColWW(-1, ypos[i] + cps[i].MinY, ypos[i]);
							p.FillColWW(widths[i], ypos[i] + 1, ypos[i] + cps[i].MaxY);
						}
					}
				}

				// ##### Outer-Walkway ######

				int lastypos = ypos[ypos.Count - 1];
				p[right, lastypos] = BCHelper.PC_Up;
				p[right, 0] = BCHelper.PC_Right;

				p.FillColWW(right, 1, lastypos);

				// ##### Statements ######

				for (int i = 0; i < cps.Count; i++)
				{
					bool first = (i == 0);
					int x = first ? -1 : 0;
					int y = ypos[i];
					CodePiece c = cps[i];

					p.SetAt(x, y, c);
				}

				#endregion

			}

			p.normalizeX();

			#endregion

			#region Extend MehodCall-Tags

			List<TagLocation> entries = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalReEntry_Tag));
			List<TagLocation> exits = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalExit_Tag));

			foreach (TagLocation entry in entries)
			{
				MethodCall_HorizontalReEntry_Tag tag_entry = entry.Tag as MethodCall_HorizontalReEntry_Tag;

				p.CreateRowWW(entry.Y, p.MinX, entry.X);

				tag_entry.deactivate();

				p.SetTag(p.MinX, entry.Y, new MethodCall_HorizontalReEntry_Tag(tag_entry.TagParam as ICodeAddressTarget), true);
			}

			foreach (TagLocation exit in exits)
			{
				MethodCall_HorizontalExit_Tag tag_exit = exit.Tag as MethodCall_HorizontalExit_Tag;

				p.CreateRowWW(exit.Y, exit.X + 1, p.MaxX);

				tag_exit.deactivate();

				p.SetTag(p.MaxX - 1, exit.Y, new MethodCall_HorizontalExit_Tag(tag_exit.TagParam), true);
			}

			#endregion

			return p;
		}

		public CodePiece generateStrippedCode()
		{
			// Always normal direction

			CodePiece p = new CodePiece();

			#region Special Cases

			if (List.Count == 0)
			{
				return new CodePiece();
			}
			else if (List.Count == 1)
			{
				return extendVerticalMCTagsUpwards(List[0].generateCode(false));
			}

			#endregion

			#region Get Statements

			List<Statement> stmts = List.ToList();
			if (stmts.Count % 2 == 0)
				stmts.Add(new Statement_NOP(Position));

			#endregion

			#region Generate Codepieces

			List<CodePiece> cps = new List<CodePiece>();
			for (int i = 0; i < stmts.Count; i++)
			{
				cps.Add(extendVerticalMCTagsUpwards(stmts[i].generateCode(i % 2 != 0)));
				cps[i].normalizeX();

				if (cps[i].Height == 0) // No total empty statements
					cps[i][0, 0] = BCHelper.Walkway;
			}

			#endregion

			#region Calculate Y-Positions

			List<int> ypos = new List<int>();
			ypos.Add(0);
			for (int i = 1; i < cps.Count; i++)
			{
				ypos.Add(ypos[i - 1] + cps[i - 1].MaxY - cps[i].MinY);
			}

			#endregion

			#region Combine Pieces

			// ##### WIDTHS ######

			List<int> widths = new List<int>();
			for (int i = 0; i < cps.Count; i += 2)
			{
				int a = i;
				int b = i + 1;

				bool first = (i == 0);
				bool last = (i == cps.Count - 1);

				int w_a;
				int w_b;

				if (first)
					w_a = cps[a].Width - 1;
				else
					w_a = cps[a].Width;

				if (last)
					w_b = 0;
				else
					w_b = cps[b].Width;

				int w = Math.Max(w_a, w_b);

				widths.Add(w);
				if (!last)
					widths.Add(w);
			}

			int right = MathExt.Max(widths[0], widths.ToArray()) + 1;

			// ##### PC's ######

			for (int i = 0; i < cps.Count; i++)
			{
				bool curr_rev = (i % 2 != 0);
				bool first = (i == 0);
				bool last = (i == cps.Count - 1);

				if (first)
				{
					p[widths[i], ypos[i]] = BCHelper.PC_Down;
				}
				else if (last)
				{
					p[-1, ypos[i]] = BCHelper.PC_Right;
				}
				else if (curr_rev) // Reversed
				{
					p[-1, ypos[i]] = BCHelper.PC_Down;
					p[widths[i], ypos[i]] = BCHelper.PC_Left;
				}
				else // Normal
				{
					p[-1, ypos[i]] = BCHelper.PC_Right;
					p[widths[i], ypos[i]] = BCHelper.PC_Down;
				}
			}

			// ##### Walkways ######

			for (int i = 0; i < cps.Count; i++)
			{
				bool curr_rev = (i % 2 != 0);
				bool first = (i == 0);
				bool last = (i == cps.Count - 1);

				if (first)
				{
					p.FillRowWW(ypos[i], cps[i].Width - 1, widths[i]);
					p.FillColWW(widths[i], ypos[i] + 1, ypos[i] + cps[i].MaxY);
				}
				else if (last)
				{
					p.FillRowWW(ypos[i], cps[i].Width, right);
					p.FillColWW(-1, ypos[i] + cps[i].MinY, ypos[i]);
				}
				else
				{
					p.FillRowWW(ypos[i], cps[i].Width, widths[i]);

					if (curr_rev) // Reversed
					{
						p.FillColWW(widths[i], ypos[i] + cps[i].MinY, ypos[i]);
						p.FillColWW(-1, ypos[i] + 1, ypos[i] + cps[i].MaxY);
					}
					else
					{
						p.FillColWW(-1, ypos[i] + cps[i].MinY, ypos[i]);
						p.FillColWW(widths[i], ypos[i] + 1, ypos[i] + cps[i].MaxY);
					}
				}
			}

			// ##### Statements ######

			for (int i = 0; i < cps.Count; i++)
			{
				bool first = (i == 0);
				int x = first ? -1 : 0;
				int y = ypos[i];
				CodePiece c = cps[i];

				p.SetAt(x, y, c);
			}

			p.normalizeX();

			#endregion

			#region Extend MehodCall-Tags

			List<TagLocation> entries = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalReEntry_Tag));
			List<TagLocation> exits = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalExit_Tag));

			foreach (TagLocation entry in entries)
			{
				MethodCall_HorizontalReEntry_Tag tag_entry = entry.Tag as MethodCall_HorizontalReEntry_Tag;

				p.CreateRowWW(entry.Y, p.MinX, entry.X);

				tag_entry.deactivate();

				p.SetTag(p.MinX, entry.Y, new MethodCall_HorizontalReEntry_Tag(tag_entry.TagParam as ICodeAddressTarget), true);
			}

			foreach (TagLocation exit in exits)
			{
				MethodCall_HorizontalExit_Tag tag_exit = exit.Tag as MethodCall_HorizontalExit_Tag;

				p.CreateRowWW(exit.Y, exit.X + 1, p.MaxX);

				tag_exit.deactivate();

				p.SetTag(p.MaxX - 1, exit.Y, new MethodCall_HorizontalExit_Tag(tag_exit.TagParam), true);
			}

			#endregion

			#region Strip LastLine

			if (List.Count % 2 == 0 && p.lastRowIsSingle(true))
			{
				p.RemoveRow(p.MaxY - 1);
			}

			#endregion

			return p;
		}
	}

	public class Statement_MethodCall : Statement, ICodeAddressTarget
	{
		public List<Expression> CallParameter;

		public string Identifier; // Temporary -- before linking;
		public Method Target;

		public Method owner;

		private int _CodePointAddr = -1;
		public int CodePointAddr
		{
			get
			{
				return _CodePointAddr;
			}
			set
			{
				throw new Exception(); // NotWriteable
			}
		}

		public Statement_MethodCall(SourceCodePosition pos, string id)
			: base(pos)
		{
			this.Identifier = id;
			this.CallParameter = new List<Expression>();
		}

		public Statement_MethodCall(SourceCodePosition pos, string id, List<Expression> cp)
			: base(pos)
		{
			this.Identifier = id;
			this.CallParameter = cp.ToList();
		}

		public override string getDebugString()
		{
			return string.Format("#MethodCall {{{0}}} ::{1}:: --> #Parameter: ({1})", Target.MethodAddr, CodePointAddr, indent(getDebugCommaStringForList(CallParameter)));
		}

		public override void linkVariables(Method owner)
		{
			this.owner = owner;

			foreach (Expression e in CallParameter)
				e.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			_CodePointAddr = CODEPOINT_ADDRESS_COUNTER;

			foreach (Expression e in CallParameter)
				e.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Target = owner.findMethodByIdentifier(Identifier) as Method;

			if (Target == null)
				throw new UnresolvableReferenceException(Identifier, Position);

			Identifier = null;
		}

		public override void linkResultTypes(Method owner)
		{
			foreach (Expression e in CallParameter)
				e.linkResultTypes(owner);

			if (CallParameter.Count != Target.Parameter.Count)
				throw new WrongParameterCountException(CallParameter.Count, Target.Parameter.Count, Position);

			for (int i = 0; i < CallParameter.Count; i++)
			{
				BType present = CallParameter[i].getResultType();
				BType expected = Target.Parameter[i].Type;

				if (present != expected)
				{
					if (present.isImplicitCastableTo(expected))
						CallParameter[i] = new Expression_Cast(CallParameter[i].Position, expected, CallParameter[i]);
					else
						throw new ImplicitCastException(CallParameter[i].Position, present, expected);
				}
			}
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			return generateCode(reversed, true);
		}

		public CodePiece generateCode(bool reversed, bool popResult)
		{
			if (CodePointAddr < 0)
				throw new InvalidASTStateException(Position);

			CodePiece p = new CodePiece();

			if (reversed)
			{
				#region Reversed

				// ######## BEFORE EXIT::JUMP-IN ########

				#region EXIT::JUMPIN

				// Put own Variables on Stack

				for (int i = 0; i < owner.Variables.Count; i++)
				{
					if (owner.Variables[i] is VarDeclaration_Value)
					{
						VarDeclaration_Value var = owner.Variables[i] as VarDeclaration_Value;

						p.AppendLeft(new Expression_DirectValuePointer(Position, var).generateCode(reversed));
					}
					else if (owner.Variables[i] is VarDeclaration_Array)
					{
						VarDeclaration_Array var = owner.Variables[i] as VarDeclaration_Array;

						p.AppendLeft(CodePieceStore.ReadArrayToStack(var, reversed));
					}
					else
					{
						throw new WTFException();
					}
				}

				// Put own JumpBack-Adress on Stack

				p.AppendLeft(NumberCodeHelper.generateCode(CodePointAddr, reversed));

				// Put Parameter on Stack

				for (int i = 0; i < CallParameter.Count; i++)
				{
					p.AppendLeft(CallParameter[i].generateCode(reversed));
				}

				// Put TargetAdress on Stack

				p.AppendLeft(NumberCodeHelper.generateCode(Target.MethodAddr, reversed));

				// Put Lane Switch on Stack

				p.AppendLeft(BCHelper.Digit_1);

				#endregion

				// ######## JUMPS ########

				p.AppendLeft(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag(Target)));
				p.AppendLeft(BCHelper.PC_Left_tagged(new MethodCall_VerticalReEntry_Tag(this)));

				// ######## AFTER ENTRY::JUMP-BACK ########

				#region ENTRY::JUMP-BACK

				// Store Result int TMP_RESULT Field

				if (popResult)
				{
					if (Target.ResultType is BType_Void)
					{
						p.AppendLeft(BCHelper.Stack_Pop);
					}
					else if (Target.ResultType is BType_Value)
					{
						p.AppendLeft(BCHelper.Stack_Pop);
					}
					else if (Target.ResultType is BType_Array)
					{
						p.AppendLeft(CodePieceStore.PopMultipleStackValues((Target.ResultType as BType_Array).Size, reversed));
					}
					else throw new WTFException();
				}
				else if (Target.ResultType is BType_Void)
				{
					p.AppendLeft(BCHelper.Stack_Pop); // Nobody cares about the result ...
				}
				else if (Target.ResultType is BType_Value)
				{
					p.AppendLeft(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, reversed));
					p.AppendLeft(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y, reversed));

					p.AppendLeft(BCHelper.Reflect_Set);
				}
				else if (Target.ResultType is BType_Array)
				{
					p.AppendLeft(CodePieceStore.WriteArrayFromStack((
						Target.ResultType as BType_Array).Size,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y,
						reversed));
				}
				else throw new WTFException();

				// Restore Variables

				for (int i = owner.Variables.Count - 1; i >= 0; i--)
				{
					p.AppendLeft(owner.Variables[i].generateCode_SetToStackVal(reversed));
				}

				// Put ReturnValue Back to Stack

				if (popResult)
				{
					// Do nothing - no really ...
				}
				else if (Target.ResultType is BType_Void)
				{
					// DO nothing - Nobody cares about the result ...
				}
				else if (Target.ResultType is BType_Value)
				{
					p.AppendLeft(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, reversed));
					p.AppendLeft(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y, reversed));

					p.AppendLeft(BCHelper.Reflect_Get);
				}
				else if (Target.ResultType is BType_Array)
				{
					p.AppendLeft(CodePieceStore.ReadArrayToStack((
						Target.ResultType as BType_Array).Size,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y,
						reversed));
				}
				else throw new WTFException();

				#endregion

				#endregion
			}
			else
			{
				#region Normal

				// ######## BEFORE EXIT::JUMP-IN ########

				#region EXIT::JUMPIN

				// Put own Variables on Stack

				for (int i = 0; i < owner.Variables.Count; i++)
				{
					if (owner.Variables[i] is VarDeclaration_Value)
					{
						VarDeclaration_Value var = owner.Variables[i] as VarDeclaration_Value;

						p.AppendRight(new Expression_DirectValuePointer(Position, var).generateCode(reversed));
					}
					else if (owner.Variables[i] is VarDeclaration_Array)
					{
						VarDeclaration_Array var = owner.Variables[i] as VarDeclaration_Array;

						p.AppendRight(CodePieceStore.ReadArrayToStack(var, reversed));
					}
					else
					{
						throw new WTFException();
					}
				}

				// Put own JumpBack-Adress on Stack

				p.AppendRight(NumberCodeHelper.generateCode(CodePointAddr, reversed));

				// Put Parameter on Stack

				for (int i = 0; i < CallParameter.Count; i++)
				{
					p.AppendRight(CallParameter[i].generateCode(reversed));
				}

				// Put TargetAdress on Stack

				p.AppendRight(NumberCodeHelper.generateCode(Target.MethodAddr ,reversed));

				// Put Lane Switch on Stack

				p.AppendRight(BCHelper.Digit_1);

				#endregion

				// ######## JUMPS ########

				p.AppendRight( BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag(Target)) );
				p.AppendRight( BCHelper.PC_Right_tagged(new MethodCall_VerticalReEntry_Tag(this)) );

				// ######## AFTER ENTRY::JUMP-BACK ########

				#region ENTRY::JUMP-BACK

				// Store Result int TMP_RESULT Field

				if (popResult)
				{
					if (Target.ResultType is BType_Void)
					{
						p.AppendRight(BCHelper.Stack_Pop);
					}
					else if (Target.ResultType is BType_Value)
					{
						p.AppendRight(BCHelper.Stack_Pop);
					}
					else if (Target.ResultType is BType_Array)
					{
						p.AppendRight(CodePieceStore.PopMultipleStackValues((Target.ResultType as BType_Array).Size, reversed));

					}
					else throw new WTFException();
				} 
				else if (Target.ResultType is BType_Void)
				{
					p.AppendRight(BCHelper.Stack_Pop); // Nobody cares about the result ...
				}
				else if (Target.ResultType is BType_Value)
				{
					p.AppendRight(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, reversed));
					p.AppendRight(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y, reversed));

					p.AppendRight(BCHelper.Reflect_Set);
				}
				else if (Target.ResultType is BType_Array)
				{
					p.AppendRight(CodePieceStore.WriteArrayFromStack((
						Target.ResultType as BType_Array).Size, 
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, 
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y, 
						reversed));
				}
				else throw new WTFException();

				// Restore Variables

				for (int i = owner.Variables.Count - 1; i >= 0; i--)
				{
					p.AppendRight(owner.Variables[i].generateCode_SetToStackVal(reversed));
				}

				// Put ReturnValue Back to Stack

				if (popResult)
				{
					// Do nothing - no really ...
				}
				else if (Target.ResultType is BType_Void)
				{
					// Do nothing - Nobody cares about the result ...
				}
				else if (Target.ResultType is BType_Value)
				{
					p.AppendRight(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, reversed));
					p.AppendRight(NumberCodeHelper.generateCode(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y, reversed));

					p.AppendRight(BCHelper.Reflect_Get);
				}
				else if (Target.ResultType is BType_Array)
				{
					p.AppendRight(CodePieceStore.ReadArrayToStack((
						Target.ResultType as BType_Array).Size,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X,
						CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y,
						reversed));
				}
				else throw new WTFException();

				#endregion

				#endregion
			}

			p.normalizeX();

			return p;

		}
	}

	#endregion

	#region Keywords

	public class Statement_Label : Statement, ICodeAddressTarget
	{
		public string Identifier;

		private int _CodePointAddr = -1;
		public int CodePointAddr
		{
			get
			{
				return _CodePointAddr;
			}
			set
			{
				throw new Exception(); // Not writeable
			}
		}

		public Statement_Label(SourceCodePosition pos, string ident)
			: base(pos)
		{
			this.Identifier = ident;
		}

		public override string getDebugString()
		{
			return string.Format("#LABEL: {{{0}}}", CodePointAddr);
		}

		public override void addressCodePoints()
		{
			_CodePointAddr = CODEPOINT_ADDRESS_COUNTER;
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return ident.ToLower() == Identifier.ToLower() ? this : null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (reversed)
			{
				return new CodePiece(BCHelper.PC_Left_tagged(new MethodCall_VerticalReEntry_Tag(this)));
			}
			else
			{
				return new CodePiece(BCHelper.PC_Right_tagged(new MethodCall_VerticalReEntry_Tag(this)));
			}
		}
	}

	public class Statement_Goto : Statement
	{
		public string TargetIdentifier;
		public Statement_Label Target;

		public Statement_Goto(SourceCodePosition pos, string id)
			: base(pos)
		{
			this.TargetIdentifier = id;
		}

		public override string getDebugString()
		{
			return string.Format("#GOTO: {{{0}}}", Target.CodePointAddr);
		}

		public override void addressCodePoints()
		{
			// NOP
		}

		public override void linkVariables(Method owner)
		{
			Target = owner.findLabelByIdentifier(TargetIdentifier);
			if (Target == null)
				throw new UnresolvableReferenceException(TargetIdentifier, Position);
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(NumberCodeHelper.generateCode(Target.CodePointAddr, reversed));

				p.AppendLeft(BCHelper.Digit_0); // Right Lane

				p.AppendLeft(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag(Target)));
			}
			else
			{
				p.AppendRight(NumberCodeHelper.generateCode(Target.CodePointAddr, reversed));

				p.AppendRight(BCHelper.Digit_0); // Right Lane

				p.AppendRight(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag(Target)));
			}

			p.normalizeX();

			return p;
		}
	}

	public class Statement_Return : Statement
	{
		public Expression Value;

		public BType ResultType;

		public Statement_Return(SourceCodePosition pos)
			: base(pos)
		{
			this.Value = new Expression_VoidValuePointer(pos);
		}

		public Statement_Return(SourceCodePosition pos, Expression v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return string.Format("#RETURN: {0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Value.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Value.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Value.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Value.linkResultTypes(owner);

			BType present = Value.getResultType();
			BType expected = owner.ResultType;

			if (present != expected)
			{
				if (present.isImplicitCastableTo(expected))
					Value = new Expression_Cast(Value.Position, expected, Value);
				else
					throw new ImplicitCastException(Value.Position, present, expected);
			}

			ResultType = owner.ResultType;
		}

		public override bool allPathsReturn()
		{
			return true;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (ResultType is BType_Void)
			{
				return generateCode_Void(reversed);
			}
			else if (ResultType is BType_Value)
			{
				return generateCode_Value(reversed);
			}
			else if (ResultType is BType_Array)
			{
				return generateCode_Array(reversed);
			}
			else throw new WTFException();
		}

		private CodePiece generateCode_Void(bool reversed)
		{
			CodePiece p = CodePiece.ParseFromLine(@"0\0");

			p.AppendRight(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag()));

			if (reversed) p.reverseX(false);

			return p;

		}

		private CodePiece generateCode_Value(bool reversed)
		{
			CodePiece p = new CodePiece(); 

			if (reversed)
			{
				#region Reversed

				p.AppendRight(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag()));

				p.AppendRight(BCHelper.Digit_0); // Right Lane

				p.AppendRight(BCHelper.Stack_Swap); // Swap BackjumpAddr back to Stack-Front

				p.AppendRight(Value.generateCode(reversed));

				#endregion
			}
			else
			{
				#region Normal

				p.AppendRight(Value.generateCode(reversed));

				p.AppendRight(BCHelper.Stack_Swap); // Swap BackjumpAddr back to Stack-Front

				p.AppendRight(BCHelper.Digit_0); // Right Lane

				p.AppendRight(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag()));

				#endregion

			}

			p.normalizeX();
			return p;
		}

		private CodePiece generateCode_Array(bool reversed)
		{
			CodePiece p = new CodePiece();

			BType_Array r_type = ResultType as BType_Array;

			if (reversed)
			{
				#region Reversed

				p.AppendLeft(Value.generateCode(reversed));


				// Switch ReturnValue (Array)  and  BackJumpAddr

				p.AppendLeft(CodePieceStore.WriteArrayFromStack(r_type.Size, CodeGenConstants.TMP_ARRFIELD_RETURNVAL, reversed));
				p.AppendLeft(CodePieceStore.WriteValueToField(CodeGenConstants.TMP_FIELD_JMP_ADDR, reversed));

				p.AppendLeft(CodePieceStore.ReadArrayToStack(r_type.Size, CodeGenConstants.TMP_ARRFIELD_RETURNVAL, reversed));
				p.AppendLeft(CodePieceStore.ReadValueFromField(CodeGenConstants.TMP_FIELD_JMP_ADDR, reversed));


				p.AppendLeft(BCHelper.Digit_0); // Right Lane

				p.AppendLeft(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag()));

				#endregion
			}
			else
			{
				#region Normal

				p.AppendRight(Value.generateCode(reversed));


				// Switch ReturnValue (Array)  and  BackJumpAddr

				p.AppendRight(CodePieceStore.WriteArrayFromStack(r_type.Size, CodeGenConstants.TMP_ARRFIELD_RETURNVAL, reversed));
				p.AppendRight(CodePieceStore.WriteValueToField(CodeGenConstants.TMP_FIELD_JMP_ADDR, reversed));

				p.AppendRight(CodePieceStore.ReadArrayToStack(r_type.Size, CodeGenConstants.TMP_ARRFIELD_RETURNVAL, reversed));
				p.AppendRight(CodePieceStore.ReadValueFromField(CodeGenConstants.TMP_FIELD_JMP_ADDR, reversed));


				p.AppendRight(BCHelper.Digit_0); // Right Lane

				p.AppendRight(BCHelper.PC_Up_tagged(new MethodCall_VerticalExit_Tag()));

				#endregion

			}

			p.normalizeX();
			return p;
		}
	}

	public class Statement_Out : Statement
	{
		public enum Out_Mode { OUT_INT, OUT_CHAR, OUT_CHAR_ARR };

		public Expression Value;

		public Out_Mode Mode;

		public Statement_Out(SourceCodePosition pos, Expression v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return string.Format("#OUT {0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Value.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Value.addressCodePoints();
		}

		public override void linkResultTypes(Method owner)
		{
			Value.linkResultTypes(owner);

			BType r = Value.getResultType();

			BType_Char t_char = new BType_Char(Position);
			BType_Int t_int = new BType_Int(Position);
			BType_CharArr t_chararr = (r is BType_Array) ? new BType_CharArr(Position, (r as BType_Array).Size) : new BType_CharArr(Position, 0);

			bool implToChar = r.isImplicitCastableTo(t_char);
			bool implToInt = r.isImplicitCastableTo(t_int);
			bool implToCharArr = (r is BType_Array) && r.isImplicitCastableTo(t_chararr);

			if (implToInt)
			{
				Mode = Out_Mode.OUT_INT;

				if (r != t_int)
				{
					Value = new Expression_Cast(Position, t_int, Value);
				}
			}
			else if (implToChar)
			{
				Mode = Out_Mode.OUT_CHAR;

				if (r != t_int)
				{
					Value = new Expression_Cast(Position, t_char, Value);
				}
			}
			else if (implToCharArr)
			{
				Mode = Out_Mode.OUT_CHAR_ARR;

				if (r != t_int)
				{
					Value = new Expression_Cast(Position, t_chararr, Value);
				}
			}
			else
			{
				throw new ImplicitCastException(Position, r, t_int, t_char, t_chararr);
			}
		}

		public override void linkMethods(Program owner)
		{
			Value.linkMethods(owner);
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			switch (Mode)
			{
				case Out_Mode.OUT_INT:
					return generateCode_Int(reversed);
				case Out_Mode.OUT_CHAR:
					return generateCode_Char(reversed);
				case Out_Mode.OUT_CHAR_ARR:
					return generateCode_CharArr(reversed);
				default:
					throw new WTFException();
			}
		}

		private CodePiece generateCode_Int(bool reversed)
		{
			CodePiece p = Value.generateCode(reversed);

			if (reversed)
				p.AppendLeft(BCHelper.Out_Int);
			else
				p.AppendRight(BCHelper.Out_Int);

			p.normalizeX();

			return p;
		}

		private CodePiece generateCode_Char(bool reversed)
		{
			CodePiece p = Value.generateCode(reversed);

			if (reversed)
				p.AppendLeft(BCHelper.Out_ASCII);
			else
				p.AppendRight(BCHelper.Out_ASCII);

			p.normalizeX();

			return p;
		}

		private CodePiece generateCode_CharArr(bool reversed)
		{
			BType_CharArr type_right = Value.getResultType() as BType_CharArr;

			CodePiece p_len = NumberCodeHelper.generateCode(type_right.Size - 1, reversed);

			CodePiece p_tpx = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_OUT_ARR.X, reversed);
			CodePiece p_tpy = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_OUT_ARR.Y, reversed);

			CodePiece p_tpx_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_OUT_ARR.X, !reversed);
			CodePiece p_tpy_r = NumberCodeHelper.generateCode(CodeGenConstants.TMP_FIELD_OUT_ARR.Y, !reversed);


			if (reversed)
			{
				// $_v#!g{TY}:{TX}, <p{TY}{TX}{M}
				//   >:{TY}g1-\{TY}p^
				CodePiece p = new CodePiece();

				#region Reversed

				p.AppendRight(BCHelper.Stack_Pop);
				p.AppendRight(BCHelper.If_Horizontal);

				p.AppendRight(BCHelper.PC_Down);
				p[p.MaxX - 1, 1] = BCHelper.PC_Right;

				CodePiece p_top = new CodePiece();
				{
					p_top.AppendRight(BCHelper.PC_Jump);
					p_top.AppendRight(BCHelper.Not);
					p_top.AppendRight(BCHelper.Reflect_Get);

					p_top.AppendRight(p_tpy);
					p_top.AppendRight(BCHelper.Stack_Dup);
					p_top.AppendRight(p_tpx);
					p_top.AppendRight(BCHelper.Out_ASCII);
				}

				CodePiece p_bot = new CodePiece();
				{
					p_bot.AppendRight(BCHelper.Stack_Dup);

					p_bot.AppendRight(p_tpy_r);
					p_bot.AppendRight(BCHelper.Reflect_Get);
					p_bot.AppendRight(BCHelper.Digit_1);
					p_bot.AppendRight(BCHelper.Sub);
					p_bot.AppendRight(BCHelper.Stack_Swap);

					p_bot.AppendRight(p_tpy_r);

					p_bot.AppendRight(BCHelper.Reflect_Set);
				}

				int top_bot_start = p.MaxX;
				int top_bot_end = top_bot_start + Math.Max(p_top.Width, p_bot.Width);

				p[top_bot_end + 0, 1] = BCHelper.PC_Up;

				p[top_bot_end + 0, 0] = BCHelper.PC_Left;
				p[top_bot_end + 1, 0] = BCHelper.Reflect_Set;

				p.AppendRight(p_tpy);
				p.AppendRight(p_tpx);
				p.AppendRight(p_len);

				p.SetAt(top_bot_start, 0, p_top);
				p.SetAt(top_bot_start, 1, p_bot);

				p.FillRowWW(0, top_bot_start + p_top.Width, top_bot_end);
				p.FillRowWW(1, top_bot_start + p_bot.Width, top_bot_end);

				p.AppendRight(Value.generateCode(reversed));

				#endregion

				return p;
			}
			else
			{
				// {M}{TX}{TY}p>,{TX}:{TY}g  #v_$
				//             ^p{TY}\-1g{TY}:<
				CodePiece p = Value.generateCode(reversed);

				#region Normal

				p.AppendRight(p_len);
				p.AppendRight(p_tpx);
				p.AppendRight(p_tpy);
				p.AppendRight(BCHelper.Reflect_Set);

				p.AppendRight(BCHelper.PC_Right);
				p[p.MaxX - 1, 1] = BCHelper.PC_Up;

				CodePiece p_top = new CodePiece();
				{
					p_top.AppendRight(BCHelper.Out_ASCII);
					p_top.AppendRight(p_tpx);
					p_top.AppendRight(BCHelper.Stack_Dup);
					p_top.AppendRight(p_tpy);
					p_top.AppendRight(BCHelper.Reflect_Get);
				}

				CodePiece p_bot = new CodePiece();
				{
					p_bot.AppendRight(BCHelper.Reflect_Set);

					p_bot.AppendRight(p_tpy_r);

					p_bot.AppendRight(BCHelper.Stack_Swap);
					p_bot.AppendRight(BCHelper.Sub);
					p_bot.AppendRight(BCHelper.Digit_1);
					p_bot.AppendRight(BCHelper.Reflect_Get);
					p_bot.AppendRight(p_tpy_r);
				}

				int top_bot_start = p.MaxX;
				int top_bot_end = top_bot_start + Math.Max(p_top.Width, p_bot.Width);

				p[top_bot_end + 0, 1] = BCHelper.Stack_Dup;
				p[top_bot_end + 1, 1] = BCHelper.PC_Left;

				p[top_bot_end + 0, 0] = BCHelper.PC_Jump;
				p[top_bot_end + 1, 0] = BCHelper.PC_Down;
				p[top_bot_end + 2, 0] = BCHelper.If_Horizontal;
				p[top_bot_end + 3, 0] = BCHelper.Stack_Pop;

				p.SetAt(top_bot_start, 0, p_top);
				p.SetAt(top_bot_start, 1, p_bot);

				p.FillRowWW(0, top_bot_start + p_top.Width, top_bot_end);
				p.FillRowWW(1, top_bot_start + p_bot.Width, top_bot_end);

				#endregion

				return p;
			}
		}
	}

	public class Statement_Out_CharArrLiteral : Statement
	{
		public Literal_CharArr Value;

		public Statement_Out_CharArrLiteral(SourceCodePosition pos, Literal_CharArr v)
			: base(pos)
		{
			this.Value = v;
		}

		public override string getDebugString()
		{
			return string.Format("#OUT {0}", Value.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}

		public override void addressCodePoints()
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (Value.Count == 0)
				return new CodePiece();

			if (reversed) //TODO Use better out code (from Alexio)
			{
				// $_ #! #: #,<"???"0
				CodePiece p = new CodePiece();

				p.AppendLeft(BCHelper.Digit_0);

				p.AppendLeft(Value.generateCode(reversed));

				p.AppendLeft(BCHelper.PC_Left);
				p.AppendLeft(BCHelper.Out_ASCII);
				p.AppendLeft(BCHelper.PC_Jump);
				p.AppendLeft(BCHelper.Walkway);
				p.AppendLeft(BCHelper.Stack_Dup);
				p.AppendLeft(BCHelper.PC_Jump);
				p.AppendLeft(BCHelper.Walkway);
				p.AppendLeft(BCHelper.Not);
				p.AppendLeft(BCHelper.PC_Jump);
				p.AppendLeft(BCHelper.Walkway);
				p.AppendLeft(BCHelper.If_Horizontal);
				p.AppendLeft(BCHelper.Stack_Pop);

				p.normalizeX();

				return p;
			}
			else
			{
				// 0"???">,# :# _$
				CodePiece p = new CodePiece();

				p.AppendRight(BCHelper.Digit_0);

				p.AppendRight(Value.generateCode(reversed));

				p.AppendRight(BCHelper.PC_Right);
				p.AppendRight(BCHelper.Out_ASCII);
				p.AppendRight(BCHelper.PC_Jump);
				p.AppendRight(BCHelper.Walkway);
				p.AppendRight(BCHelper.Stack_Dup);
				p.AppendRight(BCHelper.PC_Jump);
				p.AppendRight(BCHelper.Walkway);
				p.AppendRight(BCHelper.If_Horizontal);
				p.AppendRight(BCHelper.Stack_Pop);

				p.normalizeX();

				return p;
			}
		}
	}

	public class Statement_In : Statement
	{
		public enum In_Mode { IN_INT, IN_CHAR, IN_CHAR_ARR, IN_INT_ARR };

		public Expression_ValuePointer ValueTarget;

		public In_Mode Mode;

		public Statement_In(SourceCodePosition pos, Expression_ValuePointer vt)
			: base(pos)
		{
			this.ValueTarget = vt;
		}

		public override string getDebugString()
		{
			return string.Format("#IN {0}", ValueTarget.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			ValueTarget.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			ValueTarget.addressCodePoints();
		}

		public override void linkResultTypes(Method owner)
		{
			ValueTarget.linkResultTypes(owner);

			BType present = ValueTarget.getResultType();

			BType expec_int = new BType_Int(Position);
			BType expec_char = new BType_Char(Position);
			BType expec_chararr = (present is BType_Array) ? new BType_CharArr(Position, (present as BType_Array).Size) : new BType_CharArr(Position, 0);
			BType expec_intarr = (present is BType_Array) ? new BType_IntArr(Position, (present as BType_Array).Size) : new BType_IntArr(Position, 0);

			if (present == expec_char)
			{
				Mode = In_Mode.IN_CHAR;
			}
			else if (present == expec_int)
			{
				Mode = In_Mode.IN_INT;
			}
			else if (present == expec_chararr)
			{
				Mode = In_Mode.IN_CHAR_ARR;
			}
			else if (present == expec_intarr)
			{
				Mode = In_Mode.IN_INT_ARR;
			}
			else
			{
				throw new WrongTypeException(ValueTarget.Position, present, expec_char, expec_int, expec_chararr, expec_intarr);
			}
		}

		public override void linkMethods(Program owner)
		{
			ValueTarget.linkMethods(owner);
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			switch (Mode)
			{
				case In_Mode.IN_INT:
					return generateCode_Int(reversed);
				case In_Mode.IN_CHAR:
					return generateCode_Char(reversed);
				case In_Mode.IN_CHAR_ARR:
					return generateCode_CharArr(reversed);
				case In_Mode.IN_INT_ARR:
					return generateCode_IntArr(reversed);
				default:
					throw new WTFException();
			}
		}

		private CodePiece generateCode_Int(bool reversed)
		{
			CodePiece p = new CodePiece();

			p[0, 0] = BCHelper.In_Int;

			if (reversed)
			{
				p.AppendLeft(ValueTarget.generateCodeSingle(reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
				p.normalizeX();
			}
			else
			{
				p.AppendRight(ValueTarget.generateCodeSingle(reversed));
				p.AppendRight(BCHelper.Reflect_Set);
				p.normalizeX();
			}

			return p;
		}

		private CodePiece generateCode_Char(bool reversed)
		{
			CodePiece p = new CodePiece();

			p[0, 0] = BCHelper.In_ASCII;

			if (reversed)
			{
				p.AppendLeft(ValueTarget.generateCodeSingle(reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
				p.normalizeX();
			}
			else
			{
				p.AppendRight(ValueTarget.generateCodeSingle(reversed));
				p.AppendRight(BCHelper.Reflect_Set);
				p.normalizeX();
			}

			return p;
		}

		private CodePiece generateCode_CharArr(bool reversed)
		{
			Expression_DirectValuePointer vp = ValueTarget as Expression_DirectValuePointer;
			int len = (ValueTarget.getResultType() as BType_Array).Size;

			CodePiece p_len = NumberCodeHelper.generateCode(len, reversed);
			CodePiece p_write = CodePieceStore.WriteArrayFromReversedStack(len, vp.Target.CodePositionX, vp.Target.CodePositionY, reversed);

			if (reversed)
			{
				CodePiece p = CodePiece.ParseFromLine(@"$_>#!:$#-\#1\>#~<");

				p.AppendRight(p_len);
				p.AppendLeft(p_write);

				p.normalizeX();

				return p;
			}
			else
			{
				CodePiece p = CodePiece.ParseFromLine(@">~#<\1#\-#$:_$");

				p.AppendLeft(p_len);
				p.AppendRight(p_write);

				p.normalizeX();

				return p;
			}
		}

		private CodePiece generateCode_IntArr(bool reversed)
		{
			Expression_DirectValuePointer vp = ValueTarget as Expression_DirectValuePointer;
			int len = (ValueTarget.getResultType() as BType_Array).Size;

			CodePiece p_len = NumberCodeHelper.generateCode(len, reversed);
			CodePiece p_write = CodePieceStore.WriteArrayFromReversedStack(len, vp.Target.CodePositionX, vp.Target.CodePositionY, reversed);

			if (reversed)
			{
				CodePiece p = CodePiece.ParseFromLine(@"$_>#!:$#-\#1\>#&<");

				p.AppendRight(p_len);
				p.AppendLeft(p_write);

				p.normalizeX();

				return p;
			}
			else
			{
				CodePiece p = CodePiece.ParseFromLine(@">&#<\1#\-#$:_$");

				p.AppendLeft(p_len);
				p.AppendRight(p_write);

				p.normalizeX();

				return p;
			}
		}
	}

	public class Statement_Quit : Statement
	{
		public Statement_Quit(SourceCodePosition pos)
			: base(pos)
		{
		}

		public override string getDebugString()
		{
			return "#QUIT";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void addressCodePoints()
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override bool allPathsReturn()
		{
			return true;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			p[0, 0] = BCHelper.Stop;

			return p;
		}
	}

	public class Statement_NOP : Statement // NO OPERATION
	{
		public Statement_NOP(SourceCodePosition pos)
			: base(pos)
		{
		}

		public override string getDebugString()
		{
			return "#NOP";
		}

		public override void linkVariables(Method owner)
		{
			//NOP
		}

		public override void addressCodePoints()
		{
			//NOP
		}

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			return new CodePiece(); // easy as that ¯\_(ツ)_/¯
		}
	}

	#endregion Keywords

	#region Operations

	public class Statement_Inc : Statement
	{
		public Expression_ValuePointer Target;

		public Statement_Inc(SourceCodePosition pos, Expression_ValuePointer id)
			: base(pos)
		{
			this.Target = id;
		}

		public override string getDebugString()
		{
			return string.Format("#INC {0}", Target.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Target.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Target.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Target.linkResultTypes(owner);

			BType present = Target.getResultType();

			if (!(present == new BType_Int(Position) || present == new BType_Digit(Position) || present == new BType_Char(Position)))
			{
				throw new WrongTypeException(Target.Position, present, new BType_Int(Position), new BType_Digit(Position), new BType_Char(Position));
			}
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Target.generateCodeDoubleX(reversed));
				p.AppendLeft(BCHelper.Reflect_Get);
				p.AppendLeft(BCHelper.Digit_1);
				p.AppendLeft(BCHelper.Add);
				p.AppendLeft(BCHelper.Stack_Swap);
				p.AppendLeft(Target.generateCodeSingleY(reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight(Target.generateCodeDoubleX(reversed));
				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Digit_1);
				p.AppendRight(BCHelper.Add);
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(Target.generateCodeSingleY(reversed));
				p.AppendRight(BCHelper.Reflect_Set);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Statement_Dec : Statement
	{
		public Expression_ValuePointer Target;

		public Statement_Dec(SourceCodePosition pos, Expression_ValuePointer id)
			: base(pos)
		{
			this.Target = id;
		}

		public override string getDebugString()
		{
			return string.Format("#DEC {0}", Target.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Target.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Target.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Target.linkResultTypes(owner);

			BType present = Target.getResultType();

			if (!(present == new BType_Int(Position) || present == new BType_Digit(Position) || present == new BType_Char(Position)))
			{
				throw new WrongTypeException(Target.Position, present, new BType_Int(Position), new BType_Digit(Position), new BType_Char(Position));
			}
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Target.generateCodeDoubleX(reversed));
				p.AppendLeft(BCHelper.Reflect_Get);
				p.AppendLeft(BCHelper.Digit_1);
				p.AppendLeft(BCHelper.Sub);
				p.AppendLeft(BCHelper.Stack_Swap);
				p.AppendLeft(Target.generateCodeSingleY(reversed));
				p.AppendLeft(BCHelper.Reflect_Set);
			}
			else
			{
				p.AppendRight(Target.generateCodeDoubleX(reversed));
				p.AppendRight(BCHelper.Reflect_Get);
				p.AppendRight(BCHelper.Digit_1);
				p.AppendRight(BCHelper.Sub);
				p.AppendRight(BCHelper.Stack_Swap);
				p.AppendRight(Target.generateCodeSingleY(reversed));
				p.AppendRight(BCHelper.Reflect_Set);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Statement_Assignment : Statement
	{
		public Expression_ValuePointer Target;
		public Expression Expr;

		public Statement_Assignment(SourceCodePosition pos, Expression_ValuePointer t, Expression e)
			: base(pos)
		{
			this.Target = t;
			this.Expr = e;
		}

		public override string getDebugString()
		{
			return string.Format("#ASSIGN {0} = ({1})", Target.getDebugString(), Expr.getDebugString());
		}

		public override void linkVariables(Method owner)
		{
			Target.linkVariables(owner);
			Expr.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Target.addressCodePoints();
			Expr.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Target.linkMethods(owner);
			Expr.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Target.linkResultTypes(owner);
			Expr.linkResultTypes(owner);

			BType t_left = Target.getResultType();
			BType t_right = Expr.getResultType();

			if (t_left != t_right)
			{
				if (t_right.isImplicitCastableTo(t_left))
					Expr = new Expression_Cast(Expr.Position, t_left, Expr);
				else
					throw new ImplicitCastException(Expr.Position, t_right, t_left);
			}
		}

		public override bool allPathsReturn()
		{
			return false;
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (Target.getResultType() is BType_Array)
			{
				return generateCode_Array(reversed);
			}
			else if (Target.getResultType() is BType_Value)
			{
				return generateCode_Value(reversed);
			}
			else
			{
				throw new InvalidASTStateException(Position);
			}
		}

		private CodePiece generateCode_Value(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (reversed)
			{
				p.AppendLeft(Expr.generateCode(reversed));
				p.AppendLeft(Target.generateCodeSingle(reversed));

				p.AppendLeft(BCHelper.Reflect_Set);

				p.normalizeX();
			}
			else
			{
				p.AppendRight(Expr.generateCode(reversed));
				p.AppendRight(Target.generateCodeSingle(reversed));

				p.AppendRight(BCHelper.Reflect_Set);

				p.normalizeX();
			}

			return p;
		}

		private CodePiece generateCode_Array(bool reversed)
		{
			CodePiece p = new CodePiece();

			BType_Array type = Target.getResultType() as BType_Array;
			Expression_DirectValuePointer vPointer = Target as Expression_DirectValuePointer;

			if (reversed)
			{
				p.AppendLeft(Expr.generateCode(reversed));
				p.AppendLeft(CodePieceStore.WriteArrayFromStack(type.Size, vPointer.Target.CodePositionX, vPointer.Target.CodePositionY, reversed));

				p.normalizeX();
			}
			else
			{
				p.AppendRight(Expr.generateCode(reversed));
				p.AppendRight(CodePieceStore.WriteArrayFromStack(type.Size, vPointer.Target.CodePositionX, vPointer.Target.CodePositionY, reversed));

				p.normalizeX();
			}

			return p;
		}
	}

	#endregion Operations

	#region Constructs
	//TODO Do MC-Tags in all Structures ?
	public class Statement_If : Statement
	{
		public Expression Condition;
		public Statement_StatementList Body;
		public Statement Else;

		public Statement_If(SourceCodePosition pos, Expression c, Statement_StatementList b)
			: base(pos)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = new Statement_NOP(new SourceCodePosition());
		}

		public Statement_If(SourceCodePosition pos, Expression c, Statement_StatementList b, Statement e)
			: base(pos)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = e;
		}

		public override string getDebugString()
		{
			return string.Format("#IF ({0})\n{1}\n#IFELSE\n{2}", Condition.getDebugString(), indent(Body.getDebugString()), Else == null ? "  NULL" : indent(Else.getDebugString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
			Else.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Condition.addressCodePoints();
			Body.addressCodePoints();
			Else.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Condition.linkMethods(owner);
			Body.linkMethods(owner);
			Else.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Condition.linkResultTypes(owner);
			Body.linkResultTypes(owner);
			Else.linkResultTypes(owner);

			BType present = Condition.getResultType();
			BType expected = new BType_Bool(Position);

			if (present != expected)
			{
				if (present.isImplicitCastableTo(expected))
					Condition = new Expression_Cast(Condition.Position, expected, Condition);
				else
					throw new ImplicitCastException(Condition.Position, present, expected);
			}
		}

		public override bool allPathsReturn()
		{
			return Body.allPathsReturn() && Else.allPathsReturn();
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p;

			if (Else.GetType() == typeof(Statement_NOP))
			{
				p = generateCode_If(reversed);
			}
			else
			{
				p = generateCode_IfElse(reversed);
			}

			#region Extend MehodCall-Tags

			#region Entries

			p.normalizeX();

			List<TagLocation> entries = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalReEntry_Tag));

			// Cant generate Path - because it would collide on the left side at X==0
			bool hasLeftOutCollisions = entries.Any(x => p[0, x.Y].Type == BefungeCommandType.Walkway || p[0, x.Y].Type == BefungeCommandType.NOP);

			if (hasLeftOutCollisions)
			{

				p[-1, 0] = BCHelper.Walkway;
				foreach (TagLocation entry in entries)
				{
					MethodCall_HorizontalReEntry_Tag tag_entry = entry.Tag as MethodCall_HorizontalReEntry_Tag;

					if (p[0, entry.Y].Type == BefungeCommandType.Walkway || p[0, entry.Y].Type == BefungeCommandType.NOP)
					{
						p.CreateRowWW(entry.Y, -1, entry.X);

						tag_entry.deactivate();

						p.SetTag(-1, entry.Y, new MethodCall_HorizontalReEntry_Tag(tag_entry.TagParam as ICodeAddressTarget), true);
					}
					else
					{
						p.CreateRowWW(entry.Y, 1, entry.X);
						p[-1, entry.Y] = BCHelper.PC_Jump;

						tag_entry.deactivate();

						p.SetTag(-1, entry.Y, new MethodCall_HorizontalReEntry_Tag(tag_entry.TagParam as ICodeAddressTarget), true);
					}
				}
				p.normalizeX();
			}
			else
			{
				foreach (TagLocation entry in entries)
				{
					MethodCall_HorizontalReEntry_Tag tag_entry = entry.Tag as MethodCall_HorizontalReEntry_Tag;

					p.CreateRowWW(entry.Y, 0, entry.X);
					p[0, entry.Y] = BCHelper.PC_Jump;

					tag_entry.deactivate();

					p.SetTag(0, entry.Y, new MethodCall_HorizontalReEntry_Tag(tag_entry.TagParam as ICodeAddressTarget), true);
				}
			}

			#endregion

			#region Exits

			List<TagLocation> exits = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalExit_Tag));

			foreach (TagLocation exit in exits)
			{
				MethodCall_HorizontalExit_Tag tag_exit = exit.Tag as MethodCall_HorizontalExit_Tag;

				if (p[p.MaxX - 1, exit.Y].Type == BefungeCommandType.Walkway || p[p.MaxX - 1, exit.Y].Type == BefungeCommandType.NOP)
				{
					p.CreateRowWW(exit.Y, exit.X + 1, p.MaxX);

					tag_exit.deactivate();

					p.SetTag(p.MaxX - 1, exit.Y, new MethodCall_HorizontalExit_Tag(tag_exit.TagParam), true);
				}
				else
				{
					p.CreateRowWW(exit.Y, exit.X + 1, p.MaxX - 2);

					p.replaceWalkway(p.MaxX - 2, exit.Y, BCHelper.PC_Jump, true);

					tag_exit.deactivate();

					p.SetTag(p.MaxX - 1, exit.Y, new MethodCall_HorizontalExit_Tag(tag_exit.TagParam), true);
				}


			}

			#endregion

			#endregion

			return p;
		}

		public CodePiece generateCode_If(bool reversed)
		{
			CodePiece cp_cond = Condition.generateCode(reversed);
			cp_cond.normalizeX();

			CodePiece cp_body_if = Body.generateCode(reversed);
			cp_body_if.normalizeX();

			CodePiece p = new CodePiece();

			if (reversed)
			{
				#region Reversed

				// _v#!   CONDITION
				//  
				// 1>             v
				// 
				// ^      IF      <

				int right = Math.Max(cp_cond.Width + 1, cp_body_if.Width);
				int mid = cp_cond.MaxY;
				int bot = (mid + 1) - cp_body_if.MinY;

				// Top-Left '_v#!'
				p[-1, 0] = BCHelper.If_Horizontal;
				p[0, 0] = BCHelper.PC_Down;
				p[1, 0] = BCHelper.PC_Jump;
				p[2, 0] = BCHelper.Not;
				// Mid_Left '0>'
				p[-1, mid] = BCHelper.Digit_1;
				p[0, mid] = BCHelper.PC_Right;
				// Mid-Right 'v'
				p[right, mid] = BCHelper.PC_Down;
				// Bottom-Left '^'
				p[-1, bot] = BCHelper.PC_Up;
				// Bottom-right '<'
				p[right, bot] = BCHelper.PC_Left;

				// Walkway Top (Condition -> end)
				p.FillRowWW(0, cp_cond.Width + 3, right + 1);
				// Walkway Mid ('0>' -> 'v')
				p.FillRowWW(mid, 1, right);
				// Walkway Bot (Body_If -> '<')
				p.FillRowWW(bot, cp_body_if.Width, right);
				// Walkway Left-Upper_1 ('_' -> '0')
				p.FillColWW(-1, 1, mid);
				// Walkway Left-Upper_2 ('v' -> '>')
				p.FillColWW(0, 1, mid);
				// Walkway Left-Lower ('0' -> '^')
				p.FillColWW(-1, mid + 1, bot);
				// Walkway Right-Lower ('v' -> '<')
				p.FillColWW(right, mid + 1, bot);

				// Set Condition
				p.SetAt(3, 0, cp_cond);
				// Set Body
				p.SetAt(0, bot, cp_body_if);

				#endregion
			}
			else
			{
				#region Normal

				// CONDITION #v_
				// 
				// v            <0
				// 
				// >   IF        ^

				int right = Math.Max(cp_cond.Width, cp_body_if.Width - 1);
				int mid = cp_cond.MaxY;
				int bot = (mid + 1) - cp_body_if.MinY;

				// Top-Right '#v_'
				p[right - 1, 0] = BCHelper.PC_Jump;
				p[right, 0] = BCHelper.PC_Down;
				p[right + 1, 0] = BCHelper.If_Horizontal;
				// Mid-Left 'v'
				p[-1, mid] = BCHelper.PC_Down;
				// Mid-Right '<0'
				p[right, mid] = BCHelper.PC_Left;
				p[right + 1, mid] = BCHelper.Digit_0;
				// Bottom-Left '>'
				p[-1, bot] = BCHelper.PC_Right;
				// Bottom-Right '^'
				p[right + 1, bot] = BCHelper.PC_Up;

				// Walkway Top  (Condition -> '#v_')
				p.FillRowWW(0, cp_cond.Width - 1, right - 1);
				// Walkway Mid  ('v' -> '<0')
				p.FillRowWW(mid, 0, right);
				// Walkway Bot  (Body_If -> '^')
				p.FillRowWW(bot, cp_body_if.Width, right + 1);
				// Walkway Left-Lower  ('v' -> '>')
				p.FillColWW(-1, mid + 1, bot);
				// Walkway Right-Upper_1  ('v' -> '<')
				p.FillColWW(right, 1, mid);
				// Walkway Right-Upper_2  ('_' -> '0')
				p.FillColWW(right + 1, 1, mid);
				// Walkway Right-Lower  ('0' -> '^')
				p.FillColWW(right + 1, mid + 1, bot);

				// Set Condition
				p.SetAt(-1, 0, cp_cond);
				// Set Body
				p.SetAt(0, bot, cp_body_if);

				#endregion
			}

			return p;
		}

		public CodePiece generateCode_IfElse(bool reversed)
		{
			CodePiece cp_cond = Condition.generateCode(reversed);
			cp_cond.normalizeX();

			CodePiece cp_if = Body.generateCode(reversed);
			cp_if.normalizeX();

			CodePiece cp_else = Else.generateCode(reversed);
			cp_else.normalizeX();

			CodePiece p = new CodePiece();

			if (reversed)
			{
				#region Reversed

				// <v  CONDITION
				// 
				//  >          v
				// 
				//             #
				// ^     IF    <
				//             |
				// 
				// ^    ELSE   <

				int right = MathExt.Max(cp_cond.Width, cp_if.Width, cp_else.Width) - 1;
				int mid = cp_cond.MaxY;
				int yif = mid + MathExt.Max(-cp_if.MinY + 1, 2);
				int yelse = yif + MathExt.Max(cp_if.MaxY + -cp_else.MinY, 2);

				// Top-Left '<v'
				p[-2, 0] = BCHelper.PC_Left;
				p[-1, 0] = BCHelper.PC_Down;
				// Mid-Left '>'
				p[-1, mid] = BCHelper.PC_Right;
				// Mid-Right 'v'
				p[right, mid] = BCHelper.PC_Down;
				// yif-Left '^'
				p[-2, yif] = BCHelper.PC_Up;
				// yif-Right '#' '<' '|'
				p[right, yif - 1] = BCHelper.PC_Jump;
				p[right, yif] = BCHelper.PC_Left;
				p[right, yif + 1] = BCHelper.If_Vertical;
				// yelse-Left '^'
				p[-2, yelse] = BCHelper.PC_Up;
				// yelse-Right '<'
				p[right, yelse] = BCHelper.PC_Left;

				// Walkway Top (Condition -> end)
				p.FillRowWW(0, cp_cond.Width, right + 1);
				// Walkway Mid ('>' -> 'v')
				p.FillRowWW(mid, 0, right);
				// Walkway yif (If -> '<')
				p.FillRowWW(yif, cp_if.Width - 1, right);
				// Walkway yelse (Else -> '<')
				p.FillRowWW(yelse, cp_else.Width - 1, right);
				// Walkway Left-Upper_1 ('<' -> '^')
				p.FillColWW(-2, 1, yif);
				// Walkway Left-Upper_2 ('v' -> '>')
				p.FillColWW(-1, 1, mid);
				// Walkway Left-Lower ('^' -> '^')
				p.FillColWW(-2, yif + 1, yelse);
				// Walkway Right-Upper ('v' -> '<')
				p.FillColWW(right, mid + 1, yif - 1);
				// Walkway Right-Lower ('<' -> '<')
				p.FillColWW(right, yif + 2, yelse);

				// Insert Condition
				p.SetAt(0, 0, cp_cond);
				// Insert If
				p.SetAt(-1, yif, cp_if);
				// Insert Else
				p.SetAt(-1, yelse, cp_else);

				#endregion
			}
			else
			{
				#region Normal

				// CONDITION   v>
				// 
				// v           <
				// 
				// #
				// >     IF     ^
				// |
				// 
				// >    ELSE    ^

				int right = MathExt.Max(cp_cond.Width, cp_if.Width, cp_else.Width) - 1;
				int mid = cp_cond.MaxY;
				int yif = mid + MathExt.Max(-cp_if.MinY + 1, 2);
				int yelse = yif + MathExt.Max(cp_if.MaxY + -cp_else.MinY, 2);

				// Top-Right 'v>'
				p[right, 0] = BCHelper.PC_Down;
				p[right + 1, 0] = BCHelper.PC_Right;
				// Mid-Left 'v'
				p[-1, mid] = BCHelper.PC_Down;
				// Mid-Right '<'
				p[right, mid] = BCHelper.PC_Left;
				// yif-Left '#' '>' '|'
				p[-1, yif - 1] = BCHelper.PC_Jump;
				p[-1, yif] = BCHelper.PC_Right;
				p[-1, yif + 1] = BCHelper.If_Vertical;
				// yif-Right '^'
				p[right + 1, yif] = BCHelper.PC_Up;
				// yelse-Left '>'
				p[-1, yelse] = BCHelper.PC_Right;
				// yelse-Right '^'
				p[right + 1, yelse] = BCHelper.PC_Up;

				// Walkway Top (Condition -> 'v>')
				p.FillRowWW(0, cp_cond.Width - 1, right);
				// Walkway Mid ('v' -> '>')
				p.FillRowWW(mid, 0, right);
				// Walkway yif (If -> '^')
				p.FillRowWW(yif, cp_if.Width, right + 1);
				// Walkway yelse (Else -> '^')
				p.FillRowWW(yelse, cp_else.Width, right + 1);
				// Walkway Left-Upper ('v' -> '#')
				p.FillColWW(-1, mid + 1, yif - 1);
				// Walkway Left-Lower ('|' -> '>')
				p.FillColWW(-1, yif + 2, yelse);
				// Walkway Right-Upper_1 ('v' -> '<')
				p.FillColWW(right, 1, mid);
				// Walkway Right-Upper_2 ('>' -> '^')
				p.FillColWW(right + 1, 1, yif);
				// Walkway Right-Lower ('^' -> '^')
				p.FillColWW(right + 1, yif + 1, yelse);

				// Insert Condition
				p.SetAt(-1, 0, cp_cond);
				// Insert If
				p.SetAt(0, yif, cp_if);
				// Insert Else
				p.SetAt(0, yelse, cp_else);

				#endregion
			}

			return p;
		}
	}

	public class Statement_While : Statement
	{
		public Expression Condition;
		public Statement_StatementList Body;

		public Statement_While(SourceCodePosition pos, Expression c, Statement_StatementList b)
			: base(pos)
		{
			this.Condition = c;
			this.Body = b;
		}

		public override string getDebugString()
		{
			return string.Format("#WHILE ({0})\n{1}", Condition.getDebugString(), indent(Body.getDebugString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Condition.addressCodePoints();
			Body.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Condition.linkMethods(owner);
			Body.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Condition.linkResultTypes(owner);
			Body.linkResultTypes(owner);

			BType present = Condition.getResultType();
			BType expected = new BType_Bool(Position);

			if (present != expected)
			{
				if (present.isImplicitCastableTo(expected))
					Condition = new Expression_Cast(Condition.Position, expected, Condition);
				else
					throw new ImplicitCastException(Condition.Position, present, expected);
			}
		}

		public override bool allPathsReturn()
		{
			return false; // Its possible that the Body isnt executed at all
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece cp_body = Body.generateCode(!reversed);
			cp_body.normalizeX();

			CodePiece cp_cond = Condition.generateCode(reversed);
			cp_cond.normalizeX();

			if (reversed)
			{
				// _v#! CONDITION <
				//  >  STATEMENT  ^
				CodePiece p = new CodePiece();

				int top = cp_body.MinY - cp_cond.MaxY;
				int right = Math.Max(cp_body.Width, cp_cond.Width + 2);

				// Bottom-Left '>'
				p[-1, 0] = BCHelper.PC_Right;
				// Top-Left '_v#!'
				p[-2, top] = BCHelper.If_Horizontal;
				p[-1, top] = BCHelper.PC_Down;
				p[0, top] = BCHelper.PC_Jump;
				p[1, top] = BCHelper.Not;
				// Top-Right '<'
				p[right, top] = BCHelper.PC_Left;
				// Bottom Right '^'
				p[right, 0] = BCHelper.PC_Up;

				// Fill Walkway between condition and Left
				p.FillRowWW(top, cp_cond.Width + 2, right);
				// Fill Walkway between body and '<'
				p.FillRowWW(0, cp_body.Width, right);
				// Walkway Leftside Up
				p.FillColWW(-1, top + 1, 0);
				// Walkway righside down
				p.FillColWW(right, top + 1, 0);


				// Insert Condition
				p.SetAt(2, top, cp_cond);
				// Insert Body
				p.SetAt(0, 0, cp_body);

				p.normalizeX();
				p.AddYOffset(-top); // Set Offset relative to condition (and to insert/exit Points)

				return p;
			}
			else
			{
				// > CONDITION #v_
				// ^  STATEMENT <
				CodePiece p = new CodePiece();

				int top = cp_body.MinY - cp_cond.MaxY;
				int right = Math.Max(cp_body.Width, cp_cond.Width + 1);

				// Bottom-Left '^'
				p[-1, 0] = BCHelper.PC_Up;
				// Top-Left '>'
				p[-1, top] = BCHelper.PC_Right;
				// Tester Top-Right '#v_'
				p[right - 1, top] = BCHelper.PC_Jump;
				p[right, top] = BCHelper.PC_Down;
				p[right + 1, top] = BCHelper.If_Horizontal;
				// Bottom Right '<'
				p[right, 0] = BCHelper.PC_Left;

				// Fill Walkway between condition and Tester
				p.FillRowWW(top, cp_cond.Width, right - 1);
				// Fill Walkway between body and '<'
				p.FillRowWW(0, cp_body.Width, right);
				// Walkway Leftside Up
				p.FillColWW(-1, top + 1, 0);
				// Walkway righside down
				p.FillColWW(right, top + 1, 0);

				// Insert Condition
				p.SetAt(0, top, cp_cond);
				// Insert Body
				p.SetAt(0, 0, cp_body);

				p.normalizeX();
				p.AddYOffset(-top); // Set Offset relative to condition (and to insert/exit Points)

				return p;
			}
		}
	}

	public class Statement_RepeatUntil : Statement
	{
		public Expression Condition;
		public Statement_StatementList Body;

		public Statement_RepeatUntil(SourceCodePosition pos, Expression c, Statement_StatementList b)
			: base(pos)
		{
			this.Condition = c;
			this.Body = b;
		}

		public override string getDebugString()
		{
			return string.Format("#REPEAT-UNTIL ({0})\n{1}", Condition.getDebugString(), indent(Body.getDebugString()));
		}

		public override void linkVariables(Method owner)
		{
			Condition.linkVariables(owner);
			Body.linkVariables(owner);
		}

		public override void addressCodePoints()
		{
			Condition.addressCodePoints();
			Body.addressCodePoints();
		}

		public override void linkMethods(Program owner)
		{
			Condition.linkMethods(owner);
			Body.linkMethods(owner);
		}

		public override void linkResultTypes(Method owner)
		{
			Condition.linkResultTypes(owner);
			Body.linkResultTypes(owner);

			BType present = Condition.getResultType();
			BType expected = new BType_Bool(Position);

			if (present != expected)
			{
				if (present.isImplicitCastableTo(expected))
					Condition = new Expression_Cast(Condition.Position, expected, Condition);
				else
					throw new ImplicitCastException(Condition.Position, present, expected);
			}
		}

		public override bool allPathsReturn()
		{
			return Body.allPathsReturn(); // Body is executed at least once
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece cp_body = Body.generateCode(reversed);
			cp_body.normalizeX();

			CodePiece cp_cond = Condition.generateCode(!reversed);
			cp_cond.normalizeX();

			if (reversed)
			{
				// <v  STATEMENT  <
				// ^             _^
				//  >  CONDITION ^
				CodePiece p = new CodePiece();

				int mid = cp_body.MaxY;
				int bottom = (mid + 1) - cp_cond.MinY;
				int right = Math.Max(cp_body.Width, cp_cond.Width + 1);

				// Top-Left '<v'
				p[-2, 0] = BCHelper.PC_Left;
				p[-1, 0] = BCHelper.PC_Down;
				// Top-Right '<'
				p[right, 0] = BCHelper.PC_Left;
				// Mid-Left '^'
				p[-2, mid] = BCHelper.PC_Up;
				// Mid-Right '_^'
				p[right, mid] = BCHelper.PC_Up;
				p[right - 1, mid] = BCHelper.If_Horizontal;
				//Bottom-Left '>'
				p[-1, bottom] = BCHelper.PC_Right;
				//Bottom-Right '^'
				p[right - 1, bottom] = BCHelper.PC_Up;

				// Walkway top (Statement to '<')
				p.FillRowWW(0, cp_body.Width, right);
				// Walkway bottom (Condition to '^')
				p.FillRowWW(bottom, cp_cond.Width, right - 1);
				// Walkway left-lower ('<' to '^')
				p.FillColWW(-2, 1, mid);
				// Walkway left-full ('v' to '>')
				p.FillColWW(-1, 1, bottom);
				// Walkway right-lower ('^' to '_')
				p.FillColWW(right, 1, mid);
				// Walkway right-upper ('^' to '<')
				p.FillColWW(right - 1, mid + 1, bottom);
				// Walkway middle ('^' to '_^')
				p.FillRowWW(mid, 0, right - 1);

				// Insert Statement
				p.SetAt(0, 0, cp_body);
				// Inser Condition
				p.SetAt(0, bottom, cp_cond);

				p.normalizeX();

				return p;
			}
			else
			{
				// >  STATEMENT  v>
				// ^_             ^
				//  ^! CONDITION <
				CodePiece p = new CodePiece();

				int mid = cp_body.MaxY;
				int bottom = (mid + 1) - cp_cond.MinY;
				int right = Math.Max(cp_body.Width, cp_cond.Width + 2);

				// Top-Left '>'
				p[-1, 0] = BCHelper.PC_Right;
				// Top-Right 'v>'
				p[right, 0] = BCHelper.PC_Down;
				p[right + 1, 0] = BCHelper.PC_Right;
				// Mid-Left '^_'
				p[0, mid] = BCHelper.If_Horizontal;
				p[-1, mid] = BCHelper.PC_Up;
				// Mid-Right '^'
				p[right + 1, mid] = BCHelper.PC_Up;
				//Bottom-Left '^!'
				p[0, bottom] = BCHelper.PC_Up;
				p[1, bottom] = BCHelper.Not;
				//Bottom-Right '<'
				p[right, bottom] = BCHelper.PC_Left;

				// Walkway top (Statement to 'v')
				p.FillRowWW(0, cp_body.Width, right);
				// Walkway bottom (Condition to '<')
				p.FillRowWW(bottom, cp_cond.Width + 2, right);
				// Walkway left-lower ('>' to '^')
				p.FillColWW(-1, 1, mid);
				// Walkway left-upper ('_' to '^')
				p.FillColWW(0, mid + 1, bottom);
				// Walkway right-lower ('>' to '^')
				p.FillColWW(right + 1, 1, mid);
				// Walkway right-full ('v' to '<')
				p.FillColWW(right, 1, bottom);
				// Walkway middle ('^_' to '^')
				p.FillRowWW(mid, 1, right);

				// Insert Statement
				p.SetAt(0, 0, cp_body);
				// Inser Condition
				p.SetAt(2, bottom, cp_cond);

				p.normalizeX();

				return p;
			}
		}
	}

	#endregion Constructs
}