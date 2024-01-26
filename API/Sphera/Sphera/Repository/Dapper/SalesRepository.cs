using Dapper;
using Microsoft.Data.SqlClient;
using Sphera.Model;

namespace Sphera.Repository.Dapper
{
    public class SalesRepository : ISalesRepository
    {
        private readonly string _connectionString;

        public SalesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AdventureWorksConnectionString");
        }

        public async Task<List<OrderDetail>> GetSalesDetails(int orderYear)
        {
            var querySql = $@"SELECT soh.OrderDate, soh.ShipDate, soh.SalesOrderID, c.FirstName, c.LastName, SUM(sod.LineTotal) AS Total
FROM [Sales].[SalesOrderHeader] AS soh
JOIN [Sales].[SalesOrderDetail] AS sod ON soh.SalesOrderID = sod.SalesOrderID
JOIN [Person].[Contact] AS c ON soh.ContactID = c.ContactID
WHERE YEAR(soh.OrderDate) = {orderYear}
GROUP BY soh.OrderDate, soh.ShipDate, soh.SalesOrderID, c.FirstName, c.LastName";

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<OrderDetail>(querySql);
                return result.ToList();
            }

        }

        public async Task<List<SalesOrderYear>> GetSalesSummary(decimal maximumTotal)
        {
            var querySql = $@"SELECT YEAR(soh.OrderDate) AS Year, SUM(sod.LineTotal) AS Total, AVG(DAY(soh.ShipDate-soh.OrderDate)) AS TimeToShip
FROM [Sales].[SalesOrderHeader] AS soh
JOIN [Sales].[SalesOrderDetail] AS sod ON soh.SalesOrderID = sod.SalesOrderID
GROUP BY YEAR(soh.OrderDate)
HAVING SUM(sod.LineTotal) <= {maximumTotal}";

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<SalesOrderYear>(querySql);
                return result.ToList();
            }
        }
    }
}
