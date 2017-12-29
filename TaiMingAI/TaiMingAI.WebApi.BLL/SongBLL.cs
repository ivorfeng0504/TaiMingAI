using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAi.WebApi.Model;
using TaiMingAi.WebApi.Model.Song;
using TaiMingAI.Tools;

namespace TaiMingAI.WebApi.BLL
{
    public class SongBLL
    {

        public SearchSongResult BaiduTingSearch(string search)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("query", search);
            var url = GetUrlByMethod(SongMethod.Search, dic);

            return WebApiHelper.GetResponseJson<SearchSongResult>(url);
        }
        private string GetUrlByMethod(string method, Dictionary<string, string> dic)
        {
            var url = WebApiConfigModel.BaiduSongApi + "?method=" + method;
            if (dic != null && dic.Count() > 0)
            {
                foreach (var item in dic)
                {
                    url += "&" + item.Key + "=" + item.Value;
                }
            }
            return url;
        }
    }
}
