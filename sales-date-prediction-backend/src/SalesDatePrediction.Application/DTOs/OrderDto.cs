using System;
using System.ComponentModel.DataAnnotations;

namespace SalesDatePrediction.Application.DTOs
{
    public class OrderDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Order ID must be a positive number.")]
        public int? Orderid { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Customer ID must be a positive number.")]
        public int Custid { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number.")]
        public int Empid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Requireddate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Shippeddate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Shipper ID must be a positive number.")]
        public int Shipperid { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Freight { get; set; } = 0;

        [Required]
        [StringLength(40, ErrorMessage = "Ship name cannot exceed 40 characters.")]
        public required string Shipname { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Ship address cannot exceed 60 characters.")]
        public required string Shipaddress { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Ship city cannot exceed 15 characters.")]
        public required string Shipcity { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Ship country cannot exceed 15 characters.")]
        public required string Shipcountry { get; set; }
    }
}
