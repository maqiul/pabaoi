using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi.Tools
{
    public class BaslerCamera
    {
        private Stopwatch stopWatch = new Stopwatch();
        Camera camera;
        string cameraId;
        private PixelDataConverter converter = new PixelDataConverter();

        // 实现自定义相机回调的方法
        public delegate void OnCameraGrabbedCallback(Object sender, ImageGrabbedEventArgs e);

        // 只获取图片
        public delegate void OnlyGetBitmapCallback(string cameraId, Bitmap bitmap);

        public OnlyGetBitmapCallback onlyGetBitmapCallback;

        private int cameraReconnectTimeOut = 1000 * 300,
            heartbeatTimeout=1000;
        /// <summary>
        /// 相机断开回调 重连
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnConnectionLost(Object sender, EventArgs e)
        {
            ICamera camera = sender as ICamera;
            MessageBox.Show(string.Format("相机 {0} 断开，请重新连接，不用重启软件", camera.CameraInfo[CameraInfoKey.FriendlyName]),
                "提示",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //string name = camera.CameraInfo[CameraInfoKey.FriendlyName];
            //if(camera.CameraInfo[CameraInfoKey.FriendlyName] == camera01.CameraInfo[CameraInfoKey.FriendlyName])
            //{

            //}
            //else if (camera.CameraInfo[CameraInfoKey.FriendlyName] == camera02.CameraInfo[CameraInfoKey.FriendlyName])
            //{

            //}
            #region 相机重连接
            if (!camera.IsConnected)
            {
                // Yes, the camera device has been physically removed.

                // Close the camera object to close underlying resources used for the previous connection.
                camera.Close();

                // Try to re-establish a connection to the camera device until timeout.
                // Reopening the camera triggers the above registered Configuration.AcquireContinous.
                // Therefore, the camera is parameterized correctly again.

                // 等待相机重连这里有时间
                camera.Open(cameraReconnectTimeOut, TimeoutHandling.ThrowException);

                // Due to unplugging the camera, settings have changed, e.g. the heartbeat timeout value for GigE cameras.
                // After the camera has been reconnected, all settings must be restored. This can be done in the CameraOpened
                // event as shown for the Configuration.AcquireContinous.
                camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(heartbeatTimeout, IntegerValueCorrection.Nearest);
                if (camera.IsConnected)
                {
                    if (camera.IsOpen)
                    {
                        MessageBox.Show(string.Format("相机 {0} 已重新连接", camera.CameraInfo[CameraInfoKey.FriendlyName]),
             "提示",
             MessageBoxButtons.OK,
             MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                #endregion
            }


        }

        // 设置触发
        public void Line1Trigger(object sender, EventArgs e)
        {
            ICamera camera = sender as ICamera;
            // Get required Enumerations.
            IEnumParameter triggerSelector = camera.Parameters[PLCamera.TriggerSelector];
            IEnumParameter triggerMode = camera.Parameters[PLCamera.TriggerMode];
            IEnumParameter triggerSource = camera.Parameters[PLCamera.TriggerSource];

            string triggerName = "FrameStart";
            if (triggerSelector.CanSetValue(triggerName))
            {
                try
                {
                    foreach (string trigger in triggerSelector)
                    {
                        triggerSelector.SetValue(trigger);

                        if (triggerName == trigger)
                        {
                            // Activate trigger.
                            triggerMode.SetValue(PLCamera.TriggerMode.On);

                            // Set the trigger source to software.
                            triggerSource.SetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            // Turn trigger mode off.
                            triggerMode.SetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                finally
                {
                    // Set selector for software trigger.
                    triggerSelector.SetValue(triggerName);
                }
            }
            // Set acquisition mode to Continuous
            camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
        }

       
        void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            //if (InvokeRequired)
            //{
            //    // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
            //    // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
            //    BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
            //    return;
            //}

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;
                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                        bitmap.UnlockBits(bmpData);

                        onlyGetBitmapCallback(cameraId, bitmap);
                    }
                }
            }
            catch (Exception exception)
            {
                onlyGetBitmapCallback(cameraId, null);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        public ICameraInfo GetDevice(string camId)
        {
            try
            {
                // Ask the camera finder for a list of camera devices.
                List<ICameraInfo> allCameras = CameraFinder.Enumerate();

                // Loop over all cameras found.
                foreach (ICameraInfo cameraInfo in allCameras)
                {
                    string cameraName = cameraInfo[CameraInfoKey.FriendlyName];
                    if (cameraName.Contains(camId))
                    {
                        return cameraInfo;
                    }
                }
                return null;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="cameraId">相机的用户id，使用前一定要设置相机{正面，反面}</param>
        /// <param name="onlyGetBitmapCallback">只回调图片，更加稳定</param>
        /// <returns></returns>
        public int RunCamera(string cameraId, OnlyGetBitmapCallback myCallback)
        {
            this.cameraId = cameraId;
            onlyGetBitmapCallback = myCallback;
            ICameraInfo ca = GetDevice(cameraId);
            if (ca != null)
            {
                Func<ICameraInfo, int> doS = (cameraInfo) =>
                {
                    try
                    {
                        camera = new Camera(cameraInfo);
                        camera.CameraOpened += Line1Trigger;
                        camera.ConnectionLost += OnConnectionLost;
                        camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                        camera.Open();
                        camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(heartbeatTimeout, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                        return 1;
                    }
                    catch (Exception er)
                    {
                        return 0;
                    }
                };
                return MySmartThreadPool.Instance().QueueWorkItem(doS, ca).GetResult();
            }
            return 0;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="cameraId">相机的用户id，使用前一定要设置相机{正面，反面}</param>
        /// <param name="onCameraGrabbedCallback">重写整个相机回调</param>
        /// <returns></returns>
        public int RunCamera(string cameraId, EventHandler<ImageGrabbedEventArgs> onCameraGrabbedCallback)
        {
            this.cameraId = cameraId;
            ICameraInfo ca = GetDevice(cameraId);
            if (ca != null)
            {
                Func<ICameraInfo, EventHandler<ImageGrabbedEventArgs>,int> doS = (cameraInfo, onCalBack) =>
                {
                    try
                    {
                        camera = new Camera(cameraInfo);
                        camera.CameraOpened += Line1Trigger;
                        camera.ConnectionLost += OnConnectionLost;
                        camera.StreamGrabber.ImageGrabbed += onCalBack;
                        camera.Open();
                        camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(heartbeatTimeout, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                        return 1;
                    }
                    catch (Exception er) {
                        return 0;
                    }
                };
                return MySmartThreadPool.Instance().QueueWorkItem(doS, ca, onCameraGrabbedCallback).GetResult();
            }
            return 0;
        }



        public void Dispose()
        {
            try
            {
                if (camera != null)
                {
                    camera.Close();
                    camera.Dispose();
                    camera = null;
                }
                // Destroy the converter object.
                if (converter != null)
                {
                    converter.Dispose();
                    converter = null;
                }
            }
            catch (Exception exception)
            {

            }
        }

        ~BaslerCamera()
        {
            Dispose();
        }


    }
}
