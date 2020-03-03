namespace pcbaoi
{
    partial class Workspace
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
            this.CreatePjbt = new System.Windows.Forms.Button();
            this.OpenPjbt = new System.Windows.Forms.Button();
            this.Showpanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreatePjbt
            // 
            this.CreatePjbt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CreatePjbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CreatePjbt.Location = new System.Drawing.Point(54, 82);
            this.CreatePjbt.Name = "CreatePjbt";
            this.CreatePjbt.Size = new System.Drawing.Size(108, 54);
            this.CreatePjbt.TabIndex = 0;
            this.CreatePjbt.Text = "创建项目";
            this.CreatePjbt.UseVisualStyleBackColor = false;
            this.CreatePjbt.Click += new System.EventHandler(this.CreatePjbt_Click);
            // 
            // OpenPjbt
            // 
            this.OpenPjbt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OpenPjbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenPjbt.Location = new System.Drawing.Point(54, 154);
            this.OpenPjbt.Name = "OpenPjbt";
            this.OpenPjbt.Size = new System.Drawing.Size(108, 54);
            this.OpenPjbt.TabIndex = 1;
            this.OpenPjbt.Text = "打开项目";
            this.OpenPjbt.UseVisualStyleBackColor = false;
            this.OpenPjbt.Click += new System.EventHandler(this.OpenPjbt_Click);
            // 
            // Showpanel
            // 
            this.Showpanel.Location = new System.Drawing.Point(281, 82);
            this.Showpanel.Margin = new System.Windows.Forms.Padding(0);
            this.Showpanel.Name = "Showpanel";
            this.Showpanel.Size = new System.Drawing.Size(517, 543);
            this.Showpanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 64);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始";
            // 
            // Workspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(850, 659);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Showpanel);
            this.Controls.Add(this.OpenPjbt);
            this.Controls.Add(this.CreatePjbt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Workspace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开始";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Workspace_FormClosing);
            this.Load += new System.EventHandler(this.Workspace_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreatePjbt;
        private System.Windows.Forms.Button OpenPjbt;
        private System.Windows.Forms.Panel Showpanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}