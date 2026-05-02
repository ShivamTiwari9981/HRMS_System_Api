using AutoMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class UtilityService :BaseService, IUtilityService
    {
        public UtilityService(
      IUnitOfWork unitOfWork,
      ICurrentSession currentsession
      )
      : base(unitOfWork, currentsession ) { }
    }
}
