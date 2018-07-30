using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAi.WebApi.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.WebApi.BLL
{
    public class TaskBll
    {
        /// <summary>
        ///     创建任务
        /// </summary>
        /// <returns></returns>
        public bool CreateTask(CreateTaskRequest request)
        {
            LogHelper.InfoLog(JsonHelper.ToJson(request));
            return true;
        }
    }
}
