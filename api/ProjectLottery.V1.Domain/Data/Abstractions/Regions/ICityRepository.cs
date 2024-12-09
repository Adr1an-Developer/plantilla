using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Regions
{
    public interface ICityRepository : IGenericReadOnlyRepository<City>
    {
        Task<IEnumerable<City>> GetAllCities();

        Task<City> GetCityByID(string id);

        Task<City> GetCityByName(string CityName);

        Task<IEnumerable<City>> GetCitiesByStateId(string stateId);
    }
}