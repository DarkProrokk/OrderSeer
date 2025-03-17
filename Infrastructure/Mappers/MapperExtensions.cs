using Infrastructure.Interfaces;
using KafkaMessages;
using Scheme.OrderStatusChanged;

namespace Infrastructure.Mappers;

public static class MapperExtensions
{
    public static OrderStatusChange ToAvroModel(this IMapper mapper, OrderStatusChangedEvent model)
    {
        return mapper.ToAvroModel(model);
    }
}