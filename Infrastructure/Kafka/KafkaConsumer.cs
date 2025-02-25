using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka;

public class KafkaConsumer(ConsumerConfig consumerConfig, IEnumerable<IMessageHandler> messageHandlers, ILogger<KafkaConsumer> logger): IKafkaConsumer
{
    //Dictionary containing pairs Topic:Handler for compare topic name - handler
    private readonly Dictionary<string, IMessageHandler> _messageHandlers = messageHandlers
        .ToDictionary(handler => 
            handler.Topic, handler => handler);
    
    private readonly IConsumer<string, string> _consumer = new ConsumerBuilder<String,String>(consumerConfig).Build();
    
    /// <summary>
    /// Method which launch consuming from kafka. Subscribe on topic which contain in `_messageHandlers`
    /// After consume message send message in his handler
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StartConsuming(CancellationToken cancellationToken = default)
    {   
        logger.LogInformation("Starting consumer");
        _consumer.Subscribe(_messageHandlers.Keys.ToList());
        logger.LogInformation($"Topic for subscription: {_messageHandlers.Keys.ToList()[0]}");
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await Task.Run(() => _consumer.Consume(cancellationToken), cancellationToken);
                    if (_messageHandlers.TryGetValue(result.Topic, out var handler))
                    {
                        await handler.HandleMessage(result.Message.Value, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        finally
        {
            _consumer.Close();
        }
    }
}