using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Application.DTOs;
using System.Threading.Tasks;

namespace SalesDatePrediction.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

}
