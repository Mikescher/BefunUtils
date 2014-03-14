using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Program : ASTObject
	{
		public string Identifier;
		public Method MainStatement;
		public List<Method> MethodList; // Includes MainStatement (at 0)

		public Program(SourceCodePosition pos, string id, Method m, List<Method> mlst)
			: base(pos)
		{
			this.Identifier = id;
			this.MainStatement = m;
			this.MethodList = mlst.ToList();

			MethodList.Insert(0, MainStatement);
		}

		public override string getDebugString()
		{
			return string.Format("#Program ({0})\n[\n{1}\n]", Identifier, indent(getDebugStringForList(MethodList)));
		}

		public void prepare()
		{
			// Reset ID-Counter
			Method.resetCounter();
			VarDeclaration.resetCounter();
			Statement.resetCounter();

			addressMethodsVariables();	// Methods get their Address
			linkVariables();			// Variable-uses get their ID
			linkMethods();				// Methodcalls get their ID   &&   Labels + MethodCalls get their CodePointAddress
			linkResultTypes();			// Statements get their Result-Type (and implicit casting is added)
		}

		private void addressMethodsVariables()
		{
			foreach (Method m in MethodList)
				m.createCodeAddress();
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

		public Method findMethodByIdentifier(string ident)
		{
			return MethodList.Count(p => p.Identifier.ToLower() == ident.ToLower()) == 1 ? MethodList.Single(p => p.Identifier.ToLower() == ident.ToLower()) : null;
		}

		public CodePiece generateCode()
		{
			List<Tuple<int, int, CodePiece>> meth_pieces = new List<Tuple<int, int, CodePiece>>();

			CodePiece p = new CodePiece();

			int meth_offset_x = 1;
			int meth_offset_y = 1;

			p[0, 0] = BCHelper.PC_Down;

			for (int i = 0; i < MethodList.Count; i++)
			{
				Method m = MethodList[i];

				CodePiece p_method = m.generateCode(meth_offset_x, meth_offset_y);

				int mx = meth_offset_x - p_method.MinX;
				int my = meth_offset_y - p_method.MinY;

				meth_pieces.Add(Tuple.Create(mx, my, p_method));

				p.SetAt(mx, my, p_method);

				meth_offset_y += p_method.Height + CodeGenConstants.VERTICAL_METHOD_DISTANCE;
			}

			// Path to main --TEMP--
			int entry_y = meth_pieces[0].Item2 + (meth_pieces[0].Item3.findTagSingle(typeof(MethodEntry_FullInitialization_Tag)).Y - meth_pieces[0].Item3.MinX);
			p[0, entry_y] = BCHelper.PC_Right;
			p.FillColWW(0, 1, entry_y);
			p.FillRowWW(entry_y, 1, meth_pieces[0].Item1);

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

		public Program_Header(SourceCodePosition pos, string id)
			: base(pos)
		{
			this.Identifier = id;
		}

		public override string getDebugString()
		{
			throw new AccessTemporaryASTObjectException(Position);
		}
	}
}