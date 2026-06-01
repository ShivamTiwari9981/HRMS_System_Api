using HRMS.Application.DTOs.RequestDto;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.ExtensionMapper
{
    public static class EmployeeSalaryMapper
    {
        public static EmployeeSalaryEntity GetEntity(
        EmployeeSalaryRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new EmployeeSalaryEntity
            {
                BasicSalary = dto.BasicSalary,
                HRA = dto.HRA,
                Allowance = dto.Allowance,
                Deduction = dto.Deduction,
                NetSalary = dto.NetSalary,
                EffectiveFrom = dto.EffectiveFrom,
                IsCurrent = dto.IsCurrent,
            };
        }
    }
}
