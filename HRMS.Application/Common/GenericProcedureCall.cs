using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace HRMS.Application.Common
{
    public static class GenericProcedureCall
    {
        #region StoredProcedureName
        public static class StoredProcedure
        {

            public const string sp_GenerateMasterCode = "sp_GenerateMasterCode";
            public const string Sp_Sign_Up = "Sp_Sign_Up";
            public const string Sp_User_Login = "Sp_User_Login";
            public const string Sp_GetBranchById = "Sp_GetBranchById";
            public const string Sp_AddBranch = "Sp_AddBranch";


            public const string Sp_AddUser = "Sp_Add_User";
            public const string Sp_Add_Default_User = "Sp_Add_Default_User";
            public const string Sp_GetModul_SubModule = "Sp_GetModul_SubModule";
            public const string Sp_Get_Role = "Sp_Get_Role";
            public const string SP_GET_CLIENT = "SP_GET_CLIENT";
            public const string SP_GET_CLIENT_BY_ID = "SP_GET_CLIENT_BY_ID";
            public const string SP_ADD_UPDATE_ROLE = "SP_ADD_UPDATE_ROLE";

            public const string Sp_Get_Menu = "Sp_Get_Menu";
        }
        #endregion
        //public static async Task<T> UseConnectionAsync<T>(DbContext context, Func<DbConnection, Task<T>> action)
        //{
        //    var connection = context.Database.GetDbConnection();
        //    try
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            await connection.OpenAsync();

        //        return await action(connection);
        //    }
        //    finally
        //    {
        //        if (connection.State == ConnectionState.Open)
        //            await connection.CloseAsync();
        //    }
        //}
        #region StoredProcedure
        public static DataSet ExecuteStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            using (var cmd = dbConnection.CreateCommand())
            {
                cmd.Transaction = dbTransaction;
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                using (var da = DbProviderFactories.GetFactory(dbConnection).CreateDataAdapter())
                {
                    da.SelectCommand = cmd;
                    var ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        public static DataSet ExecuteStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters, DbConnection dbConnection)
        {
            using (var cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                using (var da = DbProviderFactories.GetFactory(dbConnection).CreateDataAdapter())
                {
                    da.SelectCommand = cmd;
                    var ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        public static DataTable ExecuteFunctionProcedure(string functionProcedureName, IEnumerable<SqlParameter> parameters, DbConnection dbConnection)
        {
            var ds = new DataSet();
            using (var cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM DBO." + functionProcedureName;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                using (var da = DbProviderFactories.GetFactory(dbConnection).CreateDataAdapter())
                {
                    da.SelectCommand = cmd;
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public static string ExecuteFunctionProcedureScalar(string functionProcedureName, IEnumerable<SqlParameter> parameters, DbConnection dbConnection)
        {
            var ds = new DataSet();
            using (var cmd = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                cmd.CommandText = "SELECT DBO." + functionProcedureName;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                var result = cmd.ExecuteScalar().ToString();
                dbConnection.Close();
                return result;
            }
        }
        #endregion
        public static IList<T> ToIList<T>(List<T> t)
        {
            return t;
        }
        #region CommonMethod
        public static class CommonMethod
        {
            public static List<T> ConvertToList<T>(DataTable dt)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
                var properties = typeof(T).GetProperties();
                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            try
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    return objT;
                }).ToList();
            }
        }
        #endregion
    }
}
