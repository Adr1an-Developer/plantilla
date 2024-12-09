using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.MenuPermission;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Services.Abstractions.Config
{
    public interface IMenuPermissionServices : IServiceBase<MenuPermission>
    {
        Task<DataResults<MenuPermission>> GetAll();

        Task<DataResult<MenuPermission>> GetbyId(string id);

        Task<DataResult<MenuPermission>> Create(AddMenuPermission entity);

        Task<DataResult<MenuPermission>> Update(MenuPermission entity);

        Task<DataResult<MenuPermission>> Delete(string id);

        Task<DataResults<MenuPermission>> GetMenuPermissionByProfileId(string profileId);
    }
}