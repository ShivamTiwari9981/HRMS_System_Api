using HRMS.Application.Common;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HRMS.Application.Services
{
    public class UtilityService :IUtilityService
    {
        public (int err_no, string err_msg) GenerateMasterCode(IUnitOfWork _unitOfWork,Guid ClientId,string TableName)
        {
            int err_no = 0;
            string err_msg = "";
            try
            {
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ClientId", ClientId));
                param.Add(new SqlParameter("@TableName", TableName));
                param.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_no));
                param.Add(new SqlParameter("@ErrMsg", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_msg));
                var result = GenericProcedureCall.ExecuteStoredProcedure(GenericProcedureCall.StoredProcedure.sp_GenerateMasterCode, param, _unitOfWork.GetConnection());
                err_no = (int)param.Find(x => x.ParameterName == "@ErrNo")?.Value;
                err_msg = param.Find(x => x.ParameterName == "@ErrMsg")?.Value.ToString() ?? "";

            }
            catch (Exception ex)
            {
                err_no = 1;
                err_msg = ex.Message;
            }
            return (err_no, err_msg);
        }


    }
}
