using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.SystemClient;
using ProjectLottery.V1.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Services.Abstractions.System
{
    public interface IClientServices : IServiceBase<SystemClient>
    {
        Task<DataResults<SystemClient>> GetAll();

        Task<DataResult<SystemClient>> GetbyId(string id);

        Task<DataResult<SystemClient>> GetByName(string clientName);

        Task<DataResult<SystemClient>> Create(AddSystemClient entity);

        Task<DataResult<SystemClient>> Update(SystemClient entity);

        Task<DataResult<SystemClient>> Delete(string id);
    }
}