using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Abstractions
{
    public interface IJWTManagerRepository
    {
        Task<Tokens> GetToken(User datos);
       // Task<IEnumerable<User>> Login(Login login);
    }
}
