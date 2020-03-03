namespace pcbaoi.usercontrol
{
    partial class History
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
            this.label2 = new System.Windows.Forms.Label();
            this.HistoryPanel = new System.Windows.Forms.Panel();
            this.CLosebt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(67, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "PCB板名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(343, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "上次打开时间";
            // 
            // HistoryPanel
            // 
            this.HistoryPanel.Location = new System.Drawing.Point(60, 64);
            this.HistoryPanel.Margin = new System.Windows.Forms.Padding(0);
            this.HistoryPanel.Name = "HistoryPanel";
            this.HistoryPanel.Size = new System.Drawing.Size(395, 302);
            this.HistoryPanel.TabIndex = 2;
            // 
            // CLosebt
            // 
            this.CLosebt.Location = new System.Drawing.Point(346, 382);
            this.CLosebt.Name = "CLosebt";
            this.CLosebt.Size = new System.Drawing.Size(108, 54);
            this.CLosebt.TabIndex = 3;
            this.CLosebt.Text = "关闭";
            this.CLosebt.UseVisualStyleBackColor = true;
            this.CLosebt.Click += new System.EventHandler(this.CLosebt_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.CLosebt);
            this.Controls.Add(this.HistoryPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "History";
            this.Size = new System.Drawing.Size(517, 472);
            this.Load += new System.EventHandler(this.History_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel HistoryPanel;
        private System.Windows.Forms.Button CLosebt;
    }
}
