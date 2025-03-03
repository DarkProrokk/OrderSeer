using Application.Models;
using Domain.Enum;
using Infrastructure.Interfaces;
using Scheme.OrderStatusChanged;

namespace Infrastructure.Mappers;

public static class MapperExtensions
{
    public static OrderStatusChange ToAvroModel(this IMapper mapper, KafkaOrderStatusChangedModel model)
    {
        return mapper.ToAvroModel(model);
    }
}