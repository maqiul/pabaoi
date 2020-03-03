using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class SaveFileForm : Form
    {
        public SaveFileForm()
        {
            InitializeComponent();
        }

        private void projectsavepath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.Windows;
          
            dialog.Description = "请选择保存项目文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                projectsavepath.Text = dialog.SelectedPath + "\\";


            }

        }

        private void allpicpath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.Windows;

            dialog.Description = "请选择全部图片保存文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                allpicpath.Text = dialog.SelectedPath + "\\";


            }
        }

        private void fillpicpath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.Windows;

            dialog.Description = "请选择保存项目文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                fillpicpath.Text = dialog.SelectedPath + "\\";


            }
        }

        private void Canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void projectsavepath_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(projectsavepath, projectsavepath.Text);
        }

        private void allpicpath_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(allpicpath, allpicpath.Text);
        }

        private void fillpicpath_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(fillpicpath, fillpicpath.Text);

        }

        private void Savebt_Click(object sender, EventArgs e)
        {
            if (Savecheck.Checked)
            {



            }
            else { 
            
            
            
            }
        }

        private void allpicpath_TextChanged(object sender, EventArgs e)
        {
            Savecheck.Enabled = true;

        }
    }
}
