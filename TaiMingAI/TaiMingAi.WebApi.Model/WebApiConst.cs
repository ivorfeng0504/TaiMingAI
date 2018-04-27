using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAi.WebApi.Model
{
    /// <summary>
    /// 常量参数类
    /// </summary>
    public class WebApiConst
    {
        /// <summary>
        /// 用户密码加密密钥
        /// </summary>
        public const string PaswordKey = "TmingUsesPasword:md5";
        /// <summary>
        /// appId对应的秘钥
        /// </summary>
        public static readonly Dictionary<int, string> SecretKeyDic = new Dictionary<int, string> {
            {1000, "D78AF91F-2C51-4B16-8DE5-FDB3D180BCFD"},
            {1001, "D9B0C2E7-B25E-43AC-B69B-FAF7AA4DAADB"},
            {1002, "691E0A8A-84DD-4A8C-B6EA-B4920705B68E"},
            {1003, "FBA62D1E-DAE4-4FC3-B8B2-DEE2A0672FEA"},
            {1004, "45E0B645-0D62-4B1E-AF15-FE6C9BD6AA4A"},
            {1005, "57098CB4-D603-4B0C-A9EE-1D3C0C64C1C3"},
            {1006, "BD3C5349-CD0D-4F26-9EE8-984C9F920FA3"},
        };
    }
}
