using Application.Models;
using KafkaMessages;
using Scheme.OrderStatusChanged;

namespace Infrastructure.Interfaces;

public interface IMapper
{
    OrderStatusChange ToAvroModel(OrderStatusChangedEvent source);
}