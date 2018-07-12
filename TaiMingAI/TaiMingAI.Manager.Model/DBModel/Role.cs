using System;

namespace TaiMingAI.Manager.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限范围
        /// </summary>
        public string Limits { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否使用角色；0不启用，1:启用
        /// </summary>
        public bool IsUse { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
