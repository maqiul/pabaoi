using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public class FileHandler
    {
        /// <summary>
        /// 最近文件菜单项
        /// </summary>
        public ToolStripMenuItem RecentFileMenu { get; set; }

        private StringCollection fileList;
        private StringCollection filetimelist;

        private int fileNumbers;

        public FileHandler()
        {
            fileNumbers = Properties.Settings.Default.FileNember;
            fileList = Properties.Settings.Default.FilePaths;
            if (fileList == null)
            {
                fileList = new StringCollection();
            }
            filetimelist = Properties.Settings.Default.Filetimes;
            if (filetimelist == null)
            {
                filetimelist = new StringCollection();
            }

        }


        /// <summary>
        /// 更新最近菜单单项
        /// </summary>
        public void UpdateMenu()
        {
            if (RecentFileMenu == null) return;
            int i;
            //清除当前菜单项
            for (i = RecentFileMenu.DropDownItems.Count - 1; i >= 0; i--)
            {
                RecentFileMenu.DropDownItems.RemoveAt(i);
            }

            for (i = 0; i < fileList.Count; i++)
            {
                ToolStripItem menuItem = new ToolStripMenuItem();
                menuItem.Text = fileList[i];
                menuItem.Tag = fileList[i];
                menuItem.Click += menuItem_Click;

                RecentFileMenu.DropDownItems.Add(menuItem);
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            //点击最近打开菜单项要执行的动作。
        }


        /// <summary>
        /// 添加最近文件路径(每次打开文件时，调用该方法)
        /// </summary>
        /// <param name="filePath"></param>
        public void AddRecentFile(string filePath)
        {
            fileList.Insert(0, filePath);
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            filetimelist.Insert(0,datetime);
            int num =0;

            //从最后位置开始倒着找，如果找到一致名称，则移除旧记录
            for (int i = fileList.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (fileList[i] == fileList[j])
                    {
                        fileList.RemoveAt(i);
                        num = 0;
                        break;
                    }
                }
            }
            if (num !=0) {
                filetimelist.RemoveAt(num);
            }


            //最后，仅保留指定的文件列表数量
            for (int bynd = fileList.Count - 1; bynd > fileNumbers - 1; bynd--)
            {
                fileList.RemoveAt(bynd);
            }
            //最后，仅保留指定的文件列表数量
            for (int bynd = filetimelist.Count - 1; bynd > fileNumbers - 1; bynd--)
            {
                filetimelist.RemoveAt(bynd);
            }

            Properties.Settings.Default.FilePaths = fileList;
            Properties.Settings.Default.Filetimes = filetimelist;
            Properties.Settings.Default.Save();

            UpdateMenu();
        }

    }
}
