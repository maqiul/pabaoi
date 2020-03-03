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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.RangeTrackBar = new DevExpress.XtraEditors.RangeTrackBarControl();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.RangeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RangeTrackBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // RangeTrackBar
            // 
            this.RangeTrackBar.EditValue = new DevExpress.XtraEditors.Repository.TrackBarRange(0, 17);
            this.RangeTrackBar.Location = new System.Drawing.Point(195, 44);
            this.RangeTrackBar.Name = "RangeTrackBar";
            this.RangeTrackBar.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.RangeTrackBar.Properties.Appearance.Options.UseBackColor = true;
            this.RangeTrackBar.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.RangeTrackBar.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.RangeTrackBar.Properties.Maximum = 17;
            this.RangeTrackBar.Properties.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RangeTrackBar.Properties.TickStyle = System.Windows.Forms.TickStyle.None;
            this.RangeTrackBar.Size = new System.Drawing.Size(45, 141);
            this.RangeTrackBar.TabIndex = 0;
            this.RangeTrackBar.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(0, 17);
            this.RangeTrackBar.ValueChanged += new System.EventHandler(this.RangeTrackBar_ValueChanged);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "new";
            legend1.TitleBackColor = System.Drawing.Color.Transparent;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 3);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series1.Legend = "new";
            series1.Name = "new";
            this.chart1.Series.Add(series1);
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
            this.Controls.Add(this.RangeTrackBar);
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Barchart";
            this.Size = new System.Drawing.Size(236, 235);
            this.Load += new System.EventHandler(this.Barchart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Barchart_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.RangeTrackBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RangeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RangeTrackBarControl RangeTrackBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
