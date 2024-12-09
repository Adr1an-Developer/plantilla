using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.Profile;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Security
{
    public interface IProfileServices : IServiceBase<Profile>
    {
        Task<DataResults<Profile>> GetAll();

        Task<DataResult<Profile>> GetbyId(string id);

        Task<DataResult<Profile>> GetByName(string ProfileName);

        Task<DataResult<Profile>> Create(AddProfile entity);

        Task<DataResult<Profile>> Update(Profile entity);

        Task<DataResult<Profile>> Delete(string id);
    }
}