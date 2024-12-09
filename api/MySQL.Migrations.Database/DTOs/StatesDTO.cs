namespace MySQL.Migrations.Database.DTOs
{
    public class StatesDTO
  {
    public int id { get; set; }
    public string name { get; set; }
    public int country_id { get; set; }
    public string state_code { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
  }
}
