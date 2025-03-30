using System.ComponentModel.DataAnnotations;
namespace SalesDatePrediction.Domain.Models;

public class Product
{
    [Key]
    public int Productid { get; set; }
    public required string Productname { get; set; }
}
