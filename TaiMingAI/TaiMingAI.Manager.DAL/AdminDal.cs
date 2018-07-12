using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.DAL
{
    public class AdminDal
    {
        #region 属性
        private readonly static object obj = new Object();
        private static AdminDal adminDal;
        private AdminDal() { }
        public static AdminDal CreatDal()
        {
            if (adminDal == null)
            {
                lock (obj)
                {
                    if (adminDal == null)
                    {
                        adminDal = new AdminDal();
                    }
                }
            }
            return adminDal;
        }
        private DBContext dBContext = DBContext.InitDBContext;
        #endregion

        public List<Administrator> GetAdminList()
        {
            var sql = dBContext.GetSql("Administrator.GetAdminList");
            var result = dBContext.ExecuteDataTable(ManagerConfig.DbManager, CommandType.Text, sql, null);
            return DataConvertHelper.DataTableToList<Administrator>(result);
        }

        public bool InsertAdmin(AdministratorDto dto)
        {
            var sql = dBContext.GetSql("Administrator.InsertAdmin");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@LoginName",dto.LoginName),
                new SqlParameter("@Password",dto.Password),
                new SqlParameter("@RoleName",dto.RoleName),
                new SqlParameter("@Role",dto.Role),
                new SqlParameter("@Mobile",dto.Mobile),
                new SqlParameter("@Email",dto.Email),
                new SqlParameter("@State",dto.State),
                new SqlParameter("@NickName",dto.NickName)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }

        public bool UpdateAdmin(AdministratorDto dto)
        {
            var sql = dBContext.GetSql("Administrator.UpdateAdmin");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@LoginName",dto.LoginName),
                new SqlParameter("@Password",dto.Password),
                new SqlParameter("@RoleName",dto.RoleName),
                new SqlParameter("@Role",dto.Role),
                new SqlParameter("@Mobile",dto.Mobile),
                new SqlParameter("@Email",dto.Email),
                new SqlParameter("@NickName",dto.NickName),
                new SqlParameter("@State",dto.State),
                new SqlParameter("@Id",dto.Id)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="dto">管理员信息</param>
        /// <returns>操作结果</returns>
        public bool ResetPassword(AdministratorDto dto)
        {
            var sql = dBContext.GetSql("Administrator.ResetPassword");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@Password",dto.Password),
                new SqlParameter("@Id",dto.Id)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }

        /// <summary>
        /// 更新管理员状态
        /// </summary>
        /// <param name="dto">管理员信息</param>
        /// <returns>操作结果</returns>
        public bool UpdateAdminState(AdministratorDto dto)
        {
            var sql = dBContext.GetSql("Administrator.UpdateAdminState");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@State",dto.State),
                new SqlParameter("@Id",dto.Id)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }

        public Administrator AdminLogin(string name, string password)
        {
            var sql = dBContext.GetSql("Administrator.AdminLogin");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@LoginName",name),
                new SqlParameter("@Password",password)
            };
            var result = dBContext.ExecuteDataTable(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            if (result == null || result.Rows.Count == 0)
            {
                return null;
            }
            return DataConvertHelper.DataRowToModel<Administrator>(result.Rows[0]);
        }
    }
}
