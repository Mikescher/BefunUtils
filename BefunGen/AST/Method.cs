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

		public Program Owner;

		public readonly BType ResultType;
		public readonly string Identifier;
		public readonly List<VarDeclaration> Parameter;

		public readonly List<VarDeclaration> Variables; // Includes Parameter & Temps
		public readonly Statement_StatementList Body;

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

		public void addressCodePoints()
		{
			Body.addressCodePoints();
		}

		public void linkMethods(Program owner)
		{
			Body.linkMethods(owner);
		}

		public void linkResultTypes()
		{
			Body.linkResultTypes(this);
		}

		public void forceMethodReturn(bool isMain)
		{
			if (!Body.allPathsReturn())
			{
				if (isMain)
				{
					Body.List.Add(new Statement_Quit(Position));
				} 
				else 	if (ResultType is BType_Void)
				{
					Body.List.Add(new Statement_Return(Position));
				}
				else
				{
					throw new NotAllPathsReturnException(this, Position);
				}
			}
		}

		public VarDeclaration findVariableByIdentifier(string ident)
		{
			List<VarDeclaration> r = Variables.Where(p => p.Identifier.ToLower() == ident.ToLower())
				.Concat(Owner.Variables.Where(p => p.Identifier.ToLower() == ident.ToLower()))
				.Concat(Owner.Constants.Where(p => p.Identifier.ToLower() == ident.ToLower()))
				.ToList();

			return r.Count() == 1 ? r.Single() : null;
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

			// Generate Initialization of Variables
			CodePiece p_vi = generateCode_VariableIntialization();
			p_vi.SetTag(0, 0, new MethodEntry_FullInitialization_Tag(this));  //v<-- Entry Point
			p.AppendBottom(p_vi);

			// Generate Initialization of Parameters
			p.AppendBottom(generateCode_ParameterIntialization());

			// Generate Statements
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


				cp_a.FillColWW(cp_a.MaxX - 1, 1, cp_a.MaxY);
				cp_a.FillColWW(cp_a.MinX, cp_a.MinY, 0);

				cp_b.FillColWW(cp_b.MaxX - 1, cp_b.MinY, 0);
				cp_b.FillColWW(cp_b.MinX, 1, cp_b.MaxY);


				cp_a.normalizeX();
				cp_b.normalizeX();

				p.AppendBottom(cp_a);
				p.AppendBottom(cp_b);
			}

			p.normalizeX();

			p.forceNonEmpty(BCHelper.PC_Down);

			return p;
		}

		private CodePiece generateCode_ParameterIntialization()
		{
			CodePiece p = new CodePiece();

			List<TwoDirectionCodePiece> paramDecls = new List<TwoDirectionCodePiece>();

			for (int i = Parameter.Count - 1; i >= 0; i--)
			{
				VarDeclaration var = Parameter[i];

				paramDecls.Add(new TwoDirectionCodePiece(var.generateCode_SetToStackVal(false), var.generateCode_SetToStackVal(true)));
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
			CodePiece p = Body.generateStrippedCode();

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
		public readonly BType ResultType;
		public readonly string Identifier;
		public readonly List<VarDeclaration> Parameter;

		public Method_Header(SourceCodePosition pos, BType t, string ident, List<VarDeclaration> p)
			: base(pos)
		{
			this.ResultType = t;
			this.Identifier = ident;
			this.Parameter = p;

			if (ASTObject.isKeyword(ident))
			{
				throw new IllegalIdentifierException(Position, ident);
			}
		}

		public override string getDebugString()
		{
			throw new InvalidASTStateException(Position);
		}
	}

	public class Method_Body : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public readonly List<VarDeclaration> Variables;
		public readonly Statement_StatementList Body;

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