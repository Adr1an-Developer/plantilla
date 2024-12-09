using ProjectLottery.V1.Domain.Data.Repository;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Regions
{
    public interface ICountryRepository : IGenericReadOnlyRepository<Country>
    {
        string LoggedUserId
        {
            get;
        }

        bool IsAdmin
        {
            get;
        }

        void SetLoggedUserInfo(string loggedUserId, bool isAdmin = false);

        Task<IEnumerable<Country>> GetAllCountrys();

        Task<Country> GetCountryByID(string id);

        Task<Country> GetCountryByName(string CountryName);
    }
}