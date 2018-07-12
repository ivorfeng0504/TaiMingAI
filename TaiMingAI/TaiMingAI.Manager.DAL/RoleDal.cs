using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.DAL
{
    public class RoleDal
    {
        private DBContext dBContext = DBContext.InitDBContext;
        public List<Role> GetRoleList()
        {
            var sql = dBContext.GetSql("RoleSql.GetRoleList");
            var result = dBContext.ExecuteDataTable(ManagerConfig.DbManager, CommandType.Text, sql, null);
            return DataConvertHelper.DataTableToList<Role>(result);
        }

        public bool InsertRole(RoleDto dto)
        {
            var sql = dBContext.GetSql("RoleSql.InsertRole");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@Name",dto.Name),
                new SqlParameter("@Limits",dto.Limits),
                new SqlParameter("@Description",dto.Description),
                new SqlParameter("@IsUse",dto.IsUse),
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }

        public bool UpdateRole(RoleDto dto)
        {
            var sql = dBContext.GetSql("RoleSql.UpdateRole");
            SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("@Name",dto.Name),
                new SqlParameter("@Limits",dto.Limits),
                new SqlParameter("@Description",dto.Description),
                new SqlParameter("@IsUse",dto.IsUse),
                new SqlParameter("@Id",dto.Id)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlParameter);
            return result > 0;
        }
    }
}
