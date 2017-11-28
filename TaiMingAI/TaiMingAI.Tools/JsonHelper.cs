using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaiMingAI.Tools
{
    public class JsonHelper
    {
        private static readonly JsonSerializerSettings JsonSettings;

        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

            JsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonSettings.Converters.Add(datetimeConverter);
        }

        #region 序列化
        /// <summary>
        /// 应用Formatting.None和指定的JsonSerializerSettings设置,序列化对象到JSON格式的字符串
        /// </summary>
        /// <param name="obj">任意一个对象</param>
        /// <returns>标准的JSON格式的字符串</returns>
        public static string ToJson(object obj)
        {
            return ToJson(obj, Formatting.None, JsonSettings);
        }

        /// <summary>
        /// 应用Formatting.None和指定的JsonSerializerSettings设置,序列化对象到JSON格式的字符串
        /// </summary>
        /// <param name="obj">任意一个对象</param>
        /// <param name="jsonSettings">在一个 Newtonsoft.Json.JsonSerializer 对象上指定设置，如果为null，则使用默认设置</param>
        /// <returns>标准的JSON格式的字符串</returns>
        public static string ToJson(object obj, JsonSerializerSettings jsonSettings)
        {
            return ToJson(obj, Formatting.None, jsonSettings);
        }

        // <summary>
        /// 应用指定的Formatting枚举值None和指定的JsonSerializerSettings设置,序列化对象到JSON格式的字符串
        /// </summary>
        /// <param name="obj">任意一个对象</param>
        /// <param name="format">指定 Newtonsoft.Json.JsonTextWriter 的格式设置选项</param>
        /// <param name="jsonSettings">在一个 Newtonsoft.Json.JsonSerializer 对象上指定设置，如果为null，则使用默认设置</param>
        /// <returns>标准的JSON格式的字符串</returns>
        public static string ToJson(object obj, Formatting format, JsonSerializerSettings jsonSettings)
        {
            try
            {
                return obj == null ? null : JsonConvert.SerializeObject(obj, format, jsonSettings ?? JsonSettings);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("ToJson序列化异常", ex);
                return null;
            }
        }
        #endregion

        #region 反序列化
        /// <summary>
        /// 应用指定的JsonSerializerSettings设置,反序列化JSON数据为dynamic对象
        /// <para>如果发生JsonSerializationException异常，再以集合的方式重试一次，取出集合的第一个dynamic对象。</para>
        /// <para>转换失败，或发生其它异常，则返回dynamic对象的默认值</para>
        /// </summary>
        /// <param name="json">需要反序列化的JSON字符串</param>
        /// <returns>dynamic对象</returns>
        public static dynamic FromJson(string json)
        {
            return FromJson<dynamic>(json);
        }

        /// <summary>
        /// 反序列化JSON数据到指定的.NET类型对象
        /// <para>如果发生JsonSerializationException异常，再以集合的方式重试一次，取出集合的第一个T对象。</para>
        /// <para>转换失败，或发生其它异常，则返回T对象的默认值</para>
        /// </summary>
        /// <param name="json">需要反序列化的JSON字符串</param>
        /// <typeparam name="T">反序列化对象的类型</typeparam>
        /// <returns></returns>
        public static T FromJson<T>(string json) where T : class, new()
        {
            return FromJson<T>(json, JsonSettings);
        }

        /// <summary>
        /// 应用指定的JsonSerializerSettings设置,反序列化JSON数据到指定的.NET类型对象
        /// <para>如果发生JsonSerializationException异常，再以集合的方式重试一次，取出集合的第一个T对象。</para>
        /// <para>转换失败，或发生其它异常，则返回T对象的默认值</para>
        /// </summary>
        /// <param name="json">需要反序列化的JSON字符串</param>
        /// <param name="jsonSettings">在一个 Newtonsoft.Json.JsonSerializer 对象上指定设置，如果为null，则使用默认设置</param>
        /// <typeparam name="T">反序列化对象的类型</typeparam>
        /// <returns></returns>
        public static T FromJson<T>(string json, JsonSerializerSettings jsonSettings) where T : class, new()
        {
            T result;
            try
            {
                result = string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, jsonSettings ?? JsonSettings);
            }
            catch (JsonSerializationException jx) //在发生该异常后，再以集合的方式重试一次.
            {
                LogHelper.ErrorLog("FromJson反序列化异常", jx);
                try
                {
                    var array = JsonConvert.DeserializeObject<IEnumerable<T>>(json, jsonSettings ?? JsonSettings);
                    result = array.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog("FromJson反序列化异常", ex);
                    result = default(T);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("FromJson反序列化异常", ex);
                result = default(T);
            }
            return result;
        }
        #endregion
    }
}
