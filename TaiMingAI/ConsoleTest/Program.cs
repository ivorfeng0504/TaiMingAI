using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Caches;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonSection person = ConfigurationManager.GetSection("Person") as PersonSection;
            Console.WriteLine("name={0},age={1}", person.Age, person.Name);
            var key = "斗战胜佛";
            var value = CacheHelper.DoRedisString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                value = "我若成佛,天下无魔";
                CacheHelper.DoRedisString.Set(key, value);
            }
            Console.WriteLine(value);
            key = "齐天大圣";
            value = CacheHelper.DoRedisString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                value = "我若成魔,佛奈我何";
                CacheHelper.DoRedisString.Set(key, value);
            }
            CacheHelper.DoRedisList.LPush(key + "_list", value);
            Console.WriteLine(value);
            Console.ReadKey();
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
