using BefunRep.FileHandling;
using BefunRep.OutputHandling;
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

			int lowerBoundary = cmda.GetIntDefault("lower", 0);
			int upperBoundary = cmda.GetIntDefault("upper", 0);
			bool testResults = !cmda.IsSet("notest");
			bool doReset = cmda.IsSet("reset");
			int algorithm = cmda.GetIntDefaultRange("algorithm", -1, -1, RepCalculator.algorithms.Length);
			string safepath = cmda.GetStringDefault("safe", "out.csv");
			string outpath = cmda.GetStringDefault("out", null);

			//#################################################################

			if (doReset)
				File.Delete(safepath); // reset;

			RepresentationSafe safe;
			if (safepath.ToLower().EndsWith(".csv"))
				safe = new CSVSafe(safepath);
			else if (safepath.ToLower().EndsWith(".json"))
				safe = new JSONSafe(safepath);
			else if (safepath.ToLower().EndsWith(".bin") || safepath.ToLower().EndsWith(".dat"))
				safe = new BinarySafe(safepath, lowerBoundary, upperBoundary);
			else
				safe = new CSVSafe(safepath);

			OutputFormatter formatter;
			if (outpath != null)
			{
				if (outpath.ToLower().EndsWith(".csv"))
					formatter = new CSVOutputFormatter(outpath);
				else if (outpath.ToLower().EndsWith(".json"))
					formatter = new JSONOutputFormatter(outpath);
				else if (outpath.ToLower().EndsWith(".xml"))
					formatter = new XMLOutputFormatter(outpath);
				else
					formatter = new CSVOutputFormatter(outpath);
			}
			else
			{
				formatter = new DummyOutputFormatter();
			}

			//#################################################################

			RepCalculator r = new RepCalculator(lowerBoundary, upperBoundary, testResults, safe);

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Limits     := [{1}, {2}]", DateTime.Now, lowerBoundary, upperBoundary));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Testing    := {1}", DateTime.Now, testResults.ToString().ToLower()));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Reset      := {1}", DateTime.Now, doReset.ToString().ToLower()));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Algorithm  := {1}", DateTime.Now, algorithm == -1 ? "all" : RepCalculator.algorithmNames[algorithm]));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Safetype   := {1}", DateTime.Now, safe.GetType().Name));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Safepath   := {1}", DateTime.Now, safepath));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputtype := {1}", DateTime.Now, formatter.GetType().Name));
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputpath := {1}", DateTime.Now, outpath));
			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Starting ...", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine();

			//##############

			r.calculate(algorithm);

			safe.start();
			formatter.Output(safe);
			safe.stop();

			//##############

			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Finished.", DateTime.Now));
			Console.ReadLine();
		}
	}
}
