namespace PseudoCodeCompiler
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.backgroundButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.plainTextButton = new System.Windows.Forms.Button();
            this.commentsButton = new System.Windows.Forms.Button();
            this.keywordsButton = new System.Windows.Forms.Button();
            this.commandsButton = new System.Windows.Forms.Button();
            this.fontButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.exampleBox = new PseudoCodeCompiler.SyntaxHighlightingTextbox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundButton
            // 
            this.backgroundButton.Location = new System.Drawing.Point(3, 3);
            this.backgroundButton.Name = "backgroundButton";
            this.backgroundButton.Size = new System.Drawing.Size(115, 23);
            this.backgroundButton.TabIndex = 0;
            this.backgroundButton.Text = "Background";
            this.backgroundButton.UseVisualStyleBackColor = true;
            this.backgroundButton.Click += new System.EventHandler(this.editColorClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.exampleBox);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 214);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor Colors";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.backgroundButton);
            this.flowLayoutPanel1.Controls.Add(this.plainTextButton);
            this.flowLayoutPanel1.Controls.Add(this.commentsButton);
            this.flowLayoutPanel1.Controls.Add(this.keywordsButton);
            this.flowLayoutPanel1.Controls.Add(this.commandsButton);
            this.flowLayoutPanel1.Controls.Add(this.fontButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(123, 195);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // plainTextButton
            // 
            this.plainTextButton.Location = new System.Drawing.Point(3, 32);
            this.plainTextButton.Name = "plainTextButton";
            this.plainTextButton.Size = new System.Drawing.Size(115, 23);
            this.plainTextButton.TabIndex = 1;
            this.plainTextButton.Text = "Plain Text";
            this.plainTextButton.UseVisualStyleBackColor = true;
            this.plainTextButton.Click += new System.EventHandler(this.editColorClick);
            // 
            // commentsButton
            // 
            this.commentsButton.Location = new System.Drawing.Point(3, 61);
            this.commentsButton.Name = "commentsButton";
            this.commentsButton.Size = new System.Drawing.Size(115, 23);
            this.commentsButton.TabIndex = 2;
            this.commentsButton.Text = "Comments";
            this.commentsButton.UseVisualStyleBackColor = true;
            this.commentsButton.Click += new System.EventHandler(this.editColorClick);
            // 
            // keywordsButton
            // 
            this.keywordsButton.Location = new System.Drawing.Point(3, 90);
            this.keywordsButton.Name = "keywordsButton";
            this.keywordsButton.Size = new System.Drawing.Size(115, 23);
            this.keywordsButton.TabIndex = 3;
            this.keywordsButton.Text = "Keywords";
            this.keywordsButton.UseVisualStyleBackColor = true;
            this.keywordsButton.Click += new System.EventHandler(this.editColorClick);
            // 
            // commandsButton
            // 
            this.commandsButton.Location = new System.Drawing.Point(3, 119);
            this.commandsButton.Name = "commandsButton";
            this.commandsButton.Size = new System.Drawing.Size(115, 23);
            this.commandsButton.TabIndex = 4;
            this.commandsButton.Text = "Commands";
            this.commandsButton.UseVisualStyleBackColor = true;
            this.commandsButton.Click += new System.EventHandler(this.editColorClick);
            // 
            // fontButton
            // 
            this.fontButton.Location = new System.Drawing.Point(3, 148);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(115, 23);
            this.fontButton.TabIndex = 5;
            this.fontButton.Text = "Font";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Click += new System.EventHandler(this.fontButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(386, 255);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(467, 255);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Cancel";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Preset Options:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(287, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(255, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // exampleBox
            // 
            this.exampleBox.Commands = System.Drawing.Color.Yellow;
            this.exampleBox.Comments = System.Drawing.Color.Lime;
            this.exampleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exampleBox.Keywords = System.Drawing.Color.Blue;
            this.exampleBox.Location = new System.Drawing.Point(126, 16);
            this.exampleBox.Name = "exampleBox";
            this.exampleBox.PlainText = System.Drawing.Color.Black;
            this.exampleBox.Size = new System.Drawing.Size(404, 195);
            this.exampleBox.TabIndex = 1;
            this.exampleBox.Text = "";
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 290);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsDialog";
            this.Text = "Editor Options";
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button backgroundButton;
        private SyntaxHighlightingTextbox exampleBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button plainTextButton;
        private System.Windows.Forms.Button commentsButton;
        private System.Windows.Forms.Button keywordsButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button commandsButton;
        private System.Windows.Forms.Button fontButton;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}