using System.ComponentModel;
using Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Kafka;

public class KafkaConsumerService(IKafkaConsumer kafkaConsumer): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await kafkaConsumer.StartConsuming(stoppingToken);
    }
}