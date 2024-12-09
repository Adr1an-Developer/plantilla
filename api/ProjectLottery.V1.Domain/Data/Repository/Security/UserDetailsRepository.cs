using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Domain.Data.Contexts;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Repository.Security
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
