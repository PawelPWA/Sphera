using System.ComponentModel.DataAnnotations.Schema;

namespace Sphera.Repository.EntityFramework.DbModels
{
    [Table("SalesOrderHeader", Schema = "Sales")]
    public class SalesOrderHeader
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
