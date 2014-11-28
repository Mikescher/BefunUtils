using BefunRep.FileHandling;
using System;
using System.IO;

namespace BefunRep
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetBufferSize(256, 8192);

			//#################################################################

			CommandLineArguments cmda = new CommandLineArguments(args);

			int lowerBoundary = cmda.GetIntDefault("lower", -256);
			int upperBoundary = cmda.GetIntDefault("upper", 1024);
			bool testResults = !cmda.IsSet("notest");
			bool doReset = cmda.IsSet("reset");
			int algorithm = cmda.GetIntDefaultRange("algorithm", -1, -1, RepCalculator.algorithms.Length);

			//#################################################################

			if (doReset)
				File.WriteAllText("out.csv", string.Empty); // reset;

			RepCalculator r = new RepCalculator(lowerBoundary, upperBoundary, testResults, new CSVSafe("out.csv"));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Limits    := [{1}, {2}]", DateTime.Now, lowerBoundary, upperBoundary));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Testing   := {1}", DateTime.Now, testResults.ToString().ToLower()));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Reset     := {1}", DateTime.Now, doReset.ToString().ToLower()));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Algorithm := {1}", DateTime.Now, algorithm == -1 ? "all" : RepCalculator.algorithmNames[algorithm]));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Starting ...", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine();

			//##############
			r.start();
			//##############

			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Finished.", DateTime.Now));
			Console.ReadLine();
		}
	}
}
