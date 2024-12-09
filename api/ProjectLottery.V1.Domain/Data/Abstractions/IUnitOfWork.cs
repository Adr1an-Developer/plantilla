namespace ProjectLottery.V1.Domain.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
    }
}
