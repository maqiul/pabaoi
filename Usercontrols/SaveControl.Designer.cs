namespace pcbaoi.usercontrol
{
    partial class Savecontrol
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Pcbnametx = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Pcbwidthtx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Pcbheighttx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Niptx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Okbt = new System.Windows.Forms.Button();
            this.Canelbt = new System.Windows.Forms.Button();
            this.Carrierplateheighttx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Carrierplatewidthtx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(87, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "PCB 板名";
            // 
            // Pcbnametx
            // 
            this.Pcbnametx.Font = new System.Drawing.Font("宋体", 12F);
            this.Pcbnametx.Location = new System.Drawing.Point(184, 32);
            this.Pcbnametx.Margin = new System.Windows.Forms.Padding(0);
            this.Pcbnametx.Name = "Pcbnametx";
            this.Pcbnametx.Size = new System.Drawing.Size(157, 26);
            this.Pcbnametx.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.textBox2.Location = new System.Drawing.Point(184, 80);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(157, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "默认客户";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(87, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "客户归属";
            // 
            // Pcbwidthtx
            // 
            this.Pcbwidthtx.Font = new System.Drawing.Font("宋体", 12F);
            this.Pcbwidthtx.Location = new System.Drawing.Point(184, 225);
            this.Pcbwidthtx.Margin = new System.Windows.Forms.Padding(0);
            this.Pcbwidthtx.Name = "Pcbwidthtx";
            this.Pcbwidthtx.Size = new System.Drawing.Size(157, 26);
            this.Pcbwidthtx.TabIndex = 5;
            this.Pcbwidthtx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pcbwidthtx_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(87, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "PCB 板宽";
            // 
            // Pcbheighttx
            // 
            this.Pcbheighttx.Font = new System.Drawing.Font("宋体", 12F);
            this.Pcbheighttx.Location = new System.Drawing.Point(184, 273);
            this.Pcbheighttx.Margin = new System.Windows.Forms.Padding(0);
            this.Pcbheighttx.Name = "Pcbheighttx";
            this.Pcbheighttx.Size = new System.Drawing.Size(157, 26);
            this.Pcbheighttx.TabIndex = 7;
            this.Pcbheighttx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pcbheighttx_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(87, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "PCB 板长";
            // 
            // Niptx
            // 
            this.Niptx.Font = new System.Drawing.Font("宋体", 12F);
            this.Niptx.Location = new System.Drawing.Point(184, 326);
            this.Niptx.Margin = new System.Windows.Forms.Padding(0);
            this.Niptx.Name = "Niptx";
            this.Niptx.Size = new System.Drawing.Size(157, 26);
            this.Niptx.TabIndex = 9;
            this.Niptx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Niptx_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(87, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "轨道夹紧量";
            // 
            // Okbt
            // 
            this.Okbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Okbt.Location = new System.Drawing.Point(121, 395);
            this.Okbt.Name = "Okbt";
            this.Okbt.Size = new System.Drawing.Size(108, 54);
            this.Okbt.TabIndex = 10;
            this.Okbt.Text = "保存";
            this.Okbt.UseVisualStyleBackColor = true;
            this.Okbt.Click += new System.EventHandler(this.Okbt_Click);
            // 
            // Canelbt
            // 
            this.Canelbt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Canelbt.Location = new System.Drawing.Point(262, 395);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(108, 54);
            this.Canelbt.TabIndex = 11;
            this.Canelbt.Text = "关闭";
            this.Canelbt.UseVisualStyleBackColor = true;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // Carrierplateheighttx
            // 
            this.Carrierplateheighttx.Font = new System.Drawing.Font("宋体", 12F);
            this.Carrierplateheighttx.Location = new System.Drawing.Point(184, 176);
            this.Carrierplateheighttx.Margin = new System.Windows.Forms.Padding(0);
            this.Carrierplateheighttx.Name = "Carrierplateheighttx";
            this.Carrierplateheighttx.Size = new System.Drawing.Size(157, 26);
            this.Carrierplateheighttx.TabIndex = 15;
            this.Carrierplateheighttx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Carrierplateheighttx_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(87, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "载板板长";
            // 
            // Carrierplatewidthtx
            // 
            this.Carrierplatewidthtx.Font = new System.Drawing.Font("宋体", 12F);
            this.Carrierplatewidthtx.Location = new System.Drawing.Point(184, 128);
            this.Carrierplatewidthtx.Margin = new System.Windows.Forms.Padding(0);
            this.Carrierplatewidthtx.Name = "Carrierplatewidthtx";
            this.Carrierplatewidthtx.Size = new System.Drawing.Size(157, 26);
            this.Carrierplatewidthtx.TabIndex = 13;
            this.Carrierplatewidthtx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Carrierplatewidthtx_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(87, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "载板板宽";
            // 
            // Savecontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.Carrierplateheighttx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Carrierplatewidthtx);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Canelbt);
            this.Controls.Add(this.Okbt);
            this.Controls.Add(this.Niptx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Pcbheighttx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Pcbwidthtx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Pcbnametx);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Savecontrol";
            this.Size = new System.Drawing.Size(517, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Pcbnametx;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Pcbwidthtx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Pcbheighttx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Niptx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Okbt;
        private System.Windows.Forms.Button Canelbt;
        private System.Windows.Forms.TextBox Carrierplateheighttx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Carrierplatewidthtx;
        private System.Windows.Forms.Label label7;
    }
}
