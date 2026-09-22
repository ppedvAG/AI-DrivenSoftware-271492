namespace Demo.WebAPI.Models.Domain;

public class Order
{
    public string OrderId { get; set; }

    public string CustomerId { get; set; }

    public string ProductId { get; set; }

    public DateTime DeliveryDate { get; set; }

    public bool IsCancelled { get; set; }
}