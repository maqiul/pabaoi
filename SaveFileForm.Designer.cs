namespace pcbaoi
{
    partial class SaveFileForm
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
            this.titlelabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.projectsavepath = new System.Windows.Forms.TextBox();
            this.allpicpath = new System.Windows.Forms.TextBox();
            this.fillpicpath = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Canelbt = new System.Windows.Forms.Button();
            this.Savebt = new System.Windows.Forms.Button();
            this.Titlepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Titlepanel
            // 
            this.Titlepanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Titlepanel.Controls.Add(this.titlelabel);
            this.Titlepanel.Location = new System.Drawing.Point(0, 0);
            this.Titlepanel.Margin = new System.Windows.Forms.Padding(0);
            this.Titlepanel.Name = "Titlepanel";
            this.Titlepanel.Size = new System.Drawing.Size(502, 42);
            this.Titlepanel.TabIndex = 0;
            // 
            // titlelabel
            // 
            this.titlelabel.AutoSize = true;
            this.titlelabel.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.titlelabel.Location = new System.Drawing.Point(37, 15);
            this.titlelabel.Name = "titlelabel";
            this.titlelabel.Size = new System.Drawing.Size(103, 15);
            this.titlelabel.TabIndex = 0;
            this.titlelabel.Text = "文件保存管理";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(72, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "程序文件保存地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(72, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "所有图片保存地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(72, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "程序整图保存地址";
            // 
            // projectsavepath
            // 
            this.projectsavepath.Location = new System.Drawing.Point(230, 79);
            this.projectsavepath.Name = "projectsavepath";
            this.projectsavepath.ReadOnly = true;
            this.projectsavepath.Size = new System.Drawing.Size(198, 21);
            this.projectsavepath.TabIndex = 4;
            this.projectsavepath.Click += new System.EventHandler(this.projectsavepath_Click);
            this.projectsavepath.MouseHover += new System.EventHandler(this.projectsavepath_MouseHover);
            // 
            // allpicpath
            // 
            this.allpicpath.Location = new System.Drawing.Point(230, 153);
            this.allpicpath.Name = "allpicpath";
            this.allpicpath.ReadOnly = true;
            this.allpicpath.Size = new System.Drawing.Size(198, 21);
            this.allpicpath.TabIndex = 5;
            this.allpicpath.Click += new System.EventHandler(this.allpicpath_Click);
            this.allpicpath.TextChanged += new System.EventHandler(this.allpicpath_TextChanged);
            this.allpicpath.MouseHover += new System.EventHandler(this.allpicpath_MouseHover);
            // 
            // fillpicpath
            // 
            this.fillpicpath.Location = new System.Drawing.Point(230, 221);
            this.fillpicpath.Name = "fillpicpath";
            this.fillpicpath.ReadOnly = true;
            this.fillpicpath.Size = new System.Drawing.Size(198, 21);
            this.fillpicpath.TabIndex = 6;
            this.fillpicpath.Click += new System.EventHandler(this.fillpicpath_Click);
            this.fillpicpath.MouseHover += new System.EventHandler(this.fillpicpath_MouseHover);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(51, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Canelbt
            // 
            this.Canelbt.Location = new System.Drawing.Point(346, 306);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(75, 23);
            this.Canelbt.TabIndex = 10;
            this.Canelbt.Text = "取消";
            this.Canelbt.UseVisualStyleBackColor = true;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // Savebt
            // 
            this.Savebt.Location = new System.Drawing.Point(230, 306);
            this.Savebt.Name = "Savebt";
            this.Savebt.Size = new System.Drawing.Size(75, 23);
            this.Savebt.TabIndex = 9;
            this.Savebt.Text = "保存";
            this.Savebt.UseVisualStyleBackColor = true;
            this.Savebt.Click += new System.EventHandler(this.Savebt_Click);
            // 
            // SaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(502, 375);
            this.Controls.Add(this.Canelbt);
            this.Controls.Add(this.Savebt);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.fillpicpath);
            this.Controls.Add(this.allpicpath);
            this.Controls.Add(this.projectsavepath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Titlepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaveFileForm";
            this.Titlepanel.ResumeLayout(false);
            this.Titlepanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Titlepanel;
        private System.Windows.Forms.Label titlelabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox projectsavepath;
        private System.Windows.Forms.TextBox allpicpath;
        private System.Windows.Forms.TextBox fillpicpath;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Canelbt;
        private System.Windows.Forms.Button Savebt;
    }
}