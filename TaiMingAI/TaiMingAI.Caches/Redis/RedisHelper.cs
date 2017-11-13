﻿using ServiceStack.Redis;
using System;

namespace TaiMingAI.Caches.Redis
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisHelper : IDisposable
    {
        public static IRedisClient Core { get; private set; }
        private bool _disposed = false;
        static RedisHelper()
        {
            Core = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Core.Dispose();
                    Core = null;
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Core.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Core.SaveAsync();
        }
    }
}
