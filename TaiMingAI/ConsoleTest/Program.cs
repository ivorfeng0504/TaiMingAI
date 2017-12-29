using System;
using System.Configuration;
using System.Diagnostics;
using TaiMingAi.WebApi.Model;
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
            //var url = "http://tingapi.ting.baidu.com/v1/restserver/ting?method=baidu.ting.search.catalogSug&query=妈妈";
            //var json= WebApiHelper.GetResponseJson(url);
            //SongBLL songBll = new SongBLL();
            //var result = songBll.BaiduTingSearch("同班同学");
            var s = "1";
            var s1 = DataConvertHelper.ToDateTime(s);
            var s2 = DataConvertHelper.ToDateTime(s, DateTime.Now);
            var s3 = DataConvertHelper.ToInt(s);
            var s4 = DataConvertHelper.ToInt(s,-1);
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
