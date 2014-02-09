﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST
{
	public class Program : ASTObject
	{
		public string Identifier;
		public Method MainStatement;
		public List<Method> MethodList;

		public Program(string id, Method m, List<Method> mlst)
		{
			this.Identifier = id;
			this.MainStatement = m;
			this.MethodList = mlst.ToList();
		}

		public override string getDebugString()
		{
			return string.Format("#Program ({0})\n[\n{1}\n{2}\n]", Identifier, indent(MainStatement.getDebugString()), indent(getDebugStringForList(MethodList)));
		}
	}

	public class Program_Footer : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public Program_Footer()
		{
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}

	public class Program_Header : ASTObject // TEMPORARY -- NOT IN RESULTING AST
	{
		public string Identifier;

		public Program_Header(string id)
		{
			this.Identifier = id;
		}

		public override string getDebugString()
		{
			throw new ArgumentException();
		}
	}
}