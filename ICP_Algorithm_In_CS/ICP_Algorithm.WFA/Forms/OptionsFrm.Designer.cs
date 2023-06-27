
namespace ICP_Algorithm.WFA.Forms
{
    partial class OptionsFrm
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
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.cmbBoxLang = new System.Windows.Forms.ComboBox();
            this.lbLang = new System.Windows.Forms.Label();
            this.tabControlOptions.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabPageGeneral);
            this.tabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOptions.Location = new System.Drawing.Point(0, 0);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(522, 303);
            this.tabControlOptions.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.cmbBoxLang);
            this.tabPageGeneral.Controls.Add(this.lbLang);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(514, 277);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // cmbBoxLang
            // 
            this.cmbBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxLang.FormattingEnabled = true;
            this.cmbBoxLang.Location = new System.Drawing.Point(67, 6);
            this.cmbBoxLang.Name = "cmbBoxLang";
            this.cmbBoxLang.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxLang.TabIndex = 2;
            this.cmbBoxLang.SelectedIndexChanged += new System.EventHandler(this.cmbBoxLang_SelectedIndexChanged);
            // 
            // lbLang
            // 
            this.lbLang.AutoSize = true;
            this.lbLang.Location = new System.Drawing.Point(3, 9);
            this.lbLang.Name = "lbLang";
            this.lbLang.Size = new System.Drawing.Size(58, 13);
            this.lbLang.TabIndex = 1;
            this.lbLang.Text = "Language:";
            // 
            // OptionsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 303);
            this.Controls.Add(this.tabControlOptions);
            this.Name = "OptionsFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tabControlOptions.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label lbLang;
        private System.Windows.Forms.ComboBox cmbBoxLang;
    }
}