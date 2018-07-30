using System;

namespace TaiMingAI.Manager.Model
{
    /// <summary>
    /// 菜单导航
    /// </summary>
    public class Navbar
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 是否展开；false：否；true：是；
        /// </summary>
        public bool spread { get; set; }

        /// <summary>
        /// 是否新窗口打开；false：否；true：是；
        /// </summary>
        public bool target { get; set; }

        /// <summary>
        /// 是否显示;false：否；true：是；
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
