using System;
using System.Web.Mvc;
using TaiMingAi.WebApi.Model;
using TaiMingAI.WebApi.BLL;

namespace TaiMingAI.WebApi.Controllers
{
    public class TaskController : BaseController
    {
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> Create(CreateTaskRequest request)
        {
            try
            {
                TaskBll taskBll = new TaskBll();
                var result = taskBll.CreateTask(request);
                return result
                    ? SuccessResponseMsg(true)
                    : ErrorResponseMsg(false, "任务创建失败");
            }
            catch (Exception ex)
            {
                return ExceptionResponseMsg<bool>("TaskController.Create", ex);
            }
        }
    }
}