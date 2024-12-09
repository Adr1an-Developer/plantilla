using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.DTOs.Profile
{
    public class AddProfile
    {
        public string Name
        {
            get; set;
        }
        public string Abbreviation
        {
            get; set;
        }
    }
}
