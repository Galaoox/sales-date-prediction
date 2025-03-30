using SalesDatePrediction.Application.Mapping;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Infrastructure.Data;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Application.Services;

namespace SalesDatePrediction.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile));

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();

            // Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}