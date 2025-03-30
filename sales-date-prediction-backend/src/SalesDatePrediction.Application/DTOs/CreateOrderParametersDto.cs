using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.DTOs;
public class CreateOrderParametersDto
{
    [Required]
    public required OrderDto Order { get; set; }

    [Required]
    public required List<OrderDetailDto> OrderDetailDtos { get; set; }
}
