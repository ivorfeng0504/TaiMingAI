using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAi.WebApi.Model;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;

namespace TaiMingAI.WebApi.DLL
{
    public class UserDll
    {
        #region 私有属性
        private static UserDll _userDll { get; set; }
        private static readonly object Obj = new object();
        private UserDll() { }
        public static UserDll CreateUserDll
        {
            get
            {
                if (_userDll == null)
                {
                    lock (Obj)
                    {
                        if (_userDll == null)
                        {
                            _userDll = new UserDll();
                        }
                    }
                }
                return _userDll;
            }
        }
        #endregion
        /// <summary>
        /// 内部注册用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InternalRegisterUser(TmingUserInfo info)
        {
            var dBContext = DBContext.InitDBContext;
            var sql = dBContext.GetSql("UserSql.InternalRegisterUser");
            var result = dBContext.ExecuteNonQuery(WebApiConfig.DbUserConnectionString, CommandType.Text, sql,
                new SqlParameter("@Name", info.Name),
                new SqlParameter("@Powsword", info.Powsword),
                new SqlParameter("@Mobile", info.Mobile),
                new SqlParameter("@Email", info.Email));
            return result > 0;
        }

        public List<TmingUserInfo> GetUserInfoList()
        {
            var dBContext = DBContext.InitDBContext;
            var sql = dBContext.GetSql("UserSql.GetUserInfoList");
            var data = dBContext.ExecuteDataTable(WebApiConfig.DbUserConnectionString, CommandType.Text, sql);
            var list = DataConvertHelper.DataTableToList<TmingUserInfo>(data);
            return DataConvertHelper.DataTableToList<TmingUserInfo>(data);
        }
    }
}
