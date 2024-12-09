using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Global
{
    [Table("city")]
    public class City
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

        [Column("city_code")]
        public int CityCode
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