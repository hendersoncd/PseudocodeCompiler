using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler
{
    public partial class SyntaxHighlightingTextbox : RichTextBox
    {
        bool processing = false;
        int lastLength = 0;
        string[] keywords = { 
                                "main", "end", "case", "of", "endcase", "other", 
                                "repeat", "until", "do", "to", "for", "from",
                                "if", "then", "else", "endif",  "elseif",
                                "while", "dowhile", "enddo", "endwhile", 
                                "and", "not", "or", "eof"
                            };
        string[] commands = { 
                                "print", "display", "put", "output", "write", 
                                "prompt", "get", "input", "read", 
                                "add", "subtract", "multiply", "divide", 
                                "set", "initialize"
                            };

        public Color Comments { get; set; }
        public Color PlainText { get; set; }
        public Color Keywords { get; set; }
        public Color Commands { get; set; }

        public SyntaxHighlightingTextbox()
        {
            Comments = Color.Lime;
            PlainText = Color.Black;
            Keywords = Color.Blue;
            Commands = Color.Yellow;
            //this.AcceptsTab = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (processing)
                return;

            processing = true;
            this.SuspendLayout();
            this.Enabled = false;

            // backup selection
            int startSelection = this.SelectionStart;
            int startLength = this.SelectionLength;

            if (Math.Abs(this.Text.Length - this.lastLength) > 1)
            {
                this.SanitizeText();
                this.RefreshColors();
            }
            else
            {
                this.ParseCurrentLine();
            }

            this.lastLength = this.Text.Length;

            // reset selection
            this.SelectionStart = startSelection;
            this.SelectionLength = startLength;

            this.ResumeLayout();
            processing = false;
            this.Enabled = true;
            this.Focus();

            base.OnTextChanged(e);
        }

        private void SanitizeText()
        {
            string tempText = this.Text;
            bool update = false;
            if (tempText.Contains("“") || tempText.Contains("”"))
            {
                update = true;
                tempText = tempText.Replace('“', '"').Replace('”', '"');
            }

            if (update)
                this.Text = tempText;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Ignore the Insert key
            if (e.KeyCode == Keys.Insert)
                e.Handled = true;
            if (e.KeyCode == Keys.Enter)
            {
                int tabs = GetNextLineTabs() + 1;
                string tabstring = "\n".PadRight(tabs, ' ');
                int tempStart = this.SelectionStart;

                this.processing = true;
                string tempText = this.Text;
                if (this.SelectionLength > 0)
                    tempText = this.Text.Remove(this.SelectionStart, this.SelectionLength);
                this.Text = tempText.Insert(this.SelectionStart, tabstring);
                this.SelectionStart = tempStart + tabs;
                this.processing = false;

                this.OnTextChanged(new EventArgs());

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (this.SelectionLength == 0)
                {
                    string allPrev = this.Text.Substring(0, this.SelectionStart);
                    int lineStartIndex = allPrev.LastIndexOf('\n') + 1;
                    string lineStart = this.Text.Substring(lineStartIndex, this.SelectionStart - lineStartIndex);

                    if (lineStart.Length > 0 && lineStart.Trim() == "") // Do we only have whitespace?
                    {
                        if (lineStart.Length > SPACES_PER_TAB)
                        {
                            int newStart = this.SelectionStart - SPACES_PER_TAB;
                            this.Text = this.Text.Remove(newStart, SPACES_PER_TAB);
                            this.SelectionStart = newStart;
                        }
                        else
                        {
                            this.Text = this.Text.Remove(lineStartIndex, lineStart.Length);
                            this.SelectionStart = lineStartIndex;
                        }
                        //this.OnTextChanged(new EventArgs());
                        e.Handled = true;
                    }
                }
            }// end backspace

            base.OnKeyDown(e);
        }

        private const int SPACES_PER_TAB = 5;
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\t')
            {
                string tabstring = "".PadRight(SPACES_PER_TAB, ' ');
                int tempStart = this.SelectionStart;
                int tempLength = this.SelectionLength;
                string tempText = this.Text;

                if (this.SelectionLength == 0)
                {
                    tempText = tempText.Insert(this.SelectionStart, tabstring);
                    tempStart += SPACES_PER_TAB - 1;
                }
                else
                {
                    string allPrev = this.Text.Substring(0, tempStart);
                    int lineStartIndex = allPrev.LastIndexOf('\n');
                    if (lineStartIndex < 0)
                        lineStartIndex = 0;

                    tempLength += tempStart - lineStartIndex;
                    if (tempLength >= this.Text.Length)
                        tempLength = this.Text.Length;
                    tempStart = lineStartIndex;

                    string selection = tempText.Substring(tempStart, tempLength);
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        string old = selection;
                        selection = Regex.Replace(selection, "\n" + tabstring, "\n", RegexOptions.Compiled);
                        tempText = tempText.Remove(tempStart, tempLength);
                        tempText = tempText.Insert(tempStart, selection);
                    }
                    else
                    {
                        string old = selection;
                        selection = Regex.Replace(selection, "\n", "\n" + tabstring, RegexOptions.Compiled);
                        tempText = tempText.Remove(tempStart, tempLength);
                        tempText = tempText.Insert(tempStart, selection);
                        int shift = GetShiftDistance(old, selection);
                        //tempStart += shift;
                        tempLength -= shift;
                    }
                    tempLength = selection.Length - 1;
                }

                this.Text = tempText;
                this.SelectionStart = tempStart + 1;
                this.SelectionLength = tempLength;

                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private int GetShiftDistance(string first, string second)
        {
            string firstLine = first.Substring(1);
            if(firstLine.Contains('\n'))
                firstLine = firstLine.Substring(0, firstLine.IndexOf('\n'));

            string secondLine = second.Substring(1);
            if (secondLine.Contains('\n'))
                secondLine = secondLine.Substring(0, secondLine.IndexOf('\n'));

            int result = firstLine.IndexOf(secondLine);
            if (result < 0)
                result = secondLine.IndexOf(firstLine);

            return result;
        }

        private int GetNextLineTabs()
        {
            int lineStart = this.SelectionStart - 1;
            if (lineStart == this.Text.Length)
                lineStart--;

            while (lineStart >= 0 && this.Text[lineStart] != '\n')
            {
                lineStart --;
            }

            int num = 0;
            for (int i = lineStart + 1; i < this.Text.Length && this.Text[i] == ' '; i++)
                num ++;

            return num;
        }

        private void ParseLine(string line, int start)
        {
            // Split the line into tokens.
            Regex r = new Regex("([ \\t{}();])");
            string[] tokens = r.Split(line);
            int index = start;

            // Set the default color and font.
            this.SelectionStart = start;
            this.SelectionLength = line.Length;
            this.SelectionColor = PlainText;
            //this.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);

            foreach (string token in tokens)
            {
                this.SelectionStart = index;
                this.SelectionLength = token.Length;

                // Check for a comment.
                if (token == "#" || token.StartsWith("#"))
                {
                    // Find the start of the comment and then extract the whole comment.
                    int length = line.Length - (index - start);
                    string commentText = this.Text.Substring(index, length);
                    this.SelectionStart = index;
                    this.SelectionLength = length;
                    this.SelectionColor = Comments;
                    //this.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);
                    break;
                }

                // Check whether the token is a keyword. 
                for (int i = 0; i < keywords.Length; i++)
                {
                    if (keywords[i] == token.ToLower())
                    {     
                        this.SelectionColor = Keywords;
                        break;
                    }
                }

                // Check whether the token is a keyword. 
                for (int i = 0; i < commands.Length; i++)
                {
                    if (commands[i] == token.ToLower())
                    {    
                        this.SelectionColor = Commands;
                        break;
                    }
                }
                index += token.Length;
            }
        }

        private void ParseCurrentLine()
        {
            // Calculate the starting position of the current line.
            int start = 0, end = 0;
            for (start = this.SelectionStart - 1; start > 0; start--)
            {
                if (this.Text[start] == '\n') { start++; break; }
            }

            if (start == this.SelectionStart)
            {
                this.SelectionStart--;
                this.ParseCurrentLine();
                this.SelectionStart++;
            }

            // Calculate the end position of the current line.
            for (end = start + 1; end < this.Text.Length; end++)
            {
                if (this.Text[end] == '\n') break;
            }

            if (start < 0)
                return;

            if (start >= this.Text.Length)
                return;

            // Extract the current line that is being edited.
            string line = this.Text.Substring(start, end - start);

            this.ParseLine(line, start);
        }

        public void RefreshColors()
        {
            string[] lines = this.Text.Split('\n');
            int index = 0;
            foreach (string line in lines)
            {
                this.ParseLine(line, index);
                index += line.Length + 1;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.processing)
                return;

            base.OnPaint(e);
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            if (this.processing)
                return;

            base.OnSelectionChanged(e);
        }
    }
}
