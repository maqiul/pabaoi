namespace pcbaoi
{
    partial class Barchart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rangeTrackBarControl1 = new DevExpress.XtraEditors.RangeTrackBarControl();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.rangeTrackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeTrackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // rangeTrackBarControl1
            // 
            this.rangeTrackBarControl1.EditValue = new DevExpress.XtraEditors.Repository.TrackBarRange(0, 17);
            this.rangeTrackBarControl1.Location = new System.Drawing.Point(195, 44);
            this.rangeTrackBarControl1.Name = "rangeTrackBarControl1";
            this.rangeTrackBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rangeTrackBarControl1.Properties.Appearance.Options.UseBackColor = true;
            this.rangeTrackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.rangeTrackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rangeTrackBarControl1.Properties.Maximum = 17;
            this.rangeTrackBarControl1.Properties.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.rangeTrackBarControl1.Properties.TickStyle = System.Windows.Forms.TickStyle.None;
            this.rangeTrackBarControl1.Size = new System.Drawing.Size(45, 141);
            this.rangeTrackBarControl1.TabIndex = 0;
            this.rangeTrackBarControl1.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(0, 17);
            this.rangeTrackBarControl1.ValueChanged += new System.EventHandler(this.rangeTrackBarControl1_ValueChanged);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea2.BorderColor = System.Drawing.Color.Transparent;
            chartArea2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.BorderWidth = 0;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "new";
            legend2.TitleBackColor = System.Drawing.Color.Transparent;
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 3);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.IsVisibleInLegend = false;
            series2.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series2.Legend = "new";
            series2.Name = "new";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(190, 182);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // Barchart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rangeTrackBarControl1);
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Barchart";
            this.Size = new System.Drawing.Size(236, 235);
            this.Load += new System.EventHandler(this.Barchart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Barchart_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.rangeTrackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeTrackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RangeTrackBarControl rangeTrackBarControl1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
