using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Global
{
    [Table("language")]
    public class Language : AuditEntityNotUserFields
    {
        [Column("id")]
        public Guid Id
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
    }
}
