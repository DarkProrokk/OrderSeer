using Application.Interfaces;

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
}