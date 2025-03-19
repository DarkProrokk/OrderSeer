using Entities;
using MediatR;
using Results;

namespace Application.Command;

public record OrderStatusChangeCommand(Guid OrderGuid, OrderStatus NewStatus): IRequest<OperationResult>;