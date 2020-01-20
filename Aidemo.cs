﻿using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Aidemo
    {
        private int sdkin()
        {

            // VS要用管理员权限打开
            // VS要用管理员权限打开
            // VS要用管理员权限打开

            //初始化检测器
            return AITestSDK.init(@"F:\newdemoall\newdemoall\bin\x64\Debug\suomi\data\config.data",
                 // 特别说明，这里的路径一定要用 '/' 不能用反斜杠
                 // 特别说明，这里的路径一定要用 '/' 不能用反斜杠
                 // 特别说明，这里的路径一定要用 '/' 不能用反斜杠
                 "F:/newdemoall/newdemoall/bin/x64/Debug/suomi/data/voc.weights",
                 0);

        }
        //bitmap转byte
        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        //检测错误保存文件
        public bool savepic(Bitmap bitmap, String path)
        {

            bbox_t_container boxlist = new bbox_t_container();
            bool ng;
            byte[] byteImg = Bitmap2Byte(bitmap);
            long intime = DateTimeUtil.DateTimeToLongTimeStamp();
            int n = AITestSDK.detect_opencv_mat(byteImg, byteImg.Length, ref boxlist);
            long endtime = DateTimeUtil.DateTimeToLongTimeStamp();
            long gettime = endtime - intime;
            Console.WriteLine(gettime);

            if (n == -1)
            {
                Loghelper.WriteLog("调用失败，请检测目录是否包含opencv的dll");
            }
            if (boxlist.bboxlist.Length > 0)
            {
                for (int i = 0; i < boxlist.bboxlist.Length; i++)
                {
                    DrawRectangleInPicture(bitmap, (int)boxlist.bboxlist[i].x,
                        (int)boxlist.bboxlist[i].y,
                        (int)boxlist.bboxlist[i].w,
                        (int)boxlist.bboxlist[i].h, Color.Red, 2, DashStyle.Solid);
                }
                ng = true;
            }
            else
            {
                ng = false;
            }
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            return ng;

        }
        public static Bitmap DrawRectangleInPicture(Bitmap bmp, int x, int y, int width, int height, Color RectColor, int LineWidth, DashStyle ds)
        {
            if (bmp == null) return null;


            Graphics g = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(RectColor);
            Pen pen = new Pen(brush, LineWidth);
            pen.DashStyle = ds;

            g.DrawRectangle(pen, new Rectangle(x, y, width, height));

            g.Dispose();

            return bmp;
        }

    }
}