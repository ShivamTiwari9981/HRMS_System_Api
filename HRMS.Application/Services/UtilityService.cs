using HRMS.Application.Common;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace HRMS.Application.Services
{
    public class UtilityService : BaseService, IUtilityService
    {
        public UtilityService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession) { }
        public (int err_no, string err_msg) GenerateMasterCode(string TableName)
        {
            int err_no = 0;
            string err_msg = "";
            try
            {
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ClientId", ClientId));
                param.Add(new SqlParameter("@TableName", TableName));
                param.Add(new SqlParameter("@CreatedBy", UserId));
                param.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_no));
                param.Add(new SqlParameter("@Msg", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_msg));
                var result = GenericProcedureCall.ExecuteStoredProcedureWithTransation(GenericProcedureCall.StoredProcedure.sp_GenerateMasterCode,
                    param, _unitOfWork.GetConnection(),
                    _unitOfWork.GetTransaction()
                    );
                err_no = (int)param.Find(x => x.ParameterName == "@ErrNo")?.Value;
                err_msg = param.Find(x => x.ParameterName == "@Msg")?.Value.ToString() ?? "";

            }
            catch (Exception ex)
            {
                err_no = 1;
                err_msg = ex.Message;
            }
            return (err_no, err_msg);
        }

        public async Task<int> GetNextDisplayOrderAsync(DisplayOrderType type, Guid Id )
        {
            switch (type)
            {
                case DisplayOrderType.Department:

                    int departmentMaxOrder = await _unitOfWork
                        .DepartmentRepository
                        .MaxAsync(
                            x => x.ClientId == ClientId,
                              x => x.DisplayOrder
                        );

                    return departmentMaxOrder + 1;


                case DisplayOrderType.Designation:

                    int designationMaxOrder = await _unitOfWork
                        .DesignationRepository
                        .MaxAsync(
                            x => x.ClientId == ClientId
                              && x.DepartmentId == Id, 
                              x=> x.DisplayOrder
                        ) ?? 0;

                    return designationMaxOrder + 1;


                default:
                    throw new Exception("Invalid display order type");
            }
        }

    }
}
