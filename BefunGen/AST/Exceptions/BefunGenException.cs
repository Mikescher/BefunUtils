﻿using BefunGen.AST.CodeGen;
using System;
using System.Text.RegularExpressions;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenException : Exception
	{
		public BefunGenException(string eid, SourceCodePosition pos)
			: base(String.Format("[{0}] Exception ({1})", eid, pos))
		{

		}

		public BefunGenException(string eid, string msg, SourceCodePosition pos)
			: base(String.Format("[{0}] Exception ({1}): \r\n   {2}", pos, eid, msg))
		{

		}

		public BefunGenException(string eid, string msg)
			: base(String.Format("Exception ({0}): \r\n   {1}", eid, msg))
		{

		}

		public override string ToString()
		{
			return Regex.Replace(base.ToString().Replace(" in ", Environment.NewLine + "      in "), @"in.*BefunGen\\", "in ");
		}
	}
}
