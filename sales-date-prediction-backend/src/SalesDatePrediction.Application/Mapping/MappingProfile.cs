using AutoMapper;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderSummary, ClientOrderDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Shipper, ShipperDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CustomerOrderPrediction, CustomerOrderPredictionDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Orderid, opt => opt.Ignore());
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();
        }
    }
}