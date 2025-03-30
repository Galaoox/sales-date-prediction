using AutoMapper;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Interfaces;

namespace SalesDatePrediction.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
