namespace pcbaoi
{
    partial class Motioncontrol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Motioncontrol));
            this.Titlepanel = new System.Windows.Forms.Panel();
            this.Titlelabel = new System.Windows.Forms.Label();
            this.leftpanel = new System.Windows.Forms.Panel();
            this.Checkbt = new System.Windows.Forms.Button();
            this.Restbt = new System.Windows.Forms.Button();
            this.XYspeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Xup = new System.Windows.Forms.Button();
            this.Ydown = new System.Windows.Forms.Button();
            this.Xdown = new System.Windows.Forms.Button();
            this.Yup = new System.Windows.Forms.Button();
            this.canelbt = new System.Windows.Forms.Button();
            this.savebt = new System.Windows.Forms.Button();
            this.righttitlebt = new System.Windows.Forms.Panel();
            this.sidelabel = new System.Windows.Forms.Label();
            this.lefttitlebt = new System.Windows.Forms.Panel();
            this.frontlabel = new System.Windows.Forms.Label();
            this.lightpicbox = new System.Windows.Forms.PictureBox();
            this.Mainpicbox = new System.Windows.Forms.PictureBox();
            this.Titlepanel.SuspendLayout();
            this.leftpanel.SuspendLayout();
            this.righttitlebt.SuspendLayout();
            this.lefttitlebt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightpicbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mainpicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // Titlepanel
            // 
            this.Titlepanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Titlepanel.Controls.Add(this.Titlelabel);
            this.Titlepanel.Location = new System.Drawing.Point(0, 0);
            this.Titlepanel.Margin = new System.Windows.Forms.Padding(0);
            this.Titlepanel.Name = "Titlepanel";
            this.Titlepanel.Size = new System.Drawing.Size(850, 50);
            this.Titlepanel.TabIndex = 0;
            // 
            // Titlelabel
            // 
            this.Titlelabel.AutoSize = true;
            this.Titlelabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Titlelabel.Location = new System.Drawing.Point(34, 18);
            this.Titlelabel.Name = "Titlelabel";
            this.Titlelabel.Size = new System.Drawing.Size(76, 16);
            this.Titlelabel.TabIndex = 0;
            this.Titlelabel.Text = "运动调试";
            // 
            // leftpanel
            // 
            this.leftpanel.Controls.Add(this.Checkbt);
            this.leftpanel.Controls.Add(this.Restbt);
            this.leftpanel.Controls.Add(this.XYspeed);
            this.leftpanel.Controls.Add(this.label2);
            this.leftpanel.Controls.Add(this.label1);
            this.leftpanel.Controls.Add(this.Xup);
            this.leftpanel.Controls.Add(this.Ydown);
            this.leftpanel.Controls.Add(this.Xdown);
            this.leftpanel.Controls.Add(this.Yup);
            this.leftpanel.Controls.Add(this.canelbt);
            this.leftpanel.Controls.Add(this.savebt);
            this.leftpanel.Controls.Add(this.righttitlebt);
            this.leftpanel.Controls.Add(this.lefttitlebt);
            this.leftpanel.Controls.Add(this.lightpicbox);
            this.leftpanel.Location = new System.Drawing.Point(0, 50);
            this.leftpanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftpanel.Name = "leftpanel";
            this.leftpanel.Size = new System.Drawing.Size(275, 500);
            this.leftpanel.TabIndex = 4;
            // 
            // Checkbt
            // 
            this.Checkbt.Location = new System.Drawing.Point(151, 271);
            this.Checkbt.Name = "Checkbt";
            this.Checkbt.Size = new System.Drawing.Size(75, 23);
            this.Checkbt.TabIndex = 18;
            this.Checkbt.Text = "运动自检";
            this.Checkbt.UseVisualStyleBackColor = true;
            // 
            // Restbt
            // 
            this.Restbt.Location = new System.Drawing.Point(39, 271);
            this.Restbt.Name = "Restbt";
            this.Restbt.Size = new System.Drawing.Size(100, 23);
            this.Restbt.TabIndex = 17;
            this.Restbt.Text = "相机运动到原点";
            this.Restbt.UseVisualStyleBackColor = true;
            this.Restbt.Click += new System.EventHandler(this.Restbt_Click);
            // 
            // XYspeed
            // 
            this.XYspeed.Location = new System.Drawing.Point(138, 204);
            this.XYspeed.Name = "XYspeed";
            this.XYspeed.Size = new System.Drawing.Size(87, 21);
            this.XYspeed.TabIndex = 16;
            this.XYspeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XYspeed_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(55, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "XY运动速度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // Xup
            // 
            this.Xup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Xup.BackgroundImage")));
            this.Xup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Xup.Location = new System.Drawing.Point(161, 100);
            this.Xup.Name = "Xup";
            this.Xup.Size = new System.Drawing.Size(38, 38);
            this.Xup.TabIndex = 13;
            this.Xup.UseVisualStyleBackColor = true;
            // 
            // Ydown
            // 
            this.Ydown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Ydown.BackgroundImage")));
            this.Ydown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Ydown.Location = new System.Drawing.Point(118, 149);
            this.Ydown.Name = "Ydown";
            this.Ydown.Size = new System.Drawing.Size(38, 38);
            this.Ydown.TabIndex = 12;
            this.Ydown.UseVisualStyleBackColor = true;
            // 
            // Xdown
            // 
            this.Xdown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Xdown.BackgroundImage")));
            this.Xdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Xdown.Location = new System.Drawing.Point(72, 100);
            this.Xdown.Name = "Xdown";
            this.Xdown.Size = new System.Drawing.Size(38, 38);
            this.Xdown.TabIndex = 11;
            this.Xdown.UseVisualStyleBackColor = true;
            // 
            // Yup
            // 
            this.Yup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Yup.BackgroundImage")));
            this.Yup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Yup.Location = new System.Drawing.Point(118, 51);
            this.Yup.Name = "Yup";
            this.Yup.Size = new System.Drawing.Size(38, 38);
            this.Yup.TabIndex = 10;
            this.Yup.UseVisualStyleBackColor = true;
            // 
            // canelbt
            // 
            this.canelbt.Location = new System.Drawing.Point(151, 461);
            this.canelbt.Name = "canelbt";
            this.canelbt.Size = new System.Drawing.Size(75, 23);
            this.canelbt.TabIndex = 9;
            this.canelbt.Text = "取消";
            this.canelbt.UseVisualStyleBackColor = true;
            this.canelbt.Click += new System.EventHandler(this.canelbt_Click);
            // 
            // savebt
            // 
            this.savebt.Location = new System.Drawing.Point(45, 461);
            this.savebt.Name = "savebt";
            this.savebt.Size = new System.Drawing.Size(75, 23);
            this.savebt.TabIndex = 8;
            this.savebt.Text = "保存";
            this.savebt.UseVisualStyleBackColor = true;
            // 
            // righttitlebt
            // 
            this.righttitlebt.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.righttitlebt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.righttitlebt.Controls.Add(this.sidelabel);
            this.righttitlebt.Location = new System.Drawing.Point(138, 0);
            this.righttitlebt.Name = "righttitlebt";
            this.righttitlebt.Size = new System.Drawing.Size(137, 25);
            this.righttitlebt.TabIndex = 7;
            this.righttitlebt.Click += new System.EventHandler(this.righttitlebt_Click);
            // 
            // sidelabel
            // 
            this.sidelabel.AutoSize = true;
            this.sidelabel.Location = new System.Drawing.Point(29, 5);
            this.sidelabel.Name = "sidelabel";
            this.sidelabel.Size = new System.Drawing.Size(77, 12);
            this.sidelabel.TabIndex = 1;
            this.sidelabel.Text = "下方运动控制";
            // 
            // lefttitlebt
            // 
            this.lefttitlebt.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lefttitlebt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lefttitlebt.Controls.Add(this.frontlabel);
            this.lefttitlebt.Location = new System.Drawing.Point(0, 0);
            this.lefttitlebt.Name = "lefttitlebt";
            this.lefttitlebt.Size = new System.Drawing.Size(137, 25);
            this.lefttitlebt.TabIndex = 6;
            this.lefttitlebt.Click += new System.EventHandler(this.lefttitlebt_Click);
            // 
            // frontlabel
            // 
            this.frontlabel.AutoSize = true;
            this.frontlabel.Location = new System.Drawing.Point(33, 6);
            this.frontlabel.Name = "frontlabel";
            this.frontlabel.Size = new System.Drawing.Size(77, 12);
            this.frontlabel.TabIndex = 0;
            this.frontlabel.Text = "上方运动控制";
            // 
            // lightpicbox
            // 
            this.lightpicbox.Location = new System.Drawing.Point(0, 303);
            this.lightpicbox.Margin = new System.Windows.Forms.Padding(0);
            this.lightpicbox.Name = "lightpicbox";
            this.lightpicbox.Size = new System.Drawing.Size(275, 142);
            this.lightpicbox.TabIndex = 0;
            this.lightpicbox.TabStop = false;
            // 
            // Mainpicbox
            // 
            this.Mainpicbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Mainpicbox.Location = new System.Drawing.Point(276, 50);
            this.Mainpicbox.Margin = new System.Windows.Forms.Padding(0);
            this.Mainpicbox.Name = "Mainpicbox";
            this.Mainpicbox.Size = new System.Drawing.Size(574, 500);
            this.Mainpicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Mainpicbox.TabIndex = 3;
            this.Mainpicbox.TabStop = false;
            // 
            // Motioncontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.leftpanel);
            this.Controls.Add(this.Mainpicbox);
            this.Controls.Add(this.Titlepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Motioncontrol";
            this.Text = "Motioncontrol";
            this.Titlepanel.ResumeLayout(false);
            this.Titlepanel.PerformLayout();
            this.leftpanel.ResumeLayout(false);
            this.leftpanel.PerformLayout();
            this.righttitlebt.ResumeLayout(false);
            this.righttitlebt.PerformLayout();
            this.lefttitlebt.ResumeLayout(false);
            this.lefttitlebt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightpicbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mainpicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Titlepanel;
        private System.Windows.Forms.Label Titlelabel;
        private System.Windows.Forms.Panel leftpanel;
        private System.Windows.Forms.Button canelbt;
        private System.Windows.Forms.Button savebt;
        private System.Windows.Forms.Panel righttitlebt;
        private System.Windows.Forms.Label sidelabel;
        private System.Windows.Forms.Panel lefttitlebt;
        private System.Windows.Forms.Label frontlabel;
        private System.Windows.Forms.PictureBox lightpicbox;
        private System.Windows.Forms.PictureBox Mainpicbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Xup;
        private System.Windows.Forms.Button Ydown;
        private System.Windows.Forms.Button Xdown;
        private System.Windows.Forms.Button Yup;
        private System.Windows.Forms.TextBox XYspeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Checkbt;
        private System.Windows.Forms.Button Restbt;
    }
}