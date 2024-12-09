using ProjectLottery.V1.Domain.DTOs.Auth;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Security
{
    public interface IAuthorizeServices
    {
        Task<Tokens> Authenticate(Login login);
    }
}