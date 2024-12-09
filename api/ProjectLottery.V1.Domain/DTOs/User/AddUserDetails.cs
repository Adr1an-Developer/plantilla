using ProjectLottery.V1.Domain.DTOs.Common;

namespace ProjectLottery.V1.Domain.DTOs.User
{
    public class AddUserDetails : AuditCreate
  {
    public Guid UserId { get; set; }
    public Guid CityId { get; set; }
    public Guid LanguageId { get; set; }
    public string? Address { get; set; }
    public string? Telefone { get; set; }

  }
}
