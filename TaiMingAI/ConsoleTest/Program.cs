using System;
using System.Collections.Generic;
using System.Configuration;
using TaiMingAI.Caches;
using TaiMingAI.Tools;
using TaiMingAI.WebApi;
using TaiMingAI.WebApi.BLL;
using TaiMingAI.WebApi.Models;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var appId = "1001";
            var key = "D9B0C2E7-B25E-43AC-B69B-FAF7AA4DAADB";
            var timestamp = TimestampHelper.GetNowTimestampOfJs();
            var sign = appId + timestamp + key;
            sign = sign.Sha256();

            Dictionary<string, string> headers = new Dictionary<string, string> {
                {"appId",appId },
                {"time",timestamp.ToString() },
                {"sign",sign }
            };
            var url = "http://api.taiming.com/api/User/LoginDo?LoginName=admin&Password=123456";
            var s = WebApiHelper.GetResponseJson(url, headers);
            var response = JsonHelper.FromJson<ApiResult<LoginDoRespons>>(s);
            url = "http://api.taiming.com/api/Song/BaiduTingSearch?search=123";
            var t ="";
            t = response.ResultData.Ticket;
            headers.Add("Authorization", "BasicAuth " + t);
            s = WebApiHelper.GetResponseJson(url, headers);
            //SongBLL songBll = new SongBLL();
            //var result = songBll.BaiduTingSearch("双节棍");
            Console.Write(s);
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
