namespace Demo.WebAPI.Models.Dto;

public class OrderDto
{
    public string OrderId { get; set; }

    public string CustomerId { get; set; }

    public string ProductId { get; set; }

    public DateTime DeliveryDate { get; set; }

    public bool IsDelivered { get; set; }

    public bool IsCancelled { get; set; }
}