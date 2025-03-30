using System.ComponentModel.DataAnnotations;
namespace SalesDatePrediction.Domain.Models;

public class Customer
{
    [Key]
    public int Customerid { get; set; }
    public string Name { get; set; }
    public DateTime? LastOrderDate { get; set; }
    public DateTime? NextPredictedOrder { get; set; }
}
