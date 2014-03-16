using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Method : ASTObject
	{
		private static int _METHODADDRESS_COUNTER = 0;
		protected static int METHODADDRESS_COUNTER { get { return _METHODADDRESS_COUNTER++; } }

		public BType ResultType;
		public string Identifier;
		public List<VarDeclaration> Parameter;

		public List<VarDeclaration> Variables; // Includes Parameter & Temps
		public Statement_StatementList Body;

		private int _METHODADDRESS = -1;
		public int MethodAddr { get { return _METHODADDRESS; } private set { _METHODADDRESS = value; } }

		public Method(SourceCodePosition pos, Method_Header h, Method_Body b)
			: this(pos, h.ResultType, h.Identifier, h.Parameter, b.Variables, b.Body)
		{
			//--
		}

		public Method(SourceCodePosition pos, BType t, string id, List<VarDeclaration> p, List<VarDeclaration> v, Statement_StatementList b)
			: base(pos)
		{
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
				MethodAddr,
				Identifier,
				ResultType.getDebugString(),
				indent(getDebugStringForList(Parameter)),
				indent(getDebugStringForList(Variables.Where(p => !Parameter.Contains(p)).ToList())),
				indent(Body.getDebugString()));
		}

		public void createCodeAddress()
		{
			MethodAddr = METHODADDRESS_COUNTER;
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
			_METHODADDRESS_COUNTER = 0;
		}

		#region GenerateCode

		public CodePiece generateCode(int meth_offset_x, int meth_offset_y)
		{
			CodePiece p = new CodePiece();

			// Generate Space for Variables
			p.AppendBottom(generateCode_Variables(meth_offset_x, meth_offset_y));

			//<<-- Entry Point

			// Generate Initialization of Variables
			CodePiece p_vi = generateCode_VariableIntialization();
			p_vi.SetTag(0, 0, new MethodEntry_FullInitialization_Tag(this));
			p.AppendBottom(p_vi);

			// Generate Initialization of Parameters
			p.AppendBottom(generateCode_ParameterIntialization());

			p.AppendBottom(generateCode_Body());

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
					lit[0, 0] = CodeGenOptions.DefaultVarDeclarationSymbol.copyWithTag(new VarDeclaration_Tag(var));
				}
				else
				{
					int sz = (var as VarDeclaration_Array).Size;
					lit.Fill(0, 0, sz, 1, CodeGenOptions.DefaultVarDeclarationSymbol, new VarDeclaration_Tag(var));
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

			List<TwoDirectionCodePiece> varDecls = new List<TwoDirectionCodePiece>();

			for (int i = 0; i < Variables.Count; i++)
			{
				VarDeclaration var = Variables[i];

				if (Parameter.Contains(var))
					continue;

				varDecls.Add(new TwoDirectionCodePiece(var.generateCode(false), var.generateCode(true)));
			}

			if (varDecls.Count % 2 != 0)
				varDecls.Add(new TwoDirectionCodePiece());

			varDecls = varDecls.OrderByDescending(t => t.MaxWidth).ToList();


			for (int i = 0; i < varDecls.Count; i += 2)
			{
				CodePiece cp_a = varDecls[i].Normal;
				CodePiece cp_b = varDecls[i + 1].Reversed;

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

			p.forceNonEmpty();

			return p;
		}

		private CodePiece generateCode_ParameterIntialization()
		{
			CodePiece p = new CodePiece();

			List<TwoDirectionCodePiece> paramDecls = new List<TwoDirectionCodePiece>();

			for (int i = Parameter.Count - 1; i >= 0; i--)
			{
				VarDeclaration var = Parameter[i];

				CodePiece p_init_lr = var.generateCode_Parameter(false);
				CodePiece p_init_rl = var.generateCode_Parameter(true);

				paramDecls.Add(new TwoDirectionCodePiece(var.generateCode(false), var.generateCode(true)));
			}

			if (paramDecls.Count % 2 != 0)
				paramDecls.Add(new TwoDirectionCodePiece());

			for (int i = 0; i < paramDecls.Count; i += 2)
			{
				CodePiece cp_a = paramDecls[i].Normal;
				CodePiece cp_b = paramDecls[i + 1].Reversed;

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

		private CodePiece generateCode_Body()
		{
			CodePiece p = Body.generateCode(false);

			p.normalizeX();

			p[-1, 0] = BCHelper.PC_Right;

			p.normalizeX();

			p.Fill(0, p.MinY, 1, 0, BCHelper.Walkway);

			return p;
		}

		#endregion
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
		public Statement_StatementList Body;

		public Method_Body(SourceCodePosition pos, List<VarDeclaration> v, Statement_StatementList b)
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