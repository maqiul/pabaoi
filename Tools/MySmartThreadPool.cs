using Amib.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi.Tools
{
    public class MySmartThreadPool
    {
        static SmartThreadPool Pool = new SmartThreadPool() { MaxThreads = 30 };
        public static SmartThreadPool Instance()
        {
            return Pool;
        }
    }
}
