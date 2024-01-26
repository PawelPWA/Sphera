using Microsoft.AspNetCore.Mvc;
using Sphera.Model;
using Sphera.Repository;

namespace Sphera.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;

        public OrdersController(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        [HttpGet("")]
        [HttpGet("{maximumNumber}")]
        public async Task<IEnumerable<SalesOrderYear>> Get(decimal maximumNumber = decimal.MaxValue)
        {
            return await _salesRepository.GetSalesSummary(maximumNumber);
        }

        [HttpGet("{orderYear}")]
        public async Task<IEnumerable<OrderDetail>> GetDetails(int orderYear)
        {
            return await _salesRepository.GetSalesDetails(orderYear);
        }
    }
}
