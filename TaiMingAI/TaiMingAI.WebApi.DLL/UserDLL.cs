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
            var sql = @"INSERT INTO [TmingUserInfo]([Name],[Powsword],[Mobile],[Email])
                        VALUES(@Name,@Powsword,@Mobile,@Email)";
            var dBContext = DBContext.InitDBContext;
            var result = dBContext.ExecuteNonQuery(WebApiConfig.DbUserConnectionString, CommandType.Text, sql,
                new SqlParameter("@Name", info.Name),
                new SqlParameter("@Powsword", info.Powsword),
                new SqlParameter("@Mobile", info.Mobile),
                new SqlParameter("@Email", info.Email));
            return result > 0;
        }

        public List<TmingUserInfo> GetUserInfoList()
        {
            var sql = "select * from TmingUserInfo";
            var dBContext = DBContext.InitDBContext;
            var data = dBContext.ExecuteDataTable("Data Source=.;Initial Catalog=TaiMingUser;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", CommandType.Text, sql);
            return DataConvertHelper.DataTableToList<TmingUserInfo>(data);
        }
    }
}
