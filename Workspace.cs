﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using pcbaoi.Properties;
using pcbaoi.usercontrol;

namespace pcbaoi
{
    public partial class Workspace : Form
    {
        string path;
        bool isclose = true;
        FileHandler filehandler;
        public Workspace()
        {
            InitializeComponent();
            filehandler = new FileHandler();
            loadsetting();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "请选择保存图片文件夹";
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (string.IsNullOrEmpty(dialog.SelectedPath))
            //    {
            //        MessageBox.Show(this, "文件夹路径不能为空", "提示");
            //        return;
            //    }
            //    textBox3.Text = dialog.SelectedPath + "\\";
            //}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.Windows;
            path = Setting.Projectpath.Substring(0, Setting.Projectpath.Length - 1);
            dialog.SelectedPath = @path;
            dialog.Description = "请选择保存项目文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                path = dialog.SelectedPath +"\\";
                filehandler.AddRecentFile(path);
                Settings.Default.path = path;
                this.DialogResult = DialogResult.OK;
                isclose = false;
                this.Close();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //path = textBox3.Text + textBox1.Text+"\\";
            //if (Directory.Exists(path))
            //{
            //    MessageBox.Show("文件夹已存在");
            //    return;

            //}
            //else {
            //    Directory.CreateDirectory(path);
            //    filehandler.AddRecentFile(path);
            //    this.DialogResult = DialogResult.OK;
            //    Settings.Default.path = path;
            //    isclose = false;
            //    this.Close();


            //}

        }

        private void Workspace_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isclose) {
                Application.Exit();
            }

        }

        private void Workspace_Load(object sender, EventArgs e)
        {
            History history = new History();
            history.MyEvent += workhistor;
            
            panel1.Controls.Add(history);
        }

        void workhistor(object sender, EventArgs e) {

            Settings.Default.path = sender.ToString();
            this.DialogResult = DialogResult.OK;
            isclose = false;
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.RemoveAt(0);
            Savecontrol savecontrol = new Savecontrol();
            savecontrol.MyEvent += projectsettingall;
            panel1.Controls.Add(savecontrol);
        }
        void projectsettingall(object sender, EventArgs e) {
            ProjectSetting projectSetting = (ProjectSetting)sender;
            path = Setting.Projectpath +projectSetting.Name+ "\\";
            if (Directory.Exists(path))
            {
                MessageBox.Show("文件夹已存在");
                return;

            }
            else {
                Directory.CreateDirectory(path);
                filehandler.AddRecentFile(path);
                this.DialogResult = DialogResult.OK;
                Settings.Default.path = path;
                string OrignFile, NewFile;
                OrignFile = @"demo.db";
                NewFile = path+projectSetting.Name+".db";
                Settings.Default.dbpath = NewFile;
                File.Copy(OrignFile, NewFile, true);
                string insertsql = string.Format("INSERT INTO bad(badname, badwidth, badheight, badjiajin) VALUES ('{0}', '{1}', '{2}', '{3}')", projectSetting.Name,projectSetting.Width, projectSetting.Height,projectSetting.Nip);
                SQLiteHelper.ExecuteSql(insertsql);
                isclose = false;
                this.Close();


            }


        }

        private void loadsetting() {

            string picsettingsql = string.Format("select * from setting");
            DataRowCollection picseeting = MySqlHelper.GetallRow(picsettingsql);
            Setting.Projectpath = picseeting[0]["settingvalue"].ToString();
            Setting.Completepicturepath = picseeting[1]["settingvalue"].ToString();





        }


    }
}
