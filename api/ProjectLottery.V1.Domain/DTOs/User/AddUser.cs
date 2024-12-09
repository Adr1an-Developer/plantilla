using ProjectLottery.V1.Domain.DTOs.Common;

namespace ProjectLottery.V1.Domain.DTOs.User
{
    public class AddUser : AuditCreate
    {
        public string UserName
        {
            get; set;
        }

        public string? Password
        {
            get; set;
        }

        public string ProfileId
        {
            get; set;
        }

        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string ExternalCode
        {
            get; set;
        }

        public AddUserDetails? Detail
        {
            get; set;
        }
    }
}