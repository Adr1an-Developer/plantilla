using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Config;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Repository.Config
{
    public class MenuPermissionRepository : GenericRepository<MenuPermission>, IMenuPermissionRepository
    {
        public MenuPermissionRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MenuPermission>> GetAllMenuPermissions()
        {
            var QueryRows = await (from mp in Context.GetDbSet<MenuPermission>()
                                   join p in Context.GetDbSet<Profile>() on mp.ProfileId equals p.Id
                                   join m in Context.GetDbSet<SystemMenu>() on mp.MenuId equals m.Id
                                   where mp.IsDeleted == false
                                   select new MenuPermission
                                   {
                                       Id = mp.Id,
                                       MenuId = mp.MenuId,
                                       MenuName = m.Name,
                                       ProfileId = mp.ProfileId,
                                       ProfileName = p.Name,
                                       CanAdd = mp.CanAdd,
                                       CanEdit = mp.CanEdit,
                                       CanDelete = mp.CanDelete,
                                       CanAuthorize = mp.CanAuthorize,
                                       CanExport = mp.CanExport,
                                       CanView = mp.CanView,
                                       IsActive = mp.IsActive,
                                       IsDeleted = mp.IsDeleted,
                                       CreateByUser = mp.CreateByUser,
                                       CreationDate = mp.CreationDate,
                                       ModificationDate = mp.ModificationDate,
                                       UpdateByUser = mp.UpdateByUser
                                   }).ToListAsync();
            return QueryRows;
        }

        public async Task<MenuPermission> GetMenuPermissionByID(string id)
        {
            var QueryRow = await (from mp in Context.GetDbSet<MenuPermission>()
                                  join p in Context.GetDbSet<Profile>() on mp.ProfileId equals p.Id
                                  join m in Context.GetDbSet<SystemMenu>() on mp.MenuId equals m.Id
                                  where mp.IsDeleted == false && mp.Id == id
                                  select new MenuPermission
                                  {
                                      Id = mp.Id,
                                      MenuId = mp.MenuId,
                                      MenuName = m.Name,
                                      ProfileId = mp.ProfileId,
                                      ProfileName = p.Name,
                                      CanAdd = mp.CanAdd,
                                      CanEdit = mp.CanEdit,
                                      CanDelete = mp.CanDelete,
                                      CanAuthorize = mp.CanAuthorize,
                                      CanExport = mp.CanExport,
                                      CanView = mp.CanView,
                                      IsActive = mp.IsActive,
                                      IsDeleted = mp.IsDeleted,
                                      CreateByUser = mp.CreateByUser,
                                      CreationDate = mp.CreationDate,
                                      ModificationDate = mp.ModificationDate,
                                      UpdateByUser = mp.UpdateByUser
                                  }).FirstOrDefaultAsync();
            return QueryRow;
        }

        public async Task<IEnumerable<MenuPermission>> GetMenuPermissionByProfileId(string profileId)
        {
            var QueryRows = await (from mp in Context.GetDbSet<MenuPermission>()
                                   join p in Context.GetDbSet<Profile>() on mp.ProfileId equals p.Id
                                   join m in Context.GetDbSet<SystemMenu>() on mp.MenuId equals m.Id
                                   join u in Context.GetDbSet<User>() on p.Id equals u.ProfileId
                                   where mp.IsDeleted == false &&
                                   mp.ProfileId == profileId &&
                                   u.IsDeleted == false &&
                                   p.IsDeleted == false &&
                                   m.IsDeleted == false
                                   select new MenuPermission
                                   {
                                       Id = mp.Id,
                                       MenuId = mp.MenuId,
                                       MenuName = m.Name,
                                       ProfileId = mp.ProfileId,
                                       ProfileName = p.Name,
                                       ParentMenuId = m.ParentMenuId,
                                       MenuOrder = m.Order,
                                       CanAdd = mp.CanAdd,
                                       CanEdit = mp.CanEdit,
                                       CanDelete = mp.CanDelete,
                                       CanAuthorize = mp.CanAuthorize,
                                       CanExport = mp.CanExport,
                                       CanView = mp.CanView,
                                       IsActive = mp.IsActive,
                                       IsDeleted = mp.IsDeleted,
                                       CreateByUser = mp.CreateByUser,
                                       CreationDate = mp.CreationDate,
                                       ModificationDate = mp.ModificationDate,
                                       UpdateByUser = mp.UpdateByUser
                                   }).ToListAsync();
            return QueryRows;
        }
    }
}