using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;

using System.Web;

namespace quanlybenh.Services.Implementation
{
    public class BaseService : IBaseService
    {
        protected IUnitOfWork _unitOfWork { get; set; }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseService(string item)
        {
        }

        public string GetCurrentUserId()
        {
            try
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetCurrentUserName()
        {
            try
            {
                return HttpContext.Current.User.Identity.GetUserName();
            }
            catch (Exception)
            {
                return null;
            }
        }


    }

}
