using System.ComponentModel.DataAnnotations;
namespace SalesDatePrediction.Domain.Models;

public class Shipper
{
    [Key]
    public int Shipperid { get; set; }
    public required string CompanyName { get; set; }
}
