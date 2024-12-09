using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Security
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> Login(Login login);

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserByID(string id);

        Task<User> GetUserByName(string userName);
    }
}