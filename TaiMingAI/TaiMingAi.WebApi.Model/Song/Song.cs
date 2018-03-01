using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAi.WebApi.Model.Song
{
    public class SearchSongResult
    {
        public List<Song> song { get; set; }
        public List<Artist> album { get; set; }
        public List<Album> artist { get; set; }
        public string order { get; set; }
        public int error_code { get; set; }
    }

    /// <summary>
    /// 歌曲
    /// </summary>
    public class Song
    {
        public string bitrate_fee { get; set; }
        public int weight { get; set; }
        public string songname { get; set; }
        public int resource_type { get; set; }
        public int songid { get; set; }
        public int has_mv { get; set; }
        public int yyr_artist { get; set; }
        public int resource_type_ext { get; set; }
        public string artistname { get; set; }
        public string info { get; set; }
        public int resource_provider { get; set; }
        public string control { get; set; }
        public string encrypted_songid { get; set; }
    }
    /// <summary>
    /// 专辑
    /// </summary>
    public class Album
    {
        public string albumname { get; set; }
        public int weight { get; set; }
        public string artistname { get; set; }
        public int resource_type_ext { get; set; }
        public string artistpic { get; set; }
        public int albumid { get; set; }
    }

    /// <summary>
    /// 歌手
    /// </summary>
    public class Artist
    {
        public int yyr_artist { get; set; }
        public string artistname { get; set; }
        public int artistid { get; set; }
        public string artistpic { get; set; }
        public int weight { get; set; }
    }

    /// <summary>
    /// 百度音乐api方法名
    /// </summary>
    public struct SongMethod
    {
        /// <summary>
        /// 百度音乐api 搜索接口方法名
        /// </summary>
        public const string Search = "baidu.ting.search.catalogSug";
    }
}
