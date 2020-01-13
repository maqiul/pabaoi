using pcbaoi.COM;
using QTing.PLC;
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
    public partial class LightsourceForm : Form
    {
        private int D2000 = 0;
        int wopassway = 0;
        int rpassway = 0;
        int gpassway = 0;
        int bpassway = 0;
        int wfpassway = 0;
        string thisbad;
        public LightsourceForm()
        {
            InitializeComponent();          
            conn();
            panel1_Click(lefttitlebt, null);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            righttitlebt.BackColor = Color.Gray;
            blowbt.Hide();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
            wopassway = Convert.ToInt32(IniFile.iniRead("AW1", "passageway"));
            WotrackBar.Value = Convert.ToInt32(IniFile.iniRead("AW1", "value"));
            rpassway = Convert.ToInt32(IniFile.iniRead("AR", "passageway"));
            RtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AR", "value"));
            gpassway = Convert.ToInt32(IniFile.iniRead("AG", "passageway"));
            GtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AG", "value"));
            bpassway = Convert.ToInt32(IniFile.iniRead("AB", "passageway"));
            BtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AB", "value"));
            wfpassway = Convert.ToInt32(IniFile.iniRead("AW2", "passageway"));
            WftrackBar.Value = Convert.ToInt32(IniFile.iniRead("AW2", "value"));
            thisbad = "A";

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            lefttitlebt.BackColor = Color.Gray;
            blowbt.Show();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
            wopassway = Convert.ToInt32(IniFile.iniRead("BW1", "passageway"));
            WotrackBar.Value = Convert.ToInt32(IniFile.iniRead("BW1", "value"));
            rpassway = Convert.ToInt32(IniFile.iniRead("BR", "passageway"));
            RtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BR", "value"));
            gpassway = Convert.ToInt32(IniFile.iniRead("BG", "passageway"));
            GtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BG", "value"));
            bpassway = Convert.ToInt32(IniFile.iniRead("BB", "passageway"));
            BtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BB", "value"));
            wfpassway = Convert.ToInt32(IniFile.iniRead("BW2", "passageway"));
            WftrackBar.Value = Convert.ToInt32(IniFile.iniRead("BW2", "value"));
            thisbad = "B";
        }

        private void WotrackBar_ValueChanged(object sender, EventArgs e)
        {
           

        }

        private void RtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GtrackBar_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void BtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void WftrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void canelbt_Click(object sender, EventArgs e)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            wopassway = Convert.ToInt32(IniFile.iniRead("AW1", "passageway"));            
            WotrackBar.Value = Convert.ToInt32(IniFile.iniRead("AW1", "value"));
            rpassway = Convert.ToInt32(IniFile.iniRead("AR", "passageway"));
            RtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AR", "value"));
            gpassway = Convert.ToInt32(IniFile.iniRead("AG", "passageway"));
            GtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AG", "value"));
            bpassway = Convert.ToInt32(IniFile.iniRead("AB", "passageway"));
            BtrackBar.Value = Convert.ToInt32(IniFile.iniRead("AB", "value"));
            wfpassway = Convert.ToInt32(IniFile.iniRead("AW2", "passageway"));
            WftrackBar.Value = Convert.ToInt32(IniFile.iniRead("AW2", "value"));
            list1.Add(wopassway);
            list1.Add(rpassway);
            list1.Add(gpassway);
            list1.Add(bpassway);
            list1.Add(wfpassway);
            list2.Add(WotrackBar.Value);
            list2.Add(RtrackBar.Value);
            list2.Add(GtrackBar.Value);
            list2.Add(BtrackBar.Value);
            list2.Add(WftrackBar.Value);
            wopassway = Convert.ToInt32(IniFile.iniRead("BW1", "passageway"));
            WotrackBar.Value = Convert.ToInt32(IniFile.iniRead("BW1", "value"));
            rpassway = Convert.ToInt32(IniFile.iniRead("BR", "passageway"));
            RtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BR", "value"));
            gpassway = Convert.ToInt32(IniFile.iniRead("BG", "passageway"));
            GtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BG", "value"));
            bpassway = Convert.ToInt32(IniFile.iniRead("BB", "passageway"));
            BtrackBar.Value = Convert.ToInt32(IniFile.iniRead("BB", "value"));
            wfpassway = Convert.ToInt32(IniFile.iniRead("BW2", "passageway"));
            WftrackBar.Value = Convert.ToInt32(IniFile.iniRead("BW2", "value"));
            list1.Add(wopassway);
            list1.Add(rpassway);
            list1.Add(gpassway);
            list1.Add(bpassway);
            list1.Add(wfpassway);
            list2.Add(WotrackBar.Value);
            list2.Add(RtrackBar.Value);
            list2.Add(GtrackBar.Value);
            list2.Add(BtrackBar.Value);
            list2.Add(WftrackBar.Value);
            for (int i = 0; i < list1.Count; i++) {
                try
                {

                    byte channel = (byte)list1[i];
                    byte value = (byte)list2[i];

                    if (value > 0xff)
                    {
                        MessageBox.Show("超出设置范围");
                        return;
                    }


                    if (ControlBroad.Instance.IsOpen)
                    {
                        byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                        if (receiveData[0] == 0x55)
                            //MessageBox.Show("设置成功");
                            Loghelper.WriteLog("设置成功");
                        else
                            MessageBox.Show("设置失败");
                    }
                    else
                    {
                        Loghelper.WriteLog("串口连接失败");
                        MessageBox.Show("串口未连接,请连接串口");
                    }

                }
                catch (Exception exp)
                {

                }

            }
            this.Close();
        }

        private void conn()
        {
            try { 
            if (!ControlBroad.Instance.IsOpen)
            {
                    bool b = ControlBroad.Instance.Initialize("COM1", 57600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One, 2000);
                    Loghelper.WriteLog("连接com1成功");                                
            }
            else
            {
                    ControlBroad.Instance.Close();
            }

            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message);
                Loghelper.WriteLog("连接com1失败",exp);
            }

        }

        private void blowbt_Click(object sender,EventArgs e) {


        }

        private void WotrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                byte channel = (byte)wopassway;
                byte value = (byte)WotrackBar.Value;

                if (value > 0xff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                if (ControlBroad.Instance.IsOpen)
                {
                    byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                    if (receiveData[0] == 0x55)
                        //MessageBox.Show("设置成功");
                        Loghelper.WriteLog("设置成功");
                    else
                        MessageBox.Show("设置失败");
                }
                else
                {
                    Loghelper.WriteLog("串口连接失败");
                    MessageBox.Show("串口未连接,请连接串口");
                }

            }
            catch (Exception exp)
            {

            }
        }

        private void RtrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                byte channel = (byte)rpassway;
                byte value = (byte)RtrackBar.Value;

                if (value > 0xff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                if (ControlBroad.Instance.IsOpen)
                {
                    byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                    if (receiveData[0] == 0x55)
                        Loghelper.WriteLog("设置成功");
                    else
                        MessageBox.Show("设置失败");
                }
                else
                {
                    MessageBox.Show("串口未连接,请连接串口");
                }

            }
            catch (Exception exp)
            {

            }

        }

        private void GtrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                byte channel = (byte)gpassway;
                byte value = (byte)GtrackBar.Value;

                if (value > 0xff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                if (ControlBroad.Instance.IsOpen)
                {
                    byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                    if (receiveData[0] == 0x55)
                        Loghelper.WriteLog("设置成功");
                    else
                        MessageBox.Show("设置失败");
                }
                else
                {
                    MessageBox.Show("串口未连接,请连接串口");
                }

            }
            catch (Exception exp)
            {

            }

        }

        private void BtrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                byte channel = (byte)bpassway;
                byte value = (byte)BtrackBar.Value;

                if (value > 0xff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                if (ControlBroad.Instance.IsOpen)
                {
                    byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                    if (receiveData[0] == 0x55)
                        Loghelper.WriteLog("设置成功");
                    else
                        MessageBox.Show("设置失败");
                }
                else
                {
                    MessageBox.Show("串口未连接,请连接串口");
                }

            }
            catch (Exception exp)
            {

            }
        }

        private void WftrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                byte channel = (byte)wfpassway;
                byte value = (byte)WftrackBar.Value;

                if (value > 0xff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                if (ControlBroad.Instance.IsOpen)
                {
                    byte[] receiveData = ControlBroad.Instance.SynSendLightValue(channel, (byte)value);

                    if (receiveData[0] == 0x55)
                        Loghelper.WriteLog("设置成功");
                    else
                        MessageBox.Show("设置失败");
                }
                else
                {
                    MessageBox.Show("串口未连接,请连接串口");
                }

            }
            catch (Exception exp)
            {

            }
        }

        private void savebt_Click(object sender, EventArgs e)
        {
            if (thisbad == "A")
            {
                IniFile.iniWrite("AW1", "value",WotrackBar.Value.ToString());
                IniFile.iniWrite("AR", "value",RtrackBar.Value.ToString());

                IniFile.iniWrite("AG", "value",GtrackBar.Value.ToString());

                IniFile.iniWrite("AB", "value",BtrackBar.Value.ToString());
                IniFile.iniWrite("AW2", "value", WftrackBar.Value.ToString());
            }
            else {
                IniFile.iniWrite("BW1", "value", WotrackBar.Value.ToString());
                IniFile.iniWrite("BR", "value", RtrackBar.Value.ToString());

                IniFile.iniWrite("BG", "value", GtrackBar.Value.ToString());

                IniFile.iniWrite("BB", "value", BtrackBar.Value.ToString());
                IniFile.iniWrite("BW2", "value", WftrackBar.Value.ToString());

            }
        }
    }
}
