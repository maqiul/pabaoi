using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace pcbaoi
{
    public class IniFile
    {
        private static string strFilePath = System.Environment.CurrentDirectory + "\\FileConfig.ini";//获取INI文件路径
        private static  string strSec = ""; //INI文件名
        
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        //读取ini文件
        public static string iniRead(string section,string key)
        {
            string fileOrNot = "";
            try
            {
                if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
                {

                    //strSec = Path.GetFileNameWithoutExtension(strFilePath);
                    strSec = section;
                    string content = ContentValue(strSec, key);
                    return content;
                }
                else
                {
                    fileOrNot = strFilePath + " is not exist!";
                    return "";

                }
            }
            catch(Exception e )
            {
               // Utils.WriteReqErrorLogAndPrint(e.ToString(), "error");
                return "";
            }
        }
        //写入ini文件
        public static void iniWrite(string section,string key, string value)
        {
            try
            {

                //根据INI文件名设置要写入INI文件的节点名称
                //此处的节点名称完全可以根据实际需要进行配置
                //strSec = Path.GetFileNameWithoutExtension(strFilePath);
                if(string.IsNullOrEmpty(section))
                {
                    throw new ArgumentException("必须指定节点名称", "section");  
                }
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) 
                {
                    throw new ArgumentException("必须指定键值对", "Key||Value"); 
                }
                WritePrivateProfileString(section, key, value, strFilePath);
                
            }
            catch (Exception e)
            {
               // Utils.WriteReqErrorLogAndPrint(e.ToString(), "error");

            }
        }
        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        private static string ContentValue(string Section, string key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
                return temp.ToString();
            }
            catch(Exception e)
            {
               // Utils.WriteReqErrorLogAndPrint(e.ToString(),"error");
                return string.Empty;
            }

        }
        
    }
}
