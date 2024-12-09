using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Abstractions.Config
{
    public interface IMenuPermissionRepository : IGenericRepository<MenuPermission>
    {
        Task<IEnumerable<MenuPermission>> GetAllMenuPermissions();

        Task<MenuPermission> GetMenuPermissionByID(string id);

        Task<IEnumerable<MenuPermission>> GetMenuPermissionByProfileId(string profileId);
    }
}