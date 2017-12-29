using System;
using System.Configuration;
using System.Diagnostics;
using TaiMingAi.WebApi.Model;
using TaiMingAi.WebApi.Model.Song;
using TaiMingAI.Caches;
using TaiMingAI.DataHelper;
using TaiMingAI.Tools;
using TaiMingAI.Tools.Xml;
using TaiMingAI.WebApi;
using TaiMingAI.WebApi.BLL;
using TaiMingAI.WebApi.DLL;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SongBLL songBll = new SongBLL();
            var result = songBll.BaiduTingSearch("双节棍");
            Console.WriteLine();
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
