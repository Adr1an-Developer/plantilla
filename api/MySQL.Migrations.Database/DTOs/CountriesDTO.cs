using System.Collections.Generic;

namespace MySQL.Migrations.Database.DTOS
{
    public class CountriesDTO
  {
    public int id { get; set; }
    public string name { get; set; }
    public string iso2 { get; set; }
    public string numeric_code { get; set; }
    public string phone_code { get; set; }
    public string capital { get; set; }
    public string currency { get; set; }
    public string currency_name { get; set; }
    public string currency_symbol { get; set; }
    public string region { get; set; }
    public string subregion { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string emoji { get; set; }
    public string emojiU { get; set; }
    public List<Timezone> timezones { get; set; }
    public Translations translations { get; set; }
  }

  public class Timezone
  {
    public string zoneName { get; set; }
    public int gmtOffset { get; set; }
    public string gmtOffsetName { get; set; }
    public string abbreviation { get; set; }
    public string tzName { get; set; }
  }

  public class Translations
  {
    public string pt { get; set; }
    public string es { get; set; }
  }
}
