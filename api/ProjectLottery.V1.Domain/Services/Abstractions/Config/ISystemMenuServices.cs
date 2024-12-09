using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.SystemMenu;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Config
{
    public interface ISystemMenuServices : IServiceBase<SystemMenu>
    {
        Task<DataResults<SystemMenu>> GetAll();

        Task<DataResult<SystemMenu>> GetbyId(string id);

        Task<DataResult<SystemMenu>> GetByName(string SystemMenuName);

        Task<DataResult<SystemMenu>> Create(AddSystemMenu entity);

        Task<DataResult<SystemMenu>> Update(SystemMenu entity);

        Task<DataResult<SystemMenu>> Delete(string id);
    }
}