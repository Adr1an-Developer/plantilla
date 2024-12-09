using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Config;
using ProjectLottery.V1.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Repository.Config
{
    public class SystemMenuRepository : GenericRepository<SystemMenu>, ISystemMenuRepository
    {
        public SystemMenuRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SystemMenu>> GetAllSystemMenus()
        {
            var QueryRows = await (from sm in Context.GetDbSet<SystemMenu>()
                                   join leftJoinPM in Context.GetDbSet<SystemMenu>() on sm.ParentMenuId equals leftJoinPM.Id into ParentMenu
                                   from pm in ParentMenu.DefaultIfEmpty()
                                   where sm.IsDeleted == false
                                   select new SystemMenu
                                   {
                                       Id = sm.Id,
                                       Name = sm.Name,
                                       Title = sm.Title,
                                       Url = sm.Url,
                                       Icon = sm.Icon,
                                       Group = sm.Group,
                                       ParentMenuId = sm.ParentMenuId,
                                       ParentMenu = pm.Name ?? "No asignado",
                                       Order = sm.Order,
                                       IsActive = sm.IsActive,
                                       IsDeleted = sm.IsDeleted,
                                       CreateByUser = sm.CreateByUser,
                                       CreationDate = sm.CreationDate,
                                       ModificationDate = sm.ModificationDate,
                                       UpdateByUser = sm.UpdateByUser
                                   }).OrderBy(x => x.Group)
                                   .ThenBy(o => o.ParentMenuId)
                                   .ThenBy(o => o.Order)
                                   .ToListAsync();
            return QueryRows;
        }

        public async Task<SystemMenu> GetSystemMenuByID(string id)
        {
            var QueryRow = await (from sm in Context.GetDbSet<SystemMenu>()
                                  join leftJoinPM in Context.GetDbSet<SystemMenu>() on sm.ParentMenuId equals leftJoinPM.Id into ParentMenu
                                  from pm in ParentMenu.DefaultIfEmpty()
                                  where sm.IsDeleted == false && sm.Id == id
                                  select new SystemMenu
                                  {
                                      Id = sm.Id,
                                      Name = sm.Name,
                                      Title = sm.Title,
                                      Url = sm.Url,
                                      Icon = sm.Icon,
                                      Group = sm.Group,
                                      ParentMenuId = sm.ParentMenuId,
                                      ParentMenu = pm.Name ?? "No asignado",
                                      Order = sm.Order,
                                      IsActive = sm.IsActive,
                                      IsDeleted = sm.IsDeleted,
                                      CreateByUser = sm.CreateByUser,
                                      CreationDate = sm.CreationDate,
                                      ModificationDate = sm.ModificationDate,
                                      UpdateByUser = sm.UpdateByUser
                                  }).FirstOrDefaultAsync();
            return QueryRow;
        }

        public async Task<SystemMenu> GetSystemMenuByName(string SystemMenuName)
        {
            var QueryRow = await (from sm in Context.GetDbSet<SystemMenu>()
                                  join leftJoinPM in Context.GetDbSet<SystemMenu>() on sm.ParentMenuId equals leftJoinPM.Id into ParentMenu
                                  from pm in ParentMenu.DefaultIfEmpty()
                                  where sm.IsDeleted == false && sm.Name.ToLower() == SystemMenuName.ToLower().Trim()
                                  select new SystemMenu
                                  {
                                      Id = sm.Id,
                                      Name = sm.Name,
                                      Title = sm.Title,
                                      Url = sm.Url,
                                      Icon = sm.Icon,
                                      Group = sm.Group,
                                      ParentMenuId = sm.ParentMenuId,
                                      ParentMenu = pm.Name ?? "No asignado",
                                      Order = sm.Order,
                                      IsActive = sm.IsActive,
                                      IsDeleted = sm.IsDeleted,
                                      CreateByUser = sm.CreateByUser,
                                      CreationDate = sm.CreationDate,
                                      ModificationDate = sm.ModificationDate,
                                      UpdateByUser = sm.UpdateByUser
                                  }).FirstOrDefaultAsync();
            return QueryRow;
        }

        public async Task<IEnumerable<SystemMenu>> GetAllSystemMenuGroup()
        {
            var QueryRows = await (from sm in Context.GetDbSet<SystemMenu>()
                                   join leftJoinPM in Context.GetDbSet<SystemMenu>() on sm.ParentMenuId equals leftJoinPM.Id into ParentMenu
                                   from pm in ParentMenu.DefaultIfEmpty()
                                   where sm.IsDeleted == false && sm.Group == true
                                   select new SystemMenu
                                   {
                                       Id = sm.Id,
                                       Name = sm.Name,
                                       Title = sm.Title,
                                       Url = sm.Url,
                                       Icon = sm.Icon,
                                       Group = sm.Group,
                                       ParentMenuId = sm.ParentMenuId,
                                       ParentMenu = pm.Name ?? "No asignado",
                                       Order = sm.Order,
                                       IsActive = sm.IsActive,
                                       IsDeleted = sm.IsDeleted,
                                       CreateByUser = sm.CreateByUser,
                                       CreationDate = sm.CreationDate,
                                       ModificationDate = sm.ModificationDate,
                                       UpdateByUser = sm.UpdateByUser
                                   }).OrderBy(x => x.Group)
                                   .ThenBy(o => o.ParentMenuId)
                                   .ThenBy(o => o.Order)
                                   .ToListAsync();
            return QueryRows;
        }
    }
}