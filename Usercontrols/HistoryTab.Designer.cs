﻿namespace pcbaoi.usercontrol
{
    partial class HistoryTab
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
            this.Filepathlb = new System.Windows.Forms.Label();
            this.Datetimelb = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Filepathlb
            // 
            this.Filepathlb.AutoSize = true;
            this.Filepathlb.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Filepathlb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Filepathlb.Location = new System.Drawing.Point(9, 15);
            this.Filepathlb.Name = "Filepathlb";
            this.Filepathlb.Size = new System.Drawing.Size(47, 12);
            this.Filepathlb.TabIndex = 0;
            this.Filepathlb.Text = "label1";
            this.Filepathlb.Click += new System.EventHandler(this.Filepathlb_Click);
            // 
            // Datetimelb
            // 
            this.Datetimelb.AutoSize = true;
            this.Datetimelb.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Datetimelb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Datetimelb.Location = new System.Drawing.Point(268, 15);
            this.Datetimelb.Name = "Datetimelb";
            this.Datetimelb.Size = new System.Drawing.Size(47, 12);
            this.Datetimelb.TabIndex = 1;
            this.Datetimelb.Text = "label2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 39);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 1);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // HistoryTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Datetimelb);
            this.Controls.Add(this.Filepathlb);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HistoryTab";
            this.Size = new System.Drawing.Size(395, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Filepathlb;
        private System.Windows.Forms.Label Datetimelb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
