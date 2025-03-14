using Application.Models;
using KafkaMessages;

namespace Infrastructure.Interfaces;

public interface IKafkaDeserializer
{
    OrderStatusChangedEvent Deserialize(string json);
}