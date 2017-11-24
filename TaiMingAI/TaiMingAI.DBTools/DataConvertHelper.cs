using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.DataHelper
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public class DataConvertHelper
    {
        /// <summary>
        /// 将DataTable转换为实体Model
        /// </summary>
        /// <typeparam name="T">实体Model类型</typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> DataTableToModel<T>(DataTable dataTable) where T : class, new()
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return new List<T>();
            //获取转换类型
            Type t = typeof(T);
            //获取转换类型的公有属性
            PropertyInfo[] propertyInfos = t.GetProperties();
            var list = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
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

        /// <summary>
        /// model转换
        /// 将A实体转为B实体
        /// </summary>
        /// <typeparam name="T">被转换的类型</typeparam>
        /// <typeparam name="ToT">转换成的类型</typeparam>
        /// <param name="model">被转换的数据</param>
        /// <returns></returns>
        public static ToT ModelToModel<T, ToT>(T tModel) where T : class where ToT : class
        {
            if (tModel == null)
                return default(ToT);
            //获取被转换类型
            Type t = typeof(T);
            //获取被转换类型的公有属性
            PropertyInfo[] tPros = t.GetProperties();
            //获取结果类型
            Type tot = typeof(ToT);
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
            return totModel as ToT;
        }
    }
}
