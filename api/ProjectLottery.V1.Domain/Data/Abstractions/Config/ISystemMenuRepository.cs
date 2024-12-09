using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Config
{
    public interface ISystemMenuRepository : IGenericRepository<SystemMenu>
    {
        Task<IEnumerable<SystemMenu>> GetAllSystemMenus();

        Task<SystemMenu> GetSystemMenuByID(string id);

        Task<SystemMenu> GetSystemMenuByName(string SystemMenuName);
    }
}