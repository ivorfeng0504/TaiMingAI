using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Caches.Redis;

namespace TaiMingAI.Caches
{
    public class CacheHelper
    {
        public static DoRedisHash DoRedisHash { get; private set; }
        public static DoRedisList DoRedisList { get; private set; }
        public static DoRedisSet DoRedisSet { get; private set; }
        public static DoRedisSortedSet DoRedisSortedSet { get; private set; }
        public static DoRedisString DoRedisString { get; private set; }
        static CacheHelper()
        {
            DoRedisHash = DoRedisHash.CreateDoRedis;
            DoRedisList = DoRedisList.CreateDoRedis;
            DoRedisSet = DoRedisSet.CreateDoRedis;
            DoRedisSortedSet = DoRedisSortedSet.CreateDoRedis;
            DoRedisString = DoRedisString.CreateDoRedis;
        }
    }
}
