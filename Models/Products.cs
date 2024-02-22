using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string ? Name { get; set; }
        public string ? Description { get; set; }
        public string ? ImageURL { get; set; }
        public decimal ? Price { get; set; }
        public ICollection<Orders> Orders {  get; set; }
    }
}
