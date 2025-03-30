using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static string GenerateConnectionString(IConfiguration configuration)
        {
            var csBuilder = new SqlConnectionStringBuilder
            {
                DataSource = configuration["Database:Server"] ?? throw new InvalidOperationException("Server not found"),
                InitialCatalog = configuration["Database:Name"] ?? throw new InvalidOperationException("Database name not found"),
                UserID = configuration["Database:Username"] ?? throw new InvalidOperationException("Username not found"),
                Password = configuration["Database:Password"] ?? throw new InvalidOperationException("Password not found"),
                TrustServerCertificate = true
            };
            return csBuilder.ConnectionString;
        }
    }
}
