using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi.Tools
{
    class ImageManger
    {
        /// <summary>
        /// 图片缩放 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int Mode)
        {
            try
            {
                int newW = bmp.Width / Mode;
                int newH = bmp.Height / Mode;
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                Loghelper.WriteLog("缩小图片成功");
                return b;
            }
            catch (Exception ex )
            {
                Loghelper.WriteLog("缩小图片失败",ex);
                return null;
            }
        }
        /// <summary>
        /// 截取目标范围图片
        /// </summary>
        /// <param name="source">原图片</param>
        /// <param name="section">截取范围</param>
        /// <returns></returns>
        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        /// <summary>
        /// 获取PictureBox在Zoom下显示的位置和大小
        /// </summary>
        /// <param name="p_PictureBox">Picture 如果没有图形或则非ZOOM模式 返回的是PictureBox的大小</param>
        /// <returns>如果p_PictureBox为null 返回 Rectangle(0, 0, 0, 0)</returns>
        public static Rectangle GetPictureBoxZoomSize(PictureBox p_PictureBox)
        {
            if (p_PictureBox != null)
            {
                PropertyInfo _ImageRectanglePropert = p_PictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);

                return (Rectangle)_ImageRectanglePropert.GetValue(p_PictureBox, null);
            }
            return new Rectangle(0, 0, 0, 0);
        }
        private Bitmap cropBmp(Bitmap src, Rectangle cropRect)
        {
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                      cropRect,
                      GraphicsUnit.Pixel);
            }
            return target;
        }

        /// <summary>
        /// 图片缩放并截图
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="pixelNumPerMM">每1mm对应的像素个数</param>
        /// <param name="capturePointIntervalXInMM">设置x轴拍照距离</param>
        /// <param name="singleCaptureWidthInMM">物理拍摄照片宽度</param>
        /// <param name="capturePointIntervalYInMM">设置y轴拍照距离</param>
        /// <returns></returns>
        public Bitmap singleCaptureCropAndResize(Bitmap bitmap,float pixelNumPerMM, float capturePointIntervalXInMM,float singleCaptureWidthInMM,float capturePointIntervalYInMM)
        {
            try
            {
                float cropRatioInWidth = capturePointIntervalXInMM / singleCaptureWidthInMM;
                int widthAfterCrop = (int)((float)(bitmap.Width) * cropRatioInWidth);

                float imageRatio = (float)(bitmap.Height) / (float)(bitmap.Width);
                float singleCaptureHeightInMM = singleCaptureWidthInMM * imageRatio;
                float cropRatioInHeight = capturePointIntervalYInMM / singleCaptureHeightInMM;
                int heightAfterCrop = (int)((float)(bitmap.Height) * cropRatioInHeight);

                int marginX = (bitmap.Width - widthAfterCrop) / 2;
                int marginY = (bitmap.Height - heightAfterCrop) / 2;
                Rectangle cropRect = new Rectangle(marginX, marginY, widthAfterCrop, heightAfterCrop);

                //设置高质量插值法  
                Bitmap cropedBmp = cropBmp(bitmap, cropRect);
                int widthAfterResize = (int)(pixelNumPerMM * capturePointIntervalXInMM * 3);
                int heightAfterResize = (int)(pixelNumPerMM * capturePointIntervalYInMM * 3);
                Bitmap resizedBmp = new Bitmap(widthAfterResize, heightAfterResize, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(resizedBmp);
                //设置高质量插值法  
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度  
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //消除锯齿
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawImage(cropedBmp, new Rectangle(0, 0, widthAfterResize, heightAfterResize), new Rectangle(0, 0, cropedBmp.Width, cropedBmp.Height), GraphicsUnit.Pixel);
                //resizedBmp.Save(reSizePicPath, format);
                g.Dispose();
                //originBmp.Dispose();
                return resizedBmp;
            }
            catch
            {
                return null;

            }


        }
    }
}
