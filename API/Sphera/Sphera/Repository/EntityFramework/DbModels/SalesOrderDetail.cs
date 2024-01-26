using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sphera.Repository.EntityFramework.DbModels
{
    [Table("SalesOrderDetail", Schema = "Sales")]
    public class SalesOrderDetail
    {
        public int SalesOrderId { get; set; }
        public int SalesOrderDetailId { get; set; }
        public decimal LineTotal { get; set; }

        public SalesOrderHeader SalesOrderHeader { get; set; }
    }
}
