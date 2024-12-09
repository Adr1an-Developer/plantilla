using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Security
{
    [Table("profiles")]
    public class Profile : AuditEntity

    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("name")]
        public string Name
        {
            get; set;
        }

        [Column("abbreviation")]
        public string Abbreviation
        {
            get; set;
        }

        [NotMapped]
        public IEnumerable<User>? Users
        {
            get; set;
        }
    }
}