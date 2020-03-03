namespace pcbaoi
{
    partial class Collectionform
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Typecom = new System.Windows.Forms.ComboBox();
            this.Pcbwidthtx = new System.Windows.Forms.TextBox();
            this.Pcbheighttx = new System.Windows.Forms.TextBox();
            this.Okbt = new System.Windows.Forms.Button();
            this.Canelbt = new System.Windows.Forms.Button();
            this.Heighttx = new System.Windows.Forms.TextBox();
            this.Widthtx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 53);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(32, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "采集";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(82, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "采集板面";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(91, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "pcb板长";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(91, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "pcb板宽";
            // 
            // Typecom
            // 
            this.Typecom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Typecom.Font = new System.Drawing.Font("宋体", 15F);
            this.Typecom.FormattingEnabled = true;
            this.Typecom.Items.AddRange(new object[] {
            "正面",
            "反面",
            "双面"});
            this.Typecom.Location = new System.Drawing.Point(178, 88);
            this.Typecom.Name = "Typecom";
            this.Typecom.Size = new System.Drawing.Size(175, 28);
            this.Typecom.TabIndex = 4;
            // 
            // Pcbwidthtx
            // 
            this.Pcbwidthtx.Font = new System.Drawing.Font("宋体", 15F);
            this.Pcbwidthtx.Location = new System.Drawing.Point(180, 240);
            this.Pcbwidthtx.Name = "Pcbwidthtx";
            this.Pcbwidthtx.Size = new System.Drawing.Size(175, 30);
            this.Pcbwidthtx.TabIndex = 5;
            this.Pcbwidthtx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pcbwidthtx_KeyPress);
            // 
            // Pcbheighttx
            // 
            this.Pcbheighttx.Font = new System.Drawing.Font("宋体", 15F);
            this.Pcbheighttx.Location = new System.Drawing.Point(180, 289);
            this.Pcbheighttx.Name = "Pcbheighttx";
            this.Pcbheighttx.Size = new System.Drawing.Size(175, 30);
            this.Pcbheighttx.TabIndex = 6;
            this.Pcbheighttx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pcbheighttx_KeyPress);
            // 
            // Okbt
            // 
            this.Okbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Okbt.Location = new System.Drawing.Point(178, 343);
            this.Okbt.Name = "Okbt";
            this.Okbt.Size = new System.Drawing.Size(75, 23);
            this.Okbt.TabIndex = 7;
            this.Okbt.Text = "确认";
            this.Okbt.UseVisualStyleBackColor = true;
            this.Okbt.Click += new System.EventHandler(this.Okbt_Click);
            // 
            // Canelbt
            // 
            this.Canelbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Canelbt.Location = new System.Drawing.Point(278, 343);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(75, 23);
            this.Canelbt.TabIndex = 8;
            this.Canelbt.Text = "取消";
            this.Canelbt.UseVisualStyleBackColor = true;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // Heighttx
            // 
            this.Heighttx.Font = new System.Drawing.Font("宋体", 15F);
            this.Heighttx.Location = new System.Drawing.Point(180, 189);
            this.Heighttx.Name = "Heighttx";
            this.Heighttx.Size = new System.Drawing.Size(175, 30);
            this.Heighttx.TabIndex = 12;
            this.Heighttx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Heighttx_KeyPress);
            // 
            // Widthtx
            // 
            this.Widthtx.Font = new System.Drawing.Font("宋体", 15F);
            this.Widthtx.Location = new System.Drawing.Point(180, 140);
            this.Widthtx.Name = "Widthtx";
            this.Widthtx.Size = new System.Drawing.Size(175, 30);
            this.Widthtx.TabIndex = 11;
            this.Widthtx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Widthtx_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(82, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "载板板宽";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(81, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "载板板长";
            // 
            // Collectionform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(472, 407);
            this.Controls.Add(this.Heighttx);
            this.Controls.Add(this.Widthtx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Canelbt);
            this.Controls.Add(this.Okbt);
            this.Controls.Add(this.Pcbheighttx);
            this.Controls.Add(this.Pcbwidthtx);
            this.Controls.Add(this.Typecom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Collectionform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collectionform";
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Typecom;
        private System.Windows.Forms.TextBox Pcbwidthtx;
        private System.Windows.Forms.TextBox Pcbheighttx;
        private System.Windows.Forms.Button Okbt;
        private System.Windows.Forms.Button Canelbt;
        private System.Windows.Forms.TextBox Heighttx;
        private System.Windows.Forms.TextBox Widthtx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}