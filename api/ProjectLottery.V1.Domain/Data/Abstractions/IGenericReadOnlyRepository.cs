using System.Linq.Expressions;

namespace ProjectLottery.V1.Domain.Data.Abstractions
{
    public interface IGenericReadOnlyRepository<TEntity> where TEntity : class
    {
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

        Task<TEntity> FindbyIdAsync(Guid id);

        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}