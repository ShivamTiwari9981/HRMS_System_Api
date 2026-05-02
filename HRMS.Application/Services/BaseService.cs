using AutoMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace HRMS.Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ICurrentSession _currentsession;
        private IHelperFactory _helperFactory;
        protected IUnitOfWork UnitOfWork { get { return _helperFactory.UnitOfWork; } }
        protected BaseService(
            IUnitOfWork unitOfWork,
            ICurrentSession currentsession
            )
        {
            _unitOfWork = unitOfWork;
            _currentsession = currentsession;
        }
        protected IHelperFactory MyHelperFactory
        {
            get
            {
                return _helperFactory;
            }
        }
        protected string ClientId => _currentsession.ClientId;
        protected string UserId => _currentsession.UserId;
    }
}
