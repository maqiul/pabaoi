namespace pcbaoi
{
    partial class AlgorithmSelect
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.Canelbt = new System.Windows.Forms.Button();
            this.Okbt = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SubSubstratecm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BarcodeArea = new System.Windows.Forms.RadioButton();
            this.Mark = new System.Windows.Forms.RadioButton();
            this.BadMark = new System.Windows.Forms.RadioButton();
            this.Line = new System.Windows.Forms.RadioButton();
            this.GoldenFinger = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 51);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "检测区域";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Canelbt);
            this.panel2.Controls.Add(this.Okbt);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 368);
            this.panel2.TabIndex = 2;
            // 
            // Canelbt
            // 
            this.Canelbt.Location = new System.Drawing.Point(246, 313);
            this.Canelbt.Name = "Canelbt";
            this.Canelbt.Size = new System.Drawing.Size(75, 23);
            this.Canelbt.TabIndex = 7;
            this.Canelbt.Text = "取消";
            this.Canelbt.UseVisualStyleBackColor = true;
            this.Canelbt.Click += new System.EventHandler(this.Canelbt_Click);
            // 
            // Okbt
            // 
            this.Okbt.Location = new System.Drawing.Point(146, 313);
            this.Okbt.Name = "Okbt";
            this.Okbt.Size = new System.Drawing.Size(75, 23);
            this.Okbt.TabIndex = 6;
            this.Okbt.Text = "确定";
            this.Okbt.UseVisualStyleBackColor = true;
            this.Okbt.Click += new System.EventHandler(this.Okbt_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SubSubstratecm);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.BarcodeArea);
            this.panel3.Controls.Add(this.Mark);
            this.panel3.Controls.Add(this.BadMark);
            this.panel3.Controls.Add(this.Line);
            this.panel3.Controls.Add(this.GoldenFinger);
            this.panel3.Location = new System.Drawing.Point(81, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 217);
            this.panel3.TabIndex = 5;
            // 
            // SubSubstratecm
            // 
            this.SubSubstratecm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubSubstratecm.FormattingEnabled = true;
            this.SubSubstratecm.Items.AddRange(new object[] {
            "无"});
            this.SubSubstratecm.Location = new System.Drawing.Point(146, 184);
            this.SubSubstratecm.Name = "SubSubstratecm";
            this.SubSubstratecm.Size = new System.Drawing.Size(85, 20);
            this.SubSubstratecm.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(28, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "区域归属子基板";
            // 
            // BarcodeArea
            // 
            this.BarcodeArea.AutoSize = true;
            this.BarcodeArea.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.BarcodeArea.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BarcodeArea.Location = new System.Drawing.Point(63, 152);
            this.BarcodeArea.Name = "BarcodeArea";
            this.BarcodeArea.Size = new System.Drawing.Size(94, 20);
            this.BarcodeArea.TabIndex = 4;
            this.BarcodeArea.TabStop = true;
            this.BarcodeArea.Text = "条码区域";
            this.BarcodeArea.UseVisualStyleBackColor = true;
            // 
            // Mark
            // 
            this.Mark.AutoSize = true;
            this.Mark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.Mark.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Mark.Location = new System.Drawing.Point(63, 118);
            this.Mark.Name = "Mark";
            this.Mark.Size = new System.Drawing.Size(62, 20);
            this.Mark.TabIndex = 3;
            this.Mark.TabStop = true;
            this.Mark.Text = "MARK";
            this.Mark.UseVisualStyleBackColor = true;
            // 
            // BadMark
            // 
            this.BadMark.AutoSize = true;
            this.BadMark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.BadMark.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BadMark.Location = new System.Drawing.Point(63, 86);
            this.BadMark.Name = "BadMark";
            this.BadMark.Size = new System.Drawing.Size(98, 20);
            this.BadMark.TabIndex = 2;
            this.BadMark.TabStop = true;
            this.BadMark.Text = "BAD MARK";
            this.BadMark.UseVisualStyleBackColor = true;
            // 
            // Line
            // 
            this.Line.AutoSize = true;
            this.Line.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.Line.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Line.Location = new System.Drawing.Point(63, 52);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(60, 20);
            this.Line.TabIndex = 1;
            this.Line.TabStop = true;
            this.Line.Text = "线路";
            this.Line.UseVisualStyleBackColor = true;
            // 
            // GoldenFinger
            // 
            this.GoldenFinger.AutoSize = true;
            this.GoldenFinger.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.GoldenFinger.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GoldenFinger.Location = new System.Drawing.Point(63, 17);
            this.GoldenFinger.Name = "GoldenFinger";
            this.GoldenFinger.Size = new System.Drawing.Size(77, 20);
            this.GoldenFinger.TabIndex = 0;
            this.GoldenFinger.TabStop = true;
            this.GoldenFinger.Text = "金手指";
            this.GoldenFinger.UseVisualStyleBackColor = true;
            // 
            // AlgorithmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(439, 368);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlgorithmSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlgorithmSelect";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton BarcodeArea;
        private System.Windows.Forms.RadioButton Mark;
        private System.Windows.Forms.RadioButton BadMark;
        private System.Windows.Forms.RadioButton Line;
        private System.Windows.Forms.RadioButton GoldenFinger;
        private System.Windows.Forms.Button Canelbt;
        private System.Windows.Forms.Button Okbt;
        private System.Windows.Forms.ComboBox SubSubstratecm;
        private System.Windows.Forms.Label label2;
    }
}