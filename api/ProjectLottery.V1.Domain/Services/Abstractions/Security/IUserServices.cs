using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.User;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Security
{
    public interface IUserServices : IServiceBase<User>
    {
        Task<DataResults<User>> GetAll();

        Task<DataResult<User>> GetbyId(string id);

        Task<DataResult<User>> GetByName(string userName);

        Task<DataResult<User>> Create(AddUser entity);

        Task<DataResult<User>> Update(User user);

        Task<DataResult<User>> Delete(string id);

        Task<bool> ChangePassword(string userID, string password);
    }
}