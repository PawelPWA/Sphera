using Sphera.Model;

namespace Sphera.Repository
{
    public interface ISalesRepository
    {
        Task<List<SalesOrderYear>> GetSalesSummary(decimal maximumTotal);
        Task<List<OrderDetail>> GetSalesDetails(int orderYear);
    }
}
