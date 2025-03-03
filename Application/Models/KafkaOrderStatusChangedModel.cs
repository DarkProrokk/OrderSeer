namespace Application.Models;

public class KafkaOrderStatusChangedModel
{
    public Guid OrderId { get; set; }
    
    public Status Status { get; set; }
}

public class Status
{
    public int code { get; set; } 
    public string name { get; set; }
}