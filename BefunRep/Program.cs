using BefunRep.FileHandling;
using BefunRep.OutputHandling;
using System;
using System.IO;
using System.Linq;

namespace BefunRep
{

	//TODO Add info about algorithm into safes (all 3)

	class Program
	{
		public const string VERSION = "0.1";
		public const string TITLE = "BefunRep";

		private readonly DateTime startTime = DateTime.Now;

		private long lowerBoundary;
		private long upperBoundary;
		private bool testResults;
		private bool doReset;
		private int statsLevel;
		private int algorithm;
		private string safepath;
		private string outpath;
		private bool quiet;

		private bool boundaryDiscovery = false;

		private RepresentationSafe safe;
		private OutputFormatter formatter;

		static void Main(string[] args)
		{
			new Program(args);
		}

		public Program(string[] args)
		{
			try
			{
				Console.SetBufferSize(256, 8192);
				Console.WindowHeight = Math.Max(Console.WindowHeight, 40);
				Console.WindowWidth = Math.Max(Console.WindowWidth, 140);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine("Can't configure Conole:");
				Console.Error.WriteLine(e.ToString());
			}

			printHeader();

			//##############

			CommandLineArguments cmda = loadCMDA(args);

			if (cmda.isEmpty() || cmda.IsSet("help"))
			{
				printHelp();
				return;
			}


			interpreteCMDA(cmda);

			RepCalculator r = new RepCalculator(lowerBoundary, upperBoundary, testResults, safe, quiet);
			outputCMDA();

			//##############

			r.calculate(algorithm);

			safe.start();
			formatter.Output(safe);
			safe.stop();

			//##############

			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Finished.", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine();

			printStats(safe);

			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.ReadLine();
		}

		private static void printHeader()
		{
			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine(@"                      ,...                                  // mikescher.de    ".Replace("'", "\""));
			Console.Out.WriteLine(@"7MM'''Yp,           .d' ''                      `7MM'''Mq.                     ".Replace("'", "\""));
			Console.Out.WriteLine(@" MM    Yb           dM`                           MM   `MM.                    ".Replace("'", "\""));
			Console.Out.WriteLine(@" MM    dP  .gP'Ya  mMMmm`7MM  `7MM  `7MMpMMMb.    MM   ,M9  .gP'Ya `7MMpdMAo.  ".Replace("'", "\""));
			Console.Out.WriteLine(@" MM'''bg. ,M'   Yb  MM    MM    MM    MM    MM    MMmmdM9  ,M'   Yb  MM   `Wb  ".Replace("'", "\""));
			Console.Out.WriteLine(@" MM    `Y 8M''''''  MM    MM    MM    MM    MM    MM  YM.  8M''''''  MM    M8  ".Replace("'", "\""));
			Console.Out.WriteLine(@" MM    ,9 YM.    ,  MM    MM    MM    MM    MM    MM   `Mb.YM.    ,  MM   ,AP  ".Replace("'", "\""));
			Console.Out.WriteLine(@"JMMmmmd9   `Mbmmd'.JMML.  `Mbod'YML..JMML  JMML..JMML. .JMM.`Mbmmd'  MMbmmd'   ".Replace("'", "\""));
			Console.Out.WriteLine(@"                                                                     MM        ".Replace("'", "\""));
			Console.Out.WriteLine(@"                                                                   .JMML.      ".Replace("'", "\""));
			Console.Out.WriteLine(@"VERSION: " + VERSION);
			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine("###############################################################################");
			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine();
		}

		private void printHelp()
		{
			Console.Out.WriteLine("Possible Commandline Arguments:");
			Console.Out.WriteLine();
			Console.Out.WriteLine("################################");
			Console.Out.WriteLine();
			Console.Out.WriteLine("-lower=[int]");
			Console.Out.WriteLine("-upper=[int]");
			Console.Out.WriteLine("-notest");
			Console.Out.WriteLine("-quiet");
			Console.Out.WriteLine("-reset");
			Console.Out.WriteLine("-algorithm=[0 - " + (RepCalculator.algorithms.Length - 1) + "]");
			Console.Out.WriteLine("-safe=[filename].[csv|json|bin|dat]");
			Console.Out.WriteLine("-out=[filename].[csv|json|xml]");
			Console.Out.WriteLine("-stats=[0-3]");
			Console.Out.WriteLine("-help");
			Console.Out.WriteLine();
			Console.Out.WriteLine("################################");
			Console.Out.WriteLine();
			Console.Out.WriteLine();
			Console.Out.WriteLine();
		}

		private void outputCMDA()
		{
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Limits     := [{1}, {2}]{3}", DateTime.Now,
				lowerBoundary,
				upperBoundary,
				boundaryDiscovery ? "      (via auto discovery)" : ""));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Testing    := {1}", DateTime.Now,
				testResults.ToString().ToLower()));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Reset      := {1}", DateTime.Now,
				doReset.ToString().ToLower()));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Statistics := {1}", DateTime.Now,
				new string[] { "none", "simple", "verbose", "all" }[statsLevel]));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Algorithm  := {1}", DateTime.Now,
				algorithm == -1 ? "all" : RepCalculator.algorithmNames[algorithm]));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Safetype   := {1}", DateTime.Now,
				safe.GetType().Name));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Safepath   := {1}", DateTime.Now,
				safepath));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputtype := {1}", DateTime.Now,
				formatter.GetType().Name));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputpath := {1}", DateTime.Now,
				outpath));

			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Starting ...", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine();
		}

		private void interpreteCMDA(CommandLineArguments cmda)
		{
			if (doReset)
				File.Delete(safepath); // reset;

			if (safepath.ToLower().EndsWith(".csv"))
				safe = new CSVSafe(safepath);
			else if (safepath.ToLower().EndsWith(".json"))
				safe = new JSONSafe(safepath);
			else if (safepath.ToLower().EndsWith(".bin") || safepath.ToLower().EndsWith(".dat"))
				safe = new BinarySafe(safepath, lowerBoundary, upperBoundary);
			else
				safe = new CSVSafe(safepath);

			// Init values
			safe.start();
			safe.stop();

			if (!(cmda.IsSet("lower") || cmda.IsSet("upper")))
			{
				lowerBoundary = safe.getLowestValue();
				upperBoundary = safe.getHighestValue();

				boundaryDiscovery = true;
			}

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
		}

		private CommandLineArguments loadCMDA(string[] args)
		{
			CommandLineArguments cmda = new CommandLineArguments(args);

			lowerBoundary = cmda.GetLongDefault("lower", 0);
			upperBoundary = cmda.GetLongDefault("upper", 0);
			testResults = !cmda.IsSet("notest");
			doReset = cmda.IsSet("reset");
			quiet = cmda.IsSet("q") || cmda.IsSet("quiet");
			statsLevel = cmda.GetIntDefaultRange("stats", 1, 0, 4);
			algorithm = cmda.GetIntDefaultRange("algorithm", -1, -1, RepCalculator.algorithms.Length);
			safepath = cmda.GetStringDefault("safe", "out.csv");
			outpath = cmda.GetStringDefault("out", null);
			return cmda;
		}

		private void printStats(RepresentationSafe safe)
		{
			safe.start();
			if (statsLevel >= 1) //############################################
			{

				Console.Out.WriteLine("  Statistics  ");
				Console.Out.WriteLine("##############");
				Console.Out.WriteLine();

				long valuecount = safe.getHighestValue() - safe.getLowestValue();

				long repcount = safe.getNonNullRepresentations();

				Console.Out.WriteLine(String.Format("{0}/{1} Representations found", valuecount, repcount));
				Console.Out.WriteLine(String.Format("{0} Algorithms registered", RepCalculator.algorithms.Length));
				Console.Out.WriteLine(String.Format("Run Duration = {0:mm} minutes {0:ss} seconds {0:ff} milliseconds", startTime - DateTime.Now));

				Console.Out.WriteLine();

				for (int i = 0; i < RepCalculator.algorithmTime.Length; i++)
				{
					Console.Out.WriteLine(String.Format("Time per algorithm {0, 24}: {1,6} ms",
						RepCalculator.algorithmNames[i],
						RepCalculator.algorithmTime[i]));
				}

				Console.Out.WriteLine();

				if (statsLevel >= 2) //########################################
				{
					long[] repPerAlgo = Enumerable.Range(0, RepCalculator.algorithms.Length)
						.Select(p => safe.countRepresentationsPerAlgorithm(p))
						.ToArray();

					for (int i = 0; i < repPerAlgo.Length; i++)
					{
						Console.Out.WriteLine(String.Format("{0} Representations with algorithm {1} ({2:0.##}%)",
							repPerAlgo[i],
							RepCalculator.algorithmNames[i],
							repPerAlgo[i] * 100d / repcount));
					}

					Console.Out.WriteLine();

					if (statsLevel >= 3) //####################################
					{
						double avgWidth = safe.getAverageRepresentationWidth();

						double[] avgWidthPerAlgo = Enumerable.Range(0, RepCalculator.algorithms.Length)
							.Select(p => safe.getAverageRepresentationWidthPerAlgorithm(p))
							.ToArray();

						Console.Out.WriteLine(String.Format("Average representation width = {0}", avgWidth));

						for (int i = 0; i < repPerAlgo.Length; i++)
						{
							Console.Out.WriteLine(String.Format("Average representation width with algorithm {0}  = {1:0.##}",
								RepCalculator.algorithmNames[i],
								avgWidthPerAlgo[i]));
						}

						Console.Out.WriteLine();
					}
				}
			}

			safe.stop();
		}
	}
}
