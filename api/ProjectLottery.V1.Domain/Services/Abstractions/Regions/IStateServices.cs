using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Entities.Global;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Regions
{
    public interface IStateServices : IServiceBase<State>
    {
        Task<DataResults<State>> GetAll();

        Task<DataResult<State>> GetbyId(string id);

        Task<DataResult<State>> GetByName(string StateName);
    }
}