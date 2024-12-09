using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.System
{
    public interface IClientRepository : IGenericRepository<SystemClient>
    {
        Task<IEnumerable<SystemClient>> GetAllSystemClient();

        Task<SystemClient> GetSystemClientById(string id);

        Task<SystemClient> GetSystemClientByName(string clientName);
    }
}