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
            if (dt == null) return null;

            var list = new List<T>();
            if (dt.Rows.Count == 0)
            {
                return list;
            }

            //获取转换类型
            Type t = typeof(T);
            //获取转换类型的公有属性
            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (DataRow dr in dt.Rows)
            {
                //赋值
                var model = Activator.CreateInstance(t) as T;
                foreach (var prop in propertyInfos)
                {
                    //字段在 DataRow 中存在 && 非泛型
                    if (dr.Table.Columns.Contains(prop.Name) && !prop.PropertyType.IsGenericType)
                    {
                        if (dr[prop.Name] == DBNull.Value)
                        {
                            if (prop.PropertyType.IsValueType)
                            {
                                prop.SetValue(model, Activator.CreateInstance(prop.PropertyType), null);
                            }
                            else
                            {
                                prop.SetValue(model, null, null);
                            }
                        }
                        else
                        {
                            prop.SetValue(model, Convert.ChangeType(dr[prop.Name], prop.PropertyType), null);
                        }
                    }
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
                //字段在 DataRow 中存在 && 非泛型
                if (dr.Table.Columns.Contains(prop.Name) && !prop.PropertyType.IsGenericType)
                {
                    if (dr[prop.Name] == null)
                    {
                        if (prop.PropertyType.IsValueType)
                        {
                            prop.SetValue(model, Activator.CreateInstance(prop.PropertyType), null);
                        }
                        else
                        {
                            prop.SetValue(model, null, null);
                        }
                    }
                    else
                    {
                        prop.SetValue(model, Convert.ChangeType(dr[prop.Name], prop.PropertyType), null);
                    }
                }
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
            if (tModel == null) return null;
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
        public static List<TOT> ListToList<T, TOT>(List<T> tList) where T : class, new() where TOT : class, new()
        {
            if (tList == null) return null;
            var totList = new List<TOT>();
            //获取被转换类型
            Type t = typeof(T);
            //获取被转换类型的公有属性
            PropertyInfo[] tPros = t.GetProperties();
            //获取结果类型
            Type tot = typeof(TOT);
            //获取结果类型的公有属性
            PropertyInfo[] totPros = tot.GetProperties();
            foreach (var item in tList)
            {
                var totModel = Activator.CreateInstance(tot) as TOT;

                foreach (PropertyInfo tPro in tPros)
                {
                    //赋值
                    var totPro = totPros.FirstOrDefault(x => x.Name == tPro.Name);
                    if (totPro != null)
                    {
                        totPro.SetValue(totModel, tPro.GetValue(item));
                    }
                }
                totList.Add(totModel as TOT);
            }
            //创建结果类型实体
            return totList;
        }
        #endregion

        #region ToString
        /// <summary>
        /// 转换为String类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>String</returns>
        public static string ToString(object obj, string defaultRef = "")
        {
            if (obj == null)
            {
                return defaultRef;
            }
            return obj.ToString();
        }
        #endregion

        #region ToInt
        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Int</returns>
        public static int ToInt(object obj, int defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            int result = defaultRef;
            var isSuccess = int.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }

        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="bit">转换位数</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Int16</returns>
        public static int ToInt16(object obj, Int16 defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            Int16 result = defaultRef;
            var isSuccess = Int16.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="bit">转换位数</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Int16</returns>
        public static Int64 ToInt64(object obj, Int64 defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            Int64 result = defaultRef;
            var isSuccess = Int64.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }

        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="bit">转换位数</param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Int</returns>
        public static int ToInt(object obj, int bit, out bool isSuccess, int defaultRef = 0)
        {
            if (obj == null)
            {
                isSuccess = false;
                return defaultRef;
            }
            int result = defaultRef;
            isSuccess = int.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        #endregion

        #region ToDouble
        /// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Double</returns>
        public static double ToDouble(object obj, double defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            double result = defaultRef;
            var isSuccess = double.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }

        /// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Double</returns>
        public static double ToDouble(object obj, out bool isSuccess, double defaultRef = 0)
        {
            if (obj == null)
            {
                isSuccess = false;
                return defaultRef;
            }
            double result = defaultRef;
            isSuccess = double.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        #endregion

        #region ToFloat
        /// <summary>
        /// 转换为Float类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Float</returns>
        public static float ToFloat(object obj, float defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            float result = defaultRef;
            var isSuccess = float.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }

        /// <summary>
        /// 转换为Float类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Float</returns>
        public static float ToFloat(object obj, out bool isSuccess, float defaultRef = 0)
        {
            if (obj == null)
            {
                isSuccess = false;
                return defaultRef;
            }
            float result = defaultRef;
            isSuccess = float.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        #endregion

        #region ToDecimal
        /// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(object obj, decimal defaultRef = 0)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            decimal result = defaultRef;
            var isSuccess = decimal.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }

        /// <summary>
        /// 转换为Float类型
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(object obj, out bool isSuccess, decimal defaultRef = 0)
        {
            if (obj == null)
            {
                isSuccess = false;
                return defaultRef;
            }
            decimal result = defaultRef;
            isSuccess = decimal.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        #endregion

        #region ToDateTime
        /// <summary>
        /// 转换为DateTime类型
        /// 转换失败，默认返回DateTime.MinValue
        /// </summary>
        /// <param name="obj">被转换数据</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(object obj)
        {
            if (obj == null)
            {
                return DateTime.MinValue;
            }
            DateTime result;
            var isSuccess = DateTime.TryParse(ToString(obj), out result);
            return isSuccess ? result : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为DateTime类型
        /// 转换失败，默认返回defaultRef
        /// <param name="obj">被转换数据</param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <param name="defaultRef">默认返回数据</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(object obj, DateTime defaultRef)
        {
            if (obj == null)
            {
                return defaultRef;
            }
            DateTime result;
            var isSuccess = DateTime.TryParse(ToString(obj), out result);
            return isSuccess ? result : defaultRef;
        }
        #endregion
    }
}
