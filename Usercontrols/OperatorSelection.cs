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
    public partial class OperatorSelection : UserControl
    {


        string newtypename;
        string Operatorname;

        private string value;
        OperatorSelect operatorselect = new OperatorSelect();


        public string Value { get => value; set => this.value = value; }

        public OperatorSelection(object op)
        {
            InitializeComponent();
            
            operatorselect = (OperatorSelect)op;
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
        private void Operatorcm_SelectedIndexChanged(object sender, EventArgs e)
        {


                operatorselect.Operatorname = Operatorcm.SelectedItem.ToString();



                        
            if (MyEvent != null)
            {
                MyEvent(operatorselect,e);
            }
        }

        public void controlstart() {
            if (newtypename == "金手指" )
            {
                string[] combox = {  "边缘查找" };
                Operatorcm.Items.AddRange(combox);
                Aiadd aiadd = new Aiadd(operatorselect);
                operatorselect.Confidence = aiadd.Tag.ToString();
                aiadd.MyEvent += aiaddenvent;
                panel6.Controls.Add(aiadd);

                Operatorcm.SelectedIndex = 0;
                if (operatorselect.Operatorname != Operatorname && Operatorname != null)
                {
                    Operatorcm.SelectedItem = Operatorname;
                }

                operatorselect.Operatorname = Operatorcm.SelectedItem.ToString();

            }
            else if (newtypename == "线路") {
                string[] combox = {  "边缘查找" };
                Operatorcm.Items.AddRange(combox);
                PercentageA aiadd = new PercentageA(operatorselect);
                OperatorSelect bigoperatorselect = aiadd.Tag as OperatorSelect;
                operatorselect.Percentageup = bigoperatorselect.Percentageup;
                operatorselect.Percentagedown = bigoperatorselect.Percentagedown;
                aiadd.MyEvent += usercontrl2envent;
                panel6.Controls.Add(aiadd);
                Barchart barchart = new Barchart(operatorselect);
                bigoperatorselect = barchart.Tag as OperatorSelect;
                operatorselect.Luminanceon = bigoperatorselect.Luminanceon;
                operatorselect.Luminancedown = bigoperatorselect.Luminancedown;
                barchart.MyEvent += Bevent;
                panel7.Controls.Add(barchart);
                ColorSpace userControl6 = new ColorSpace(operatorselect);
                bigoperatorselect = userControl6.Tag as OperatorSelect;
                operatorselect.Rednumon = bigoperatorselect.Rednumon;
                operatorselect.Rednumdown = bigoperatorselect.Rednumdown;
                operatorselect.Greennumon = bigoperatorselect.Greennumon;
                operatorselect.Greennumdown = bigoperatorselect.Greennumdown;
                operatorselect.Bluenumon = bigoperatorselect.Bluenumon;
                operatorselect.Bluenumdown = bigoperatorselect.Bluenumdown;
                userControl6.MyEvent += usercontrl6event;
                panel8.Controls.Add(userControl6);

                Operatorcm.SelectedIndex = 0;
                operatorselect.Operatorname = Operatorcm.SelectedItem.ToString();


            }
            else if (newtypename == "BAD MARK" || newtypename == "MARK")
            {
                string[] combox = { "mark" };
                Operatorcm.Items.AddRange(combox);
                PercentageA aiadd = new PercentageA(operatorselect);
                OperatorSelect bigoperatorselect = aiadd.Tag as OperatorSelect;
                operatorselect.Percentageup = bigoperatorselect.Percentageup;
                operatorselect.Percentagedown = bigoperatorselect.Percentagedown;
                aiadd.MyEvent += usercontrl2envent;
                panel6.Controls.Add(aiadd);
                Barchart barchart = new Barchart(operatorselect);
                bigoperatorselect = barchart.Tag as OperatorSelect;
                operatorselect.Luminanceon = bigoperatorselect.Luminanceon;
                operatorselect.Luminancedown = bigoperatorselect.Luminancedown;
                barchart.MyEvent += Bevent;
                panel7.Controls.Add(barchart);
                ColorSpace userControl6 = new ColorSpace(operatorselect);
                bigoperatorselect = userControl6.Tag as OperatorSelect;
                operatorselect.Rednumon = bigoperatorselect.Rednumon;
                operatorselect.Rednumdown = bigoperatorselect.Rednumdown;
                operatorselect.Greennumon = bigoperatorselect.Greennumon;
                operatorselect.Greennumdown = bigoperatorselect.Greennumdown;
                operatorselect.Bluenumon = bigoperatorselect.Bluenumon;
                operatorselect.Bluenumdown = bigoperatorselect.Bluenumdown;
                userControl6.MyEvent += usercontrl6event;
                panel8.Controls.Add(userControl6);

                Operatorcm.SelectedIndex = 0;
                operatorselect.Operatorname = Operatorcm.SelectedItem.ToString();



            }
            else
            {
                string[] combox = { "条码检测" };
                Operatorcm.Items.AddRange(combox);
                Code aiadd = new Code(operatorselect);
                OperatorSelect bigoperatorselect = aiadd.Tag as OperatorSelect;
                operatorselect.Percentageup = bigoperatorselect.Percentageup;
                operatorselect.Percentagedown = bigoperatorselect.Percentagedown;
                operatorselect.Codetype = bigoperatorselect.Codetype;
                aiadd.MyEvent += usercontrl3envent;
                panel6.Controls.Add(aiadd);
                Barchart barchart = new Barchart(operatorselect);
                bigoperatorselect = barchart.Tag as OperatorSelect;
                operatorselect.Luminanceon = bigoperatorselect.Luminanceon;
                operatorselect.Luminancedown = bigoperatorselect.Luminancedown;
                barchart.MyEvent += Bevent;
                panel7.Controls.Add(barchart);
                ColorSpace userControl6 = new ColorSpace(operatorselect);
                bigoperatorselect = userControl6.Tag as OperatorSelect;
                operatorselect.Rednumon = bigoperatorselect.Rednumon;
                operatorselect.Rednumdown = bigoperatorselect.Rednumdown;
                operatorselect.Greennumon = bigoperatorselect.Greennumon;
                operatorselect.Greennumdown = bigoperatorselect.Greennumdown;
                operatorselect.Bluenumon = bigoperatorselect.Bluenumon;
                operatorselect.Bluenumdown = bigoperatorselect.Bluenumdown;
                userControl6.MyEvent += usercontrl6event;
                panel8.Controls.Add(userControl6);
                Operatorcm.SelectedIndex = 0;
                operatorselect.Operatorname = Operatorcm.SelectedItem.ToString();

            }

        }
        void aiaddenvent(object sender, EventArgs e)
        {
            operatorselect.Confidence = sender.ToString();
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
        private void usercontrl2envent(object sender, EventArgs e)
       {
            OperatorSelect newoperatorselect = (OperatorSelect)sender;
            operatorselect.Percentageup = newoperatorselect.Percentageup;
            operatorselect.Percentagedown = newoperatorselect.Percentagedown;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
        private void usercontrl3envent(object sender, EventArgs e)
        {
            OperatorSelect newoperatorselect = (OperatorSelect)sender;
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
            OperatorSelect newoperatorselect = (OperatorSelect)sender;
            operatorselect.Luminanceon = newoperatorselect.Luminanceon;
            operatorselect.Luminancedown = newoperatorselect.Luminancedown;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }



        }
        private void usercontrl6event(object sender, EventArgs e)
        {
            OperatorSelect newoperatorselect = (OperatorSelect)sender;
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
