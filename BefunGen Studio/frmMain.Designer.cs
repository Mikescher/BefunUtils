﻿namespace BefunGen
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnLoad = new System.Windows.Forms.Button();
			this.txtTableFile = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtSource = new Alsing.Windows.Forms.SyntaxBoxControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtParseTree = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtParseTrimTree = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtAST = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnLoadSYN = new System.Windows.Forms.Button();
			this.txtSynFile = new System.Windows.Forms.TextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.txtGrammar = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Enabled = false;
			this.btnLoad.Location = new System.Drawing.Point(589, 3);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(94, 26);
			this.btnLoad.TabIndex = 9;
			this.btnLoad.Text = "Load EGT";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtTableFile
			// 
			this.txtTableFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTableFile.Location = new System.Drawing.Point(3, 3);
			this.txtTableFile.Name = "txtTableFile";
			this.txtTableFile.Size = new System.Drawing.Size(580, 20);
			this.txtTableFile.TabIndex = 8;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 73);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtSource);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(686, 466);
			this.splitContainer1.SplitterDistance = 218;
			this.splitContainer1.TabIndex = 11;
			// 
			// txtSource
			// 
			this.txtSource.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
			this.txtSource.AutoListPosition = null;
			this.txtSource.AutoListSelectedText = "a123";
			this.txtSource.AutoListVisible = false;
			this.txtSource.BackColor = System.Drawing.Color.White;
			this.txtSource.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
			this.txtSource.CopyAsRTF = false;
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.FontName = "Courier new";
			this.txtSource.HighLightActiveLine = true;
			this.txtSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.txtSource.Indent = Alsing.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtSource.InfoTipCount = 1;
			this.txtSource.InfoTipPosition = null;
			this.txtSource.InfoTipSelectedIndex = 1;
			this.txtSource.InfoTipVisible = false;
			this.txtSource.Location = new System.Drawing.Point(0, 0);
			this.txtSource.LockCursorUpdate = false;
			this.txtSource.Name = "txtSource";
			this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtSource.ShowGutterMargin = false;
			this.txtSource.ShowScopeIndicator = false;
			this.txtSource.ShowTabGuides = true;
			this.txtSource.Size = new System.Drawing.Size(686, 218);
			this.txtSource.SmoothScroll = true;
			this.txtSource.SplitView = false;
			this.txtSource.SplitviewH = -4;
			this.txtSource.SplitviewV = -4;
			this.txtSource.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
			this.txtSource.TabIndex = 5;
			this.txtSource.TabSize = 2;
			this.txtSource.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
			this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(686, 244);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtParseTree);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(678, 218);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Reduction Tree";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// txtParseTree
			// 
			this.txtParseTree.BackColor = System.Drawing.SystemColors.Window;
			this.txtParseTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtParseTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParseTree.Location = new System.Drawing.Point(3, 3);
			this.txtParseTree.Multiline = true;
			this.txtParseTree.Name = "txtParseTree";
			this.txtParseTree.ReadOnly = true;
			this.txtParseTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtParseTree.Size = new System.Drawing.Size(672, 212);
			this.txtParseTree.TabIndex = 6;
			this.txtParseTree.WordWrap = false;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.txtParseTrimTree);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(678, 218);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Trimmed Tree";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// txtParseTrimTree
			// 
			this.txtParseTrimTree.BackColor = System.Drawing.SystemColors.Window;
			this.txtParseTrimTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtParseTrimTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParseTrimTree.Location = new System.Drawing.Point(3, 3);
			this.txtParseTrimTree.Multiline = true;
			this.txtParseTrimTree.Name = "txtParseTrimTree";
			this.txtParseTrimTree.ReadOnly = true;
			this.txtParseTrimTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtParseTrimTree.Size = new System.Drawing.Size(672, 212);
			this.txtParseTrimTree.TabIndex = 7;
			this.txtParseTrimTree.WordWrap = false;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtAST);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(678, 218);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "AST";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtAST
			// 
			this.txtAST.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAST.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAST.Location = new System.Drawing.Point(3, 3);
			this.txtAST.Multiline = true;
			this.txtAST.Name = "txtAST";
			this.txtAST.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAST.Size = new System.Drawing.Size(672, 212);
			this.txtAST.TabIndex = 0;
			this.txtAST.WordWrap = false;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.txtLog);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(678, 218);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Log";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.Size = new System.Drawing.Size(672, 212);
			this.txtLog.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(692, 542);
			this.tableLayoutPanel2.TabIndex = 10;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel3.Controls.Add(this.btnLoad, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.txtTableFile, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnLoadSYN, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.txtSynFile, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(686, 64);
			this.tableLayoutPanel3.TabIndex = 12;
			// 
			// btnLoadSYN
			// 
			this.btnLoadSYN.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoadSYN.Enabled = false;
			this.btnLoadSYN.Location = new System.Drawing.Point(589, 35);
			this.btnLoadSYN.Name = "btnLoadSYN";
			this.btnLoadSYN.Size = new System.Drawing.Size(94, 26);
			this.btnLoadSYN.TabIndex = 10;
			this.btnLoadSYN.Text = "Load SYN";
			this.btnLoadSYN.UseVisualStyleBackColor = true;
			this.btnLoadSYN.Click += new System.EventHandler(this.btnLoadSYN_Click);
			// 
			// txtSynFile
			// 
			this.txtSynFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSynFile.Location = new System.Drawing.Point(3, 35);
			this.txtSynFile.Name = "txtSynFile";
			this.txtSynFile.Size = new System.Drawing.Size(580, 20);
			this.txtSynFile.TabIndex = 11;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.txtGrammar);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(678, 218);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Grammar";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// txtGrammar
			// 
			this.txtGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGrammar.Location = new System.Drawing.Point(3, 3);
			this.txtGrammar.Multiline = true;
			this.txtGrammar.Name = "txtGrammar";
			this.txtGrammar.ReadOnly = true;
			this.txtGrammar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtGrammar.Size = new System.Drawing.Size(672, 212);
			this.txtGrammar.TabIndex = 0;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 542);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "frmMain";
			this.Text = "Draw Parse Tree";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.Button btnLoad;
		internal System.Windows.Forms.TextBox txtTableFile;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnLoadSYN;
		private System.Windows.Forms.TextBox txtSynFile;
		internal Alsing.Windows.Forms.SyntaxBoxControl txtSource;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		internal System.Windows.Forms.TextBox txtParseTree;
		private System.Windows.Forms.TabPage tabPage3;
		internal System.Windows.Forms.TextBox txtParseTrimTree;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtAST;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TextBox txtGrammar;
	}
}
