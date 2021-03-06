﻿using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace pcbaoi
{
    public class AoiAi
    {


        [DllImport("power_aoi_v2.dll", EntryPoint = "testIplImage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int testIplImage(IntPtr iplImage);

        [DllImport("power_aoi_v2.dll", EntryPoint = "testMatOut", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int testMatOut(IntPtr mat, IntPtr res);

        /// <summary>
        /// 抽色
        /// </summary>
        /// <param name="gray">灰度图</param>
        /// <param name="rgb">彩色图</param>
        /// <param name="mask">输出的掩码图</param>
        /// <param name="pars">参数数组，依次为灰度的阈值下限、上限，r 通道的下限、上限，g 下限、上限，b 下限、上限</param>
        /// <returns></returns>
        [DllImport("power_aoi_v2.dll", EntryPoint = "range_mask", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void range_mask(IntPtr gray, IntPtr rgb, IntPtr mask, int par);

        /// <summary>
        /// 灰度图生成直方图
        /// </summary>
        /// <param name="gray">灰度图</param>
        /// <param name="n_bins">柱子的个数</param>
        /// <param name="hist">输出的直方图，n_bins x 1 的矩阵，类型为 CV_32F 的，hist.data 是数据指针，float32 型的</param>
        [DllImport("power_aoi_v2.dll", EntryPoint = "histogram", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void histogram(IntPtr gray, int n_bins, IntPtr hist);

        /// <summary>
        /// 图像拼接
        /// </summary>
        /// <param name="img">画布</param>
        /// <param name="patch">要贴上画布的图片</param>
        /// <param name="x">patch图片左上角x在画布的x</param>
        /// <param name="y">patch图片左上角y在画布的y</param>
        /// <returns></returns>
        [DllImport("power_aoi_v2.dll", EntryPoint = "add_patch", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addPatch(IntPtr img, IntPtr patch, float x, float y);



    }
}
