using System.Text;
using Application.Interfaces;
using Application.Models;
using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka;


public class KafkaProducer(ProducerConfig config, ILogger<KafkaProducer> logger): IKafkaProducer
{
    public async Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken)
    {
        IProducer<string, T> producer = new ProducerBuilder<string, T>(config).Build();
        await producer.ProduceAsync(topic, new Message<string, T> { Key = key, Value = message }, cancellationToken);
    }

    /// <summary>
    /// Method for produce message in Kafka, with using scheme from Scheme Registry
    /// </summary>
    /// <param name="topic">kafka topic name</param>
    /// <param name="key">kafka message key</param>
    /// <param name="message">kafka message data</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">A class that implement  <see cref="ISpecificRecord"/> </typeparam>
    public async Task ProduceWithSchemeAsync<T>(string topic, string key, T message, CancellationToken cancellationToken)
    {
        //Получения клиента Schema Registry
        var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = "http://localhost:8081" });
        var producer = new ProducerBuilder<string, T>(config)
            .SetValueSerializer(new AvroSerializer<T>(schemaRegistry))
            .Build();
        try
        {
            await producer.ProduceAsync(topic, new Message<string, T> { Key = key, Value = message },
                cancellationToken);
        }
        catch (SchemaRegistryException e)
        {
            logger.LogError(e, "Error while processing message in ");
        }
    }
    
}