using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PseudoCodeCompiler.Compiler;
using PseudoCodeCompiler.Runtime;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using PseudoCodeCompiler.Properties;

namespace PseudoCodeCompiler
{
    public partial class PsudoRuntimeViewer : Form
    {
        PropertyGrid variableGrid;
        PsudoCompiler compileInfo;
        PsudoProgram program;
        int curLine = 0;
        private bool isRunning = false;
        private bool isPaused = false;
        KeyboardHooks hooks = new KeyboardHooks();

        bool doStep = false;

        #region Constructor

        public PsudoRuntimeViewer()
        {
            InitializeComponent();


            variableGrid = new PropertyGrid();
            variableGrid.Dock = DockStyle.Fill;
            this.variableDataPanel.Controls.Add(variableGrid);

            this.codeLinesList.Dock = DockStyle.Fill;
            this.codeRichTextBox.Dock = DockStyle.Fill;
            this.viewsTabControl.SelectedTab = this.editCodePage;
            this.rightPaneSplitter.Visible = false;
            this.abortButton.Enabled = false;


            this.codeRichTextBox.BackColor = Settings.Default.Background;
            this.SetupHotKeys();
        }

        #endregion Constructor

        #region HotKeys


        private void SetupHotKeys()
        {
            Keys k = Keys.S | Keys.Control;
            this.hooks.RegisterHook(k, KeyboardHookCallback);

            k = Keys.Z | Keys.Control;
            this.hooks.RegisterHook(k, KeyboardHookCallback);

            k = Keys.Y | Keys.Control;
            this.hooks.RegisterHook(k, KeyboardHookCallback);

            k = Keys.F5;
            this.hooks.RegisterHook(k, KeyboardHookCallback);
        }
        protected void KeyboardHookCallback(Keys key, Keys modifier)
        {
            if (modifier == Keys.Control)
            {
                switch (key)
                {
                    case Keys.Z:
                        this.undoButton_Click(null, null);
                        break;
                    case Keys.Y:
                        this.redoButton_Click(null, null);
                        break;
                    case Keys.X:
                        this.cutCopyPaste_Clicked(this.cutButton, null);
                        break;
                    case Keys.C:
                        this.cutCopyPaste_Clicked(this.copyButton, null);
                        break;
                    case Keys.V:
                        this.cutCopyPaste_Clicked(this.pasteButton, null);
                        break;
                    case Keys.S:
                        this.saveToolStripMenuItem_Click(null, null);
                        break;
                }
            }
            else if (modifier == 0)
            {
                switch (key)
                {
                    case Keys.F5:
                        this.runButton_Click(null, null);
                        break;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            base.OnClosing(e);
        }

        #endregion HotKeys

        #region Append Text

        private delegate void AppendTextDelegate(string text, Color color);
        private void AppendText(string text, Color color)
        {
            if (this.outputTextBox.InvokeRequired)
            {
                this.Invoke(new AppendTextDelegate(AppendText), text, color);
            }
            else
            {
                if (this.outputTextBox.IsDisposed)
                    return;

                this.outputTextBox.AppendText(text);
                this.outputTextBox.Select(this.outputTextBox.Text.Length - text.Length, text.Length);
                this.outputTextBox.SelectionColor = color;
                this.outputTextBox.Select(this.outputTextBox.Text.Length, 0);
            }
        }

        #endregion Append Text

        #region View Switching

        private void viewsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rightPaneSplitter.Visible = (this.viewsTabControl.SelectedTab == this.runtimePage);

            if (this.viewsTabControl.SelectedTab == this.runtimePage)
            {
                this.outputTextBox.Focus();
            }
            else if (this.viewsTabControl.SelectedTab == this.flowChartPage)
            {
                if (this.Compile())
                {
                    this.flowChartViewerControl1.ShowProgram(this.program);
                    this.program = null;
                }
                else
                {
                    this.viewsTabControl.SelectedTab = this.editCodePage;
                }
            }
        }

        #endregion View Switching

        #region Program Runtime Methods

        public void ResetReader()
        {
            this.curLine = 0;
        }

        public string GetColumn(int i)
        {
            if (this.inputDataGridView.Columns.Count > i)
            {
                return this.inputDataGridView.Columns[i].HeaderText;
            }
            throw new Exception("Unknown Column Number");
        }

        public string[] ReadLine()
        {
            if (EOF)
            {
                this.AppendText("Attempt to read past End of File (EOF)\n", Color.Red);
                return null;
            }
            else if (this.curLine >= this.inputDataGridView.Rows.Count - 1)
            {
                //this.AppendText("End of File (EOF) has been reached. No Data Read", Color.WhiteSmoke);
                curLine++;
                return new string[this.inputDataGridView.ColumnCount];
            }

            List<string> lineItems = new List<string>();
            foreach (DataGridViewCell cell in this.inputDataGridView.Rows[curLine].Cells)
            {
                if (cell.Value == null)
                    lineItems.Add("");
                else
                    lineItems.Add(cell.Value.ToString());
            }

            curLine++;
            return lineItems.ToArray();
        }

        public bool EOF
        {
            get
            {
                return this.curLine >= this.inputDataGridView.Rows.Count;
            }
        }

        public bool DoEOF()
        {
            return this.EOF;
        }
        
        public void LoadProgram()
        {
            program.DoReadLine = this.ReadLine;
            program.DoGetColumn = this.GetColumn;
            program.DoEOF = this.DoEOF;

            this.ResetReader();

            if (program.MainMethod == null)
            {
                // Need to select the Main method
                ObjectSelectionDialog dlg = new ObjectSelectionDialog(program.GlobalMethods.Values.ToArray());
                dlg.ShowDialog(this);
                this.program.MainMethod = dlg.SelectedObject;
            }

            // Load the code lines
            this.codeLinesList.SuspendLayout();
            this.codeLinesList.Items.Clear();

            int lineNum = 1;
            int numDigits = this.compileInfo.CodeLines.Length.ToString().Length;
            foreach (string line in this.compileInfo.CodeLines)
            {
                this.codeLinesList.Items.Add(string.Format("{0," + numDigits + "}: {1}", lineNum, line));
                lineNum++;
            }

            // move to the runtime tab page
            this.viewsTabControl.SelectedTab = this.runtimePage;
            this.codeLinesList.ResumeLayout();

            // Initialize the Variable Grid object
            this.variableGrid.SelectedObject = program;

            // setup the events
            program.StandardOutput = new EventHandler<Output_EventArgs>(program_StandardOutput);
            program.ErrorOutput = new EventHandler<Output_EventArgs>(program_ErrorOutput);
            program.DebugOutput = new EventHandler<Output_EventArgs>(program_DebugOutput);
            program.AfterInstruction = new EventHandler<Instruction_EventArgs>(program_InstructionComplete);
            program.BeforeInstruction = new EventHandler<Instruction_EventArgs>(program_BeforeInstruction);
            program.ProgramComplete = new EventHandler(program_ProgramComplete);

            this.IsRunning = true;
        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                isPaused = false;
                this.abortButton.Enabled = value;
                this.pauseButton.Enabled = value;
                this.runButton.Enabled = !value;
                this.nextButton.Enabled = !value;
                if (value)
                    this.Status = "Running";
                else
                    this.Status = "Aborted";
            }
        }

        public bool IsPaused
        {
            get
            {
                return isPaused;
            }
            set
            {
                isPaused = value;
                isRunning = false;
                this.abortButton.Enabled = value;
                this.pauseButton.Enabled = !value;
                this.runButton.Enabled = value;
                this.nextButton.Enabled = value;
                if (value)
                    this.Status = "Paused";
                else
                    this.Status = "Running";
            }
        }

        #endregion Program Runtime Methods

        #region Program Events

        void program_ProgramComplete(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(this.program_ProgramComplete), sender, e);
            }
            else
            {
                this.variableGrid.Refresh();
                this.AppendText("\nProgram Completed", Color.Yellow);
               // this.rightPaneSplitter.Visible = false;
               // this.viewsTabControl.SelectedTab = this.editCodePage;
                this.codeLinesList.SelectedIndex = -1;
                this.IsRunning = false;

                this.program = null;
            }
        }

        void program_BeforeInstruction(object sender, Instruction_EventArgs e)
        {
            if (!this.debugEnabledCheckbox.Checked)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<Instruction_EventArgs>(this.program_BeforeInstruction), sender, e);
            }
            else
            {
                //this.variableGrid.Refresh();

                if (this.callStackListBox.Items.Count != this.program.callStack.Count)
                    this.callStackListBox.DataSource = this.program.callStack.ToList();

                if (this.codeLinesList.SelectedIndex != e.NextLine)
                    this.codeLinesList.SelectedIndex = e.NextLine;

                Thread.Sleep((int)this.numericUpDown1.Value);

                if (this.IsPaused)
                {
                    while (this.IsPaused && !this.doStep)
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    this.doStep = false;
                }
            }
        }

        void program_InstructionComplete(object sender, Instruction_EventArgs e)
        {
            if (!this.debugEnabledCheckbox.Checked)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<Instruction_EventArgs>(this.program_InstructionComplete), sender, e);
            }
            else
            {
                if (this.callStackListBox.Items.Count != this.program.callStack.Count)
                    this.callStackListBox.DataSource = this.program.callStack.ToList();

                if (this.codeLinesList.SelectedIndex != e.NextLine)
                    this.codeLinesList.SelectedIndex = e.NextLine;

                this.variableGrid.Refresh();
            }
        }

        void program_DebugOutput(object sender, Output_EventArgs e)
        {
            AppendText(e.Message, Color.Silver);
        }

        void program_ErrorOutput(object sender, Output_EventArgs e)
        {
            AppendText(e.Message, Color.Red);
        }

        void program_StandardOutput(object sender, Output_EventArgs e)
        {
            AppendText(e.Message, Settings.Default.PlainText);
        }

        #endregion Program Events

        #region Edit Code View

        private void CompileAndRun()
        {
            if (Compile())
            {
                this.LoadProgram();
                this.program.RunMain();
            }
        }

        private bool Compile()
        {
            if (this.dirtyFlag)
            {
                if (MessageBox.Show("Save file before compiling?", "Save File?", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.SaveCode();
                }
            }
            this.Status = "Compiling Code...";
            this.outputTextBox.Text = "";
            PsudoCompiler compiler = new PsudoCompiler(this.codeRichTextBox.Text);
            if (compiler.CheckSyntax())
            {
                if (compiler.Compile())
                {
                    this.program = compiler.program;
                    this.compileInfo = compiler;
                    this.PrintCompilerData(compiler);
                    this.Status = "Compile Successful";
                    return true;
                }
                else
                {
                    this.AppendText("Compile Failed\n\n", Color.Red);
                    this.Status = "Compile Failed";
                    foreach (CompileError error in compiler.Errors)
                    {
                        this.AppendText(
                            string.Format("Line {0}: {1}\n", error.Line, error.Details),
                            Color.Red);
                    }
                }
            }
            else
            {
                this.AppendText("Invalid Syntax\n\n", Color.Red);
                this.Status = "Invalid Syntax";
                foreach (CompileError error in compiler.scanner.errors)
                {
                    this.AppendText(
                        string.Format("Line {0}: {1}\n", error.Line, error.Details),
                        Color.Red);
                }
            }
            return false;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                this.CompileAndRun();
            }
            else
            {
                this.IsRunning = true;
            }
        }

        private void importSimpleSample_Click(object sender, EventArgs e)
        {
            this.codeRichTextBox.Text = Properties.Resources.SimpleCode;
        }

        private void importSample_Click(object sender, EventArgs e)
        {
            this.codeRichTextBox.Text = Properties.Resources.SampleCode;
        }

        private void PrintCompilerData(PsudoCompiler compiler)
        {
            this.outputTextBox.Text = "";

            foreach (var item in compiler.program.Classes)
            {
                this.AppendText(
                    string.Format("Class: {0}, Methods: {1}, Vars {2}", 
                        item.Value.Name, 
                        item.Value.Methods.Count, 
                        item.Value.ClassVariables.Count
                    ),
                    Color.White);
            }
        }

        #endregion Edit Code View

        #region Open/Save Input File

        private void OpenInputFile()
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.inputDataGridView.Columns.Clear();

                StreamReader reader = new StreamReader(this.openFileDialog.FileName);
                // first line contains headers
                string line = reader.ReadLine();
                foreach (string colName in line.Split('\t'))
                {
                    DataGridViewColumn col = new DataGridViewColumn(new DataGridViewTextBoxCell());
                    col.Name = colName;
                    this.inputDataGridView.Columns.Add(col);
                }
                // second line is a divider
                line = reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    string[] valString = line.Split('\t');
                    List<object> values = new List<object>();

                    foreach (string colName in valString)
                    {
                        values.Add(colName);
                    }

                    this.inputDataGridView.Rows.Add(values.ToArray());
                }
            }
        }

        private void SaveInputFile(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            string line = "";
            string seperator = "";
            foreach (DataGridViewColumn col in this.inputDataGridView.Columns)
            {
                line += col.Name + "\t";
                seperator += "---------------\t";
            }
            writer.WriteLine(line.Trim());
            writer.WriteLine(seperator.Trim());

            foreach (DataGridViewRow row in this.inputDataGridView.Rows)
            {
                line = "";
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                        line += "\t";
                    else
                        line += cell.Value.ToString() + "\t";
                }
                if (line.Trim().Length > 0)
                    writer.WriteLine(line.Trim());
            }

            writer.Close();
            this.inputFilenameLabel.Text = filename;
        }

        #endregion Input File

        #region Open/Save Events

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.viewsTabControl.SelectedTab == this.editCodePage)
            {
                if (!this.CheckForSave())
                    return;

                this.openFileDialog.FileName = "";
                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader reader = new StreamReader(this.openFileDialog.FileName);
                    this.codeRichTextBox.Text = reader.ReadToEnd();
                    ClearUndoRedo();
                    reader.Close();
                    this.codePathLabel.Text = this.openFileDialog.FileName;
                }
            }
            else if (this.viewsTabControl.SelectedTab == this.inputFilePage)
            {
                this.OpenInputFile();
            }
            else
            {
                MessageBox.Show("Not Supported In This View");
            }
        }

        private void SaveCode()
        {
            if (this.codePathLabel.Text.Contains('<'))
            {
                saveAsToolStripMenuItem_Click(null, new EventArgs());
            }
            else
            {
                StreamWriter writer = new StreamWriter(this.codePathLabel.Text);
                // Do this to make the code readable in Notepad
                writer.Write(this.codeRichTextBox.Text.Replace("\n", System.Environment.NewLine));
                writer.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.viewsTabControl.SelectedTab == this.editCodePage)
            {
                this.SaveCode();
            }
            else if (this.viewsTabControl.SelectedTab == this.inputFilePage)
            {
                if (this.inputFilenameLabel.Text.Contains('<'))
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    this.SaveInputFile(this.inputFilenameLabel.Text);
                }
            }
            else
            {
                MessageBox.Show("Not Supported In This View");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.viewsTabControl.SelectedTab == this.editCodePage)
            {
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(this.saveFileDialog.FileName);
                    writer.Write(this.codeRichTextBox.Text);
                    writer.Close();
                    this.codePathLabel.Text = this.saveFileDialog.FileName;
                }
            }
            else if (this.viewsTabControl.SelectedTab == this.inputFilePage)
            {
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.SaveInputFile(this.saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("Not Supported In This View");
            }
        }

        #endregion Open/Save Events

        #region Simple Events

        private void abortButton_Click(object sender, EventArgs e)
        {
            this.program.Abort();
            this.IsRunning = false;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            this.IsPaused = true;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                this.CompileAndRun();
                this.IsPaused = true;
            }
            else
            {
                this.doStep = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        #endregion Simple Events

        #region Input File Grid View Events

        private void addColumnButton_Click(object sender, EventArgs e)
        {
            StringInputDialog inp = new StringInputDialog();
            if (inp.ShowDialog() == DialogResult.OK)
            {
                DataGridViewColumn col = new DataGridViewColumn(new DataGridViewTextBoxCell());
                col.Name = inp.SelectedString;
                this.inputDataGridView.Columns.Add(col);
            }
        }

        private void inputDataGridView_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.inputDataGridView.Columns[e.ColumnIndex];
            StringInputDialog inp = new StringInputDialog();
            inp.SelectedString = col.Name;
            if (inp.ShowDialog() == DialogResult.OK)
            {
                col.Name = inp.SelectedString;
            }
        }

        private void deleteColumnButton_Click(object sender, EventArgs e)
        {
            if (this.inputDataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("No Columns Selected");
                return;
            }

            string cols = "";
            List<int> colsUsed = new List<int>();
            List<DataGridViewColumn> selectedCols = new List<DataGridViewColumn>();
            foreach (DataGridViewCell cell in this.inputDataGridView.SelectedCells)
            {
                if (!colsUsed.Contains(cell.ColumnIndex))
                {
                    colsUsed.Add(cell.ColumnIndex);
                    cols += this.inputDataGridView.Columns[cell.ColumnIndex].Name + "\n";
                    selectedCols.Add(this.inputDataGridView.Columns[cell.ColumnIndex]);
                }
            }
            cols = cols.Trim();

            string message = "Delete the following columns:\n\n" + cols;
            if (MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewColumn col in selectedCols)
                {
                    this.inputDataGridView.Columns.Remove(col);
                }
            }
        }
        
        #endregion Input File Grid View Events

        delegate string PromptDelegate(string inputString);

        public string PromptForUserInput(string inputString)
        {
            if (this.InvokeRequired)
            {
                return this.Invoke(new PromptDelegate(this.PromptForUserInput), inputString) as string;
            }
            else
            {
                StringInputDialog dialog = new StringInputDialog();

                dialog.SelectedString = "";
                dialog.PromptString = inputString;

                if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                {
                    throw new Exception("Operation Aborted by User");
                }

                return dialog.SelectedString;
            }
        }

        private void PsudoRuntimeViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.program != null)
                this.program.Abort();
            this.IsPaused = false;
        }

        private void loadExample(object sender, EventArgs e)
        {
            this.codeRichTextBox.Text = "";
            if (sender == this.helloWorldToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.HelloWorld;
            }
            else if (sender == this.userInputToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.SimpleInput;
            }
            else if (sender == this.mathToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.SimpleMath;
            }

            //Decisions
            else if (sender == this.ifthenelseToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.Decisions;
            }
            else if (sender == this.caseStatementsToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.Case;
            }
            else if (sender == this.combiningLogicToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.CombiningLogic;
            }
            else if (sender == this.nestedDecisionsToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.NestedDecisions;
            }

            // Looping
            else if (sender == this.whileLoopsToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.Looping;
            }
            else if (sender == this.repeatLoopsToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.RepeatLoops;
            }
            else if (sender == this.countedLoopsToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.CountingLoops;
            }

            // Arrays
            else if (sender == this.arraysToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.Arrays;
            }
            else if (sender == this.twoDimensionalArraysToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.TwoDimArrays;
            }
            // Data Files
            else if (sender == this.dataFilesToolStripMenuItem)
            {
                this.codeRichTextBox.Text = Properties.Resources.DataFileExample;

                // Cleanup
                this.inputDataGridView.Rows.Clear();
                this.inputDataGridView.Columns.Clear();

                this.inputDataGridView.Columns.Add("studentID", "studentID");
                this.inputDataGridView.Columns.Add("studentName", "studentName");
                this.inputDataGridView.Columns.Add("studentGPA", "studentGPA");

                this.inputDataGridView.Rows.Add(12345, "Jim Davis", 3.2);
                this.inputDataGridView.Rows.Add(23456, "Sara Adams", 4.0);
                this.inputDataGridView.Rows.Add(34567, "Bill Madison", 1.3);
                this.inputDataGridView.Rows.Add(45678, "Amy Grant", 2.6);

                this.viewsTabControl.SelectedTab = this.inputFilePage;
                MessageBox.Show(this, "Notice that test data has been added to the input data table",
                    "Test Data Inported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.viewsTabControl.SelectedTab = this.editCodePage;
            }
            

            this.dirtyFlag = false;
            this.codePathLabel.Text = "<No File Selected>";
        }

        bool processing = false;
        private void codeRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (processing)
                return;

            int start = this.codeRichTextBox.SelectionStart + 1;
            char[] text = ("\n" + this.codeRichTextBox.Text).ToCharArray();

            int line = 0;
            int lastReturn = 0;
            for (int i = 0; i < start; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    lastReturn = i;
                }
            }

            this.lineLabel.Text = "Ln " + line.ToString();
            this.columnLabel.Text = "Col " + (start - lastReturn).ToString();
        }

        private string Status
        {
            get { return this.statusLabel.Text; }
            set { this.statusLabel.Text = value; }
        }

        private void PsudoRuntimeViewer_Load(object sender, EventArgs e)
        {
            this.Text += " - Version: " + AssemblyVersion;
            this.SetupColors();
            this.numericUpDown1.Value = Settings.Default.DefaultDelay;
            this.dirtyFlag = false;
            this.CreateNewFile();
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog dlg = new OptionsDialog();
            dlg.ShowDialog(this);

            this.SetupColors();
        }

        private void SetupColors()
        {
            this.codeRichTextBox.BackColor = Settings.Default.Background;
            this.codeRichTextBox.Comments = Settings.Default.Comments;
            this.codeRichTextBox.PlainText = Settings.Default.PlainText;
            this.codeRichTextBox.Keywords = Settings.Default.Keywords;
            this.codeRichTextBox.Commands = Settings.Default.Commands;
            this.codeRichTextBox.Font = Settings.Default.Font;

            this.codeLinesList.BackColor = Settings.Default.Background;
            this.codeLinesList.ForeColor = Settings.Default.PlainText;
            this.codeLinesList.Font = Settings.Default.Font;


            this.outputTextBox.BackColor = Settings.Default.Background;
            this.outputTextBox.ForeColor = Settings.Default.PlainText;
            this.outputTextBox.Font = Settings.Default.Font;

            this.codeRichTextBox.RefreshColors();
            //this.codeRichTextBox.DeselectAll();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultDelay = this.numericUpDown1.Value;
            Settings.Default.Save();
        }

        private void cutCopyPaste_Clicked(object sender, EventArgs e)
        {
            if (sender == this.cutButton || sender == this.cutToolStripMenuItem)
            {
                this.codeRichTextBox.Cut();
            }

            else if (sender == this.pasteButton || sender == this.pasteToolStripMenuItem)
            {
                this.codeRichTextBox.Paste();
            }

            else if (sender == this.copyButton || sender == this.copyToolStripMenuItem)
            {
                this.codeRichTextBox.Copy();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateNewFile();
        }

        private void CreateNewFile()
        {
            // If this returns false that means cancel
            if (!this.CheckForSave())
                return;

            this.codeRichTextBox.Text = "Main\n     \n     \n     \nEnd";
            this.codeRichTextBox.SelectionStart = 16;
            this.dirtyFlag = false;
            this.codePathLabel.Text = "<No File Selected>";

            this.ClearUndoRedo();
        }

        private void ClearUndoRedo()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
            UpdateUndoRedoState();
        }

        private void UpdateUndoRedoState()
        {
            this.undoToolStripMenuItem.Enabled = this.undoButton.Enabled = this.undoStack.Count > 0;
            this.redoToolStripMenuItem.Enabled = this.redoButton.Enabled = this.redoStack.Count > 0;
        }

        private bool CheckForSave()
        {
            if (this.dirtyFlag)
            {
                DialogResult res = MessageBox.Show(this, 
                    "There are unsaved changes. Do you wish to save?", 
                    "Save Changes?", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    this.SaveCode();
                }
                else if (res == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        bool realDirtyFlag = false;

        bool dirtyFlag
        {
            get
            {
                return realDirtyFlag;
            }
            set
            {
                realDirtyFlag = value;
                if (realDirtyFlag)
                    this.editCodePage.Text = "Edit Code *";
                else
                    this.editCodePage.Text = "Edit Code";
            }
        }

        private void codeRichTextBox_TextChanged(object sender, EventArgs e)
        {
            this.dirtyFlag = true;

            if (!undoing)
            {
                this.undoStack.Push(this.codeRichTextBox.Text);
                UpdateUndoRedoState();
            }
        }

        bool undoing = false;
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                this.undoing = true;
                this.redoStack.Push(this.codeRichTextBox.Text);
                this.codeRichTextBox.Text = this.undoStack.Pop();
                this.undoing = false;
            }

            UpdateUndoRedoState();
        }

        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                this.undoing = true;
                this.undoStack.Push(this.codeRichTextBox.Text);
                this.codeRichTextBox.Text = this.redoStack.Pop();
                this.undoing = false;
            }

            UpdateUndoRedoState();
        }

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();

        private void undoButton_Click(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            this.Redo();
        }
    }
}
