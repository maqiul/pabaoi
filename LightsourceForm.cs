using pcbaoi.COM;
using pcbaoi.Tools;
using PylonC.NETSupportLibrary;
using QTing.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static string cameraAid;
        public static string cameraBid;
        string thisbad ="A";
        BaslerCamera aa;
        #region camera
        private Tools.ImageProvider m_imageProvider = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmap = null;
       
        private int D2001 = 0;
        private int D2002 = 0;
        private int D2003 = 0;
        private int D2004 = 0;


        /* Stops the image provider and handles exceptions. */
        public void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Handles the click on the stop frame acquisition button. */
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop(); /* Stops the grabbing of images. */
        }

        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback), grabException, additionalErrorMessage);
                return;
            }

            Loghelper.WriteLog(additionalErrorMessage, grabException);
        }

        /* Handles the event related to the removal of a currently open device. */
        private void OnDeviceRemovedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
                return;
            }
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
                return;
            }
        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
                return;
            }
        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
                return;
            }

        }

        /* Handles the event related to an image having been taken and waiting for processing. */
        private void OnImageReadyEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
                return;
            }

            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */
                        Mainpicbox.Image = m_bitmap;

                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = Mainpicbox.Image as Bitmap;
                        /* Provide the display control with the new bitmap. This action automatically updates the display. */
                        Mainpicbox.Image = m_bitmap;

                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
                        }
                    }
                    /* The processing of the image is done. Release the image buffer. */
                    // 
                    m_imageProvider.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
                return;
            }

        }

        #endregion
        public LightsourceForm()
        {
            InitializeComponent();
            cameraAid = IniFile.iniRead("CameraA", "SerialNumber");
            cameraBid = IniFile.iniRead("CameraB", "SerialNumber");
            conn();
            panel1_Click(lefttitlebt, null);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
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
                Closelight();
                thisbad = "A";
                Openlight();
                if (m_imageProvider.CameraId == cameraBid)
                {
                    Stop();
                    CloseTheImageProvider();

                }
                m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                m_imageProvider.CameraId = cameraAid;
                uint id = m_imageProvider.GetDevice(cameraAid);
                if (id == 99)
                {
                    MessageBox.Show("未连接相机");
                }
                else
                {
                    m_imageProvider.newOpen(id);
                    m_imageProvider.ContinuousShot();
                }

            }
            catch {


            }


        }

        private void panel2_Click(object sender, EventArgs e)
        {
            try
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
                Closelight();
                thisbad = "B";
                Openlight();
                if (m_imageProvider.CameraId == cameraAid)
                {
                    Stop();
                    CloseTheImageProvider();

                }
                m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                m_imageProvider.CameraId = cameraBid;
                uint id = m_imageProvider.GetDevice(cameraBid);
                if (id == 99)
                {
                    MessageBox.Show("未连接相机");
                }
                else
                {
                    m_imageProvider.newOpen(id);
                    m_imageProvider.ContinuousShot();
                }
            }
            catch {


            }

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
            Stop();
            CloseTheImageProvider();
            Closelight();
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

        private void Openlight()
        {
            try
            {
                if (thisbad == "A")
                {
                    int[] registerBitall = { 0, 1, 2, 3, 4 };
                    D2003 = 0;
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2003;
                        int registerBit = i;
                        int value = 1 << registerBit;


                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2003)
                        {
                            D2003 = D2003 + value;
                            currentValue = D2003;
                            SendValueToRegister(2003, D2003, receiveData);
                        }
                        SendValueToRegister(2003, D2003, receiveData);
                        Thread.Sleep(30);
                    }

                }
                else
                {
                    int[] registerBitall = { 5, 6, 7, 8, 9 };
                    D2003 = 0;
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2003;
                        int registerBit = i;
                        int value = 1 << registerBit;


                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2003)
                        {
                            D2003 = D2003 + value;
                            currentValue = D2003;
                            SendValueToRegister(2003, D2003, receiveData);
                        }
                        SendValueToRegister(2003, D2003, receiveData);
                        Thread.Sleep(30);
                    }

                }
            }
            catch (Exception e)
            {

                Loghelper.WriteLog("开灯失败", e);
            }


        }
        private void Closelight()
        {
            try
            {
                if (thisbad == "A")
                {
                    int[] registerBitall = { 0, 1, 2, 3, 4 };
                    D2003 = 0;
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2003;
                        int registerBit = i;
                        int value = 0 << registerBit;


                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2003)
                        {
                            D2003 = D2003 + value;
                            currentValue = D2003;
                            SendValueToRegister(2003, D2003, receiveData);
                        }
                        SendValueToRegister(2003, D2003, receiveData);
                        Thread.Sleep(30);
                    }

                }
                else
                {
                    int[] registerBitall = { 5, 6, 7, 8, 9 };
                   
                    D2003 = 0;
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2003;
                        int registerBit = i;
                        int value = 0 << registerBit;


                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2003)
                        {
                            D2003 = D2003 + value;
                            currentValue = D2003;
                            SendValueToRegister(2003, D2003, receiveData);
                        }
                        SendValueToRegister(2003, D2003, receiveData);
                        Thread.Sleep(30);
                    }

                }
            }
            catch (Exception e)
            {
                Loghelper.WriteLog("关灯失败", e);

            }


        }
        private void SendValueToRegister(int registerAddress, int value, byte[] receiveData)
        {
            try
            {
                byte[] writeValue = new byte[2] { (byte)(value / 256), (byte)(value % 256) };
                //byte[] receiveData = new byte[255];
                if (PLCController.Instance.IsConnected)
                    PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);
            }
            catch (Exception exp)
            {

            }
        }
    }
}
