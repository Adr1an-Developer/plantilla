using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Entities.Global;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Regions
{
    public interface ICityServices : IServiceBase<City>
    {
        Task<DataResults<City>> GetAll();

        Task<DataResult<City>> GetbyId(string id);

        Task<DataResult<City>> GetByName(string CityName);
    }
}