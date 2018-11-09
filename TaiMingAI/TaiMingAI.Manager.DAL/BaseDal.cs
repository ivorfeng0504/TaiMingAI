using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dapper;

namespace TaiMingAI.Manager.DAL
{
    public class BaseDal<T>
    {
        protected readonly static object obj = new Object();
        private string connectionString;
        public BaseDal(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="key">文件名.节点key</param>
        /// <returns>sql语句</returns>
        public string GetSql(string key)
        {
            if (!key.StartsWith("xml."))
            {
                return key;
            }
            key = key.Substring(4);
            //文件名.节点
            string[] sqlLayers = key.Split('.');
            //文件路径
            string filePath = string.Format(AppDomain.CurrentDomain.BaseDirectory + @"\bin\config\sql\{0}.config", sqlLayers[0]);
            //解析xml
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(filePath);
            XmlNodeList list = Xdoc.SelectNodes("configuration/sqlmapping");
            XmlNode mappingNode = list.Cast<XmlNode>().FirstOrDefault(x => x.Attributes["key"].Value.Equals(sqlLayers[1]));
            XmlNode sqlNode = mappingNode.SelectSingleNode("sql");
            return sqlNode.InnerText.Replace("\r\n", "").Trim();
        }

        public int Insert(string sqlKey, T t)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 批量插入数据，返回影响行数
        /// </summary>
        /// <param name="sqlKey"></param>
        /// <param name="ts"></param>
        /// <returns>影响行数</returns>
        public int Insert(string sqlKey, List<T> list)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, list);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="t">删除条件</param>
        /// <returns>影响行数</returns>
        public int Delete(string sqlKey, T t)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, t);
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="list">删除条件</param>
        /// <returns>影响行数</returns>
        public int Delete(string sqlKey, List<T> list)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, list);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="t">修改条件</param>
        /// <returns>影响行数</returns>
        public int Update(string sqlKey, T t)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, t);
            }
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="list"></param>
        /// <returns>影响行数</returns>
        public int Update(string sqlKey, List<T> list)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, list);
            }
        }
        /// <summary>
        /// 无参查询所有数据
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <returns>数据集合</returns>
        public List<T> Query(string sqlKey)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="t">查询条件</param>
        /// <returns></returns>
        public List<T> QueryByT(string sqlKey, T t)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql, t).ToList();
            }
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="t">查询条件</param>
        /// <returns></returns>
        public T Query(string sqlKey, T t)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql, t).SingleOrDefault();
            }
        }

        /// <summary>
        ///  In操作
        /// </summary>
        /// <param name="sqlKey">sql</param>
        /// <param name="keys">in条件</param>
        /// <returns>数据集合</returns>
        public List<T> QueryIn(string sqlKey, string[] keys)
        {
            var sql = GetSql(sqlKey);
            using (var connection = new SqlConnection(connectionString))
            {
                //参数类型是Array的时候，dappper会自动将其转化
                return connection.Query<T>(sql, new { keys }).ToList();
            }
        }
    }
}
