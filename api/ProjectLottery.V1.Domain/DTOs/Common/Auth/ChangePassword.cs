﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.DTOs.Auth
{
    public class ChangePassword
    {
        public string UserId
        {
            get; set;
        }
        public string OldPassword
        {
            get; set;
        }
        public string NewPassword
        {
            get; set;
        }
    }
}
