using Application.Models;

namespace Infrastructure.Interfaces;

public interface IKafkaDeserializer
{
    KafkaOrderStatusChangedModel Deserialize(string json);
}