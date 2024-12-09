using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Security
{
    public interface IUserDetailsRepository : IGenericRepository<UserDetails>
    {
        // Task CreateUserDetailAsync(AddUserDetails entity, Guid createAt);
    }
}
