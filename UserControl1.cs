using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pcbaoi;

namespace pcbaoi
{
    public partial class UserControl1 : UserControl
    {


        string newtypename;
        string Operatorname;

        private string value;
        Operatorselect operatorselect = new Operatorselect();


        public string Value { get => value; set => this.value = value; }

        public UserControl1(object op)
        {
            InitializeComponent();
            
            operatorselect = (Operatorselect)op;
            newtypename = operatorselect.Algorithm;
            Operatorname = operatorselect.Operatorname;
            controlstart();
            this.Tag = operatorselect;

        }



        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


                operatorselect.Operatorname = comboBox1.SelectedItem.ToString();



                        
            if (MyEvent != null)
            {
                MyEvent(operatorselect,e);
            }
        }

        public void controlstart() {
            if (newtypename == "金手指" || newtypename == "线路")
            {
                string[] combox = { "AI检测", "边缘查找" };
                comboBox1.Items.AddRange(combox);
                Aiadd aiadd = new Aiadd(operatorselect);
                operatorselect.Confidence = float.Parse(aiadd.Tag.ToString());
                aiadd.MyEvent += aiaddenvent;
                panel6.Controls.Add(aiadd);

                comboBox1.SelectedIndex = 0;
                if (operatorselect.Operatorname != Operatorname&&Operatorname!=null)
                {
                    comboBox1.SelectedItem = Operatorname;
                }

                operatorselect.Operatorname = comboBox1.SelectedItem.ToString();

            }
            else if (newtypename == "BAD MARK" || newtypename == "MARK")
            {
                string[] combox = { "mark" };
                comboBox1.Items.AddRange(combox);
                UserControl2 aiadd = new UserControl2(operatorselect);
                Operatorselect bigoperatorselect = aiadd.Tag as Operatorselect;
                operatorselect.Percentageup = bigoperatorselect.Percentageup;
                operatorselect.Percentagedown = bigoperatorselect.Percentagedown;
                aiadd.MyEvent += usercontrl2envent;
                panel6.Controls.Add(aiadd);
                Barchart barchart = new Barchart(operatorselect);
                bigoperatorselect = barchart.Tag as Operatorselect;
                operatorselect.Luminanceon = bigoperatorselect.Luminanceon;
                operatorselect.Luminancedown = bigoperatorselect.Luminancedown;
                barchart.MyEvent += Bevent;
                panel7.Controls.Add(barchart);
                UserControl6 userControl6 = new UserControl6(operatorselect);
                bigoperatorselect = userControl6.Tag as Operatorselect;
                operatorselect.Rednumon = bigoperatorselect.Rednumon;
                operatorselect.Rednumdown = bigoperatorselect.Rednumdown;
                operatorselect.Greennumon = bigoperatorselect.Greennumon;
                operatorselect.Greennumdown = bigoperatorselect.Greennumdown;
                operatorselect.Bluenumon = bigoperatorselect.Bluenumon;
                operatorselect.Bluenumdown = bigoperatorselect.Bluenumdown;
                userControl6.MyEvent += usercontrl6event;
                panel8.Controls.Add(userControl6);

                comboBox1.SelectedIndex = 0;
                operatorselect.Operatorname = comboBox1.SelectedItem.ToString();



            }
            else
            {
                string[] combox = { "条码检测" };
                comboBox1.Items.AddRange(combox);
                UserControl3 aiadd = new UserControl3(operatorselect);
                Operatorselect bigoperatorselect = aiadd.Tag as Operatorselect;
                operatorselect.Percentageup = bigoperatorselect.Percentageup;
                operatorselect.Percentagedown = bigoperatorselect.Percentagedown;
                operatorselect.Codetype = bigoperatorselect.Codetype;
                aiadd.MyEvent += usercontrl3envent;
                panel6.Controls.Add(aiadd);
                Barchart barchart = new Barchart(operatorselect);
                bigoperatorselect = barchart.Tag as Operatorselect;
                operatorselect.Luminanceon = bigoperatorselect.Luminanceon;
                operatorselect.Luminancedown = bigoperatorselect.Luminancedown;
                barchart.MyEvent += Bevent;
                panel7.Controls.Add(barchart);
                UserControl6 userControl6 = new UserControl6(operatorselect);
                bigoperatorselect = userControl6.Tag as Operatorselect;
                operatorselect.Rednumon = bigoperatorselect.Rednumon;
                operatorselect.Rednumdown = bigoperatorselect.Rednumdown;
                operatorselect.Greennumon = bigoperatorselect.Greennumon;
                operatorselect.Greennumdown = bigoperatorselect.Greennumdown;
                operatorselect.Bluenumon = bigoperatorselect.Bluenumon;
                operatorselect.Bluenumdown = bigoperatorselect.Bluenumdown;
                userControl6.MyEvent += usercontrl6event;
                panel8.Controls.Add(userControl6);
                comboBox1.SelectedIndex = 0;
                operatorselect.Operatorname = comboBox1.SelectedItem.ToString();

            }

        }
        void aiaddenvent(object sender, EventArgs e)
        {
            operatorselect.Confidence = float.Parse(sender.ToString());
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
        private void usercontrl2envent(object sender, EventArgs e)
       {
            Operatorselect newoperatorselect = (Operatorselect)sender;
            operatorselect.Percentageup = newoperatorselect.Percentageup;
            operatorselect.Percentagedown = newoperatorselect.Percentagedown;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
        private void usercontrl3envent(object sender, EventArgs e)
        {
            Operatorselect newoperatorselect = (Operatorselect)sender;
            operatorselect.Percentageup = newoperatorselect.Percentageup;
            operatorselect.Percentagedown = newoperatorselect.Percentagedown;
            operatorselect.Codetype = newoperatorselect.Codetype;
            operatorselect.Confidence = newoperatorselect.Confidence;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
        private void Bevent(object sender, EventArgs e) {
            Operatorselect newoperatorselect = (Operatorselect)sender;
            operatorselect.Luminanceon = newoperatorselect.Luminanceon;
            operatorselect.Luminancedown = newoperatorselect.Luminancedown;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }



        }
        private void usercontrl6event(object sender, EventArgs e)
        {
            Operatorselect newoperatorselect = (Operatorselect)sender;
            operatorselect.Rednumon = newoperatorselect.Rednumon;
            operatorselect.Rednumdown = newoperatorselect.Rednumdown;
            operatorselect.Greennumon = newoperatorselect.Greennumon;
            operatorselect.Greennumdown = newoperatorselect.Greennumdown;
            operatorselect.Bluenumon = newoperatorselect.Bluenumon;
            operatorselect.Bluenumdown = newoperatorselect.Bluenumdown;
            operatorselect.Confidence = newoperatorselect.Confidence;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }



        }

    }
}
