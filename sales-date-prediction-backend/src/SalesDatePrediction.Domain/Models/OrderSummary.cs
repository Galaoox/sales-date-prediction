
namespace SalesDatePrediction.Domain.Models;

public class OrderSummary
{
    public int OrderId { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public required string ShipName { get; set; }
    public required string ShipAddress { get; set; }
    public required string ShipCity { get; set; }
}