using Application.Models;

namespace Application.Interfaces;

public interface IKafkaProducerService
{
    public Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default);
    public Task ProduceWithSchemeAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default);
    
    public Task ProduceInDlqAsync<T>(string key, T message, CancellationToken cancellationToken = default);
    public Task PlaceOrderAsync(string topic, string key, KafkaOrderStatusChangedModel message, CancellationToken cancellationToken = default);
}