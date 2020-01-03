namespace pcbaoi
{
    partial class TrackForm
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
            this.Titlepanel = new System.Windows.Forms.Panel();
            this.Titlelabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Widthtextbox = new System.Windows.Forms.TextBox();
            this.Speedtextbox = new System.Windows.Forms.TextBox();
            this.Widthtest = new System.Windows.Forms.Button();
            this.speedbt = new System.Windows.Forms.Button();
            this.Savebt = new System.Windows.Forms.Button();
            this.Canelbt = new System.Windows.Forms.Button();
            this.Titlepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Titlepanel
            // 
            this.Titlepanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Titlepanel.Controls.Add(this.Titlelabel);
            this.Titlepanel.Location = new System.Drawing.Point(0, 0);
            this.Titlepanel.Margin = new System.Windows.Forms.Padding(0);
            this.Titlepanel.Name = "Titlepanel";
            this.Titlepanel.Size = new System.Drawing.Size(500, 50);
            this.Titlepanel.TabIndex = 0;
            // 
            // Titlelabel
            // 
            this.Titlelabel.AutoSize = true;
            this.Titlelabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Titlelabel.Location = new System.Drawing.Point(42, 18);
            this.Titlelabel.Name = "Titlelabel";
            this.Titlelabel.Size = new System.Drawing.Size(76, 16);
            this.Titlelabel.TabIndex = 0;
            this.Titlelabel.Text = "轨道调试";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(78, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "轨道宽度测试";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(112, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "皮带速度";
            // 
            // Widthtextbox
            // 
            this.Widthtextbox.Location = new System.Drawing.Point(208, 93);
            this.Widthtextbox.Name = "Widthtextbox";
            this.Widthtextbox.Size = new System.Drawing.Size(161, 21);
            this.Widthtextbox.TabIndex = 3;
            this.Widthtextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // Speedtextbox
            // 
            this.Speedtextbox.Location = new System.Drawing.Point(208, 162);
            this.Speedtextbox.Name = "Speedtextbox";
            this.Speedtextbox.Size = new System.Drawing.Size(161, 21);
            this.Speedtextbox.TabIndex = 4;
            this.Speedtextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // Widthtest
            // 
            this.Widthtest.Location = new System.Drawing.Point(281, 120);
            this.Widthtest.Name = "Widthtest";
            this.Widthtest.Size = new System.Drawing.Size(88, 23);
            this.Widthtest.TabIndex = 5;
            this.Widthtest.Text = "轨道宽度调整";
            this.Widthtest.UseVisualStyleBackColor = true;
            this.Widthtest.Click += new System.EventHandler(this.Widthtest_Click);
            // 
            // speedbt
            // 
            this.speedbt.Location = new System.Drawing.Point(281, 203);
            this.speedbt.Name = "speedbt";
            this.speedbt.Size = new System.Drawing.Size(88, 23);
            this.speedbt.TabIndex = 6;
            this.speedbt.Text = "皮带运动测试";
            this.speedbt.UseVisualStyleBackColor = true;
            this.speedbt.Click += new System.EventHandler(this.button1_Click);
            // 
            // Savebt
            // 
            this.Savebt.Location = new System.Drawing.Point(178, 279);
            this.Savebt.Name = "Savebt";
            this.Savebt.Size = new System.Drawing.Size(75, 23);
            this.Savebt.TabIndex = 7;
            this.Savebt.Text = "保存";
            this.Savebt.UseVisualStyleBackColor = true;
            // 
            // Canelbt
            // 
            this.Canelbt.Location = new System.Drawing.Point(294, 279);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(75, 23);
            this.Canelbt.TabIndex = 8;
            this.Canelbt.Text = "取消";
            this.Canelbt.UseVisualStyleBackColor = true;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // TrackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.Canelbt);
            this.Controls.Add(this.Savebt);
            this.Controls.Add(this.speedbt);
            this.Controls.Add(this.Widthtest);
            this.Controls.Add(this.Speedtextbox);
            this.Controls.Add(this.Widthtextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Titlepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrackForm";
            this.Titlepanel.ResumeLayout(false);
            this.Titlepanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Titlepanel;
        private System.Windows.Forms.Label Titlelabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Widthtextbox;
        private System.Windows.Forms.TextBox Speedtextbox;
        private System.Windows.Forms.Button Widthtest;
        private System.Windows.Forms.Button speedbt;
        private System.Windows.Forms.Button Savebt;
        private System.Windows.Forms.Button Canelbt;
    }
}