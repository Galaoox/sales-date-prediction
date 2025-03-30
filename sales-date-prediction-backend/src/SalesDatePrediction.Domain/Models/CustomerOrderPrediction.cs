using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Domain.Models;
public class CustomerOrderPrediction
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public DateTime LastOrderDate { get; set; }
    public DateTime? NextPredictedOrder { get; set; }
}