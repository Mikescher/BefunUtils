﻿namespace BefungExec.View
{
	partial class MainForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.glProgramView = new OpenTK.GLControl();
			this.glStackView = new OpenTK.GLControl();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.syntaxHighlightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showTrailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aSCIIStackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomCompleteOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToInitialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.skipNOPsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lowToolStripMenuItem = new BefungExec.View.ToolStripRadioButtonMenuItem();
			this.middleToolStripMenuItem = new BefungExec.View.ToolStripRadioButtonMenuItem();
			this.fastToolStripMenuItem = new BefungExec.View.ToolStripRadioButtonMenuItem();
			this.veryFastToolStripMenuItem = new BefungExec.View.ToolStripRadioButtonMenuItem();
			this.fullToolStripMenuItem = new BefungExec.View.ToolStripRadioButtonMenuItem();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.removeAllBreakpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showCompleteStackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showCompleteOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showCurrentStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.glProgramView, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.glStackView, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(882, 627);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// glProgramView
			// 
			this.glProgramView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.glProgramView.BackColor = System.Drawing.Color.Black;
			this.glProgramView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glProgramView.Location = new System.Drawing.Point(253, 3);
			this.glProgramView.Name = "glProgramView";
			this.glProgramView.Size = new System.Drawing.Size(626, 621);
			this.glProgramView.TabIndex = 0;
			this.glProgramView.VSync = false;
			this.glProgramView.Load += new System.EventHandler(this.glProgramView_Load);
			this.glProgramView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glProgramView_KeyPress);
			this.glProgramView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glProgramView_MouseDown);
			this.glProgramView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glProgramView_MouseMove);
			this.glProgramView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glProgramView_MouseUp);
			this.glProgramView.Resize += new System.EventHandler(this.glProgramView_Resize);
			// 
			// glStackView
			// 
			this.glStackView.BackColor = System.Drawing.Color.Black;
			this.glStackView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glStackView.Location = new System.Drawing.Point(3, 3);
			this.glStackView.Name = "glStackView";
			this.glStackView.Size = new System.Drawing.Size(244, 621);
			this.glStackView.TabIndex = 1;
			this.glStackView.VSync = false;
			this.glStackView.Load += new System.EventHandler(this.glStackView_Load);
			this.glStackView.Resize += new System.EventHandler(this.glStackView_Resize);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.displayToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(882, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fIleToolStripMenuItem
			// 
			this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
			this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
			this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fIleToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(100, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// displayToolStripMenuItem
			// 
			this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syntaxHighlightingToolStripMenuItem,
            this.showTrailToolStripMenuItem,
            this.aSCIIStackToolStripMenuItem,
            this.toolStripMenuItem4,
            this.zoomOutToolStripMenuItem,
            this.zoomCompleteOutToolStripMenuItem,
            this.zoomToInitialToolStripMenuItem});
			this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
			this.displayToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.displayToolStripMenuItem.Text = "View";
			// 
			// syntaxHighlightingToolStripMenuItem
			// 
			this.syntaxHighlightingToolStripMenuItem.CheckOnClick = true;
			this.syntaxHighlightingToolStripMenuItem.Name = "syntaxHighlightingToolStripMenuItem";
			this.syntaxHighlightingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.syntaxHighlightingToolStripMenuItem.Text = "Syntax Highlighting";
			this.syntaxHighlightingToolStripMenuItem.CheckedChanged += new System.EventHandler(this.syntaxHighlightingToolStripMenuItem_CheckedChanged);
			// 
			// showTrailToolStripMenuItem
			// 
			this.showTrailToolStripMenuItem.CheckOnClick = true;
			this.showTrailToolStripMenuItem.Name = "showTrailToolStripMenuItem";
			this.showTrailToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.showTrailToolStripMenuItem.Text = "Show Trail";
			this.showTrailToolStripMenuItem.Click += new System.EventHandler(this.showTrailToolStripMenuItem_Click);
			// 
			// aSCIIStackToolStripMenuItem
			// 
			this.aSCIIStackToolStripMenuItem.CheckOnClick = true;
			this.aSCIIStackToolStripMenuItem.Name = "aSCIIStackToolStripMenuItem";
			this.aSCIIStackToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.aSCIIStackToolStripMenuItem.Text = "ASCII-Stack";
			this.aSCIIStackToolStripMenuItem.Click += new System.EventHandler(this.aSCIIStackToolStripMenuItem_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
			// 
			// zoomOutToolStripMenuItem
			// 
			this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
			this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.zoomOutToolStripMenuItem.Text = "Zoom out";
			this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
			// 
			// zoomCompleteOutToolStripMenuItem
			// 
			this.zoomCompleteOutToolStripMenuItem.Name = "zoomCompleteOutToolStripMenuItem";
			this.zoomCompleteOutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.zoomCompleteOutToolStripMenuItem.Text = "Zoom complete out";
			this.zoomCompleteOutToolStripMenuItem.Click += new System.EventHandler(this.zoomCompleteOutToolStripMenuItem_Click);
			// 
			// zoomToInitialToolStripMenuItem
			// 
			this.zoomToInitialToolStripMenuItem.Name = "zoomToInitialToolStripMenuItem";
			this.zoomToInitialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.zoomToInitialToolStripMenuItem.Text = "Zoom to Initial";
			this.zoomToInitialToolStripMenuItem.Click += new System.EventHandler(this.zoomToInitialToolStripMenuItem_Click);
			// 
			// simulationToolStripMenuItem
			// 
			this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.toolStripMenuItem1,
            this.skipNOPsToolStripMenuItem,
            this.toolStripMenuItem5,
            this.speedToolStripMenuItem});
			this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
			this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.simulationToolStripMenuItem.Text = "Simulation";
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.runToolStripMenuItem.Text = "Run/Pause";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// stepToolStripMenuItem
			// 
			this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
			this.stepToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.stepToolStripMenuItem.Text = "Step";
			this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 6);
			// 
			// skipNOPsToolStripMenuItem
			// 
			this.skipNOPsToolStripMenuItem.CheckOnClick = true;
			this.skipNOPsToolStripMenuItem.Name = "skipNOPsToolStripMenuItem";
			this.skipNOPsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.skipNOPsToolStripMenuItem.Text = "Skip NOP\'s";
			this.skipNOPsToolStripMenuItem.Click += new System.EventHandler(this.skipNOPsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(129, 6);
			// 
			// speedToolStripMenuItem
			// 
			this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowToolStripMenuItem,
            this.middleToolStripMenuItem,
            this.fastToolStripMenuItem,
            this.veryFastToolStripMenuItem,
            this.fullToolStripMenuItem});
			this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
			this.speedToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.speedToolStripMenuItem.Text = "Speed";
			// 
			// lowToolStripMenuItem
			// 
			this.lowToolStripMenuItem.CheckOnClick = true;
			this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
			this.lowToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.lowToolStripMenuItem.Text = "Low";
			this.lowToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speedToolStripMenuItem_CheckedChanged);
			// 
			// middleToolStripMenuItem
			// 
			this.middleToolStripMenuItem.CheckOnClick = true;
			this.middleToolStripMenuItem.Name = "middleToolStripMenuItem";
			this.middleToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.middleToolStripMenuItem.Text = "Middle";
			this.middleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speedToolStripMenuItem_CheckedChanged);
			// 
			// fastToolStripMenuItem
			// 
			this.fastToolStripMenuItem.CheckOnClick = true;
			this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
			this.fastToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.fastToolStripMenuItem.Text = "Fast";
			this.fastToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speedToolStripMenuItem_CheckedChanged);
			// 
			// veryFastToolStripMenuItem
			// 
			this.veryFastToolStripMenuItem.CheckOnClick = true;
			this.veryFastToolStripMenuItem.Name = "veryFastToolStripMenuItem";
			this.veryFastToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.veryFastToolStripMenuItem.Text = "Very Fast";
			this.veryFastToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speedToolStripMenuItem_CheckedChanged);
			// 
			// fullToolStripMenuItem
			// 
			this.fullToolStripMenuItem.CheckOnClick = true;
			this.fullToolStripMenuItem.Name = "fullToolStripMenuItem";
			this.fullToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.fullToolStripMenuItem.Text = "Full";
			this.fullToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speedToolStripMenuItem_CheckedChanged);
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugModeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.removeAllBreakpointsToolStripMenuItem,
            this.showCompleteStackToolStripMenuItem,
            this.showCompleteOutputToolStripMenuItem,
            this.showCurrentStateToolStripMenuItem});
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// debugModeToolStripMenuItem
			// 
			this.debugModeToolStripMenuItem.CheckOnClick = true;
			this.debugModeToolStripMenuItem.Name = "debugModeToolStripMenuItem";
			this.debugModeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.debugModeToolStripMenuItem.Text = "Debug Mode";
			this.debugModeToolStripMenuItem.Click += new System.EventHandler(this.debugModeToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
			// 
			// removeAllBreakpointsToolStripMenuItem
			// 
			this.removeAllBreakpointsToolStripMenuItem.Name = "removeAllBreakpointsToolStripMenuItem";
			this.removeAllBreakpointsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.removeAllBreakpointsToolStripMenuItem.Text = "Remove all Breakpoints";
			this.removeAllBreakpointsToolStripMenuItem.Click += new System.EventHandler(this.removeAllBreakpointsToolStripMenuItem_Click);
			// 
			// showCompleteStackToolStripMenuItem
			// 
			this.showCompleteStackToolStripMenuItem.Name = "showCompleteStackToolStripMenuItem";
			this.showCompleteStackToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showCompleteStackToolStripMenuItem.Text = "Show complete Stack";
			this.showCompleteStackToolStripMenuItem.Click += new System.EventHandler(this.showCompleteStackToolStripMenuItem_Click);
			// 
			// showCompleteOutputToolStripMenuItem
			// 
			this.showCompleteOutputToolStripMenuItem.Name = "showCompleteOutputToolStripMenuItem";
			this.showCompleteOutputToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showCompleteOutputToolStripMenuItem.Text = "Show complete Output";
			this.showCompleteOutputToolStripMenuItem.Click += new System.EventHandler(this.showCompleteOutputToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.aboutToolStripMenuItem.Text = "About ...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// showCurrentStateToolStripMenuItem
			// 
			this.showCurrentStateToolStripMenuItem.Name = "showCurrentStateToolStripMenuItem";
			this.showCurrentStateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showCurrentStateToolStripMenuItem.Text = "Show current State";
			this.showCurrentStateToolStripMenuItem.Click += new System.EventHandler(this.showCurrentStateToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(882, 651);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "BefungExec";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
		private OpenTK.GLControl glProgramView;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem removeAllBreakpointsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showCompleteStackToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem syntaxHighlightingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aSCIIStackToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem zoomToInitialToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private OpenTK.GLControl glStackView;
		private System.Windows.Forms.ToolStripMenuItem showCompleteOutputToolStripMenuItem;
		private ToolStripRadioButtonMenuItem lowToolStripMenuItem;
		private ToolStripRadioButtonMenuItem middleToolStripMenuItem;
		private ToolStripRadioButtonMenuItem fastToolStripMenuItem;
		private ToolStripRadioButtonMenuItem veryFastToolStripMenuItem;
		private ToolStripRadioButtonMenuItem fullToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem skipNOPsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem zoomCompleteOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showTrailToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showCurrentStateToolStripMenuItem;
	}
}