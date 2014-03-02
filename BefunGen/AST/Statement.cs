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

		public override CodePiece generateCode(bool reversed) // TODO vllt so machen wie auch while/repUntil Stmt's (?) -> mimt SetAt
		{
			List<TwoDirectionCodePiece> cp_stmts = new List<TwoDirectionCodePiece>();

			for (int i = 0; i < List.Count; i++)
			{
				Statement stmt = List[i];

				//TODO Doing this two times makes the parse O(2^n) instead of O(n) --> what do ? (you could just dont do it - would change nothing .. just the sorting would be sometimes a little bit off)
				cp_stmts.Add(new TwoDirectionCodePiece(stmt.generateCode(false), stmt.generateCode(true)));
			}

			if (cp_stmts.Count % 2 == 0)
				cp_stmts.Add(new TwoDirectionCodePiece());

			CodePiece p = new CodePiece();
			if (reversed)
			{
				#region Reversed
				for (int i = 0; i < cp_stmts.Count - 2; i += 2)
				{
					CodePiece cp_a = cp_stmts[i].Reversed;
					CodePiece cp_b = cp_stmts[i + 1].Normal;

					cp_a.normalizeX();
					cp_b.normalizeX();

					cp_a.AddXOffset(-cp_a.MaxX + 1);
					cp_b.AddXOffset(-cp_b.MaxX + 1);

					int mw = Math.Min(cp_a.MinX - 1, cp_b.MinX - 1);

					cp_a[1, 0] = BCHelper.PC_Left;
					cp_b[1, 0] = BCHelper.PC_Down;

					cp_a.Fill(mw + 1, 0, cp_a.MinX, 1, BCHelper.Walkway);
					cp_b.Fill(mw + 1, 0, cp_b.MinX, 1, BCHelper.Walkway);

					cp_a[mw, 0] = BCHelper.PC_Down;
					cp_b[mw, 0] = BCHelper.PC_Right;


					cp_a.Fill(1, cp_a.MinY, 2, 0, BCHelper.Walkway);
					cp_a.Fill(cp_a.MinX, 1, cp_a.MinX + 1, cp_a.MaxY, BCHelper.Walkway);

					cp_b.Fill(cp_b.MinX, cp_b.MinY, cp_b.MinX + 1, 0, BCHelper.Walkway);
					cp_b.Fill(1, 1, 2, cp_b.MaxY, BCHelper.Walkway);


					p.AppendBottom(cp_a);
					p.AppendBottom(cp_b);

					if (i == 0)
					{
						p.AddYOffset(cp_a.MinY);
					}
				}

				CodePiece last = cp_stmts[cp_stmts.Count - 1].Reversed;
				last.normalizeX();
				last.AddXOffset(-last.MaxX + 1);

				if (cp_stmts.Count == 1)
				{
					p.AddYOffset(last.MinY);
				}

				int left = Math.Min(p.MinX - 1, last.MinX - 1);

				last[1, 0] = BCHelper.PC_Left;
				last.Fill(left + 1, 0, last.MinX, 1, BCHelper.Walkway);

				p.AppendBottom(last);

				int bottom = p.MaxY - last.MaxY;

				if (bottom != 0)
				{
					p[left, bottom] = BCHelper.PC_Up;
				}

				p[left, 0] = BCHelper.PC_Left;

				p.Fill(left, 1, left + 1, bottom, BCHelper.Walkway);

				p.normalizeX();
				#endregion
			}
			else
			{
				#region Normal
				for (int i = 0; i < cp_stmts.Count - 2; i += 2)
				{
					CodePiece cp_a = cp_stmts[i].Normal;
					CodePiece cp_b = cp_stmts[i + 1].Reversed;

					cp_a.normalizeX();
					cp_b.normalizeX();

					int mw = Math.Max(cp_a.MaxX, cp_b.MaxX);

					cp_a[-1, 0] = BCHelper.PC_Right;
					cp_b[-1, 0] = BCHelper.PC_Down;

					cp_a.Fill(cp_a.MaxX, 0, mw, 1, BCHelper.Walkway);
					cp_b.Fill(cp_b.MaxX, 0, mw, 1, BCHelper.Walkway);

					cp_a[mw, 0] = BCHelper.PC_Down;
					cp_b[mw, 0] = BCHelper.PC_Left;


					cp_a.Fill(-1, cp_a.MinY, 0, 0, BCHelper.Walkway);
					cp_a.Fill(cp_a.MaxX - 1, 1, cp_a.MaxX, cp_a.MaxY, BCHelper.Walkway);

					cp_b.Fill(cp_b.MaxX - 1, cp_b.MinY, cp_b.MaxX, 0, BCHelper.Walkway);
					cp_b.Fill(-1, 1, 0, cp_b.MaxY, BCHelper.Walkway);


					p.AppendBottom(cp_a);
					p.AppendBottom(cp_b);

					if (i == 0)
					{
						p.AddYOffset(cp_a.MinY);
					}
				}

				CodePiece last = cp_stmts[cp_stmts.Count - 1].Normal;
				last.normalizeX();

				if (cp_stmts.Count == 1)
				{
					p.AddYOffset(last.MinY);
				}

				int right = Math.Max(p.MaxX, last.MaxX);

				last.AppendLeft(BCHelper.PC_Right);
				last.Fill(last.MaxX, 0, right, 1, BCHelper.Walkway);

				p.AppendBottom(last);

				int bottom = p.MaxY - last.MaxY;

				if (bottom != 0)
				{
					p[right, bottom] = BCHelper.PC_Up;
				}

				p[right, 0] = BCHelper.PC_Right;

				p.Fill(right, 1, right + 1, bottom, BCHelper.Walkway);

				p.normalizeX();
				#endregion
			}

			return p;
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
						throw new ImplicitCastException(present, expected, CallParameter[i].Position);
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
					throw new ImplicitCastException(present, expected, Value.Position);
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
		public Expression Value;

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

			if (Value.getResultType() is BType_Array) //TODO Output Char Array !
				throw new ImplicitCastException(new BType_Int(Position), Value.getResultType(), Value.Position);
			if (Value.getResultType() is BType_Bool)
				throw new ImplicitCastException(new BType_Int(Position), Value.getResultType(), Value.Position);
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
			CodePiece p = Value.generateCode(reversed);

			BefungeCommand cmd_out;

			if (Value.getResultType() is BType_Char)
				cmd_out = BCHelper.Out_ASCII;
			else
				cmd_out = BCHelper.Out_Int;

			if (reversed)
				p.AppendLeft(cmd_out);
			else
				p.AppendRight(cmd_out);

			p.normalizeX();

			return p;
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
			BType expected = new BType_Char(null);

			if (present != expected)
			{
				throw new WrongTypeException(present, expected, ValueTarget.Position);
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
			throw new NotImplementedException(); //TODO Implement
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

			if (!(present == new BType_Int(null) || present == new BType_Digit(null) || present == new BType_Char(null)))
			{
				throw new WrongTypeException(present, new List<BType>() { new BType_Int(null), new BType_Digit(null), new BType_Char(null) }, Target.Position);
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

			if (!(present == new BType_Int(null) || present == new BType_Digit(null) || present == new BType_Char(null)))
			{
				throw new WrongTypeException(present, new List<BType>() { new BType_Int(null), new BType_Digit(null), new BType_Char(null) }, Target.Position);
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

			BType present = Target.getResultType();
			BType expected = Expr.getResultType();

			if (present != expected)
			{
				if (present.isImplicitCastableTo(expected))
					Expr = new Expression_Cast(Expr.Position, expected, Expr);
				else
					throw new ImplicitCastException(present, expected, Expr.Position);
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
	}

	#endregion Operations

	#region Constructs

	public class Statement_If : Statement //TODO ELSE-FI Syntax (But no new ASt-Object - it will generate Ifs with ELses in it ..,)
	{
		public Expression Condition;
		public Statement Body;
		public Statement Else;

		public Statement_If(SourceCodePosition pos, Expression c, Statement b)
			: base(pos)
		{
			this.Condition = c;
			this.Body = b;
			this.Else = new Statement_NOP(new SourceCodePosition());
		}

		public Statement_If(SourceCodePosition pos, Expression c, Statement b, Statement e)
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
					throw new ImplicitCastException(present, expected, Condition.Position);
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
		public Statement Body;

		public Statement_While(SourceCodePosition pos, Expression c, Statement b)
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
					throw new ImplicitCastException(present, expected, Condition.Position);
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
		public Statement Body;

		public Statement_RepeatUntil(SourceCodePosition pos, Expression c, Statement b)
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
					throw new ImplicitCastException(present, expected, Condition.Position);
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