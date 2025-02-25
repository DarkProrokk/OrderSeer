using Domain.Entities;

namespace Application.Models;

public class OrderViewModel
{
    public Guid OrderId { get; set; }
    public string Status { get; set; }
}