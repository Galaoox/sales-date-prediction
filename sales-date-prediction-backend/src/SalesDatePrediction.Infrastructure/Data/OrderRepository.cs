
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;
using System.Data;

namespace SalesDatePrediction.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }




    public async Task<IEnumerable<OrderSummary>> GetByCustomerIdAsync(
       int customerId,
       string sortColumn,
       string sortOrder,
       int pageNumber,
       int pageSize)
    {
        var parameters = new List<SqlParameter>
    {
        new SqlParameter("@CustomerId", customerId),
        new SqlParameter("@PageNumber", pageNumber),
        new SqlParameter("@PageSize", pageSize)
    };

        // Validar la columna de ordenamiento para evitar SQL Injection
        string safeSortColumn = sortColumn switch
        {
            "OrderId" => "Orderid",
            "RequiredDate" => "Requireddate",
            "ShippedDate" => "Shippeddate",
            "ShipName" => "Shipname",
            "ShipAddress" => "Shipaddress",
            "ShipCity" => "Shipcity",
            _ => "Orderid" // Valor por defecto
        };

        string safeSortOrder = sortOrder?.ToUpper() == "DESC" ? "DESC" : "ASC";

        string sqlQuery = $@"
        DECLARE @sql NVARCHAR(MAX);
        SET @sql = '
        WITH OrderedOrders AS (
            SELECT 
                Orderid, 
                Requireddate, 
                Shippeddate, 
                Shipname, 
                Shipaddress, 
                Shipcity,
                ROW_NUMBER() OVER (ORDER BY {safeSortColumn} {safeSortOrder}) AS RowNum
            FROM Sales.Orders
            WHERE custid = @CustomerId
        )
        SELECT 
            OrderId, 
            RequiredDate, 
            ShippedDate, 
            ShipName, 
            ShipAddress, 
            ShipCity
        FROM OrderedOrders
        WHERE RowNum BETWEEN (@PageSize * (@PageNumber - 1)) + 1 AND @PageSize * @PageNumber
        ORDER BY RowNum;';

        EXEC sp_executesql @sql, 
                           N'@CustomerId INT, @PageNumber INT, @PageSize INT', 
                           @CustomerId, @PageNumber, @PageSize;
    ";

        return await _context.Database.SqlQueryRaw<OrderSummary>(sqlQuery, parameters.ToArray()).ToListAsync();
    }


    public async Task<int> CountByCustomerIdAsync(int customerId)
    {
        string query = @"
        SELECT COUNT(*) AS Value
        FROM StoreSample.Sales.Orders 
        WHERE custid = @CustomerId";

        return await _context.Database
            .SqlQueryRaw<int>(query, new SqlParameter("@CustomerId", customerId))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustomerOrderPrediction>> GetOrderPredictionsDataAsync(
      string search,
      string sortColumn,
      string sortOrder,
      int pageNumber,
      int pageSize)
    {
        var parameters = new List<SqlParameter>
    {
        new SqlParameter("@SearchText", string.IsNullOrEmpty(search) ? "" : search),
        new SqlParameter("@PageNumber", pageNumber),
        new SqlParameter("@PageSize", pageSize)
    };

        // Asegurar que la columna de ordenamiento es segura
        string safeSortColumn = sortColumn switch
        {
            "CompanyName" => "companyname",
            "LastOrderDate" => "last_orderdate",
            "NextPredictedOrder" => "next_predicted_order",
            _ => "companyname" // Default
        };

        string safeSortOrder = sortOrder?.ToUpper() == "DESC" ? "DESC" : "ASC";

        string sqlQuery = $@"
        DECLARE @sql NVARCHAR(MAX);
        SET @sql = '
        WITH OrderDifferences AS (
            SELECT 
                ord.custid,  
                ord.orderdate, 
                LEAD(ord.orderdate) OVER (PARTITION BY ord.custid ORDER BY ord.orderdate) AS next_orderdate
            FROM Sales.Orders ord
        ),
        CustomerStats AS (
            SELECT 
                ord.custid,
                cus.companyname,
                MAX(ord.orderdate) AS last_orderdate,
                AVG(DATEDIFF(DAY, ord.orderdate, ord.next_orderdate)) AS avg_days_between_orders,
                CASE
                    WHEN AVG(DATEDIFF(DAY, ord.orderdate, ord.next_orderdate)) IS NULL THEN NULL
                    ELSE DATEADD(day, AVG(DATEDIFF(DAY, ord.orderdate, ord.next_orderdate)), MAX(ord.orderdate)) 
                END AS next_predicted_order
            FROM OrderDifferences ord
            JOIN Sales.Customers cus ON cus.custid = ord.custid
            WHERE 
                (@SearchText IS NULL OR 
                 @SearchText = '''' OR 
                 cus.companyname LIKE ''%'' + @SearchText + ''%'')
            GROUP BY ord.custid, cus.companyname
        ),
        SortedResults AS (
            SELECT *,
                ROW_NUMBER() OVER (ORDER BY {safeSortColumn} {safeSortOrder}) AS RowNum
            FROM CustomerStats
        )
        SELECT 
            custid AS CustomerId,
            companyname AS CompanyName,
            last_orderdate AS LastOrderDate,
            next_predicted_order AS NextPredictedOrder
        FROM SortedResults
        WHERE RowNum BETWEEN (@PageSize * (@PageNumber - 1)) + 1 AND @PageSize * @PageNumber;';
        
        EXEC sp_executesql @sql, N'@SearchText NVARCHAR(50), @PageNumber INT, @PageSize INT', 
                           @SearchText, @PageNumber, @PageSize;";

        var results = await _context.Database.SqlQueryRaw<CustomerOrderPrediction>(sqlQuery, parameters.ToArray()).ToListAsync();
        return results;
    }

    public async Task<int> GetOrderPredictionsCountAsync(string search)
    {
        var parameters = new[]
        {
        new SqlParameter("@SearchText", string.IsNullOrEmpty(search) ? DBNull.Value : search)
    };

        string countQuery = @"
        SELECT COUNT(*) as Value
        FROM (
            SELECT 1 AS CountColumn
            FROM Sales.Orders ord
            JOIN Sales.Customers cus ON cus.custid = ord.custid
            WHERE 
                (@SearchText IS NULL OR 
                 @SearchText = '' OR 
                 cus.companyname LIKE '%' + @SearchText + '%')
            GROUP BY ord.custid, cus.companyname
        ) AS OrderCounts";

        return await _context.Database.SqlQueryRaw<int>(countQuery, parameters).FirstOrDefaultAsync();
    }





    public async Task<(IEnumerable<CustomerOrderPrediction> data, int totalCount)> GetOrderPredictionsAsync(
        string search,
        string sortColumn,
        string sortOrder,
        int pageNumber,
        int pageSize)
    {
        var data = await GetOrderPredictionsDataAsync(search, sortColumn, sortOrder, pageNumber, pageSize);
        var totalCount = await GetOrderPredictionsCountAsync(search);

        return (data, totalCount);
    }

    public async Task CreateOrderWithDetails(Order order, List<OrderDetail> orderDetails)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            int orderId = await InsertOrderAsync(order, transaction);  // 🔹 Pasar la transacción

            await InsertOrderDetailsAsync(orderId, orderDetails);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<int> InsertOrderAsync(Order order, IDbContextTransaction transaction)
    {
        var connection = _context.Database.GetDbConnection();

        await using var command = connection.CreateCommand();
        command.Transaction = (SqlTransaction)transaction.GetDbTransaction();  // 🔹 Usar la misma transacción

        command.CommandText = @"
    INSERT INTO Sales.Orders (custid, empid, shipperid, orderdate, requireddate, shippeddate, shipname, shipaddress, shipcity, shipcountry)
    VALUES (@Custid, @Empid, @Shipperid, @Orderdate, @Requireddate, @Shippeddate, @Shipname, @Shipaddress, @Shipcity, @Shipcountry);

    SELECT SCOPE_IDENTITY();";

        command.Parameters.Add(new SqlParameter("@Custid", order.Custid));
        command.Parameters.Add(new SqlParameter("@Empid", order.Empid));
        command.Parameters.Add(new SqlParameter("@Shipperid", order.Shipperid));
        command.Parameters.Add(new SqlParameter("@Orderdate", order.Orderdate));
        command.Parameters.Add(new SqlParameter("@Requireddate", (object?)order.Requireddate ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@Shippeddate", (object?)order.Shippeddate ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@Shipname", order.Shipname));
        command.Parameters.Add(new SqlParameter("@Shipaddress", order.Shipaddress));
        command.Parameters.Add(new SqlParameter("@Shipcity", order.Shipcity));
        command.Parameters.Add(new SqlParameter("@Shipcountry", order.Shipcountry));

        var orderId = await command.ExecuteScalarAsync();
        return Convert.ToInt32(orderId);
    }

    private async Task InsertOrderDetailsAsync(int orderId, List<OrderDetail> orderDetails)
    {
        foreach (var detail in orderDetails)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO Sales.OrderDetails (orderid, productid, unitprice, qty, discount)
                VALUES (@OrderId, @Productid, @Unitprice, @Quantity, @Discount);",
                new SqlParameter("@OrderId", orderId),
                new SqlParameter("@Productid", detail.Productid),
                new SqlParameter("@Unitprice", detail.Unitprice),
                new SqlParameter("@Quantity", detail.Quantity),
                new SqlParameter("@Discount", detail.Discount));
        }
    }


}
