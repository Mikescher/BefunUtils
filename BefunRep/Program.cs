using BefunRep.FileHandling;
using System;

namespace BefunRep
{
	class Program
	{
		static void Main(string[] args)
		{
			CommandLineArguments cmda = new CommandLineArguments(args);

			int lowerBoundary = cmda.GetIntDefault("lower", -256);
			int upperBoundary = cmda.GetIntDefault("upper", 1024);

			//System.IO.File.WriteAllText("out.csv", string.Empty); // reset;

			RepCalculator r = new RepCalculator(lowerBoundary, upperBoundary, new CSVSafe("out.csv"));

			r.start();

			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine("Finished.");
			Console.ReadLine();
		}
	}
}
