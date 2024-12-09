using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Entities.Security;

namespace ProjectLottery.V1.Domain.Data.Repository.Security
{
    public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(IEfDbContext Context) : base(Context)
        {
        }

        public async Task<IEnumerable<Profile>> GetAllProfiles()
        {
            var rows = await (from p in Context.GetDbSet<Profile>()
                              where p.IsDeleted == false
                              select new Profile()
                              {
                                  Id = p.Id,
                                  Abbreviation = p.Abbreviation,
                                  Name = p.Name,
                                  IsActive = p.IsActive,
                                  IsDeleted = p.IsDeleted,
                                  CreationDate = p.CreationDate,
                                  UpdateByUser = p.UpdateByUser,
                                  ModificationDate = p.ModificationDate,
                                  CreateByUser = p.CreateByUser,
                              }
                                  ).ToListAsync();

            if (rows == null) return new List<Profile>();

            return rows;
        }

        public async Task<Profile> GetProfileByID(string id)
        {
            var row = await (from p in Context.GetDbSet<Profile>()
                             where p.IsDeleted == false && p.Id.Equals(id)
                             select new Profile()
                             {
                                 Id = p.Id,
                                 Abbreviation = p.Abbreviation,
                                 Name = p.Name,
                                 IsActive = p.IsActive,
                                 IsDeleted = p.IsDeleted,
                                 CreationDate = p.CreationDate,
                                 CreateByUser = p.CreateByUser,
                                 ModificationDate = p.ModificationDate,
                                 UpdateByUser = p.UpdateByUser,
                             }).FirstOrDefaultAsync();

            if (row == null) return new Profile();

            return row;
        }

        public async Task<Profile> GetProfileByName(string ProfileName)
        {
            var row = await (from p in Context.GetDbSet<Profile>()
                             where p.IsActive == true && p.IsDeleted == false && p.Name == ProfileName
                             select new Profile()
                             {
                                 Id = p.Id,
                                 Abbreviation = p.Abbreviation,
                                 Name = p.Name,
                                 IsActive = p.IsActive,
                                 IsDeleted = p.IsDeleted,
                                 CreationDate = p.CreationDate,
                             }).FirstOrDefaultAsync();
            if (row == null) return new Profile();
            return row;
        }
    }
}