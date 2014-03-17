using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public class ArrayLiteralTooBigException : BefunGenUserException
	{
		public ArrayLiteralTooBigException(SourceCodePosition pos) 
			: base("ArrayLiteral is too big for Variable", pos) { }
	}

	public class GroupErrorException : BefunGenUserException
	{
		public GroupErrorException(SourceCodePosition pos) 
			: base("Unexpected end of file", pos) { }
	}

	public class ImplicitCastException : BefunGenUserException
	{
		public ImplicitCastException(SourceCodePosition pos, BType from, BType to) 
			: base(String.Format("Cannot implicitly cast from {0} to {1}", from, to), pos) { }
		
		public ImplicitCastException(SourceCodePosition pos, BType from, params BType[] to) 
			: base(String.Format("Cannot implicitly cast from {0} to ({1})", from, string.Join(" or ", ((BType[])to).ToList())), pos) { }
	}

	public class IndexOperatorNotDefiniedException : BefunGenUserException
	{
		public IndexOperatorNotDefiniedException(SourceCodePosition pos) 
			: base("Cant perform index operation here", pos) { }
	}

	public class InvalidCompareException : BefunGenUserException
	{
		public InvalidCompareException(BType a, BType b, SourceCodePosition pos) 
			: base(String.Format("Cannot compare Types of {0} and {1}", a, b), pos) { }
	}

	public class InvalidFormatSpecifierException : BefunGenUserException
	{
		public InvalidFormatSpecifierException(string fs, SourceCodePosition pos) 
			: base(String.Format("Formatspecifier '{0}' is unknown", fs), pos) { }
	}

	public class NoConditionException : BefunGenUserException
	{
		public NoConditionException(SourceCodePosition pos) 
			: base("Expression is not a valid Condition", pos) { }
	}

	public class NotAllPathsReturnException : BefunGenUserException
	{
		public NotAllPathsReturnException(Method m, SourceCodePosition pos) 
			: base("Not all Paths of Method " + m.Identifier + "() return a value.", pos) { }
	}

	public class SyntaxErrorException : BefunGenUserException
	{
		public SyntaxErrorException(object data, string expect, SourceCodePosition pos) 
			: base("Lexical Error on data='" + data + "', expected='" + expect + "'", pos) { }
	}

	public class UnresolvableReferenceException : BefunGenUserException
	{
		public UnresolvableReferenceException(string s, SourceCodePosition pos) 
			: base("Could not resolve reference: \"" + s + "\"", pos) { }
	}

	public class VoidObjectCallException : BefunGenUserException
	{
		public VoidObjectCallException(SourceCodePosition pos) 
			: base("Operation not possible on <void>", pos) { }
	}

	public class WrongParameterCountException : BefunGenUserException
	{
		public WrongParameterCountException(int actual, int expected, SourceCodePosition pos) 
			: base(String.Format("Parametercount ({0}) differs from expected ({1})", actual, expected), pos) { }
	}

	public class WrongTypeException : BefunGenUserException
	{
		public WrongTypeException(SourceCodePosition pos, BType found, BType expec) 
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, expec), pos) { }
		
		public WrongTypeException(SourceCodePosition pos, BType found, params BType[] expec) 
			: base(String.Format("Wrong Type found: Found = {0}. Expected = {1}", found, string.Join(" or ", expec.ToList())), pos) { }
	}
}
