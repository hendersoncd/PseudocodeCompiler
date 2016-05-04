using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PseudoCodeCompiler.Properties;

namespace PseudoCodeCompiler
{
    public partial class OptionsDialog : Form
    {
        internal class OptionSet
        {
            public string Name { get; set; }
            public Color Background { get; set; }
            public Color Comments { get; set; }
            public Color PlainText { get; set; }
            public Color Keywords { get; set; }
            public Color Commands { get; set; }
            public Font Font { get; set; }

            public override string ToString()
            {
                return Name;
            }

            public void ApplyOptions(SyntaxHighlightingTextbox exampleBox)
            {
                exampleBox.BackColor = this.Background;
                exampleBox.Comments = this.Comments;
                exampleBox.PlainText = this.PlainText;
                exampleBox.Keywords = this.Keywords;
                exampleBox.Commands = this.Commands;
                exampleBox.Font = this.Font;

                exampleBox.RefreshColors();
                exampleBox.DeselectAll();
            }

            public bool IsApplied(SyntaxHighlightingTextbox exampleBox)
            {
                return (exampleBox.BackColor == this.Background &&
                exampleBox.Comments == this.Comments &&
                exampleBox.PlainText == this.PlainText &&
                exampleBox.Keywords == this.Keywords &&
                exampleBox.Commands == this.Commands &&
                exampleBox.Font.Name == this.Font.Name &&
                exampleBox.Font.Size == this.Font.Size &&
                exampleBox.Font.Italic == this.Font.Italic &&
                exampleBox.Font.Bold == this.Font.Bold);
            }
        }

        private List<OptionSet> GetAvialableOptions()
        {
            List<OptionSet> list = new List<OptionSet>();
            list.Add(new OptionSet()
            {
                Name = "Normal",
                Background = Color.White,
                PlainText = Color.Black,
                Keywords = Color.Blue,
                Commands = Color.Red,
                Font = new Font(FontFamily.GenericMonospace, 10),
                Comments = Color.Green
            });

            list.Add(new OptionSet() { 
                Name = "Inverse Contrast",
                Background = Color.Black,
                PlainText=Color.White,
                Keywords = Color.RoyalBlue,
                Commands = Color.Yellow,
                Font = new Font(FontFamily.GenericMonospace, 10),
                Comments = Color.Chartreuse
            });

            list.Add(new OptionSet()
            {
                Name = "Normal BIG",
                Background = Color.White,
                PlainText = Color.Black,
                Keywords = Color.Blue,
                Commands = Color.Red,
                Font = new Font(FontFamily.GenericMonospace, 14),
                Comments = Color.Green
            });

            list.Add(new OptionSet()
            {
                Name = "Inverse Contrast BIG",
                Background = Color.Black,
                PlainText = Color.White,
                Keywords = Color.RoyalBlue,
                Commands = Color.Yellow,
                Font = new Font(FontFamily.GenericMonospace, 14),
                Comments = Color.Chartreuse
            });

            list.Add(new OptionSet()
            {
                Name = "Current",
                Background = Settings.Default.Background,
                PlainText = Settings.Default.PlainText,
                Keywords = Settings.Default.Keywords,
                Commands = Settings.Default.Commands,
                Font = Settings.Default.Font,
                Comments = Settings.Default.Comments
            });

            return list;
        }

        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void editColorClick(object sender, EventArgs e)
        {
            Color initColor = Color.White;

            if (sender == this.backgroundButton)
            {
                initColor = this.exampleBox.BackColor;
            }
            else if (sender == this.commentsButton)
            {
                initColor = this.exampleBox.Comments;
            }
            else if (sender == this.plainTextButton)
            {
                initColor = this.exampleBox.PlainText;
            }
            else if (sender == this.keywordsButton)
            {
                initColor = this.exampleBox.Keywords;
            }
            else if (sender == this.commandsButton)
            {
                initColor = this.exampleBox.Commands;
            }

            this.colorDialog.Color = initColor;
            if (this.colorDialog.ShowDialog() != DialogResult.OK)
                return;

            if (sender == this.backgroundButton)
            {
                this.exampleBox.BackColor = this.colorDialog.Color;
            }
            else if (sender == this.commentsButton)
            {
                this.exampleBox.Comments = this.colorDialog.Color;
            }
            else if (sender == this.plainTextButton)
            {
                this.exampleBox.PlainText = this.colorDialog.Color;
            }
            else if (sender == this.keywordsButton)
            {
                this.exampleBox.Keywords = this.colorDialog.Color;
            }
            else if (sender == this.commandsButton)
            {
                this.exampleBox.Commands = this.colorDialog.Color;
            }

            this.exampleBox.RefreshColors();
            this.exampleBox.DeselectAll();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.Default.Background = this.exampleBox.BackColor;
            Settings.Default.Comments = this.exampleBox.Comments;
            Settings.Default.PlainText = this.exampleBox.PlainText;
            Settings.Default.Keywords = this.exampleBox.Keywords;
            Settings.Default.Commands = this.exampleBox.Commands;
            Settings.Default.Font = this.exampleBox.Font;

            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            List<OptionSet> set = this.GetAvialableOptions();
            this.comboBox1.DataSource = set;

            this.exampleBox.BackColor = Settings.Default.Background;
            this.exampleBox.Comments = Settings.Default.Comments;
            this.exampleBox.PlainText = Settings.Default.PlainText;
            this.exampleBox.Keywords = Settings.Default.Keywords;
            this.exampleBox.Commands = Settings.Default.Commands;
            this.exampleBox.Font = Settings.Default.Font;

            this.exampleBox.Text = Resources.SampleCode;

            foreach (OptionSet item in set)
            {
                if (item.IsApplied(this.exampleBox))
                {
                    this.comboBox1.SelectedItem = item;
                    break;
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = this.exampleBox.Font;
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
                this.exampleBox.Font = this.fontDialog.Font;
                this.exampleBox.RefreshColors();
                this.exampleBox.DeselectAll();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((OptionSet)this.comboBox1.SelectedItem).ApplyOptions(this.exampleBox);
        }
    }
}
