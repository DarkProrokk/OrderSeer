using Application.Interfaces;
using Confluent.Kafka;

namespace Infrastructure.Kafka;

public class KafkaProducer(ProducerConfig config): IKafkaProducer
{
    public async Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken)
    {
        IProducer<string, T> producer = new ProducerBuilder<string, T>(config).Build();
        await producer.ProduceAsync(topic, new Message<string, T> { Key = key, Value = message }, cancellationToken);
    }
}