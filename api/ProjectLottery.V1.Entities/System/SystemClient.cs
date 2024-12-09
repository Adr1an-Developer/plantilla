using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.System
{
    [Table("system_client")]
    public class SystemClient : AuditEntity
    {
        [Column("id")]
        public string Id
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

        [Column("company_name")]
        public string CompanyName
        {
            get; set;
        }

        [Column("register_id")]
        public string Register_Id
        {
            get; set;
        }

        [Column("email")]
        public string Email
        {
            get; set;
        }

        [Column("phone")]
        public string Phone
        {
            get; set;
        }

        [Column("address")]
        public string Address
        {
            get; set;
        }

        [Column("city_id")]
        public string CityId
        {
            get; set;
        }

        [Column("postal_code")]
        public string PostalCode
        {
            get; set;
        }

        [Column("status")]
        public string Status
        {
            get; set;
        }

        [Column("client_type_id")]
        public string ClientTypeId
        {
            get; set;
        }

        [Column("notes")]
        public string Notes
        {
            get; set;
        }

        [NotMapped]
        public string CountryName
        {
            get; set;
        }

        [NotMapped]
        public string CountryId
        {
            get; set;
        }

        [NotMapped]
        public string StateName
        {
            get; set;
        }

        [NotMapped]
        public string StateId
        {
            get; set;
        }

        [NotMapped]
        public string CityName
        {
            get; set;
        }
    }
}