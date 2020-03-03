using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{

        /// <summary>
        /// JSON数据类
        /// </summary>
        /// <typeparam name="T"><peparam>
        public class JsonData<T>
        {
            #region 系统参数
            public string version { get; set; } = "1.0.0";
            public string key { get; set; } = "yining";
            public T data { get; set; }
            #endregion
        }
    
}
