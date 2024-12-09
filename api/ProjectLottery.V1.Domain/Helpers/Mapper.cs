using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Helpers
{
    public class Mapper
    {
        public static LoggedOutput GetLoggedInfo(IHttpContextAccessor httpContextAccessor)
        {
            var us = UtilsSegurity.getLoggedUser(httpContextAccessor);

           return new LoggedOutput()
            {
                Email = us.Email,
                UserID = us.UserID,
                Fullname = us.Fullname,
                isValidExp = us.isValidExp,
                Name = us.Name,
                Profile = us.Profile,
                UserName = us.UserName
            };
        }

    }
}
