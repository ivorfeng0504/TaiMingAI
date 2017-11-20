using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace TaiMingAI.Caches.Redis
{
    /// <summary>
    /// 字符串
    /// </summary>
    public class DoRedisString : RedisHelper
    {
        private static DoRedisString doRedis;
        private DoRedisString() { }
        public static DoRedisString CreateDoRedis
        {
            get
            {
                if (doRedis == null)
                {
                    lock (lockObj)
                    {
                        if (doRedis == null)
                        {
                            doRedis = new DoRedisString();
                        }
                    }
                }
                return doRedis;
            }
        }
        #region 赋值
        /// <summary>
        /// 设置key的value
        /// </summary>
        public bool Set(string key, string value)
        {
            var set = Core.GetHashCode();
            return Core.Set(key, value);
        }
        public bool Set<T>(string key, T value)
        {
            return Core.Set(key, value);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public bool Set(string key, string value, DateTime dt)
        {
            return Core.Set(key, value, dt);
        }
        public bool Set<T>(string key, T value, DateTime dt)
        {
            return Core.Set(key, value, dt);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public bool Set(string key, string value, TimeSpan sp)
        {
            return Core.Set(key, value, sp);
        }
        public bool Set<T>(string key, T value, TimeSpan sp)
        {
            return Core.Set(key, value, sp);
        }
        /// <summary>
        /// 设置多个key/value
        /// </summary>
        public void Set(Dictionary<string, string> dic)
        {
            Core.SetAll(dic);
        }
        #endregion

        #region 追加
        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public long Append(string key, string value)
        {
            return Core.AppendToValue(key, value);
        }
        #endregion

        #region 获取值
        /// <summary>
        /// 获取key的value值
        /// </summary>
        public string Get(string key)
        {

            var get = Core.GetHashCode();
            return Core.GetValue(key);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public List<string> Get(List<string> keys)
        {
            return Core.GetValues(keys);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public List<T> Get<T>(List<string> keys)
        {
            return Core.GetValues<T>(keys);
        }
        #endregion

        #region 获取旧值赋上新值
        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        public string GetAndSetEntry(string key, string value)
        {
            return Core.GetAndSetEntry(key, value);
        }
        #endregion
        #region 辅助方法
        /// <summary>
        /// 获取值的长度
        /// </summary>
        public long GetCount(string key)
        {
            return Core.GetSetCount(key);
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public long Incr(string key)
        {
            return Core.IncrementValue(key);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public double IncrBy(string key, int count)
        {
            return Core.IncrementValueBy(key, count);
        }
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public long Decr(string key)
        {
            return Core.DecrementValue(key);
        }
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public long DecrBy(string key, int count)
        {
            return Core.DecrementValueBy(key, count);
        }
        #endregion
    }
}
