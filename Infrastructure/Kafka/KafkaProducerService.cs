using Application.Interfaces;
using Application.Models;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;

namespace Infrastructure.Kafka;

public class KafkaProducerService(IKafkaProducer kafkaProducer): IKafkaProducerService
{
    private readonly string _dqlTopic = "dead_letter_queue";
    public async Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default)
    {
        await kafkaProducer.ProduceAsync(topic, key, message, cancellationToken);
    }

    public async Task ProduceInDlqAsync<T>(string key, T message, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(_dqlTopic);
        await kafkaProducer.ProduceAsync(_dqlTopic, key, message, cancellationToken);
    }
    
    public async Task ProduceWithSchemeAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default)
    {
        await kafkaProducer.ProduceWithSchemeAsync(topic, key, message, cancellationToken);
    }

    public async Task PlaceOrderAsync(string topic, string key, KafkaOrderStatusChangedModel message,
        CancellationToken cancellationToken = default)
    {
        IMapper mapper = new OrderMapper();
        var mappedModel = mapper.ToAvroModel(message);
        await kafkaProducer.ProduceWithSchemeAsync(topic, key, mappedModel, cancellationToken);
    }
}