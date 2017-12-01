using System;
using System.Configuration;
using System.Diagnostics;
using TaiMingAi.WebApi.Model;
using TaiMingAI.Caches;
using TaiMingAI.DataHelper;
using TaiMingAI.Tools;
using TaiMingAI.Tools.Xml;
using TaiMingAI.WebApi;
using TaiMingAI.WebApi.DLL;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "ff9586";
            var value = MD5Helper.MD5Encrypt(s);
            var value1 = MD5Helper.MD5Encrypt(s, 16);
            var value2 = MD5Helper.MD5Encrypt(s, 32, false);
            var value3 = MD5Helper.MD5Encrypt(s, 16, false);

            Console.WriteLine(value);
            Console.WriteLine(value1);
            Console.WriteLine(value2);
            Console.WriteLine(value3);
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
