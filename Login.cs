using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string haveusersql = string.Format("select count(*) as count from users where username = '{0}' and password = '{1}'",username, GenerateMD5(password));
            int usernum = MySqlHelper.GetCount(haveusersql);
            if (usernum == 1)
            {
                Form1 form1 = new Form1(1);
                form1.Show();
                //Platmake platmake = new Platmake();
                //platmake.Show();
                //RunForm runForm = new RunForm();
                //runForm.Show();
                this.Hide();
                


            }
            else {
                MessageBox.Show("用户名 密码错误");
            
            
            }
        }
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
