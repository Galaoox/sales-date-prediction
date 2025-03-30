using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.DTOs;
public class CustomerOrderPredictionDto
{
    public int CustomerId { get; set; }
    public required string CompanyName { get; set; }
    public DateTime LastOrderDate { get; set; }
    public DateTime? NextPredictedOrder { get; set; }
}