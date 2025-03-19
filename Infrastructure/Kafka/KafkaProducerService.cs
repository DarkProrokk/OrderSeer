using System.Text.Json;
using Application.Command;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;
using KafkaMessages;

namespace Infrastructure.Kafka;

public class KafkaProducerService(IKafkaProducer kafkaProducer) : IKafkaProducerService
{
    private readonly string _dqlTopic = "dead_letter_queue";

    public async Task ProduceAsync<T>(string topic, string key, T message,
        CancellationToken cancellationToken = default)
    {
        await kafkaProducer.ProduceAsync(topic, key, message, cancellationToken);
    }

    public async Task ProduceInDlqAsync<T>(string key, T message, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(_dqlTopic);
        await kafkaProducer.ProduceAsync(_dqlTopic, key, message, cancellationToken);
    }

    public async Task ProduceInStatusChangedAsync(string key, OrderStatusChangeCommand message,
        CancellationToken cancellationToken = default)
    {
        var @event = Mappers.Mapper.Map(message);
        var msg = JsonSerializer.Serialize(@event);
        await kafkaProducer.ProduceAsync("order_status_changed", key, msg, cancellationToken);
    }

    public async Task ProduceWithSchemeAsync<T>(string topic, string key, T message,
        CancellationToken cancellationToken = default)
    {
        await kafkaProducer.ProduceWithSchemeAsync(topic, key, message, cancellationToken);
    }

    public async Task PlaceOrderAsync(string topic, string key, OrderStatusChangedEvent message,
        CancellationToken cancellationToken = default)
    {
        IMapper mapper = new OrderMapper();
        var mappedModel = mapper.ToAvroModel(message);
        await kafkaProducer.ProduceWithSchemeAsync(topic, key, mappedModel, cancellationToken);
    }
}