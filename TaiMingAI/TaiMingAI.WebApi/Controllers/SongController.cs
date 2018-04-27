using System;
using System.Web.Http;
using System.Web.Http.Results;
using TaiMingAi.WebApi.Model.Song;
using TaiMingAI.WebApi.App_Start;
using TaiMingAI.WebApi.BLL;

namespace TaiMingAI.WebApi.Controllers
{
    public class SongController : BaseController
    {
        [HttpGet]
        [RequestAuthorize]
        public ApiResult<SearchSongResult> BaiduTingSearch(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return ErrorResponseMsg<SearchSongResult>(null, "搜索内容不能为空");
                }
                SongBLL songBll = new SongBLL();
                var result = songBll.BaiduTingSearch(search);
                return SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                return ExceptionResponseMsg<SearchSongResult>("BaiduTingSearch", ex);
            }
        }

    }
}