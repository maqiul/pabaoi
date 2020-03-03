namespace pcbaoi
{
    partial class Code
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PercentageDowntx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PercentageOntx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Codetypecm = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.PercentageDowntx, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PercentageOntx, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Codetypecm, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 234);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // PercentageDowntx
            // 
            this.PercentageDowntx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PercentageDowntx.Location = new System.Drawing.Point(134, 59);
            this.PercentageDowntx.Name = "PercentageDowntx";
            this.PercentageDowntx.Size = new System.Drawing.Size(100, 21);
            this.PercentageDowntx.TabIndex = 4;
            this.PercentageDowntx.Text = "0";
            this.PercentageDowntx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PercentageDowntx_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "百分比上限";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(27, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "百分比下限";
            // 
            // PercentageOntx
            // 
            this.PercentageOntx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PercentageOntx.Location = new System.Drawing.Point(134, 13);
            this.PercentageOntx.Name = "PercentageOntx";
            this.PercentageOntx.Size = new System.Drawing.Size(100, 21);
            this.PercentageOntx.TabIndex = 3;
            this.PercentageOntx.Text = "100";
            this.PercentageOntx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PercentageOntx_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(27, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "一维码编码";
            // 
            // Codetypecm
            // 
            this.Codetypecm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Codetypecm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Codetypecm.FormattingEnabled = true;
            this.Codetypecm.Items.AddRange(new object[] {
            "1D-Code39",
            "1D-Code93",
            "1D-Code128"});
            this.Codetypecm.Location = new System.Drawing.Point(134, 106);
            this.Codetypecm.Name = "Codetypecm";
            this.Codetypecm.Size = new System.Drawing.Size(100, 20);
            this.Codetypecm.TabIndex = 6;
            this.Codetypecm.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // UserControl3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControl3";
            this.Size = new System.Drawing.Size(246, 234);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox PercentageDowntx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PercentageOntx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Codetypecm;
    }
}
