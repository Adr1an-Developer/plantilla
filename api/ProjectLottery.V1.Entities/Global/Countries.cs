using ProjectLottery.V1.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLottery.V1.Entities.Global
{
    [Table("country")]
    public class Country : AuditEntityNotUserFields
    {
        [Column("id")]
        public string Id
        {
            get; set;
        }

        [Column("country_code")]
        public int CountryCode
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

        [Column("numeric_code")]
        public string NumericCode
        {
            get; set;
        }

        [Column("phone_code")]
        public string PhoneCode
        {
            get; set;
        }

        [Column("capital")]
        public string Capital
        {
            get; set;
        }

        [Column("currency")]
        public string Currency
        {
            get; set;
        }

        [Column("currency_name")]
        public string CurrencyName
        {
            get; set;
        }

        [Column("currency_symbol")]
        public string CurrencySymbol
        {
            get; set;
        }

        [Column("region")]
        public string Region
        {
            get; set;
        }

        [Column("subregion")]
        public string SubRegion
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

        [Column("emoji")]
        public string Emoji
        {
            get; set;
        }

        [Column("emojiU")]
        public string EmojiU
        {
            get; set;
        }
    }

    public class CountryTimezone
    {
        public string Id
        {
            get; set;
        }

        public int CountryCode
        {
            get; set;
        }

        public string zoneName
        {
            get; set;
        }

        public int gmtOffset
        {
            get; set;
        }

        public string gmtOffsetName
        {
            get; set;
        }

        public string abbreviation
        {
            get; set;
        }

        public string tzName
        {
            get; set;
        }
    }

    public class CountryTranslations
    {
        public string Id
        {
            get; set;
        }

        public int CountryCode
        {
            get; set;
        }

        public string pt
        {
            get; set;
        }

        public string es
        {
            get; set;
        }
    }
}