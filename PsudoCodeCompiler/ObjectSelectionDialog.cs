using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using PseudoCodeCompiler.Runtime;

namespace PseudoCodeCompiler
{
    public partial class ObjectSelectionDialog : Form
    {
        public ObjectSelectionDialog()
        {
            InitializeComponent();
        }

        public ObjectSelectionDialog(PsudoMethod[] list)
        {
            InitializeComponent();

            this.comboBox1.Items.AddRange(list);
            this.comboBox1.SelectedIndex = 0;
        }

        public PsudoMethod SelectedObject
        {
            get
            {
                return this.comboBox1.SelectedItem as PsudoMethod;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
