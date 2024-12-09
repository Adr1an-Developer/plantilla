using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Regions
{
    public interface IStateRepository : IGenericReadOnlyRepository<State>
    {
        Task<IEnumerable<State>> GetAllStates();

        Task<State> GetStateByID(string id);

        Task<State> GetStateByName(string stateName);

        Task<IEnumerable<State>> GetStatesByCountryId(string countryId);
    }
}