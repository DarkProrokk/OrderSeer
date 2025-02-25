namespace Application.Interfaces;

public interface IKafkaConsumer
{
    Task StartConsuming(CancellationToken cancellationToken);
}