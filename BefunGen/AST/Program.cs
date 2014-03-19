using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using BefunGen.MathExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Program : ASTObject
	{
		//TODO Add global Variables
		//TODO Add global constants (like #define )
		//TODO Add For-Loop (-> Convert to While)-> Direct when Gen AST
		//TODO Identifier dürfen keine Keywords sein -> Keywordlist
		public string Identifier;
		public Method MainMethod;
		public List<Method> MethodList; // Includes MainStatement (at 0)

		public Program(SourceCodePosition pos, string id, Method m, List<Method> mlst)
			: base(pos)
		{
			this.Identifier = id;
			this.MainMethod = m;
			this.MethodList = mlst.ToList();

			MethodList.Insert(0, MainMethod);
		}

		public override string getDebugString()
		{
			return string.Format("#Program ({0})\n[\n{1}\n]", Identifier, indent(getDebugStringForList(MethodList)));
		}

		#region Prepare

		public void prepare()
		{
			// Reset ID-Counter
			Method.resetCounter();
			VarDeclaration.resetCounter();
			Statement.resetCounter();

			forceMethodReturn();		// Every Method must always end with a RETURN  {{ Can manipulate Code (Append Returns) }}
			addressMethods();			// Methods get their Address
			addressCodePoints();		// CodeAdressesTargets get their Address
			linkVariables();			// Variable-uses get their ID
			linkMethods();				// Methodcalls get their ID   &&   Labels + MethodCalls get their CodePointAddress
			linkResultTypes();			// Statements get their Result-Type (and implicit casting is added)
		}

		private void addressMethods()
		{
			foreach (Method m in MethodList)
				m.createCodeAddress();
		}

		private void addressCodePoints()
		{
			foreach (Method m in MethodList)
				m.addressCodePoints();
		}

		private void linkVariables()
		{
			foreach (Method m in MethodList)
				m.linkVariables();
		}

		private void linkMethods()
		{
			foreach (Method m in MethodList)
				m.linkMethods(this);
		}

		private void linkResultTypes()
		{
			foreach (Method m in MethodList)
				m.linkResultTypes();
		}

		private void forceMethodReturn()
		{
			foreach (Method m in MethodList)
				m.forceMethodReturn(m == MainMethod);
		}

		#endregion

		public Method findMethodByIdentifier(string ident)
		{
			return MethodList.Count(p => p.Identifier.ToLower() == ident.ToLower()) == 1 ? MethodList.Single(p => p.Identifier.ToLower() == ident.ToLower()) : null;
		}

		public int getMaxReturnValueWidth()
		{
			return MathExt.Max(1, MethodList.Select(p => p.ResultType.GetSize()).ToArray());
		}

		public CodePiece generateCode()
		{
			List<Tuple<MathExt.Point, CodePiece>> meth_pieces = new List<Tuple<MathExt.Point, CodePiece>>();

			CodePiece p = new CodePiece();

			int maxReturnValWidth = getMaxReturnValueWidth();

			int lane_start_y = 4;

			int meth_offset_x = 4 + CodeGenConstants.LANE_VERTICAL_MARGIN;
			int meth_offset_y = lane_start_y + 3; // +3 For the MinY=3 of VerticalLaneTurnout_Dec || bzw +2 for LaneChooser

			#region Insert Methods

			for (int i = 0; i < MethodList.Count; i++)
			{
				Method m = MethodList[i];

				CodePiece p_method = m.generateCode(meth_offset_x, meth_offset_y);

				if (p.hasActiveTag(typeof(MethodEntry_FullInitialization_Tag))) // Force MethodEntry_FullIntialization Distance (at least so that lanes can be generated)
				{
					int pLast = p.findAllActiveCodeTags(typeof(MethodEntry_FullInitialization_Tag)).Last().Y;
					int pNext = p_method.findAllActiveCodeTags(typeof(MethodEntry_FullInitialization_Tag)).First().Y + (meth_offset_y - p_method.MinY);
					int overflow = (pNext - pLast) - CodePieceStore.VerticalLaneTurnout_Dec(false).Height;

					if (overflow < 0)
					{
						meth_offset_y -= overflow;
					}
				}

				int mx = meth_offset_x - p_method.MinX;
				int my = meth_offset_y - p_method.MinY;

				meth_pieces.Add(Tuple.Create(new MathExt.Point(mx, my), p_method));

				p.SetAt(mx, my, p_method);

				meth_offset_y += p_method.Height + CodeGenConstants.VERTICAL_METHOD_DISTANCE;
			}

			#endregion

			int highway_x = MathExt.Max(p.MaxX, 3 + CodePieceStore.BooleanStackFlooder().Width, CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X + maxReturnValWidth); //MaxMethodWidth, TopLane_Left, Space for TempVars

			#region Generate Lanes (Left Lane && Right Lane)

			List<TagLocation> method_entries = p.findAllActiveCodeTags(typeof(MethodEntry_FullInitialization_Tag)) // Left Lane
				.OrderBy(tp => tp.Y)
				.ToList();
			List<TagLocation> code_entries = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalReEntry_Tag)) // Right Lane
				.OrderBy(tp => tp.Y)
				.ToList();

			int last;
			bool first;

			//######### LEFT LANE #########

			first = true;
			last = lane_start_y;
			foreach (TagLocation method_entry in method_entries)
			{
				CodePiece p_turnout = CodePieceStore.VerticalLaneTurnout_Dec(first);

				p.FillColWW(0, last, method_entry.Y + p_turnout.MinY);
				p.SetAt(0, method_entry.Y, p_turnout);
				p.FillRowWW(method_entry.Y, 4, method_entry.X);
				last = method_entry.Y + p_turnout.MaxY;
				first = false;
			}
			//p.FillColWW(0, last, p.MaxY);

			//######### RIGHT LANE #########

			first = true;
			last = lane_start_y;
			foreach (TagLocation code_entry in code_entries)
			{
				CodePiece p_turnout = CodePieceStore.VerticalLaneTurnout_Test();

				p.FillColWW(2, last, code_entry.Y + p_turnout.MinY);
				p.SetAt(2, code_entry.Y, p_turnout);
				p.CreateRowWW(code_entry.Y, 4, code_entry.X);
				last = code_entry.Y + p_turnout.MaxY;
				first = false;
			}
			//p.FillColWW(2, last, p.MaxY);

			//######### MIDDLE LANE #########

			p.Fill(1, lane_start_y, 2, p.MaxY, BCHelper.PC_Jump);

			//######### POP LANE #########

			p.Fill(3, lane_start_y, 4, p.MaxY, BCHelper.Stack_Pop);

			#endregion

			#region Generate Top Lane

			// v
			// 1 v{STACKFLOODER}        <
			//                          |
			// v                        <
			// .#.$                     #
			// .#.$                     !
			// .#.$
			CodePiece p_TopLane = new CodePiece();

			p_TopLane[0, 0] = BCHelper.PC_Down;
			p_TopLane[0, 1] = BCHelper.Digit_0;
			p_TopLane[0, 2] = BCHelper.Walkway;
			p_TopLane[0, 3] = BCHelper.PC_Down;

			p_TopLane[1, 3] = BCHelper.Walkway;

			p_TopLane[2, 1] = BCHelper.PC_Down;
			p_TopLane[2, 2] = BCHelper.Walkway;
			p_TopLane[2, 3] = BCHelper.Walkway;

			p_TopLane.FillRowWW(3, 3, highway_x);

			CodePiece p_flooder = CodePieceStore.BooleanStackFlooder();
			p_TopLane.SetAt(3, 1, p_flooder);
			p_TopLane.FillRowWW(1, 3 + p_flooder.Width, highway_x);

			p_TopLane[highway_x, 1] = BCHelper.PC_Left;
			p_TopLane[highway_x, 2] = BCHelper.If_Vertical;
			p_TopLane[highway_x, 3] = BCHelper.PC_Left;
			p_TopLane[highway_x, 4] = BCHelper.PC_Jump;
			p_TopLane[highway_x, 5] = BCHelper.Not;

			p[CodeGenConstants.TMP_FIELD_IO_ARR.X, CodeGenConstants.TMP_FIELD_IO_ARR.Y] = CodeGenOptions.DefaultTempSymbol.copyWithTag(new TemporaryCodeField_Tag());
			p[CodeGenConstants.TMP_FIELD_OUT_ARR.X, CodeGenConstants.TMP_FIELD_OUT_ARR.Y] = CodeGenOptions.DefaultTempSymbol.copyWithTag(new TemporaryCodeField_Tag());
			p[CodeGenConstants.TMP_FIELD_JMP_ADDR.X, CodeGenConstants.TMP_FIELD_JMP_ADDR.Y] = CodeGenOptions.DefaultTempSymbol.copyWithTag(new TemporaryCodeField_Tag());
			p.Fill(CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X, CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y,
				CodeGenConstants.TMP_ARRFIELD_RETURNVAL.X + maxReturnValWidth, CodeGenConstants.TMP_ARRFIELD_RETURNVAL.Y + 1,
				CodeGenOptions.DefaultResultTempSymbol,
				new TemporaryResultCodeField_Tag(maxReturnValWidth));

			p.SetAt(0, 0, p_TopLane, true);

			#endregion

			#region Generate Highway (Path on right side of code)

			List<TagLocation> code_exits = p.findAllActiveCodeTags(typeof(MethodCall_HorizontalExit_Tag))
				.OrderBy(tp => tp.Y)
				.ToList();

			first = true;
			last = 6;
			foreach (TagLocation exit in code_exits)
			{
				p.FillColWW(highway_x, last, exit.Y);
				p[highway_x, exit.Y] = BCHelper.PC_Up;
				p.CreateRowWW(exit.Y, exit.X + 1, highway_x);
				last = exit.Y + 1;

				exit.Tag.deactivate();

				first = false;
			}

			#endregion

			return p;
		}
	}

	public class Program_Footer : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public Program_Footer(SourceCodePosition pos)
			: base(pos)
		{
		}

		public override string getDebugString()
		{
			throw new AccessTemporaryASTObjectException(Position);
		}
	}

	public class Program_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public string Identifier;

		public int DisplayWidth;
		public int DisplayHeight;

		public Program_Header(SourceCodePosition pos, string id)
			: base(pos)
		{
			this.Identifier = id;
			DisplayHeight = 0;
			DisplayWidth = 0;
		}

		public Program_Header(SourceCodePosition pos, string id, int w, int h)
			: base(pos)
		{
			this.Identifier = id;
			DisplayHeight = w;
			DisplayWidth = h;
		}

		public override string getDebugString()
		{
			throw new AccessTemporaryASTObjectException(Position);
		}
	}
}