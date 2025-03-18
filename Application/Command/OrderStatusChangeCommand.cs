using Domain.Results;
using Entities;
using MediatR;
namespace Application.Command;

public record OrderStatusChangeCommand(Guid OrderGuid, OrderStatus NewStatus): IRequest<OperationResult>;