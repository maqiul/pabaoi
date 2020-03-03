namespace pcbaoi
{
    partial class Ruleresult
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
            this.Canelbt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Starttx = new System.Windows.Forms.TextBox();
            this.Endtx = new System.Windows.Forms.TextBox();
            this.Lenthlb = new System.Windows.Forms.Label();
            this.Widthlb = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.Canelbt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 53);
            this.panel1.TabIndex = 0;
            // 
            // Canelbt
            // 
            this.Canelbt.BackColor = System.Drawing.Color.Transparent;
            this.Canelbt.BackgroundImage = global::pcbaoi.Properties.Resources.关__闭;
            this.Canelbt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Canelbt.FlatAppearance.BorderSize = 0;
            this.Canelbt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Canelbt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Canelbt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Canelbt.Location = new System.Drawing.Point(325, 12);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(35, 35);
            this.Canelbt.TabIndex = 1;
            this.Canelbt.UseVisualStyleBackColor = false;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(31, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "测量结果";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(64, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "起点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(64, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "终点";
            // 
            // Starttx
            // 
            this.Starttx.Enabled = false;
            this.Starttx.Font = new System.Drawing.Font("宋体", 15F);
            this.Starttx.Location = new System.Drawing.Point(122, 79);
            this.Starttx.Name = "Starttx";
            this.Starttx.Size = new System.Drawing.Size(166, 30);
            this.Starttx.TabIndex = 3;
            // 
            // Endtx
            // 
            this.Endtx.Enabled = false;
            this.Endtx.Font = new System.Drawing.Font("宋体", 15F);
            this.Endtx.Location = new System.Drawing.Point(122, 129);
            this.Endtx.Name = "Endtx";
            this.Endtx.Size = new System.Drawing.Size(166, 30);
            this.Endtx.TabIndex = 4;
            // 
            // Lenthlb
            // 
            this.Lenthlb.AutoSize = true;
            this.Lenthlb.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.Lenthlb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Lenthlb.Location = new System.Drawing.Point(64, 193);
            this.Lenthlb.Name = "Lenthlb";
            this.Lenthlb.Size = new System.Drawing.Size(51, 20);
            this.Lenthlb.TabIndex = 5;
            this.Lenthlb.Text = "长度";
            // 
            // Widthlb
            // 
            this.Widthlb.AutoSize = true;
            this.Widthlb.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.Widthlb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Widthlb.Location = new System.Drawing.Point(184, 193);
            this.Widthlb.Name = "Widthlb";
            this.Widthlb.Size = new System.Drawing.Size(51, 20);
            this.Widthlb.TabIndex = 6;
            this.Widthlb.Text = "宽度";
            // 
            // Ruleresult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(372, 250);
            this.Controls.Add(this.Widthlb);
            this.Controls.Add(this.Lenthlb);
            this.Controls.Add(this.Endtx);
            this.Controls.Add(this.Starttx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Ruleresult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ruleresult";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Starttx;
        private System.Windows.Forms.TextBox Endtx;
        private System.Windows.Forms.Label Lenthlb;
        private System.Windows.Forms.Label Widthlb;
        private System.Windows.Forms.Button Canelbt;
    }
}