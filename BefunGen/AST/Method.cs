using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Method : ASTObject
	{
		private static int _M_ID_COUNTER = 100;
		protected static int M_ID_COUNTER { get { return _M_ID_COUNTER++; } }

		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public List<VarDeclaration> Variables; // Includes Parameter
		public Statement Body;

		public readonly int ID;

		public Method(SourceCodePosition pos, Method_Header h, Method_Body b)
			: this(pos, h.ResultType, h.Identifier, h.Parameter, b.Variables, b.Body)
		{
			//--
		}

		public Method(SourceCodePosition pos, BType t, string id, List<VarDeclaration> p, List<VarDeclaration> v, Statement b)
			: base(pos)
		{
			this.ID = M_ID_COUNTER;

			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;

			this.Variables = v;
			this.Body = b;

			Variables.AddRange(Parameter);
		}

		public override string getDebugString()
		{
			return string.Format("#Method ({{{0}}}({1}):{2})\n[\n#Parameter:\n{3}\n#Variables:\n{4}\n#Body:\n{5}\n]",
				ID,
				Identifier,
				ResultType.getDebugString(),
				indent(getDebugStringForList(Parameter)),
				indent(getDebugStringForList(Variables.Where(p => !Parameter.Contains(p)).ToList())),
				indent(Body.getDebugString()));
		}

		public void linkVariables()
		{
			Body.linkVariables(this);
		}

		public void linkMethods(Program owner)
		{
			Body.linkMethods(owner);
		}

		public void linkResultTypes()
		{
			Body.linkResultTypes(this);
		}

		public VarDeclaration findVariableByIdentifier(string ident)
		{
			return Variables.Count(p => p.Identifier.ToLower() == ident.ToLower()) == 1 ? Variables.Single(p => p.Identifier.ToLower() == ident.ToLower()) : null;
		}

		public Statement_Label findLabelByIdentifier(string ident)
		{
			return Body.findLabelByIdentifier(ident);
		}

		public static void resetCounter()
		{
			_M_ID_COUNTER = 1;
		}

		public CodePiece generateCode(int meth_offset_x, int meth_offset_y)
		{
			CodePiece p = new CodePiece();

			p.AppendBottom(generateCode_Variables(meth_offset_x, meth_offset_y));

			//<<-- Entry Point

			p.AppendBottom(generateCode_VariableIntialization());

			//<<-- Intialize Parameter

			//<<-- Statement Block

			return p;
		}

		private CodePiece generateCode_Variables(int mo_x, int mo_y)
		{
			CodePiece p = new CodePiece();

			int paramX = 0;
			int paramY = 0;

			int max_arr = 0;
			if (Variables.Count(t => t is VarDeclaration_Array) > 0)
				max_arr = Variables.Where(t => t is VarDeclaration_Array).Select(t => t as VarDeclaration_Array).Max(t => t.Size);

			int maxwidth = Math.Max(max_arr, CodeGenOptions.DefaultVarDeclarationWidth);

			for (int i = 0; i < Variables.Count; i++)
			{
				VarDeclaration var = Variables[i];

				CodePiece lit = new CodePiece();

				if (paramX >= maxwidth)
				{	// Next Line
					paramX = 0;
					paramY++;
				}

				if (paramX > 0 && var is VarDeclaration_Array && (paramX + (var as VarDeclaration_Array).Size) > maxwidth)
				{	// Next Line
					paramX = 0;
					paramY++;
				}

				if (var is VarDeclaration_Value)
				{
					lit[0, 0] = CodeGenOptions.DefaultVarDeclarationSymbol.copyWithTag(var);
				}
				else
				{
					int sz = (var as VarDeclaration_Array).Size;
					lit.Fill(0, 0, sz, 1, CodeGenOptions.DefaultVarDeclarationSymbol, var);
				}

				var.CodePositionX = mo_x + paramX;
				var.CodePositionY = mo_y + paramY;

				p.SetAt(paramX, paramY, lit);
				paramX += lit.Width;
			}

			return p;
		}

		private CodePiece generateCode_VariableIntialization()
		{
			CodePiece p = new CodePiece();

			List<Tuple<CodePiece, CodePiece>> varDecls = new List<Tuple<CodePiece, CodePiece>>();

			for (int i = 0; i < Variables.Count; i++)
			{
				VarDeclaration var = Variables[i];

				CodePiece p_init_lr = var.generateCode(false);
				CodePiece p_init_rl = var.generateCode(true);

				varDecls.Add(Tuple.Create(p_init_lr, p_init_rl));
			}

			if (varDecls.Count % 2 != 0)
				varDecls.Add(Tuple.Create(new CodePiece(), new CodePiece()));

			Console.WriteLine();
			varDecls = varDecls.OrderByDescending(t => t.Item2.Width).ToList();


			for (int i = 0; i < varDecls.Count; i += 2)
			{
				CodePiece cp_a = varDecls[i].Item1;
				CodePiece cp_b = varDecls[i + 1].Item2;

				cp_a.normalizeX();
				cp_b.normalizeX();

				int mw = Math.Max(cp_a.MaxX, cp_b.MaxX);

				cp_a.AppendLeft(BCHelper.PC_Right);
				cp_b.AppendLeft(BCHelper.PC_Down);

				cp_a.Fill(cp_a.MaxX, 0, mw, 1, BCHelper.Walkway);
				cp_b.Fill(cp_b.MaxX, 0, mw, 1, BCHelper.Walkway);

				cp_a[mw, 0] = BCHelper.PC_Down;
				cp_b[mw, 0] = BCHelper.PC_Left;

				for (int y = cp_a.MinY; y < cp_a.MaxY; y++)
					if (y != 0)
						cp_a[cp_a.MaxX - 1, y] = BCHelper.Walkway;

				for (int y = cp_b.MinY; y < cp_b.MaxY; y++)
					if (y != 0)
						cp_b[cp_b.MaxX - 1, y] = BCHelper.Walkway;

				cp_a.normalizeX();
				cp_b.normalizeX();

				p.AppendBottom(cp_a);
				p.AppendBottom(cp_b);
			}

			p.normalizeX();

			return p;
		}
	}

	public class Method_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public Method_Header(SourceCodePosition pos, BType t, string id, List<VarDeclaration> p)
			: base(pos)
		{
			this.ResultType = t;
			this.Identifier = id;
			this.Parameter = p;
		}

		public override string getDebugString()
		{
			throw new InvalidASTStateException(Position);
		}
	}

	public class Method_Body : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public List<VarDeclaration> Variables;
		public Statement Body;

		public Method_Body(SourceCodePosition pos, List<VarDeclaration> v, Statement b)
			: base(pos)
		{
			this.Variables = v;
			this.Body = b;
		}

		public override string getDebugString()
		{
			throw new InvalidASTStateException(Position);
		}
	}
}