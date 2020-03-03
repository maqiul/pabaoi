using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.Common;
using pcbaoi.Properties;

namespace pcbaoi
{
    /// <summary>
    /// SQLite 操作类
    /// </summary>
    public class SQLiteHelper
    {

        //生成连接字符串
        private static string CreateConnectionString()
        {
            SQLiteConnectionStringBuilder connectionString = new SQLiteConnectionStringBuilder();
            connectionString.DataSource = Settings.Default.dbpath;
            string conStr = connectionString.ToString();
            return conStr;
        }

        /// <summary>
        /// 对插入到数据库中的空值进行处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 对从数据库中读取的空值进行处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDbValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }


  

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            string connectionString = CreateConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static void ExecuteSqlnew(string SQLString)
        {
            string connectionString = CreateConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DbTransaction trans = connection.BeginTransaction();
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        trans.Rollback();
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行非查询的数据库操作
        /// </summary>
        /// <param name="sqlString">要执行的sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回受影响的条数</returns>
        public static int ExecuteNonQuery(string sqlString, Object[] list)
        {
            string connectionString = CreateConnectionString();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    AttachParameters(cmd, sqlString, list);
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 执行查询并返回查询结果第一行第一列
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="sqlparams">参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlString, params SQLiteParameter[] parameters)
        {
            string connectionString = CreateConnectionString();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    foreach (SQLiteParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回查询的数据表</returns>
        public static DataTable GetDataTable(string sqlString)
        {
            try
            {
                string connectionString = CreateConnectionString();
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sqlString;

                        DataSet ds = new DataSet();
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        adapter.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
            catch(Exception e) {
                throw e;
                

            }
            

        }
        #region AttachParameters(SQLiteCommand,commandText,object[] paramList)
        /// <summary>
        /// 增加参数到命令（自动判断类型）
        /// </summary>
        /// <param name="commandText">命令语句</param>
        /// <param name="paramList">object参数列表</param>
        /// <returns>返回SQLiteParameterCollection参数列表</returns>
        /// <remarks>Status experimental. Regex appears to be handling most issues. Note that parameter object array must be in same ///order as parameter names appear in SQL statement.</remarks>
        private static SQLiteParameterCollection AttachParameters(SQLiteCommand cmd, string commandText, params object[] paramList)
        {
            if (paramList == null || paramList.Length == 0) return null;

            SQLiteParameterCollection coll = cmd.Parameters;
            string parmString = commandText.Substring(commandText.IndexOf("@"));
            // pre-process the string so always at least 1 space after a comma.
            parmString = parmString.Replace(",", " ,");
            // get the named parameters into a match collection
            string pattern = @"(@)\S*(.*?)\b";
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = ex.Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }

            // now let's type the parameters
            int j = 0;
            Type t = null;
            foreach (object o in paramList)
            {
                t = o.GetType();

                SQLiteParameter parm = new SQLiteParameter();
                switch (t.ToString())
                {

                    case ("DBNull"):
                    case ("Char"):
                    case ("SByte"):
                    case ("UInt16"):
                    case ("UInt32"):
                    case ("UInt64"):
                        throw new SystemException("Invalid data type");


                    case ("System.String"):
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Byte[]"):
                        parm.DbType = DbType.Binary;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[])paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Int32"):
                        parm.DbType = DbType.Int32;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Boolean"):
                        parm.DbType = DbType.Boolean;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.DateTime"):
                        parm.DbType = DbType.DateTime;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Double"):
                        parm.DbType = DbType.Double;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Decimal"):
                        parm.DbType = DbType.Decimal;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(paramList[j]);
                        break;

                    case ("System.Guid"):
                        parm.DbType = DbType.Guid;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (System.Guid)(paramList[j]);
                        break;

                    case ("System.Object"):

                        parm.DbType = DbType.Object;
                        parm.ParameterName = paramNames[j];
                        parm.Value = paramList[j];
                        coll.Add(parm);
                        break;

                    default:
                        throw new SystemException("Value is of unknown data type");

                } // end switch

                j++;
            }
            return coll;
        }

    }
    #endregion
}