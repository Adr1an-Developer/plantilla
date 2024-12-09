using System.Linq.Expressions;

namespace ProjectLottery.V1.Domain.Data.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork
        {
            get;
        }

        string LoggedUserId
        {
            get;
        }

        bool IsAdmin
        {
            get;
        }

        void SetLoggedUserInfo(string loggedUserId, bool isAdmin = false);

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> CreateAsync(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}