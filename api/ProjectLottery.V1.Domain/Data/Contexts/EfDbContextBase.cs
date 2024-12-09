using ProjectLottery.V1.Domain.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ProjectLottery.V1.Domain.Data.Contexts
{
    public class EfDbContextBase : DbContext, IEfDbContext
    {
        private DbContextOptions<EfDbContextBase> options;
        public EfDbContextBase(DbContextOptions options) : base(options) { }


        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public Task<int> SaveAsync()
        {
            
                return base.SaveChangesAsync();
          
        }

     
    }
  
}
