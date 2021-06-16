using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;


namespace CommunityGroup.Common
{
    /// <summary>
    /// 定义一个Dapper泛型通用类，来完成通用增删改查的写法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Dapper<T>
    {
        static string connection = ConfigHelper.constr;

        /// <summary>
        /// 执行增删改的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int RUD(string sql,object param = null)
        {
            using(IDbConnection conn=new SqlConnection(connection))
            {
                conn.Open();

                return conn.Execute(sql, param);
            }

        }

        /// <summary>
        /// 查询所有数据的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> Query(string sql)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                List<T> list = conn.Query<T>(sql).ToList();

                return list;
            }

        }


        // <summary>
        /// 查询某条数据的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> Query(string sql,object param)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                List<T> list = conn.Query<T>(sql,param).ToList();

                return list;
            }

        }


        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirst(string sql,object param)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                return conn.QueryFirst<T>(sql, param);
            }

        }

        /// <summary>
        /// 查询第一条数据没有默认返回值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirstOrDefault(string sql, object param)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                return conn.QueryFirstOrDefault<T>(sql, param);
            }

        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle(string sql, object param)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                return conn.QuerySingle<T>(sql, param);
            }

        }


        /// <summary>
        /// 查询单条数据没有默认返回值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingleOrDefault(string sql, object param)
        {
            using (IDbConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                return conn.QuerySingleOrDefault<T>(sql, param);
            }

        }

        /// <summary>
        /// dapper通用的分页方法
        /// </summary>
        /// <typeparam name="T">泛型集合实体类</typeparam>
        /// <param name="conn">数据库连接池连接对象</param>
        /// <param name="files">列</param>
        /// <param name="tableName">表</param>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页显示条数</param>
        /// <param name="total">结果集条数</param>
        /// <returns></returns>
        public static IEnumerable<T> GetPageList<Tpage>(IDbConnection conn, string files, string tableName, string where, string orderby, int pageIndex, int pageSize, out int total)
        {
            
            int skip = 1;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize + 1;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0} where {1};", tableName, where);
            sb.AppendFormat(@"SELECT  {0}
                                FROM(SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS RowNum,{0}
                                          FROM  {1}
                                          WHERE {2}
                                        ) AS result
                                WHERE  RowNum >= {4}   AND RowNum <= {5}
                                ORDER BY {3}", files, tableName, where, orderby, skip, pageIndex * pageSize);
            using (var reader = conn.QueryMultiple(sb.ToString()))
            {
                total = reader.ReadFirst<int>();
                return reader.Read<T>();
            }
        }



    }
}
