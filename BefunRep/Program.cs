using BefunRep.FileHandling;
using BefunRep.OutputHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BefunRep
{
	class Program
	{
		public const string VERSION = "1.0";
		public const string TITLE = "BefunRep";

		private readonly DateTime startTime = DateTime.Now;
		private List<int> founds = new List<int>();

		private long lowerBoundary;
		private long upperBoundary;
		private bool testResults;
		private bool doReset;
		private int statsLevel;
		private int algorithm;
		private string safepath;
		private string outpath;
		private bool quiet;
		private int iterations;

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
				Console.Error.WriteLine("Can't configure Console:");
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

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Calculations Started.", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine();

			for (int i = 0; i < iterations || iterations < 0; i++) // iterations neg => run until no changes
			{
				int foundcount = r.calculate(algorithm);
				founds.Add(foundcount);

				Console.Out.WriteLine();
				Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Iteration {1} Finished (+{2})", DateTime.Now, i, foundcount));
				Console.Out.WriteLine();

				if (foundcount == 0)
					break;
			}

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Caclulations Finished.", DateTime.Now));
			Console.Out.WriteLine();
			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputting Started.", DateTime.Now));

			safe.start();
			formatter.Output(safe);
			safe.stop();

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Outputting Finished.", DateTime.Now));

			//##############

			Console.Out.WriteLine();

			printStats(safe);

			Console.Out.WriteLine();
			Console.Out.WriteLine();

			printAnyKeyMessage();
		}

		[ConditionalAttribute("DEBUG")]
		private static void printAnyKeyMessage()
		{
			Console.Out.WriteLine("Press any Key to exit.");

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
			Console.Out.WriteLine("-iterations=[-1 | 0-n ]");
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

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Iterations := {1}", DateTime.Now,
				iterations < 0 ? "INF" : (iterations == 0 ? "NONE" : (iterations.ToString()))));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Testing    := {1}", DateTime.Now,
				testResults.ToString().ToLower()));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Reset      := {1}", DateTime.Now,
				doReset.ToString().ToLower()));

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Quiet      := {1}", DateTime.Now,
				quiet.ToString().ToLower()));

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
			iterations = cmda.GetIntDefault("iterations", 1);
			return cmda;
		}

		private void printStats(RepresentationSafe safe)
		{
			safe.start();

			SafeInfo info = safe.getInformations();

			if (statsLevel >= 1) //############################################
			{

				Console.Out.WriteLine("  Statistics  ");
				Console.Out.WriteLine("##############");
				Console.Out.WriteLine();

				for (int i = 0; i < founds.Count; i++)
				{
					Console.Out.WriteLine(String.Format("{0,-8} Updates found in iteration {1}", founds[i], i));
				}

				Console.Out.WriteLine();

				Console.Out.WriteLine(String.Format("{0}/{1} Representations found", info.nonNullCount, info.count));
				Console.Out.WriteLine(String.Format("{0} Algorithms registered", RepCalculator.algorithms.Length));
				Console.Out.WriteLine(String.Format("Run Duration = {0:hh} hours {0:mm} minutes {0:ss} seconds {0:ff} milliseconds", startTime - DateTime.Now));

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
					for (int i = 0; i < RepCalculator.algorithms.Length; i++)
					{
						Console.Out.WriteLine(String.Format("{0,6} Representations with algorithm {1,16} ({2:0.##}%)",
							info.nonNullPerAlgorithm[i],
							RepCalculator.algorithmNames[i],
							info.nonNullPerAlgorithm[i] * 100d / info.count));
					}

					Console.Out.WriteLine();

					if (statsLevel >= 3) //####################################
					{
						Console.Out.WriteLine(String.Format("Average representation width = {0:0.###}", info.avgLen));

						for (int i = 0; i < RepCalculator.algorithms.Length; i++)
						{
							Console.Out.WriteLine(String.Format("Average representation width with algorithm {0,16}  = {1:0.###}",
								RepCalculator.algorithmNames[i],
								info.avgLenPerAlgorithm[i]));
						}

						Console.Out.WriteLine();

						Console.Out.WriteLine(String.Format("Representation length (min|max) = [{0,3},{1,3}]", info.minLen, info.maxLen));

						for (int i = 0; i < RepCalculator.algorithms.Length; i++)
						{
							Console.Out.WriteLine(String.Format("Representation length (min|max) for {0,16} = [{1,3},{2,3}]",
								RepCalculator.algorithmNames[i],
								info.minLenPerAlgorithm[i],
								info.maxLenPerAlgorithm[i]));
						}

						Console.Out.WriteLine();
					}
				}
			}

			safe.stop();
		}
	}
}
