using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.DTOs.SystemClient
{
    public class AddSystemClient
    {
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public string CompanyName
        {
            get; set;
        }

        public string Register_Id
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string Phone
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }

        public string CityId
        {
            get; set;
        }

        public string PostalCode
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }

        public string ClientTypeId
        {
            get; set;
        }

        public string Notes
        {
            get; set;
        }
    }
}