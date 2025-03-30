using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
}