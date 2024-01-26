using System.ComponentModel.DataAnnotations.Schema;

namespace Sphera.Repository.EntityFramework.DbModels
{
    [Table("Contact", Schema = "Person")]
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
