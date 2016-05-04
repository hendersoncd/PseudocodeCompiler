using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PseudoCodeCompiler
{
    public partial class StringInputDialog : Form
    {
        #region Constructors

        public StringInputDialog(string prompt, string initialValue)
            : this(prompt)
        {
            this.SelectedString = initialValue;
        }

        public StringInputDialog(string prompt) : this()
        {
            this.PromptString = prompt;
        }

        public StringInputDialog()
        {
            InitializeComponent();
        }

        #endregion Constructors

        public string SelectedString
        {
            get
            {
                return this.inputTextBox.Text;
            }
            set
            {
                this.inputTextBox.Text = value;
            }
        }

        public string PromptString
        {
            get
            {
                return this.promptLabel.Text;
            }
            set
            {
                this.promptLabel.Text = value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.inputTextBox.Focus();
        }

        private void StringInputDialog_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }
    }
}
