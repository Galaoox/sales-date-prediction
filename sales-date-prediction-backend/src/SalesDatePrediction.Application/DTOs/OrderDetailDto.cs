using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.Application.DTOs
{
    public class OrderDetailDto
    {
        [Required]
        [JsonPropertyName("product_id")] // esto deberia aplicarlo para los demas dto
        public int Productid { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        [Column(TypeName = "money")]
        [JsonPropertyName("unit_price")]
        public decimal Unitprice { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1.")]
        [Column(TypeName = "numeric(3,4)")]
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
    }
}
