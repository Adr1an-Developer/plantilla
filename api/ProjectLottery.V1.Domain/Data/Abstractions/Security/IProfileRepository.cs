using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Security
{
    public interface IProfileRepository : IGenericRepository<Profile>
    {

        Task<IEnumerable<Profile>> GetAllProfiles();
        Task<Profile> GetProfileByID(string id);
        Task<Profile> GetProfileByName(string profileName);

    }
}
