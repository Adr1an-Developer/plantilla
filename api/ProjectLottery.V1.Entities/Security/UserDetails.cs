using ProjectLottery.V1.Entities.Common;
using ProjectLottery.V1.Entities.Global;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Security
{
    [Table("user_detail")]
    public class UserDetails : AuditEntity
    {
        [Column("id")]
        public Guid Id
        {
            get; set;
        }
        [Column("user_id")]
        public Guid UserId
        {
            get; set;
        }
        [Column("language_id")]
        public Guid LanguageId
        {
            get; set;
        }
        [Column("city_id")]
        public Guid? CityId
        {
            get; set;
        }
        [Column("address")]
        public string? Address
        {
            get; set;
        }
        [Column("telefone")]
        public string? Telefone
        {
            get; set;
        }

        // public virtual City? City { get; set; }
        public virtual Language? Language
        {
            get; set;
        }
    }
}
