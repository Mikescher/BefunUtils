using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public static class NumberCodeHelper
	{
		public static NumberRep lastRep;

		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p = generateCode(Value);
			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(int Value)
		{
			CodePiece p;

			if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Best)
			{
				List<Tuple<NumberRep, CodePiece>> representations = generateAllCode(Value, true);

				int min = representations.Min(lp => lp.Item2.Width);

				foreach (var rep in representations)
				{
					if (rep.Item2.Width == min)
					{
						lastRep = rep.Item1;
						p = rep.Item2;

						return p;
					}
				}
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.StringmodeChar)
			{
				p = NumberCodeFactory_StringmodeChar.generateCode(Value);
				lastRep = NumberRep.StringmodeChar;

				return p;
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Base9)
			{
				p = NumberCodeFactory_Base9.generateCode(Value);
				lastRep = NumberRep.Base9;

				return p;
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Factorization)
			{
				p = NumberCodeFactory_Factorization.generateCode(Value);
				lastRep = NumberRep.Factorization;

				return p;
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Stringify)
			{
				p = NumberCodeFactory_Stringify.generateCode(Value);
				lastRep = NumberRep.Stringify;

				return p;
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Digit)
			{
				p = NumberCodeFactory_Digit.generateCode(Value);
				lastRep = NumberRep.Digit;

				return p;
			}
			else if (ASTObject.CGO.NumberLiteralRepresentation == NumberRep.Boolean)
			{
				p = NumberCodeFactory_Boolean.generateCode(Value);
				lastRep = NumberRep.Boolean;

				return p;
			}

			throw new WTFException();
		}

		public static List<Tuple<NumberRep, CodePiece>> generateAllCode(int Value, bool filter, bool reversed = false)
		{
			List<Tuple<NumberRep, CodePiece>> result = new List<Tuple<NumberRep, CodePiece>>();

			// Order is Priority !!!

			result.Add(Tuple.Create(NumberRep.Boolean, NumberCodeFactory_Boolean.generateCode(Value, reversed)));
			result.Add(Tuple.Create(NumberRep.Digit, NumberCodeFactory_Digit.generateCode(Value, reversed)));
			result.Add(Tuple.Create(NumberRep.Base9, NumberCodeFactory_Base9.generateCode(Value, reversed)));
			result.Add(Tuple.Create(NumberRep.Factorization, NumberCodeFactory_Factorization.generateCode(Value, reversed)));
			result.Add(Tuple.Create(NumberRep.StringmodeChar, NumberCodeFactory_StringmodeChar.generateCode(Value, reversed)));
			result.Add(Tuple.Create(NumberRep.Stringify, NumberCodeFactory_Stringify.generateCode(Value, reversed)));

			if (filter)
				return result.Where(p => p.Item2 != null).ToList();
			else
				return result;

		}

		public static string generateBenchmark(int cnt, bool doNeg)
		{
			ASTObject.CGO.NumberLiteralRepresentation = NumberRep.Best;

			int MIN = (doNeg) ? -(cnt / 2) : (0);
			int MAX = (doNeg) ? +(cnt / 2) : (cnt);

			List<NumberRep> reps = Enum.GetValues(typeof(NumberRep)).Cast<NumberRep>().Where(p => p != NumberRep.Best).ToList();

			int[] count = new int[reps.Count];
			Array.Clear(count, 0, reps.Count);

			//int mxw = Enumerable.Range(MIN, MAX + 1).Max(p1 => generateAllCode(p1, true).Max(p2 => p2.Item2.Width)) + 3;
			int mxw = 24;

			StringBuilder txt = new StringBuilder();

			txt.AppendFormat("{0, -7} ", "Number");
			txt.AppendFormat("{0, -16} {1, -" + mxw + "}", "Best", "Best");
			reps.ForEach(p => txt.AppendFormat("{0, -" + mxw + "} ", p.ToString()));
			txt.AppendLine();
			txt.AppendLine();

			long ticks = Environment.TickCount;
			for (int i = MIN; i <= MAX; i++)
			{
				List<Tuple<NumberRep, CodePiece>> all = generateAllCode(i, false);
				CodePiece best = generateCode(i);
				NumberRep rbest = lastRep;

				count[reps.IndexOf(rbest)]++;

				txt.AppendFormat("{0, -7} ", i.ToString());
				txt.AppendFormat("{0, -16} ", rbest.ToString());
				txt.AppendFormat("{0, -" + mxw + "} ", best.ToSimpleString());
				reps.ForEach(p => txt.AppendFormat("{0, -" + mxw + "} ", (all.Single(p2 => p2.Item1 == p).Item2 != null) ? (all.Single(p2 => p2.Item1 == p).Item2.ToSimpleString()) : ("")));
				txt.AppendLine();
			}

			txt.AppendLine();
			txt.AppendLine(new String('#', 32));
			txt.AppendLine();

			ticks = Environment.TickCount - ticks;

			reps.ForEach(p => txt.AppendLine(String.Format("{0,-16}: {1}", p.ToString(), count[reps.IndexOf(p)])));

			txt.AppendLine();

			txt.AppendLine(String.Format("Time taken for {0} Elements: {1}ms", cnt, ticks));
			txt.AppendLine(String.Format("Time/Number: {0:0.000}ms", (ticks * 1.0) / cnt));

			return txt.ToString();
		}
	}
}
