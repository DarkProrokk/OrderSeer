using Application.Command;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Results;

namespace Application.Handlers;

public class OrderStatusChangeHandler(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducer): IRequestHandler<OrderStatusChangeCommand, OperationResult>
{
    public async Task<OperationResult> Handle(OrderStatusChangeCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetAsync(request.OrderGuid);
        if (order is null) return OperationResult.Failure($"Order with guid {request.OrderGuid} not found");
        try
        {
            order.ChangeStatus(request.NewStatus);
        }
        catch (WrongStatusException e)
        {
            return OperationResult.Failure(e.Message);
        }
        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.SaveChangesAsync();
        await kafkaProducer.ProduceInStatusChangedAsync(order.Guid.ToString(), request);
        return OperationResult.Success();
    }
}