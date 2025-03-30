using System.ComponentModel.DataAnnotations;
namespace SalesDatePrediction.Domain.Models;

public class Employee
{
    [Key]
    public int Empid { get; set; }
    public required string Fullname { get; set; }
}
