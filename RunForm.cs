using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pcbaoi.Properties;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class RunForm : Form
    {
        Snowflake snowflake = new Snowflake(2);
        string path;
        FileInfo fileInfonew;
        public RunForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long i = snowflake.nextId();
            CopyDir(@"f:\ftp\",@"f:\"+ i+"\\");
            
            
            //本机要上传的目录的父目录
            string localPath = @"F:\";
            //要上传的目录名
            string fileName = @i.ToString();

            Ftp.UploadDirectory(localPath, fileName);
            JsonData<Pcb> jsonData = new JsonData<Pcb>();
            List<Result> results = new List<Result>();
            Snowflake snowflake2 = new Snowflake(3);
            Result result = new Result();
            result.Id =snowflake2.nextId().ToString() ;
            result.IsBack = 0;
            result.PcbId = i.ToString();
            result.Area = "";
            result.Region = "1855";
            result.NgType = "缺陷1";
            result.PartImagePath = "front.jpg";
            result.CreateTime = DateTime.Now;
            results.Add(result);
            Pcb pcb = new Pcb();
            pcb.Id = i.ToString();
            pcb.PcbNumber = "4";
            pcb.PcbName = "testpcb";
            pcb.PcbWidth = 123;
            pcb.PcbHeight = 123;
            pcb.PcbChildenNumber = 8;
            pcb.SurfaceNumber = 1;
            pcb.PcbPath = i.ToString();
            pcb.IsError = 1;
            pcb.CreateTime = DateTime.Now;
            pcb.results = results;
            jsonData.data = pcb;
            string jo = JsonConvert.SerializeObject(jsonData);
            var factory = new ConnectionFactory();
            factory.AutomaticRecoveryEnabled = true;
            factory.HostName = "192.168.31.157"; // RabbitMQ服务在本地运行
            factory.UserName = "admin"; // 用户名
            factory.Password = "admin"; // 密码
            factory.VirtualHost = "my_vhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 将消息标记为持久性。
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.QueueDeclare("work", true, false, false, null); // 创建一个名称为hello的消息队列
                    string message = jo; // 传递的消息内容
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "work", properties, body); // 开始传递
                    //MessageBox.Show("已发送： {0}", message);
                }
            }


        }

        private void panel3_Click(object sender, EventArgs e)
        {
            
            panel3.BackColor = Color.DarkGray;
            panel4.BackColor = panel1.BackColor;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.DarkGray;
            panel3.BackColor = panel1.BackColor;
        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
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

                path = dialog.SelectedPath + "\\";
                DirectoryInfo folder = new DirectoryInfo(path);
                
                foreach (FileInfo file in folder.GetFiles("*.db"))
                {
                    fileInfonew = file;
                    
                }
                if (fileInfonew == null)
                {
                    MessageBox.Show("请选择正确文件,保证文件夹下包含 .db文件");
                    return;
                }
                else {
                    textBox1.Text = path;
                    Settings.Default.dbpath = fileInfonew.FullName;
                    string selectsql = "select * from bad";
                    DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
                    if (dataTable.Rows.Count > 0)
                    {
                        label6.Text = "pcb名称:"+dataTable.Rows[0]["badname"].ToString();
                        label7.Text = "pcb板宽:"+dataTable.Rows[0]["badwidth"].ToString();
                        label10.Text = "pcb板长:" + dataTable.Rows[0]["badheight"].ToString();
                    }
                    string zijibansql = "select *  from zijiban";
                    DataTable dataTable2 = SQLiteHelper.GetDataTable(zijibansql);
                    label9.Text = "子基板数:" + dataTable2.Rows.Count;

                    string zijibanfrontsql = "select *  from zijiban group by frontorside";
                    DataTable dataTable3 = SQLiteHelper.GetDataTable(zijibanfrontsql);
                    label8.Text = "子基板数:" + dataTable3.Rows.Count;
                }



            }
        }
    }
}
