using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Manager.Model
{
    public class BaseResult
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 错误提示
        /// </summary>
        public string ErrMessage { get; set; }
    }

    public class ControllerResult<T> : BaseResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回数据列表
        /// </summary>
        public List<T> List { get; set; }
    }
    public class ControllerResult : BaseResult
    {
        public void DefaultSuccess(string msg = null)
        {
            IsSuccess = true;
            Message = string.IsNullOrEmpty(msg) ? "处理成功~" : msg;
        }
        public void DefaultError(string msg = null, string errMsg = null)
        {
            IsSuccess = false;
            Message = string.IsNullOrEmpty(msg) ? "处理失败！" : msg;
            ErrMessage = string.IsNullOrEmpty(errMsg) ? "处理失败！" : errMsg;
        }
    }
}
