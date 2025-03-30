namespace SalesDatePrediction.Application.DTOs;

public class ClientOrderDto
{
    public int Orderid { get; set; }
    public DateTime Requireddate { get; set; }
    public DateTime? Shippeddate { get; set; }
    public required string Shipname { get; set; }
    public required string Shipaddress { get; set; }
    public required string Shipcity { get; set; }
}
