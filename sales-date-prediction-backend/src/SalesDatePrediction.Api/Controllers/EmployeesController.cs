using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Models;
using System.Threading.Tasks;

namespace SalesDatePrediction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var results = await _service.GetAllAsync();
        return Ok(results);
    }

}
