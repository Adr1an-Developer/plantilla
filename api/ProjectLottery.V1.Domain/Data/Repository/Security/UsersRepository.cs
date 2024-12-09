using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;
using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;

namespace ProjectLottery.V1.Domain.Data.Repository.Security
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IEfDbContext Context) : base(Context)
        {
        }

        public async Task<IEnumerable<User>> Login(Login login)
        {
            string user = login.UserName;
            string password = login.Password;

            var usuario = await (from u in Context.GetDbSet<User>()
                                 join tu in Context.GetDbSet<Profile>() on u.ProfileId equals tu.Id
                                 where u.IsDeleted == false
                                 && u.IsActive == true
                                 && (u.UserName == user || u.Email == user)
                                 && u.Password == password
                                 select new User()
                                 {
                                     Id = u.Id,
                                     ProfileId = u.ProfileId,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     Email = u.Email,
                                     ProfileName = tu.Name,
                                     Password = u.Password,
                                     UserName = u.UserName,
                                     IsActive = u.IsActive,
                                     IsDeleted = u.IsDeleted,
                                     CreateByUser = u.CreateByUser,
                                     CreationDate = u.CreationDate,
                                     ModificationDate = u.ModificationDate,
                                     UpdateByUser = u.UpdateByUser,
                                     isFirstLogin = u.isFirstLogin,
                                     ExternalCode = u.ExternalCode
                                 }
                                 ).ToListAsync();
            return usuario;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var usuarios = await (from u in Context.GetDbSet<User>()
                                  join tu in Context.GetDbSet<Profile>() on u.ProfileId equals tu.Id
                                  where u.IsDeleted == false
                                  select new User()
                                  {
                                      Id = u.Id,
                                      ProfileId = u.ProfileId,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Email = u.Email,
                                      ProfileName = tu.Name,
                                      Password = u.Password,
                                      UserName = u.UserName,
                                      IsActive = u.IsActive,
                                      IsDeleted = u.IsDeleted,
                                      CreateByUser = u.CreateByUser,
                                      CreationDate = u.CreationDate,
                                      ModificationDate = u.ModificationDate,
                                      UpdateByUser = u.UpdateByUser,
                                      isFirstLogin = u.isFirstLogin,
                                      ExternalCode = u.ExternalCode
                                  }
                                  ).ToListAsync();

            if (usuarios == null) return new List<User>();

            return usuarios;
        }

        public async Task<User> GetUserByID(string id)
        {
            var usuarios = await (from u in Context.GetDbSet<User>()
                                  join tu in Context.GetDbSet<Profile>() on u.ProfileId equals tu.Id
                                  where u.IsDeleted == false && u.Id == id
                                  select new User()
                                  {
                                      Id = u.Id,
                                      ProfileId = u.ProfileId,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Email = u.Email,
                                      ProfileName = tu.Name,
                                      Password = u.Password,
                                      UserName = u.UserName,
                                      IsActive = u.IsActive,
                                      IsDeleted = u.IsDeleted,
                                      CreateByUser = u.CreateByUser,
                                      CreationDate = u.CreationDate,
                                      ModificationDate = u.ModificationDate,
                                      UpdateByUser = u.UpdateByUser,
                                      isFirstLogin = u.isFirstLogin,
                                      ExternalCode = u.ExternalCode
                                  }
                                 ).FirstOrDefaultAsync();

            if (usuarios == null) return new User();

            return usuarios;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var usuarios = await (from u in Context.GetDbSet<User>()
                                  join tu in Context.GetDbSet<Profile>() on u.ProfileId equals tu.Id
                                  where u.IsActive == true && u.IsDeleted == false & u.UserName == userName
                                  select new User()
                                  {
                                      Id = u.Id,
                                      ProfileId = u.ProfileId,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Email = u.Email,
                                      ProfileName = tu.Name,
                                      Password = u.Password,
                                      UserName = u.UserName,
                                      IsActive = u.IsActive,
                                      IsDeleted = u.IsDeleted,
                                      CreateByUser = u.CreateByUser,
                                      CreationDate = u.CreationDate,
                                      ModificationDate = u.ModificationDate,
                                      UpdateByUser = u.UpdateByUser,
                                      isFirstLogin = u.isFirstLogin,
                                      ExternalCode = u.ExternalCode
                                  }
                                 ).FirstOrDefaultAsync();
            if (usuarios == null) return new User();
            return usuarios;
        }
    }
}