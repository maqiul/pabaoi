using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTing.SingleTon
{
   public class SingleTon<T> where T:new ()
    {
        private static T instance;
        private static object obj = new object();
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
