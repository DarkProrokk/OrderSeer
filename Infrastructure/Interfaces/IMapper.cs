using Application.Models;
using Scheme.OrderStatusChanged;

namespace Infrastructure.Interfaces;

public interface IMapper
{
    OrderStatusChange ToAvroModel(KafkaOrderStatusChangedModel source);
}