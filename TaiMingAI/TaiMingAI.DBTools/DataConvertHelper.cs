using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TaiMingAI.DataHelper
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public class DataConvertHelper
    {
        #region DataTable TO List
        /// <summary>
        /// 将DataTable转换为实体List
        /// </summary>
        /// <typeparam name="T">实体Model类型</typeparam>
        /// <param name="dt">数据源DataTable</param>
        /// <returns>实体List</returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            //获取转换类型
            Type t = typeof(T);
            //获取转换类型的公有属性
            PropertyInfo[] propertyInfos = t.GetProperties();
            var list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //赋值
                var model = Activator.CreateInstance(t) as T;
                foreach (var prop in propertyInfos)
                {
                    prop.SetValue(model, dr[prop.Name]);
                }
                list.Add(model);
            }
            return list;
        }
        #endregion

        #region DataRow TO Model
        /// <summary>
        /// 将DataRow转换为实体Model
        /// </summary>
        /// <typeparam name="T">实体Model类型</typeparam>
        /// <param name="dr">数据源DataRow</param>
        /// <returns>实体Model</returns>
        public static T DataRowToModel<T>(DataRow dr) where T : class, new()
        {
            if (dr == null) return null;
            //获取转换类型
            Type t = typeof(T);
            //获取转换类型的公有属性
            PropertyInfo[] propertyInfos = t.GetProperties();

            //赋值
            var model = Activator.CreateInstance(t) as T;
            foreach (var prop in propertyInfos)
            {
                prop.SetValue(model, dr[prop.Name]);
            }
            return model;
        }
        #endregion

        #region A TO B
        /// <summary>
        /// model转换
        /// 将A实体转为B实体
        /// </summary>
        /// <typeparam name="T">被转换的类型</typeparam>
        /// <typeparam name="TOT">转换成的类型</typeparam>
        /// <param name="model">被转换的数据</param>
        /// <returns>返回转换后的实体B</returns>
        public static TOT ModelToModel<T, TOT>(T tModel) where T : class, new() where TOT : class, new()
        {
            if (tModel == null)
                return default(TOT);
            //获取被转换类型
            Type t = typeof(T);
            //获取被转换类型的公有属性
            PropertyInfo[] tPros = t.GetProperties();
            //获取结果类型
            Type tot = typeof(TOT);
            //获取结果类型的公有属性
            PropertyInfo[] totPros = tot.GetProperties();
            //创建结果类型实体
            var totModel = Activator.CreateInstance(tot);

            foreach (PropertyInfo tPro in tPros)
            {
                //赋值
                var totPro = totPros.FirstOrDefault(x => x.Name == tPro.Name);
                if (totPro != null)
                {
                    totPro.SetValue(totModel, tPro.GetValue(tModel));
                }
            }
            return totModel as TOT;
        }
        #endregion
    }
}
