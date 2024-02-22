using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Orders
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public bool IsOpen { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
