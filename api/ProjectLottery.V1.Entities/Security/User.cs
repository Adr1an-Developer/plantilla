using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Security
{
    [Table("user")]
    public class User : AuditEntity
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("user_name")]
        public string UserName
        {
            get; set;
        }

        [Column("password")]
        public string Password
        {
            get; set;
        }

        [Column("profile_id")]
        public string ProfileId
        {
            get; set;
        }

        [Column("first_name")]
        public string FirstName
        {
            get; set;
        }

        [Column("last_name")]
        public string LastName
        {
            get; set;
        }

        [Column("email")]
        public string Email
        {
            get; set;
        }

        [Column("is_first_login")]
        public bool isFirstLogin { get; set; } = true;

        [Column("external_code")]
        public string? ExternalCode
        {
            get; set;
        }

        [NotMapped]
        public string ProfileName
        {
            get; set;
        }
    }
}