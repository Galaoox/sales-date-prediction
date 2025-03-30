namespace SalesDatePrediction.Domain.Models;

public class OrderDetail
{
    public int Orderid { get; set; }
    public int Productid { get; set; }
    public decimal Unitprice { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}
