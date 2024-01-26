namespace Sphera.Model
{
    public class OrderDetail
    {
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ShipDate { get; set; }
        public int SalesOrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Decimal Total { get; set; }
    }
}
