namespace pcbaoi
{
    partial class ShieldForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Openbt = new System.Windows.Forms.Button();
            this.Buzzerbt = new System.Windows.Forms.Button();
            this.Closebt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(95, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "开门屏蔽:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(95, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "蜂鸣器屏蔽:";
            // 
            // Openbt
            // 
            this.Openbt.Location = new System.Drawing.Point(223, 78);
            this.Openbt.Name = "Openbt";
            this.Openbt.Size = new System.Drawing.Size(75, 23);
            this.Openbt.TabIndex = 2;
            this.Openbt.Text = "开门屏蔽";
            this.Openbt.UseVisualStyleBackColor = true;
            this.Openbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Openbt_MouseDown);
            this.Openbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Openbt_MouseUp);
            // 
            // Buzzerbt
            // 
            this.Buzzerbt.Location = new System.Drawing.Point(223, 121);
            this.Buzzerbt.Name = "Buzzerbt";
            this.Buzzerbt.Size = new System.Drawing.Size(75, 23);
            this.Buzzerbt.TabIndex = 3;
            this.Buzzerbt.Text = "蜂鸣器屏蔽";
            this.Buzzerbt.UseVisualStyleBackColor = true;
            this.Buzzerbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Buzzerbt_MouseDown);
            this.Buzzerbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Buzzerbt_MouseUp);
            // 
            // Closebt
            // 
            this.Closebt.Location = new System.Drawing.Point(172, 226);
            this.Closebt.Name = "Closebt";
            this.Closebt.Size = new System.Drawing.Size(75, 23);
            this.Closebt.TabIndex = 4;
            this.Closebt.Text = "关闭";
            this.Closebt.UseVisualStyleBackColor = true;
            this.Closebt.Click += new System.EventHandler(this.Closebt_Click);
            // 
            // PingbiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(442, 324);
            this.Controls.Add(this.Closebt);
            this.Controls.Add(this.Buzzerbt);
            this.Controls.Add(this.Openbt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PingbiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PingbiForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Openbt;
        private System.Windows.Forms.Button Buzzerbt;
        private System.Windows.Forms.Button Closebt;
    }
}