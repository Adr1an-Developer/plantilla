using ProjectLottery.V1.Domain.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProjectLottery.V1.Domain.Data.Repository
{
    public class GenericReadOnlyRepository<TEntity> : IGenericReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly IEfDbContext Context;

        public string LoggedUserId
        {
            get; private set;
        }

        public bool IsAdmin
        {
            get; private set;
        }

        public GenericReadOnlyRepository(IEfDbContext context)
        {
            Context = context;
        }

        public IUnitOfWork UnitOfWork => Context;

        public void SetLoggedUserInfo(string loggedUserId, bool isAdmin)
        {
            LoggedUserId = loggedUserId;
            IsAdmin = isAdmin;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            var result = Context.GetDbSet<TEntity>();
            return await result
                  .AsNoTracking()
                  .ToListAsync();
        }

        public async Task<TEntity> FindbyIdAsync(Guid id)
        {
            var result = await Context.GetDbSet<TEntity>().FirstOrDefaultAsync(t => t.Equals(id));
            return result;
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await Context.GetDbSet<TEntity>().Where(expression).ToListAsync();
            return result;
        }
    }
}