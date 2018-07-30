using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAi.WebApi.Model;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;

namespace TaiMingAI.WebApi.DAL
{
    public class UserDal
    {
        #region 私有属性
        private static UserDal _userDll { get; set; }
        private static readonly object Obj = new object();
        private UserDal() { }
        public static UserDal CreateUserDll
        {
            get
            {
                if (_userDll == null)
                {
                    lock (Obj)
                    {
                        if (_userDll == null)
                        {
                            _userDll = new UserDal();
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
                new SqlParameter("@Password", info.Password),
                new SqlParameter("@Mobile", info.Mobile),
                new SqlParameter("@Email", info.Email));
            return result > 0;
        }

        public List<TmingUserInfo> GetUserInfoList()
        {
            var dBContext = DBContext.InitDBContext;
            var sql = dBContext.GetSql("UserSql.GetUserInfoList");
            var data = dBContext.ExecuteDataTable(WebApiConfig.DbUserConnectionString, CommandType.Text, sql);
            return DataConvertHelper.DataTableToList<TmingUserInfo>(data);
        }

        public TmingUserInfo GetUserInfoById(int id)
        {
            var dBContext = DBContext.InitDBContext;
            var sql = dBContext.GetSql("UserSql.GetUserInfoById");
            var data = dBContext.ExecuteDataTable(WebApiConfig.DbUserConnectionString, CommandType.Text, sql,
                new SqlParameter("@id", id));
            if (data == null || data.Rows.Count == 0) return null;
            return DataConvertHelper.DataRowToModel<TmingUserInfo>(data.Rows[0]);
        }
    }
}
