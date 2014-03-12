using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using BefunGen.MathExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public abstract class Statement : ASTObject //TODO GET/SET/DEFINE DISPLAY
	{
		public Statement(SourceCodePosition pos)
			: base(pos)
		{
			//--
		}

		public abstract void linkVariables(Method owner);
		public abstract void linkResultTypes(Method owner);
		public abstract void linkMethods(Program owner);

		public abstract Statement_Label findLabelByIdentifier(string ident);

		public abstract CodePiece generateCode(bool reversed);
	}

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

		public override CodePiece generateCode(bool reversed)
		{
			if (List.Count == 0)
			{
				return new CodePiece();
			}
			else if (List.Count == 1)
			{
				return List[0].generateCode(reversed);
			}

			// ##### STATEMENTS ######

			List<Statement> stmts = List.ToList();
			if (stmts.Count % 2 == 0)
				stmts.Add(new Statement_NOP(Position));

			// ##### CODEPIECES ######

			List<CodePiece> cps = new List<CodePiece>();
			for (int i = 0; i < stmts.Count; i++)
			{
				cps.Add(stmts[i].generateCode(reversed ^ (i % 2 != 0)));
				cps[i].normalizeX();

				if (cps[i].Height == 0) // No total empty statements
					cps[i][0, 0] = BCHelper.Walkway;
			}

			// ##### Y-POSITIONS ######

			List<int> ypos = new List<int>();
			ypos.Add(0);
			for (int i = 1; i < cps.Count; i++)
			{
				ypos.Add(ypos[i - 1] + cps[i - 1].MaxY - cps[i].MinY);
			}

			if (reversed)
			{
				CodePiece p = new CodePiece();

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

				p.normalizeX();

				#endregion

				return p;
			}
			else
			{
				CodePiece p = new CodePiece();

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

				p.normalizeX();

				#endregion

				return p;
			}
		}
	}

	public class Statement_MethodCall : Statement
	{
		public List<Expression> CallParameter;

		public string Identifier; // Temporary -- before linking;
		public Method Target;

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
			return string.Format("#MethodCall {{{0}}} --> #Parameter: ({1})", Target.ID, indent(getDebugCommaStringForList(CallParameter)));
		}

		public override void linkVariables(Method owner)
		{
			foreach (Expression e in CallParameter)
				e.linkVariables(owner);
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

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new NotImplementedException(); //TODO Implement
		}
	}

	#endregion

	#region Keywords

	public class Statement_Label : Statement
	{
		private static int _L_ID_COUNTER = 100;
		protected static int L_ID_COUNTER { get { return _L_ID_COUNTER++; } }

		public string Identifier;
		public readonly int ID;

		public Statement_Label(SourceCodePosition pos, string ident)
			: base(pos)
		{
			this.Identifier = ident;
			ID = L_ID_COUNTER;
		}

		public static void resetCounter()
		{
			_L_ID_COUNTER = 1;
		}

		public override string getDebugString()
		{
			return string.Format("#LABEL: {{{0}}}", ID);
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

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return ident.ToLower() == Identifier.ToLower() ? this : null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new NotImplementedException(); //TODO Implement
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
			return string.Format("#GOTO: {{{0}}}", Target.ID);
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

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new NotImplementedException(); //TODO Implement
		}
	}

	public class Statement_Return : Statement
	{
		public Expression Value;

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
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			throw new NotImplementedException(); //TODO Implement
		}
	}

	public class Statement_Out : Statement
	{
		enum Out_Mode { OUT_INT, OUT_CHAR, OUT_CHAR_ARR };

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
			if (reversed)
			{
				CodePiece p = Value.generateCode(reversed);

				#region Reversed

				throw new NotImplementedException();

				#endregion

				return p;
			}
			else
			{
				// {M}{TX}{TY}p>,{TX}:{TY}g  #v_$
				//             ^p{TY}\-1g{TY}:<
				CodePiece p = Value.generateCode(reversed);

				#region Normal

				throw new NotImplementedException();

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

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (Value.Count == 0)
				return new CodePiece();

			if (reversed)
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
		public Expression_ValuePointer ValueTarget;

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

		public override void linkResultTypes(Method owner)
		{
			ValueTarget.linkResultTypes(owner);

			BType present = ValueTarget.getResultType();

			if (!(present is BType_Int || present is BType_Char))
			{
				throw new WrongTypeException(ValueTarget.Position, present, new BType_Int(Position), new BType_Char(Position));
			}
		}

		public override void linkMethods(Program owner)
		{
			ValueTarget.linkMethods(owner);
		}

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			CodePiece p = new CodePiece();

			if (ValueTarget.getResultType() is BType_Char)
				p[0, 0] = BCHelper.In_ASCII;
			else if (ValueTarget.getResultType() is BType_Int)
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

		public override void linkMethods(Program owner)
		{
			//NOP
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

		public override void linkResultTypes(Method owner)
		{
			//NOP
		}

		public override void linkMethods(Program owner)
		{
			//NOP
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

		public override Statement_Label findLabelByIdentifier(string ident)
		{
			return null;
		}

		public override CodePiece generateCode(bool reversed)
		{
			if (Else.GetType() == typeof(Statement_NOP))
			{
				return generateCode_If(reversed);
			}
			else
			{
				return generateCode_IfElse(reversed);
			}
		}

		public CodePiece generateCode_If(bool reversed)
		{
			CodePiece cp_cond = Condition.generateCode(reversed);
			cp_cond.normalizeX();

			CodePiece cp_body_if = Body.generateCode(reversed);
			cp_body_if.normalizeX();

			if (reversed)
			{
				// _v#!   CONDITION
				//  
				// 1>             v
				// 
				// ^      IF      <
				CodePiece p = new CodePiece();

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

				return p;
			}
			else
			{
				// CONDITION #v_
				// 
				// v            <0
				// 
				// >   IF        ^
				CodePiece p = new CodePiece();

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

				return p;
			}
		}

		public CodePiece generateCode_IfElse(bool reversed)
		{
			CodePiece cp_cond = Condition.generateCode(reversed);
			cp_cond.normalizeX();

			CodePiece cp_if = Body.generateCode(reversed);
			cp_if.normalizeX();

			CodePiece cp_else = Else.generateCode(reversed);
			cp_else.normalizeX();

			if (reversed)
			{
				// <v  CONDITION
				// 
				//  >          v
				// 
				//             #
				// ^     IF    <
				//             |
				// 
				// ^    ELSE   <
				CodePiece p = new CodePiece();

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

				return p;
			}
			else
			{
				// CONDITION   v>
				// 
				// v           <
				// 
				// #
				// >     IF     ^
				// |
				// 
				// >    ELSE    ^
				CodePiece p = new CodePiece();

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

				return p;
			}
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