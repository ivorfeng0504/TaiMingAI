using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiMingAI.Manager.Models
{
    /// <summary>
    /// 数据的异步请求参数
    /// </summary>
    public class TableRequestBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页数据量
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 返回的数据
    /// </summary>
    public class TableResponse<T>
    {
        /// <summary>
        /// 数据状态;成功的状态码200，默认：0
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 每页数据量
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> data { get; set; }
    }
}