using Application.Models;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Infrastructure.Interfaces;
using Scheme.OrderStatusChanged;

namespace Infrastructure.Deserializers;

// public class KafkaDeserializer: IKafkaDeserializer
// {
//     public KafkaOrderStatusChangedModel Deserialize(string json, string topic)
//     {
//         var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = "http://localhost:8081" });
//         var avroDeserializer = new AvroDeserializer<OrderStatusChange>(schemaRegistry);
//         var serializationContext = new SerializationContext(MessageComponentType.Value, topic);
//         var deserializedMessage = await avroDeserializer.DeserializeAsync(json, false, serializationContext);
//     }
// }