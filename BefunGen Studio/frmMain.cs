using BefunGen.AST;
using BefunGen.AST.CodeGen;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BefunGen
{
	public partial class frmMain : Form
	{
		private DemoParser MyParser = new DemoParser();
		private TextFungeParser GParser = new TextFungeParser();

		private bool loaded = false;

		private string currentSC = "";
		private Thread parseThread;
		bool threadRunning = true;

		public frmMain()
		{
			InitializeComponent();
			tabControl1.SelectedIndex = 6;
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			doLoad();
		}

		private void btnLoadSYN_Click(object sender, EventArgs e)
		{
			doLoadSYN();
		}

		private void doLoadSYN()
		{
			try
			{
				txtSource.Document.SyntaxFile = txtSynFile.Text;
				txtSource.Document.ParseAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void doLoad()
		{
			try
			{
				if (MyParser.Setup(new BinaryReader(new MemoryStream(GParser.getGrammar()))))
				{
					loaded = true;
				}
				else
				{
					txtParseTree.Text = MyParser.FailMessage + Environment.NewLine + "CGT failed to load";
					txtAST.Text = GParser.FailMessage + Environment.NewLine + "CGT failed to load";
				}
			}
			catch (GOLD.ParserException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			txtTableFile.Text = Path.Combine(Application.StartupPath, "TextFunge.egt");

			if (File.Exists(txtTableFile.Text))
				doLoad();
			else
				txtLog.AppendText(string.Format("EGT not found: {0} \r\n", txtTableFile.Text));

			//#########

			txtSynFile.Text = Path.Combine(Application.StartupPath, "TextFunge.syn");
			if (File.Exists(txtSynFile.Text))
				doLoadSYN();
			else
				txtLog.AppendText(string.Format("Syntaxfile not found: {0} \r\n", txtSynFile.Text));

			string path_tf = Path.Combine(Application.StartupPath, "example_00.tf");
			if (File.Exists(path_tf))
			{
				txtSource.Document.Text = File.ReadAllText(path_tf);
				currentSC = txtSource.Document.Text;
			}
			else
				txtLog.AppendText(string.Format("Example not found: {0} \r\n", path_tf));

			//##########

			doLoad();

			//##########

			parseThread = new Thread(work);
			parseThread.Start();
		}

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			currentSC = txtSource.Document.Text;
		}

		private Tuple<string, string, string, string, string> doParse(string txt)
		{
			long time_full = Environment.TickCount;

			string redtree = "";
			string trimtree = "";
			string asttree = "";
			string log = "";
			string grammar = "";

			if (loaded)
			{
				string refout1 = "";
				MyParser.Parse(new StringReader(txt), ref refout1, false);
				redtree = refout1;
				log += (string.Format("Reduction Tree: {0}ms\r\n", MyParser.Time));

				string refout2 = "";
				MyParser.Parse(new StringReader(txt), ref refout2, true);
				trimtree = refout2;
				log += (string.Format("Trimmed Reduction Tree: {0}ms\r\n", MyParser.Time));

				BefunGen.AST.Program p = GParser.generateAST(txt);
				if (p == null)
					asttree = GParser.FailMessage;
				else
				{
					log += (string.Format("AST-Gen: {0}ms\r\n", MyParser.Time));

					long gdst = Environment.TickCount;
					string debug = p.getDebugString().Replace("\n", Environment.NewLine);
					gdst = Environment.TickCount - gdst;

					asttree = debug;

					log += (string.Format("AST-DebugOut: {0}ms\r\n", gdst));
				}

				grammar = GParser.getGrammarDefinition();
			}
			else
			{
				redtree = "Grammar not loaded";
				trimtree = "Grammar not loaded";
				asttree = "Grammar not loaded";
			}

			time_full = Environment.TickCount - time_full;

			log += "\r\n----------\r\n";
			log += (string.Format("Parse: {0}ms\r\n", time_full));

			return Tuple.Create(redtree, trimtree, asttree, log, grammar);
		}

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			currentSC = txtSource.Document.Text;
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			threadRunning = false;
		}

		private void work()
		{
			string currentTxt = null;

			while (threadRunning)
			{
				string newtxt = currentSC;

				if (newtxt != currentTxt)
				{
					bool hasChangedAgain = true;
					while (hasChangedAgain)
					{
						Thread.Sleep(500);
						hasChangedAgain = (currentSC != newtxt);
						newtxt = currentSC;
					}

					var result = doParse(newtxt);

					txtParseTree.SetPropertyThreadSafe(() => txtParseTree.Text, result.Item1);
					txtParseTrimTree.SetPropertyThreadSafe(() => txtParseTrimTree.Text, result.Item2);
					txtAST.SetPropertyThreadSafe(() => txtAST.Text, result.Item3);
					txtLog.SetPropertyThreadSafe(() => txtLog.Text, result.Item4);
					txtGrammar.SetPropertyThreadSafe(() => txtGrammar.Text, result.Item5);

					currentTxt = newtxt;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}

		private Expression parseExpression(string expr)
		{
			string txt = String.Format("program b var  bool a; begin a = (bool)({0}); end end", expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return ((Expression_Cast)((Statement_Assignment)((Statement_StatementList)p.MainStatement.Body).List[0]).Expr).Expr;
		}

		private Statement parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return ((Statement_StatementList)p.MainStatement.Body).List[0];
		}

		private Method parseMethod(string meth)
		{
			string txt = String.Format("program b begin end {0} end", meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p.MethodList[1];
		}

		private void debugExpression(string expr)
		{
			expr = expr.Replace(@"''", "\"");

			txtDebug.Text += expr + Environment.NewLine;

			Expression e = parseExpression(expr);
			CodePiece pc = e.generateCode(false);
			txtDebug.Text += pc.ToString() + Environment.NewLine;
		}

		private void debugStatement(string stmt)
		{
			stmt = stmt.Replace(@"''", "\"");

			txtDebug.Text += stmt + Environment.NewLine;

			Statement e = parseStatement(stmt);
			CodePiece pc = e.generateCode(false);
			txtDebug.Text += pc.ToString() + Environment.NewLine;
		}

		private void debugMethod(string meth)
		{
			meth = meth.Replace(@"''", "\"");

			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
			meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");

			Method e = parseMethod(meth);
			txtDebug.Text += "[METHOD] " + e.Identifier + ":" + e.ID + Environment.NewLine;
			CodePiece pc = e.generateCode(0, 0);
			txtDebug.Text += pc.ToString() + Environment.NewLine;
		}

		private void btnExecuteDebug_Click(object sender, EventArgs earg)
		{
			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;

				while (i <= 128) do
					out (int)(bool)(i % 3);
					out lb[0];
					out lb[1];
					i++;
				end
				
				QUIT;
			END
			");
		}

		private void btnDebugNumberRep_Click(object sender, EventArgs e)
		{
			int count_CharConstant = 0;
			int count_Base9 = 0;
			int count_Factorization = 0;
			int count_Digit = 0;

			StringBuilder txt = new StringBuilder();

			for (int i = -1024; i < 1024; i++)
			{
				CodePiece p = NumberCodeHelper.generateCode(i, false);
				CodePiece b9 = Base9Converter.generateCodeForLiteral(i);
				CodePiece nf = NumberFactorization.generateCodeForLiteral(i);

				txt.AppendLine( String.Format(@"{0}{1:0000}: {2, -24} {3, -24} FAC: {4, -24} B9: {5, -24}",(i < 0) ? "" : " ", i, NumberCodeHelper.lastRep, p.ToSimpleString(), nf.ToSimpleString(), b9.ToSimpleString()) );
				switch (NumberCodeHelper.lastRep)
				{
					case NumberRep.CharConstant:
						count_CharConstant++;
						break;
					case NumberRep.Base9:
						count_Base9++;
						break;
					case NumberRep.Factorization:
						count_Factorization++;
						break;
					case NumberRep.Digit:
						count_Digit++;
						break;
				}
			}

			txt.AppendLine( );
			txt.AppendLine( new String('#', 32) );
			txt.AppendLine( );
			
			txt.AppendLine( String.Format("{0,-16}", "CharConstant:") + count_CharConstant );
			txt.AppendLine( String.Format("{0,-16}", "Base9:") + count_Base9 );
			txt.AppendLine( String.Format("{0,-16}", "Factorization:") + count_Factorization );
			txt.AppendLine( String.Format("{0,-16}", "Digit:") + count_Digit );

			txtDebug.Text += txt.ToString();
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			try
			{
				string code = txtCode.Text;

				string path = Path.Combine(Application.StartupPath, "code_tmp.b93");

				File.WriteAllText(path, code);

				//-----

				ProcessStartInfo start = new ProcessStartInfo();

				start.Arguments = String.Format("-file=\"{0}\"", path);
				start.FileName = Path.Combine(Application.StartupPath, "BefungExec.exe");

				Process.Start(start);
			}
			catch (Exception ex)
			{
				txtCode.Text = ex.ToString();
			}
		}

		private void btnGen_Click(object sender, EventArgs e)
		{
			BefunGen.AST.Program p = GParser.generateAST(txtSource.Text);
			if (p == null)
			{
				txtCode.Text = GParser.FailMessage;
			}
			else
			{
				try
				{
					txtCode.Text = p.generateCode().ToString();
				}
				catch (Exception ex)
				{
					txtCode.Text = ex.ToString();
				}
			}
		}
	}
} //Form