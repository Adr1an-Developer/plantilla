using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Entities.Global;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Regions
{
    public interface ICountryServices : IServiceBase<Country>
    {
        Task<DataResults<Country>> GetAll();

        Task<DataResult<Country>> GetbyId(string id);

        Task<DataResult<Country>> GetByName(string countryName);
    }
}