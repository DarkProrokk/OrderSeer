namespace Application.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken);
    
    /// <summary>
    /// Method for produce message in Kafka, with using scheme from Scheme Registry
    /// </summary>
    /// <param name="topic">kafka topic name</param>
    /// <param name="key">kafka message key</param>
    /// <param name="message">kafka message data</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">A class that implement  <see cref="ISpecificRecord"/> </typeparam>
    Task ProduceWithSchemeAsync<T>(string topic, string key, T message, CancellationToken cancellationToken); 
}