using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Global
{
    [Table("state")]
    public class State
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

        [Column("country_code")]
        public int CountryCode
        {
            get; set;
        }

        [Column("state_code")]
        public int StateCode
        {
            get; set;
        }

        [Column("latitude")]
        public string Latitude
        {
            get; set;
        }

        [Column("longitude")]
        public string Longitude
        {
            get; set;
        }
    }
}