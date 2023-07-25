namespace ICP_Algorithm.WFA
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbX2 = new System.Windows.Forms.Label();
            this.lbY2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbAngle2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnNoisySample = new System.Windows.Forms.Button();
            this.btnShowFinal = new System.Windows.Forms.Button();
            this.btn_normal_sample = new System.Windows.Forms.Button();
            this.grpBoxTransformationTopLeft = new System.Windows.Forms.GroupBox();
            this.grpBoxInitialTopLeft = new System.Windows.Forms.GroupBox();
            this.lbY = new System.Windows.Forms.Label();
            this.Txt_InitPosY = new System.Windows.Forms.TextBox();
            this.lbX = new System.Windows.Forms.Label();
            this.Txt_InitPosX = new System.Windows.Forms.TextBox();
            this.lbAngle = new System.Windows.Forms.Label();
            this.Txt_InitAngle = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubWebPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aTutorialOnRigidRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpBoxTransformationTopLeft.SuspendLayout();
            this.grpBoxInitialTopLeft.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(745, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 52);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "50";
            // 
            // lbX2
            // 
            this.lbX2.AutoSize = true;
            this.lbX2.Location = new System.Drawing.Point(8, 54);
            this.lbX2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbX2.Name = "lbX2";
            this.lbX2.Size = new System.Drawing.Size(14, 13);
            this.lbX2.TabIndex = 7;
            this.lbX2.Text = "X";
            // 
            // lbY2
            // 
            this.lbY2.AutoSize = true;
            this.lbY2.Location = new System.Drawing.Point(8, 77);
            this.lbY2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbY2.Name = "lbY2";
            this.lbY2.Size = new System.Drawing.Size(14, 13);
            this.lbY2.TabIndex = 9;
            this.lbY2.Text = "Y";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(51, 75);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(72, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "50";
            // 
            // lbAngle2
            // 
            this.lbAngle2.AutoSize = true;
            this.lbAngle2.Location = new System.Drawing.Point(8, 102);
            this.lbAngle2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAngle2.Name = "lbAngle2";
            this.lbAngle2.Size = new System.Drawing.Size(34, 13);
            this.lbAngle2.TabIndex = 11;
            this.lbAngle2.Text = "Angle";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(51, 100);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(72, 20);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "0";
            // 
            // btnNoisySample
            // 
            this.btnNoisySample.Location = new System.Drawing.Point(749, 172);
            this.btnNoisySample.Margin = new System.Windows.Forms.Padding(2);
            this.btnNoisySample.Name = "btnNoisySample";
            this.btnNoisySample.Size = new System.Drawing.Size(89, 20);
            this.btnNoisySample.TabIndex = 12;
            this.btnNoisySample.Text = "Noisy Sample";
            this.btnNoisySample.UseVisualStyleBackColor = true;
            this.btnNoisySample.Click += new System.EventHandler(this.btnNoisySample_Click);
            // 
            // btnShowFinal
            // 
            this.btnShowFinal.Location = new System.Drawing.Point(749, 220);
            this.btnShowFinal.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowFinal.Name = "btnShowFinal";
            this.btnShowFinal.Size = new System.Drawing.Size(89, 20);
            this.btnShowFinal.TabIndex = 13;
            this.btnShowFinal.Text = "Show Final";
            this.btnShowFinal.UseVisualStyleBackColor = true;
            this.btnShowFinal.Click += new System.EventHandler(this.btnShowFinal_Click);
            // 
            // btn_normal_sample
            // 
            this.btn_normal_sample.Location = new System.Drawing.Point(749, 196);
            this.btn_normal_sample.Margin = new System.Windows.Forms.Padding(2);
            this.btn_normal_sample.Name = "btn_normal_sample";
            this.btn_normal_sample.Size = new System.Drawing.Size(89, 20);
            this.btn_normal_sample.TabIndex = 14;
            this.btn_normal_sample.Text = "Normal Sample";
            this.btn_normal_sample.UseVisualStyleBackColor = true;
            this.btn_normal_sample.Click += new System.EventHandler(this.btn_normal_sample_Click);
            // 
            // grpBoxTransformationTopLeft
            // 
            this.grpBoxTransformationTopLeft.Controls.Add(this.lbY2);
            this.grpBoxTransformationTopLeft.Controls.Add(this.textBox2);
            this.grpBoxTransformationTopLeft.Controls.Add(this.lbX2);
            this.grpBoxTransformationTopLeft.Controls.Add(this.textBox1);
            this.grpBoxTransformationTopLeft.Controls.Add(this.lbAngle2);
            this.grpBoxTransformationTopLeft.Controls.Add(this.textBox3);
            this.grpBoxTransformationTopLeft.Location = new System.Drawing.Point(966, 26);
            this.grpBoxTransformationTopLeft.Margin = new System.Windows.Forms.Padding(2);
            this.grpBoxTransformationTopLeft.Name = "grpBoxTransformationTopLeft";
            this.grpBoxTransformationTopLeft.Padding = new System.Windows.Forms.Padding(2);
            this.grpBoxTransformationTopLeft.Size = new System.Drawing.Size(251, 142);
            this.grpBoxTransformationTopLeft.TabIndex = 15;
            this.grpBoxTransformationTopLeft.TabStop = false;
            this.grpBoxTransformationTopLeft.Text = "Transformation parameters respect to Top Left";
            // 
            // grpBoxInitialTopLeft
            // 
            this.grpBoxInitialTopLeft.Controls.Add(this.lbY);
            this.grpBoxInitialTopLeft.Controls.Add(this.Txt_InitPosY);
            this.grpBoxInitialTopLeft.Controls.Add(this.lbX);
            this.grpBoxInitialTopLeft.Controls.Add(this.Txt_InitPosX);
            this.grpBoxInitialTopLeft.Controls.Add(this.lbAngle);
            this.grpBoxInitialTopLeft.Controls.Add(this.Txt_InitAngle);
            this.grpBoxInitialTopLeft.Location = new System.Drawing.Point(749, 26);
            this.grpBoxInitialTopLeft.Margin = new System.Windows.Forms.Padding(2);
            this.grpBoxInitialTopLeft.Name = "grpBoxInitialTopLeft";
            this.grpBoxInitialTopLeft.Padding = new System.Windows.Forms.Padding(2);
            this.grpBoxInitialTopLeft.Size = new System.Drawing.Size(213, 142);
            this.grpBoxInitialTopLeft.TabIndex = 16;
            this.grpBoxInitialTopLeft.TabStop = false;
            this.grpBoxInitialTopLeft.Text = "Initial parameters respect to Top Left";
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(8, 77);
            this.lbY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(14, 13);
            this.lbY.TabIndex = 9;
            this.lbY.Text = "Y";
            // 
            // Txt_InitPosY
            // 
            this.Txt_InitPosY.Location = new System.Drawing.Point(51, 75);
            this.Txt_InitPosY.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_InitPosY.Name = "Txt_InitPosY";
            this.Txt_InitPosY.Size = new System.Drawing.Size(72, 20);
            this.Txt_InitPosY.TabIndex = 8;
            this.Txt_InitPosY.Text = "400";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(8, 54);
            this.lbX.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(14, 13);
            this.lbX.TabIndex = 7;
            this.lbX.Text = "X";
            // 
            // Txt_InitPosX
            // 
            this.Txt_InitPosX.Location = new System.Drawing.Point(51, 52);
            this.Txt_InitPosX.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_InitPosX.Name = "Txt_InitPosX";
            this.Txt_InitPosX.Size = new System.Drawing.Size(72, 20);
            this.Txt_InitPosX.TabIndex = 6;
            this.Txt_InitPosX.Text = "100";
            // 
            // lbAngle
            // 
            this.lbAngle.AutoSize = true;
            this.lbAngle.Location = new System.Drawing.Point(8, 102);
            this.lbAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Size = new System.Drawing.Size(34, 13);
            this.lbAngle.TabIndex = 11;
            this.lbAngle.Text = "Angle";
            // 
            // Txt_InitAngle
            // 
            this.Txt_InitAngle.Location = new System.Drawing.Point(51, 100);
            this.Txt_InitAngle.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_InitAngle.Name = "Txt_InitAngle";
            this.Txt_InitAngle.Size = new System.Drawing.Size(72, 20);
            this.Txt_InitAngle.TabIndex = 10;
            this.Txt_InitAngle.Text = "0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1217, 24);
            this.menuStrip.TabIndex = 17;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportAnIssueToolStripMenuItem,
            this.githubWebPageToolStripMenuItem,
            this.aTutorialOnRigidRegistrationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // reportAnIssueToolStripMenuItem
            // 
            this.reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            this.reportAnIssueToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.reportAnIssueToolStripMenuItem.Text = "&Report an issue";
            this.reportAnIssueToolStripMenuItem.Click += new System.EventHandler(this.reportAnIssueToolStripMenuItem_Click);
            // 
            // githubWebPageToolStripMenuItem
            // 
            this.githubWebPageToolStripMenuItem.Name = "githubWebPageToolStripMenuItem";
            this.githubWebPageToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.githubWebPageToolStripMenuItem.Text = "&Github Web Page";
            this.githubWebPageToolStripMenuItem.Click += new System.EventHandler(this.githubWebPageToolStripMenuItem_Click);
            // 
            // aTutorialOnRigidRegistrationToolStripMenuItem
            // 
            this.aTutorialOnRigidRegistrationToolStripMenuItem.Name = "aTutorialOnRigidRegistrationToolStripMenuItem";
            this.aTutorialOnRigidRegistrationToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.aTutorialOnRigidRegistrationToolStripMenuItem.Text = "A &Tutorial on Rigid Registration";
            this.aTutorialOnRigidRegistrationToolStripMenuItem.Click += new System.EventHandler(this.aTutorialOnRigidRegistrationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 578);
            this.Controls.Add(this.grpBoxInitialTopLeft);
            this.Controls.Add(this.grpBoxTransformationTopLeft);
            this.Controls.Add(this.btn_normal_sample);
            this.Controls.Add(this.btnShowFinal);
            this.Controls.Add(this.btnNoisySample);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICP Algorithm in C#";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpBoxTransformationTopLeft.ResumeLayout(false);
            this.grpBoxTransformationTopLeft.PerformLayout();
            this.grpBoxInitialTopLeft.ResumeLayout(false);
            this.grpBoxInitialTopLeft.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbX2;
        private System.Windows.Forms.Label lbY2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbAngle2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnNoisySample;
        private System.Windows.Forms.Button btnShowFinal;
        private System.Windows.Forms.Button btn_normal_sample;
        private System.Windows.Forms.GroupBox grpBoxTransformationTopLeft;
        private System.Windows.Forms.GroupBox grpBoxInitialTopLeft;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.TextBox Txt_InitPosY;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.TextBox Txt_InitPosX;
        private System.Windows.Forms.Label lbAngle;
        private System.Windows.Forms.TextBox Txt_InitAngle;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAnIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubWebPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aTutorialOnRigidRegistrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

