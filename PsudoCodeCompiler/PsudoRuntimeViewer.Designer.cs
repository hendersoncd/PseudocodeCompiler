namespace PseudoCodeCompiler
{
    partial class PsudoRuntimeViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PsudoRuntimeViewer));
            this.rightPaneSplitter = new System.Windows.Forms.SplitContainer();
            this.variableDataPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.debugEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.callStackListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newStripButton = new System.Windows.Forms.ToolStripButton();
            this.openStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveAsStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.pauseButton = new System.Windows.Forms.ToolStripButton();
            this.abortButton = new System.Windows.Forms.ToolStripButton();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutButton = new System.Windows.Forms.ToolStripButton();
            this.copyButton = new System.Windows.Forms.ToolStripButton();
            this.pasteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.leftPaneSpitter = new System.Windows.Forms.SplitContainer();
            this.viewsTabControl = new System.Windows.Forms.TabControl();
            this.editCodePage = new System.Windows.Forms.TabPage();
            this.codeRichTextBox = new PseudoCodeCompiler.SyntaxHighlightingTextbox();
            this.lineNumbers_For_RichTextBox1 = new PseudoCodeCompiler.LineNumbers_For_RichTextBox();
            this.codePathLabel = new System.Windows.Forms.Label();
            this.runtimePage = new System.Windows.Forms.TabPage();
            this.codeLinesList = new System.Windows.Forms.ListBox();
            this.inputFilePage = new System.Windows.Forms.TabPage();
            this.inputDataGridView = new System.Windows.Forms.DataGridView();
            this.inputFilenameLabel = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addColumnButton = new System.Windows.Forms.ToolStripButton();
            this.deleteColumnButton = new System.Windows.Forms.ToolStripButton();
            this.flowChartPage = new System.Windows.Forms.TabPage();
            this.flowChartViewerControl1 = new PseudoCodeCompiler.FlowChart.FlowChartViewerControl();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helloWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decisionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ifthenelseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combiningLogicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nestedDecisionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caseStatementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whileLoopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatLoopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countedLoopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arraysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoDimensionalArraysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lineLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.rightPaneSplitter.Panel1.SuspendLayout();
            this.rightPaneSplitter.Panel2.SuspendLayout();
            this.rightPaneSplitter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.leftPaneSpitter.Panel1.SuspendLayout();
            this.leftPaneSpitter.Panel2.SuspendLayout();
            this.leftPaneSpitter.SuspendLayout();
            this.viewsTabControl.SuspendLayout();
            this.editCodePage.SuspendLayout();
            this.runtimePage.SuspendLayout();
            this.inputFilePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGridView)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.flowChartPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPaneSplitter
            // 
            this.rightPaneSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPaneSplitter.Location = new System.Drawing.Point(483, 49);
            this.rightPaneSplitter.Name = "rightPaneSplitter";
            this.rightPaneSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightPaneSplitter.Panel1
            // 
            this.rightPaneSplitter.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.rightPaneSplitter.Panel1.Controls.Add(this.variableDataPanel);
            this.rightPaneSplitter.Panel1.Controls.Add(this.label2);
            this.rightPaneSplitter.Panel1.Controls.Add(this.panel1);
            // 
            // rightPaneSplitter.Panel2
            // 
            this.rightPaneSplitter.Panel2.Controls.Add(this.callStackListBox);
            this.rightPaneSplitter.Panel2.Controls.Add(this.label1);
            this.rightPaneSplitter.Size = new System.Drawing.Size(241, 421);
            this.rightPaneSplitter.SplitterDistance = 237;
            this.rightPaneSplitter.TabIndex = 0;
            // 
            // variableDataPanel
            // 
            this.variableDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variableDataPanel.Location = new System.Drawing.Point(0, 58);
            this.variableDataPanel.Name = "variableDataPanel";
            this.variableDataPanel.Size = new System.Drawing.Size(241, 179);
            this.variableDataPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Variables";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.debugEnabledCheckbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 45);
            this.panel1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(84, 21);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(123, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Auto Step Delay";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(207, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "mSec";
            // 
            // debugEnabledCheckbox
            // 
            this.debugEnabledCheckbox.AutoSize = true;
            this.debugEnabledCheckbox.Checked = true;
            this.debugEnabledCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.debugEnabledCheckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.debugEnabledCheckbox.Location = new System.Drawing.Point(0, 0);
            this.debugEnabledCheckbox.Name = "debugEnabledCheckbox";
            this.debugEnabledCheckbox.Padding = new System.Windows.Forms.Padding(2);
            this.debugEnabledCheckbox.Size = new System.Drawing.Size(241, 21);
            this.debugEnabledCheckbox.TabIndex = 3;
            this.debugEnabledCheckbox.Text = "Debug Environment Enabled";
            this.debugEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // callStackListBox
            // 
            this.callStackListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.callStackListBox.FormattingEnabled = true;
            this.callStackListBox.IntegralHeight = false;
            this.callStackListBox.Location = new System.Drawing.Point(0, 13);
            this.callStackListBox.Name = "callStackListBox";
            this.callStackListBox.Size = new System.Drawing.Size(241, 167);
            this.callStackListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Call Stack";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripButton,
            this.openStripButton,
            this.saveStripButton,
            this.saveAsStripButton,
            this.toolStripSeparator4,
            this.runButton,
            this.pauseButton,
            this.abortButton,
            this.nextButton,
            this.toolStripSeparator3,
            this.cutButton,
            this.copyButton,
            this.pasteButton,
            this.toolStripSeparator5,
            this.undoButton,
            this.redoButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(724, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newStripButton
            // 
            this.newStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newStripButton.Image")));
            this.newStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newStripButton.Name = "newStripButton";
            this.newStripButton.Size = new System.Drawing.Size(23, 22);
            this.newStripButton.Text = "New";
            this.newStripButton.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openStripButton
            // 
            this.openStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openStripButton.Image")));
            this.openStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openStripButton.Name = "openStripButton";
            this.openStripButton.Size = new System.Drawing.Size(23, 22);
            this.openStripButton.Text = "Open";
            this.openStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveStripButton
            // 
            this.saveStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveStripButton.Image")));
            this.saveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveStripButton.Name = "saveStripButton";
            this.saveStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveStripButton.Text = "Save";
            this.saveStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsStripButton
            // 
            this.saveAsStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAsStripButton.Image = global::PseudoCodeCompiler.Properties.Resources.SaveAllHS;
            this.saveAsStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsStripButton.Name = "saveAsStripButton";
            this.saveAsStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveAsStripButton.Text = "Save As";
            this.saveAsStripButton.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // runButton
            // 
            this.runButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(23, 22);
            this.runButton.Text = "Compile and Run";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseButton.Enabled = false;
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(23, 22);
            this.pauseButton.Text = "Pause";
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // abortButton
            // 
            this.abortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abortButton.Enabled = false;
            this.abortButton.Image = ((System.Drawing.Image)(resources.GetObject("abortButton.Image")));
            this.abortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(23, 22);
            this.abortButton.Text = "Abort";
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(23, 22);
            this.nextButton.Text = "Next";
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cutButton
            // 
            this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutButton.Image = ((System.Drawing.Image)(resources.GetObject("cutButton.Image")));
            this.cutButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(23, 22);
            this.cutButton.Text = "Cut";
            this.cutButton.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // copyButton
            // 
            this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyButton.Image = ((System.Drawing.Image)(resources.GetObject("copyButton.Image")));
            this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(23, 22);
            this.copyButton.Text = "Copy";
            this.copyButton.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // pasteButton
            // 
            this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(23, 22);
            this.pasteButton.Text = "Paste";
            this.pasteButton.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Enabled = false;
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 22);
            this.undoButton.Text = "Undo";
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Enabled = false;
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 22);
            this.redoButton.Text = "Redo";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // leftPaneSpitter
            // 
            this.leftPaneSpitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPaneSpitter.Location = new System.Drawing.Point(0, 49);
            this.leftPaneSpitter.Name = "leftPaneSpitter";
            this.leftPaneSpitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftPaneSpitter.Panel1
            // 
            this.leftPaneSpitter.Panel1.Controls.Add(this.viewsTabControl);
            // 
            // leftPaneSpitter.Panel2
            // 
            this.leftPaneSpitter.Panel2.Controls.Add(this.outputTextBox);
            this.leftPaneSpitter.Size = new System.Drawing.Size(483, 421);
            this.leftPaneSpitter.SplitterDistance = 266;
            this.leftPaneSpitter.TabIndex = 0;
            // 
            // viewsTabControl
            // 
            this.viewsTabControl.Controls.Add(this.editCodePage);
            this.viewsTabControl.Controls.Add(this.runtimePage);
            this.viewsTabControl.Controls.Add(this.inputFilePage);
            this.viewsTabControl.Controls.Add(this.flowChartPage);
            this.viewsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewsTabControl.Location = new System.Drawing.Point(0, 0);
            this.viewsTabControl.Name = "viewsTabControl";
            this.viewsTabControl.SelectedIndex = 0;
            this.viewsTabControl.Size = new System.Drawing.Size(483, 266);
            this.viewsTabControl.TabIndex = 4;
            this.viewsTabControl.SelectedIndexChanged += new System.EventHandler(this.viewsTabControl_SelectedIndexChanged);
            // 
            // editCodePage
            // 
            this.editCodePage.Controls.Add(this.codeRichTextBox);
            this.editCodePage.Controls.Add(this.lineNumbers_For_RichTextBox1);
            this.editCodePage.Controls.Add(this.codePathLabel);
            this.editCodePage.Location = new System.Drawing.Point(4, 22);
            this.editCodePage.Name = "editCodePage";
            this.editCodePage.Padding = new System.Windows.Forms.Padding(3);
            this.editCodePage.Size = new System.Drawing.Size(475, 240);
            this.editCodePage.TabIndex = 0;
            this.editCodePage.Text = "Edit Code";
            this.editCodePage.UseVisualStyleBackColor = true;
            // 
            // codeRichTextBox
            // 
            this.codeRichTextBox.AcceptsTab = true;
            this.codeRichTextBox.BackColor = System.Drawing.Color.Black;
            this.codeRichTextBox.Commands = System.Drawing.Color.Yellow;
            this.codeRichTextBox.Comments = System.Drawing.Color.Lime;
            this.codeRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeRichTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeRichTextBox.ForeColor = System.Drawing.Color.Lime;
            this.codeRichTextBox.Keywords = System.Drawing.Color.Blue;
            this.codeRichTextBox.Location = new System.Drawing.Point(20, 16);
            this.codeRichTextBox.Name = "codeRichTextBox";
            this.codeRichTextBox.PlainText = System.Drawing.Color.Black;
            this.codeRichTextBox.Size = new System.Drawing.Size(452, 221);
            this.codeRichTextBox.TabIndex = 5;
            this.codeRichTextBox.Text = "";
            this.codeRichTextBox.SelectionChanged += new System.EventHandler(this.codeRichTextBox_SelectionChanged);
            this.codeRichTextBox.TextChanged += new System.EventHandler(this.codeRichTextBox_TextChanged);
            // 
            // lineNumbers_For_RichTextBox1
            // 
            this.lineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
            this.lineNumbers_For_RichTextBox1.AutoSizing = true;
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_RichTextBox1.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lineNumbers_For_RichTextBox1.DockSide = PseudoCodeCompiler.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.lineNumbers_For_RichTextBox1.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
            this.lineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = false;
            this.lineNumbers_For_RichTextBox1.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(3, 16);
            this.lineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.MarginLines_Side = PseudoCodeCompiler.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
            this.lineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox1.ParentRichTextBox = this.codeRichTextBox;
            this.lineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
            this.lineNumbers_For_RichTextBox1.Show_BorderLines = true;
            this.lineNumbers_For_RichTextBox1.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox1.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox1.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(17, 221);
            this.lineNumbers_For_RichTextBox1.TabIndex = 6;
            // 
            // codePathLabel
            // 
            this.codePathLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.codePathLabel.Location = new System.Drawing.Point(3, 3);
            this.codePathLabel.Name = "codePathLabel";
            this.codePathLabel.Size = new System.Drawing.Size(469, 13);
            this.codePathLabel.TabIndex = 4;
            this.codePathLabel.Text = "<No File Selected>";
            // 
            // runtimePage
            // 
            this.runtimePage.Controls.Add(this.codeLinesList);
            this.runtimePage.Location = new System.Drawing.Point(4, 22);
            this.runtimePage.Name = "runtimePage";
            this.runtimePage.Padding = new System.Windows.Forms.Padding(3);
            this.runtimePage.Size = new System.Drawing.Size(475, 240);
            this.runtimePage.TabIndex = 1;
            this.runtimePage.Text = "Runtime";
            this.runtimePage.UseVisualStyleBackColor = true;
            // 
            // codeLinesList
            // 
            this.codeLinesList.BackColor = System.Drawing.Color.Black;
            this.codeLinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeLinesList.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeLinesList.ForeColor = System.Drawing.Color.Lime;
            this.codeLinesList.FormattingEnabled = true;
            this.codeLinesList.IntegralHeight = false;
            this.codeLinesList.ItemHeight = 16;
            this.codeLinesList.Location = new System.Drawing.Point(3, 3);
            this.codeLinesList.Name = "codeLinesList";
            this.codeLinesList.Size = new System.Drawing.Size(469, 234);
            this.codeLinesList.TabIndex = 2;
            // 
            // inputFilePage
            // 
            this.inputFilePage.Controls.Add(this.inputDataGridView);
            this.inputFilePage.Controls.Add(this.inputFilenameLabel);
            this.inputFilePage.Controls.Add(this.toolStrip2);
            this.inputFilePage.Location = new System.Drawing.Point(4, 22);
            this.inputFilePage.Name = "inputFilePage";
            this.inputFilePage.Size = new System.Drawing.Size(475, 240);
            this.inputFilePage.TabIndex = 2;
            this.inputFilePage.Text = "Input File";
            this.inputFilePage.UseVisualStyleBackColor = true;
            // 
            // inputDataGridView
            // 
            this.inputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDataGridView.Location = new System.Drawing.Point(0, 38);
            this.inputDataGridView.Name = "inputDataGridView";
            this.inputDataGridView.Size = new System.Drawing.Size(475, 202);
            this.inputDataGridView.TabIndex = 0;
            this.inputDataGridView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.inputDataGridView_ColumnHeaderMouseDoubleClick);
            // 
            // inputFilenameLabel
            // 
            this.inputFilenameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputFilenameLabel.Location = new System.Drawing.Point(0, 25);
            this.inputFilenameLabel.Name = "inputFilenameLabel";
            this.inputFilenameLabel.Size = new System.Drawing.Size(475, 13);
            this.inputFilenameLabel.TabIndex = 5;
            this.inputFilenameLabel.Text = "<No File Selected>";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addColumnButton,
            this.deleteColumnButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(475, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // addColumnButton
            // 
            this.addColumnButton.Image = ((System.Drawing.Image)(resources.GetObject("addColumnButton.Image")));
            this.addColumnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addColumnButton.Name = "addColumnButton";
            this.addColumnButton.Size = new System.Drawing.Size(95, 22);
            this.addColumnButton.Text = "Add Column";
            this.addColumnButton.Click += new System.EventHandler(this.addColumnButton_Click);
            // 
            // deleteColumnButton
            // 
            this.deleteColumnButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteColumnButton.Image")));
            this.deleteColumnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteColumnButton.Name = "deleteColumnButton";
            this.deleteColumnButton.Size = new System.Drawing.Size(106, 22);
            this.deleteColumnButton.Text = "Delete Column";
            this.deleteColumnButton.Click += new System.EventHandler(this.deleteColumnButton_Click);
            // 
            // flowChartPage
            // 
            this.flowChartPage.Controls.Add(this.flowChartViewerControl1);
            this.flowChartPage.Location = new System.Drawing.Point(4, 22);
            this.flowChartPage.Name = "flowChartPage";
            this.flowChartPage.Size = new System.Drawing.Size(475, 240);
            this.flowChartPage.TabIndex = 3;
            this.flowChartPage.Text = "Flow Chart (Beta)";
            this.flowChartPage.UseVisualStyleBackColor = true;
            // 
            // flowChartViewerControl1
            // 
            this.flowChartViewerControl1.AutoScroll = true;
            this.flowChartViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowChartViewerControl1.Location = new System.Drawing.Point(0, 0);
            this.flowChartViewerControl1.Name = "flowChartViewerControl1";
            this.flowChartViewerControl1.Size = new System.Drawing.Size(475, 240);
            this.flowChartViewerControl1.TabIndex = 0;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.Black;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.ForeColor = System.Drawing.Color.White;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(483, 151);
            this.outputTextBox.TabIndex = 3;
            this.outputTextBox.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.examplesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::PseudoCodeCompiler.Properties.Resources.openfolderHS;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::PseudoCodeCompiler.Properties.Resources.saveHS;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::PseudoCodeCompiler.Properties.Resources.SaveAllHS;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(100, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.cutCopyPaste_Clicked);
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloWorldToolStripMenuItem,
            this.userInputToolStripMenuItem,
            this.mathToolStripMenuItem,
            this.decisionsToolStripMenuItem,
            this.loopingToolStripMenuItem,
            this.arraysToolStripMenuItem,
            this.twoDimensionalArraysToolStripMenuItem,
            this.dataFilesToolStripMenuItem});
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.examplesToolStripMenuItem.Text = "Examples";
            // 
            // helloWorldToolStripMenuItem
            // 
            this.helloWorldToolStripMenuItem.Name = "helloWorldToolStripMenuItem";
            this.helloWorldToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.helloWorldToolStripMenuItem.Text = "Hello World";
            this.helloWorldToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // userInputToolStripMenuItem
            // 
            this.userInputToolStripMenuItem.Name = "userInputToolStripMenuItem";
            this.userInputToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.userInputToolStripMenuItem.Text = "User Input";
            this.userInputToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // mathToolStripMenuItem
            // 
            this.mathToolStripMenuItem.Name = "mathToolStripMenuItem";
            this.mathToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.mathToolStripMenuItem.Text = "Math";
            this.mathToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // decisionsToolStripMenuItem
            // 
            this.decisionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ifthenelseToolStripMenuItem,
            this.combiningLogicToolStripMenuItem,
            this.nestedDecisionsToolStripMenuItem,
            this.caseStatementsToolStripMenuItem});
            this.decisionsToolStripMenuItem.Name = "decisionsToolStripMenuItem";
            this.decisionsToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.decisionsToolStripMenuItem.Text = "Decisions";
            // 
            // ifthenelseToolStripMenuItem
            // 
            this.ifthenelseToolStripMenuItem.Name = "ifthenelseToolStripMenuItem";
            this.ifthenelseToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ifthenelseToolStripMenuItem.Text = "If-Then-Else";
            this.ifthenelseToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // combiningLogicToolStripMenuItem
            // 
            this.combiningLogicToolStripMenuItem.Name = "combiningLogicToolStripMenuItem";
            this.combiningLogicToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.combiningLogicToolStripMenuItem.Text = "Combining Logic";
            this.combiningLogicToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // nestedDecisionsToolStripMenuItem
            // 
            this.nestedDecisionsToolStripMenuItem.Name = "nestedDecisionsToolStripMenuItem";
            this.nestedDecisionsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.nestedDecisionsToolStripMenuItem.Text = "Nested Decisions";
            this.nestedDecisionsToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // caseStatementsToolStripMenuItem
            // 
            this.caseStatementsToolStripMenuItem.Name = "caseStatementsToolStripMenuItem";
            this.caseStatementsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.caseStatementsToolStripMenuItem.Text = "Case Statements";
            this.caseStatementsToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // loopingToolStripMenuItem
            // 
            this.loopingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whileLoopsToolStripMenuItem,
            this.repeatLoopsToolStripMenuItem,
            this.countedLoopsToolStripMenuItem});
            this.loopingToolStripMenuItem.Name = "loopingToolStripMenuItem";
            this.loopingToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.loopingToolStripMenuItem.Text = "Looping";
            // 
            // whileLoopsToolStripMenuItem
            // 
            this.whileLoopsToolStripMenuItem.Name = "whileLoopsToolStripMenuItem";
            this.whileLoopsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.whileLoopsToolStripMenuItem.Text = "While Loops";
            this.whileLoopsToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // repeatLoopsToolStripMenuItem
            // 
            this.repeatLoopsToolStripMenuItem.Name = "repeatLoopsToolStripMenuItem";
            this.repeatLoopsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.repeatLoopsToolStripMenuItem.Text = "Repeat Loops";
            this.repeatLoopsToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // countedLoopsToolStripMenuItem
            // 
            this.countedLoopsToolStripMenuItem.Name = "countedLoopsToolStripMenuItem";
            this.countedLoopsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.countedLoopsToolStripMenuItem.Text = "Counted Loops";
            this.countedLoopsToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // arraysToolStripMenuItem
            // 
            this.arraysToolStripMenuItem.Name = "arraysToolStripMenuItem";
            this.arraysToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.arraysToolStripMenuItem.Text = "Arrays";
            this.arraysToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // twoDimensionalArraysToolStripMenuItem
            // 
            this.twoDimensionalArraysToolStripMenuItem.Name = "twoDimensionalArraysToolStripMenuItem";
            this.twoDimensionalArraysToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.twoDimensionalArraysToolStripMenuItem.Text = "Two Dimensional Arrays";
            this.twoDimensionalArraysToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
            // 
            // dataFilesToolStripMenuItem
            // 
            this.dataFilesToolStripMenuItem.Name = "dataFilesToolStripMenuItem";
            this.dataFilesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dataFilesToolStripMenuItem.Text = "Data Files";
            this.dataFilesToolStripMenuItem.Click += new System.EventHandler(this.loadExample);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.lineLabel,
            this.columnLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(724, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(609, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lineLabel
            // 
            this.lineLabel.AutoSize = false;
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(50, 17);
            this.lineLabel.Text = "Ln 1";
            this.lineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = false;
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(50, 17);
            this.columnLabel.Text = "Col 1";
            this.columnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PsudoRuntimeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 492);
            this.Controls.Add(this.leftPaneSpitter);
            this.Controls.Add(this.rightPaneSplitter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PsudoRuntimeViewer";
            this.Text = "Pseudo Code Compiler and Runtime";
            this.Load += new System.EventHandler(this.PsudoRuntimeViewer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PsudoRuntimeViewer_FormClosing);
            this.rightPaneSplitter.Panel1.ResumeLayout(false);
            this.rightPaneSplitter.Panel2.ResumeLayout(false);
            this.rightPaneSplitter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.leftPaneSpitter.Panel1.ResumeLayout(false);
            this.leftPaneSpitter.Panel2.ResumeLayout(false);
            this.leftPaneSpitter.ResumeLayout(false);
            this.viewsTabControl.ResumeLayout(false);
            this.editCodePage.ResumeLayout(false);
            this.runtimePage.ResumeLayout(false);
            this.inputFilePage.ResumeLayout(false);
            this.inputFilePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGridView)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.flowChartPage.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer rightPaneSplitter;
        private System.Windows.Forms.Panel variableDataPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton runButton;
        private System.Windows.Forms.SplitContainer leftPaneSpitter;
        private System.Windows.Forms.ListBox codeLinesList;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.ListBox callStackListBox;
        private System.Windows.Forms.ToolStripButton abortButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl viewsTabControl;
        private System.Windows.Forms.TabPage editCodePage;
        private System.Windows.Forms.TabPage runtimePage;
        private System.Windows.Forms.TabPage inputFilePage;
        private System.Windows.Forms.DataGridView inputDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label codePathLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton addColumnButton;
        private System.Windows.Forms.ToolStripButton deleteColumnButton;
        private System.Windows.Forms.Label inputFilenameLabel;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helloWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mathToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton pauseButton;
        private System.Windows.Forms.ToolStripButton nextButton;
        private System.Windows.Forms.ToolStripMenuItem decisionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel lineLabel;
        private System.Windows.Forms.ToolStripStatusLabel columnLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private SyntaxHighlightingTextbox codeRichTextBox;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cutButton;
        private System.Windows.Forms.ToolStripButton copyButton;
        private System.Windows.Forms.ToolStripButton pasteButton;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem arraysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton newStripButton;
        private System.Windows.Forms.ToolStripButton openStripButton;
        private System.Windows.Forms.ToolStripButton saveStripButton;
        private System.Windows.Forms.ToolStripButton saveAsStripButton;
        private System.Windows.Forms.ToolStripMenuItem ifthenelseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caseStatementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whileLoopsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatLoopsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countedLoopsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem combiningLogicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nestedDecisionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem twoDimensionalArraysToolStripMenuItem;
        private System.Windows.Forms.TabPage flowChartPage;
        private PseudoCodeCompiler.FlowChart.FlowChartViewerControl flowChartViewerControl1;
        private LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
        private System.Windows.Forms.CheckBox debugEnabledCheckbox;
    }
}