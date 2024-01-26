using Microsoft.EntityFrameworkCore;
using Sphera.Model;

namespace Sphera.Repository.EntityFramework
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AdventureWorksContext _adventureWorksContext;

        public SalesRepository(AdventureWorksContext adventureWorksContext)
        {
            _adventureWorksContext = adventureWorksContext;
        }

        public async Task<List<SalesOrderYear>> GetSalesSummary(decimal maximumTotal)
        {
            var flatten = await _adventureWorksContext
                .SalesOrderHeaders
                .Join(_adventureWorksContext.SalesOrderDetails, n => n.SalesOrderId, n => n.SalesOrderId, (header, detail) =>
                new
                {
                    header.OrderDate,
                    TimeToShip = (header.ShipDate - header.OrderDate).Days,
                    detail.LineTotal
                })
                .ToListAsync();

            var result = flatten.GroupBy(n => n.OrderDate.Year)
                 .Select(n => new SalesOrderYear
                 {
                     Year = n.Key,
                     TimeToShipInDays = n.Average(header => header.TimeToShip),
                     Total = n.Sum(detail => detail.LineTotal),
                 })
                 .Where(n => n.Total <= maximumTotal)
                 .ToList();

            return result;
        }

        public async Task<List<OrderDetail>> GetSalesDetails(int orderYear)
        {
            var result = await _adventureWorksContext
                .SalesOrderHeaders
                .Where(n => n.OrderDate.Year == orderYear)
                .Select(n =>
                new OrderDetail
                {
                    OrderDate = n.OrderDate,
                    ShipDate = n.ShipDate,
                    SalesOrderId = n.SalesOrderId,
                    FirstName = n.Contact.FirstName,
                    LastName = n.Contact.LastName,
                    Total = n.SalesOrderDetails.Sum(detail => detail.LineTotal),
                })
                .ToListAsync();

            return result;
        }
    }
}
