using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShippersController : ControllerBase
{
    private readonly IShipperService _service;

    public ShippersController(IShipperService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetShippers()
    {
        var results = await _service.GetAllAsync();
        return Ok(results);
    }

}