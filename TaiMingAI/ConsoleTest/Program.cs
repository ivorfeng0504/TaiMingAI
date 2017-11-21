using System;
using System.Configuration;
using System.Diagnostics;
using TaiMingAI.Caches;
using TaiMingAI.Tools;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.InfoLog("进入主程序");
            PersonSection person = ConfigurationManager.GetSection("Person") as PersonSection;
            Console.WriteLine("name={0},age={1}", person.Age, person.Name);
            var key = "斗战胜佛";
            Test(key);
            var value = CacheHelper.DoRedisString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                value = "我若成佛,天下无魔";
                CacheHelper.DoRedisString.Set(key, value);
            }
            Console.WriteLine(value);
            key = "齐天大圣";
            Test(key);
            value = CacheHelper.DoRedisString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                value = "我若成魔,佛奈我何";
                CacheHelper.DoRedisString.Set(key, value);
            }
            LogHelper.InfoLog(string.Format("key:{0};value:{1}", key, value));
            try
            {
                Convert.ToInt16("s");
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("有异常出现", ex);
                LogHelper.FatalLog("有异常出现", ex);
            }
            CacheHelper.DoRedisList.LPush(key + "_list", value);
            Console.WriteLine(value);
            Console.ReadKey();
        }
        public static void Test(string key)
        {
            var flg = CacheHelper.DoRedisString.Incr(key + "Incr");
            LogHelper.InfoLogFormat("key:{0},访问次数：{1}", key, flg);
        }
    }
    class PersonSection : ConfigurationSection
    {
        [ConfigurationProperty("age", IsRequired = false, DefaultValue = 0)]
        public int Age
        {
            get { return (int)base["age"]; }
            set { base["age"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = false, DefaultValue = "")]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
    }
}
