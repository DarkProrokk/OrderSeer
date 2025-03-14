using Domain.Enum;

namespace Application.Models;

public class KafkaOrderCreatedModel
{
    public Guid UserReference { get; set; }
    public Guid OrderReference { get; set; }
    public bool Validate()
    {
        return UserReference != default && OrderReference != default;
    }
}