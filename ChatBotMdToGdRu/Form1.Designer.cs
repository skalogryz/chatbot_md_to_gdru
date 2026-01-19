namespace ChatBotMdToGdRu
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.textBoxGdru = new System.Windows.Forms.TextBox();
            this.textBoxMarkdown = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoCopy = new System.Windows.Forms.CheckBox();
            this.containerGamedevru = new System.Windows.Forms.SplitContainer();
            this.containerMarkdown = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).BeginInit();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerGamedevru)).BeginInit();
            this.containerGamedevru.Panel1.SuspendLayout();
            this.containerGamedevru.Panel2.SuspendLayout();
            this.containerGamedevru.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerMarkdown)).BeginInit();
            this.containerMarkdown.Panel1.SuspendLayout();
            this.containerMarkdown.Panel2.SuspendLayout();
            this.containerMarkdown.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chkAutoCopy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 81);
            this.panel1.TabIndex = 0;
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 81);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.containerMarkdown);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.containerGamedevru);
            this.containerMain.Size = new System.Drawing.Size(452, 234);
            this.containerMain.SplitterDistance = 150;
            this.containerMain.TabIndex = 1;
            // 
            // textBoxGdru
            // 
            this.textBoxGdru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGdru.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGdru.Location = new System.Drawing.Point(0, 0);
            this.textBoxGdru.Multiline = true;
            this.textBoxGdru.Name = "textBoxGdru";
            this.textBoxGdru.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxGdru.Size = new System.Drawing.Size(298, 205);
            this.textBoxGdru.TabIndex = 0;
            this.textBoxGdru.WordWrap = false;
            // 
            // textBoxMarkdown
            // 
            this.textBoxMarkdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMarkdown.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMarkdown.Location = new System.Drawing.Point(0, 0);
            this.textBoxMarkdown.Multiline = true;
            this.textBoxMarkdown.Name = "textBoxMarkdown";
            this.textBoxMarkdown.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMarkdown.Size = new System.Drawing.Size(150, 208);
            this.textBoxMarkdown.TabIndex = 2;
            this.textBoxMarkdown.WordWrap = false;
            this.textBoxMarkdown.TextChanged += new System.EventHandler(this.textBoxMarkdown_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gamedev.ru BB-markup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Markdown";
            // 
            // chkAutoCopy
            // 
            this.chkAutoCopy.AutoSize = true;
            this.chkAutoCopy.Location = new System.Drawing.Point(12, 12);
            this.chkAutoCopy.Name = "chkAutoCopy";
            this.chkAutoCopy.Size = new System.Drawing.Size(238, 17);
            this.chkAutoCopy.TabIndex = 0;
            this.chkAutoCopy.Text = "Копировать в буффер после конвертации";
            this.chkAutoCopy.UseVisualStyleBackColor = true;
            // 
            // containerGamedevru
            // 
            this.containerGamedevru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerGamedevru.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.containerGamedevru.IsSplitterFixed = true;
            this.containerGamedevru.Location = new System.Drawing.Point(0, 0);
            this.containerGamedevru.Name = "containerGamedevru";
            this.containerGamedevru.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerGamedevru.Panel1
            // 
            this.containerGamedevru.Panel1.Controls.Add(this.label1);
            // 
            // containerGamedevru.Panel2
            // 
            this.containerGamedevru.Panel2.Controls.Add(this.textBoxGdru);
            this.containerGamedevru.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.containerGamedevru.Size = new System.Drawing.Size(298, 234);
            this.containerGamedevru.SplitterDistance = 25;
            this.containerGamedevru.TabIndex = 2;
            // 
            // containerMarkdown
            // 
            this.containerMarkdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMarkdown.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.containerMarkdown.IsSplitterFixed = true;
            this.containerMarkdown.Location = new System.Drawing.Point(0, 0);
            this.containerMarkdown.Name = "containerMarkdown";
            this.containerMarkdown.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerMarkdown.Panel1
            // 
            this.containerMarkdown.Panel1.Controls.Add(this.label2);
            // 
            // containerMarkdown.Panel2
            // 
            this.containerMarkdown.Panel2.Controls.Add(this.textBoxMarkdown);
            this.containerMarkdown.Size = new System.Drawing.Size(150, 234);
            this.containerMarkdown.SplitterDistance = 25;
            this.containerMarkdown.SplitterWidth = 1;
            this.containerMarkdown.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(330, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Конвертировать";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(330, 52);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(110, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Копировать";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 315);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Markdown в gd.ru";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).EndInit();
            this.containerMain.ResumeLayout(false);
            this.containerGamedevru.Panel1.ResumeLayout(false);
            this.containerGamedevru.Panel1.PerformLayout();
            this.containerGamedevru.Panel2.ResumeLayout(false);
            this.containerGamedevru.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerGamedevru)).EndInit();
            this.containerGamedevru.ResumeLayout(false);
            this.containerMarkdown.Panel1.ResumeLayout(false);
            this.containerMarkdown.Panel1.PerformLayout();
            this.containerMarkdown.Panel2.ResumeLayout(false);
            this.containerMarkdown.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerMarkdown)).EndInit();
            this.containerMarkdown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.TextBox textBoxMarkdown;
        private System.Windows.Forms.TextBox textBoxGdru;
        private System.Windows.Forms.CheckBox chkAutoCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer containerGamedevru;
        private System.Windows.Forms.SplitContainer containerMarkdown;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCopy;
    }
}

