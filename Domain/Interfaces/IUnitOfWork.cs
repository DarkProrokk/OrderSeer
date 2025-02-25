namespace Domain.Interfaces;

public interface IUnitOfWork: IDisposable
{
    public IOrderRepository OrderRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}