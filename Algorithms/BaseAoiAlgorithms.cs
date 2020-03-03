using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi.Algorithms
{
    public class BaseAoiAlgorithms
    {
        public string hello(string str)
        {
            return "result:" + str;
        }

        #region AI
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct bbox_t
        {
            public uint x, y, w, h;       // 缺陷框坐标 定点x,y 宽高w,h
            public float prob;            // 置信度
            public uint obj_id;           // 缺陷id
            public uint track_id;         // 预留，tracking id for video (0 - untracked, 1 - inf - tracked object)
            public uint frames_counter;   // 预留，counter of frames on which the object was detected
            public float x_3d, y_3d, z_3d;// 预留 

        };
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct bbox_t_container
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
            public bbox_t[] bboxlist;
        };
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configurationFilename">配置文件路径</param>
        /// <param name="weightsFilename">权重文件路径，这里注意一定要使用斜杠，不能使用反斜杠</param>
        /// <param name="gpuID">gpuid，不清楚的直接填写0，如果要更改请查阅nvidia-smi</param>
        /// <returns></returns>
        [DllImport(@"ai_cpp_dll.dll", EntryPoint = "init", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int init(string configurationFilename, string weightsFilename, int gpuID);

        /// <summary>
        /// 通过byte[]来检测
        /// </summary>
        /// <param name="data">图片byte[]</param>
        /// <param name="data_length">长度</param>
        /// <param name="bbox_T_Container">返回结果</param>
        /// <returns>返回-1表示，调用opencv失败</returns>
        [DllImport(@"ai_cpp_dll.dll", EntryPoint = "detect_mat", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int detect_opencv_mat(byte[] data, long data_length, ref bbox_t_container bbox_T_Container);

        /// <summary>
        /// 通过图片路径检测
        /// </summary>
        /// <param name="filename">图片路径</param>
        /// <param name="bbox_T_Container">返回结果</param>
        /// <returns></returns>
        [DllImport(@"ai_cpp_dll.dll", EntryPoint = "detect_image", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int detect_image_path(string filename, ref bbox_t_container bbox_T_Container);

        /// <summary>
        /// 释放
        /// </summary>
        /// <returns></returns>
        [DllImport(@"ai_cpp_dll.dll", EntryPoint = "dispose", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int dispose();
        #endregion

        #region 传统

        public enum side { none = 0, left = 1, up = 2, right = 4, down = 8 };

        //[DllImport("aoi.dll", EntryPoint = "hello", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        //public static extern int hello(IntPtr iplImage);

        [DllImport("aoi.dll", EntryPoint = "copy_to", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int copy_to(IntPtr iplImage, IntPtr patch, Rectangle rectangle);

        [DllImport("aoi.dll", EntryPoint = "stitch", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void stitch(IntPtr img, Rectangle rectangle, IntPtr patch, ref Rectangle roi_patch, int side1, int overlap_lb1, int overlap_ub1, int drift_ub1, int side2 = 0, int overlap_lb2 = 0, int overlap_ub2 = 0, int drift_ub2 = 0);

        [DllImport("aoi.dll", EntryPoint = "stitch_v2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void stitchv2(IntPtr img, Rectangle rectangle, IntPtr patch, ref Rectangle roi_patch, int side1, int overlap_lb1, int overlap_ub1, int drift_ub1, int side2 = 0, int overlap_lb2 = 0, int overlap_ub2 = 0, int drift_ub2 = 0);


        [DllImport("power_aoi.dll", EntryPoint = "testIplImage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int testIplImage(IntPtr iplImage);

        [DllImport("power_aoi.dll", EntryPoint = "testMatOut", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int testMatOut(IntPtr mat, IntPtr res);

        /// <summary>
        /// 抽色
        /// </summary>
        /// <param name="gray">灰度图</param>
        /// <param name="rgb">彩色图</param>
        /// <param name="mask">输出的掩码图</param>
        /// <param name="pars">参数数组，依次为灰度的阈值下限、上限，r 通道的下限、上限，g 下限、上限，b 下限、上限</param>
        /// <returns></returns>
        [DllImport("power_aoi.dll", EntryPoint = "range_mask", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void range_mask(IntPtr gray, IntPtr rgb, IntPtr mask, int par);

        /// <summary>
        /// 灰度图生成直方图
        /// </summary>
        /// <param name="gray">灰度图</param>
        /// <param name="n_bins">柱子的个数</param>
        /// <param name="hist">输出的直方图，n_bins x 1 的矩阵，类型为 CV_32F 的，hist.data 是数据指针，float32 型的</param>
        [DllImport("power_aoi.dll", EntryPoint = "histogram", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void histogram(IntPtr gray, int n_bins, IntPtr hist);

        /// <summary>
        /// 图像拼接
        /// </summary>
        /// <param name="img">画布</param>
        /// <param name="patch">要贴上画布的图片</param>
        /// <param name="x">patch图片左上角x在画布的x</param>
        /// <param name="y">patch图片左上角y在画布的y</param>
        /// <returns></returns>
        [DllImport("power_aoi.dll", EntryPoint = "add_patch", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addPatch(IntPtr img, IntPtr patch, int x, int y);
        #endregion
    }
}
