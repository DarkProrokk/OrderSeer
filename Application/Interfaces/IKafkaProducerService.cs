namespace Application.Interfaces;

public interface IKafkaProducerService
{
    public Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default);
    
    public Task ProduceInDlqAsync<T>(string key, T message, CancellationToken cancellationToken = default);
}