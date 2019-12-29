using System;
using System.Collections;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace pcbaoi
{


    /// <summary>
    ///MYSQLHelper 的摘要说明
    /// </summary>
    public abstract class MySqlHelper
    {
        static string ip = IniFile.iniRead("MySqlConfig", "hostIP");
        static string user = IniFile.iniRead("MySqlConfig", "user");
        static string password = IniFile.iniRead("MySqlConfig", "password");
        static string port = IniFile.iniRead("MySqlConfig", "port");
        //数据库连接字符串
        //public static string Conn = "Database='parking';Data Source='" + ip + "';User Id='" + user + "';Password='" + password + "';Port=" + port + ";charset='utf8';pooling=true;Allow Zero Datetime=True";
        public static string Conn = "Database='power_aoi';Data Source='" + ip + "';User Id='" + user + "';Password='" + password + "';Port=" + port + ";charset='utf8';pooling=true;max pool size=500;min pool size=2;Allow Zero Datetime=True";

        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 超时（秒）
        /// </summary>
        private static int TimeOut = 100;

        //全局连接
        public static MySqlConnection conn = null;

        /// <summary>
        /// 返回第一个值
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static long ExecuteInsertId(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            long val = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    //MySqlTransaction transcation = conn.BeginTransaction();
                    //try
                    //{
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.CommandTimeout = TimeOut;
                    if (commandParameters != null)
                        foreach (MySqlParameter pram in commandParameters)
                            cmd.Parameters.Add(pram);
                    cmd.ExecuteNonQuery();
                    val = cmd.LastInsertedId;
                    //}
                    //catch (Exception e)
                    //{
                    //    transcation.Rollback();
                    //}
                }

            }
            return val;


        }

        /// <summary>
        /// 返回第一个值
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static long ExecuteInsertId(string cmdText)
        {

            return MySqlHelper.ExecuteInsertId(
                         MySqlHelper.Conn,
                         CommandType.Text,
                         cmdText,
                         null);


        }

        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            int val = 0;
            if (conn == null)
            {
                conn = new MySqlConnection(connectionString);
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //using (MySqlConnection conn = new MySqlConnection(connectionString))
            //{
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                //conn.Open();
                MySqlTransaction transcation = conn.BeginTransaction();
                try
                {
                    PrepareCommand(cmd, transcation, cmdType, cmdText, commandParameters);
                    val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    transcation.Commit();
                }
                catch (Exception e)
                {
                    transcation.Rollback();
                    //Utils.WriteReqErrorLogAndPrint(e.ToString(), "error");
                }
            }

            //}
            return val;
        }

        public static int ExecuteNonQuery(string cmdText)
        {
            return MySqlHelper.ExecuteNonQuery(
                            MySqlHelper.Conn,
                            CommandType.Text,
                            cmdText,
                            null);
        }

        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            PrepareCommand(cmd, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:
        ///  MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            //MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，

            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                //conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 返回数据集的第一行
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <returns>第一行数据(DataRow) </returns>
        public static DataRow GetRow(string cmdText)
        {
            DataRowCollection dc = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, cmdText, null).Tables[0].Rows;
            if (dc.Count > 0)
            {
                return dc[0];
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 返回数据集的第一行
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <returns>第一行数据(DataRow) </returns>
        public static DataRowCollection GetallRow(string cmdText)
        {
            DataRowCollection dc = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, cmdText, null).Tables[0].Rows;
            if (dc.Count > 0)
            {
                return dc;
            }
            else
            {
                return null;
            }

        }

        // 返回数据集个数
        public static int GetCount(string cmdText)
        {
            string dc = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, cmdText, null).Tables[0].Rows[0]["count"].ToString();
            if (dc != "0")
            {
                return Convert.ToInt32(dc);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个存在的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            PrepareCommand(cmd, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="commandParameters">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 找回缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn == null)
            {
                conn = new MySqlConnection(Conn);
            }

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

    }

}
