using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.Application.DTOs
{
    public class OrderParametersDto
    {
        public enum SortColumnOptions
        {
            Orderid,
            Requireddate,
            Shippeddate,
            Shipname,
            Shipaddress,
            Shipcity
        }

        public enum SortOrderOptions
        {
            ASC,
            DESC
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(SortColumnOptions))]
        [Display(Name = "Sort Column", Description = "Column to sort by. Acceptable values: Orderid, Requireddate, Shippeddate, Shipname, Shipaddress, Shipcity")]
        public SortColumnOptions SortColumn { get; set; } = SortColumnOptions.Orderid;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(SortOrderOptions))]
        [Display(Name = "Sort Order", Description = "Order to sort by. Acceptable values: ASC, DESC")]
        public SortOrderOptions SortOrder { get; set; } = SortOrderOptions.ASC;

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0")]
        public int PageSize { get; set; } = 10;

        public string GetSortColumnAsString() => SortColumn.ToString();
        public string GetSortOrderAsString() => SortOrder.ToString();
    }
}