using System;

namespace TaiMingAI.Manager.Model
{
    public class RoleDto
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
        public string IsUseStr
        {
            get { return IsUse ? "启用" : "禁用"; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStr
        {
            get { return UpdateTime.Year > 2000 ? UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") : "--"; }
        }
    }
}
