using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.Application.DTOs
{
    public class OrderPredictionParametersDto
    {
        public enum OrderPredictionSortColumnOptions
        {
            CompanyName,
            LastOrderDate,
            NextPredictedOrder
        }

        public enum OrderPredictionSortOrderOptions
        {
            ASC,
            DESC
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderPredictionSortColumnOptions))]
        [Display(Name = "Sort Column", Description = "Column to sort by. Acceptable values: CompanyName, LastOrderDate, NextPredictedOrder")]
        public OrderPredictionSortColumnOptions SortColumn { get; set; } = OrderPredictionSortColumnOptions.LastOrderDate;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderPredictionSortOrderOptions))]
        [Display(Name = "Sort Order", Description = "Order to sort by. Acceptable values: ASC, DESC")]
        public OrderPredictionSortOrderOptions SortOrder { get; set; } = OrderPredictionSortOrderOptions.ASC;

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0")]
        public int PageSize { get; set; } = 10;

        [Display(Name = "Search", Description = "Search term to filter results")]
        public string Search { get; set; } = string.Empty;

        public string GetSortColumnAsString() => SortColumn.ToString();
        public string GetSortOrderAsString() => SortOrder.ToString();
    }
}