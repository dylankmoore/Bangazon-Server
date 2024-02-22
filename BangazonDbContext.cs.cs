using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using System.Runtime.CompilerServices;

public class BangazonDbContext : DbContext
{

    public DbSet<Categories> Categories{ get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<PaymentType> PaymentType { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Users> Users { get; set; }

    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed data with CATEGORIES
        modelBuilder.Entity<Categories>().HasData(new Categories[]
        {
        new Categories {Id = 1, Name = "Books"},
        new Categories {Id = 2, Name = "Music"},
        new Categories {Id = 3, Name = "Games"},
        new Categories {Id = 4, Name = "Home"},
        });

        // seed data with ORDERS
        modelBuilder.Entity<Orders>().HasData(new Orders[]
        {
            new Orders {Id = 1, CustomerId = 1, IsOpen = true, PaymentTypeId = 1, DateCreated = DateTime.Now},
            new Orders {Id = 2, CustomerId = 2, IsOpen = true, PaymentTypeId = 2, DateCreated = DateTime.Now},
            new Orders {Id = 3, CustomerId = 3, IsOpen = false, PaymentTypeId = 3, DateCreated = DateTime.Now},
            new Orders {Id = 4, CustomerId = 4, IsOpen = true, PaymentTypeId = 4, DateCreated = DateTime.Now},
        });

        // seed data with PAYMENT TYPES
        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType {Id = 1, Name = "Credit Card"},
            new PaymentType {Id = 2, Name = "Debit Card"},
            new PaymentType {Id = 3, Name = "Apple Pay"},
            new PaymentType {Id = 4, Name = "Paypal"},
        });

        // seed data with PRODUCTS
        modelBuilder.Entity<Products>().HasData(new Products[]
        {
            new Products {Id = 1, SellerId = 1, CategoryId = 1, Name = "The White Album by Joan Didion", Description = "Personal essays", ImageURL = "https://m.media-amazon.com/images/I/41fm2uwWJUL._SY445_SX342_.jpg", Price = 13},
            new Products {Id = 2, SellerId = 2, CategoryId = 2, Name = "Badmotorfinger by Soundgarden", Description = "Audio CD", ImageURL = "https://m.media-amazon.com/images/I/71rRNAnVW6L._SL1400_.jpg", Price = 10},
            new Products {Id = 3, SellerId = 3, CategoryId = 4, Name = "Taboo", Description = "Guessing Game", ImageURL = "https://m.media-amazon.com/images/I/81MBgtB-Y8L._AC_SL1500_.jpg", Price = 14},
            new Products {Id = 4, SellerId = 4, CategoryId = 4, Name = "Confetti Cutting Board", Description = "Multi-colored cutting board", ImageURL = "https://fredericksandmae.com/cdn/shop/products/0511_1296x.jpg?v=1631229005", Price = 70}
        });

        // seed data with USERS
        modelBuilder.Entity<Users>().HasData(new Users[]
        {
            new Users {Id = 1, FirebaseKey = "npAVsfejgPZyg1q0OEKHq6l9zur2", UserName = "branman", FirstName = "Brandon", LastName = "Walsh", Email = "brandonwalsh74@gmail.com", Address = "1675 E Altadena Dr, Altadena, CA", IsSeller = false},
            new Users {Id = 2, FirebaseKey = "key2", UserName = "kells90210", FirstName = "Kelly", LastName = "Taylor", Email = "kelltaylor@hotmail.com", Address = "3959 Longridge Ave Sherman Oaks, CA", IsSeller = true},
            new Users {Id = 3, FirebaseKey = "key3", UserName = "dmckay", FirstName = "Dylan", LastName = "McKay", Email = "dmckay74@aol.com", Address = "1605 E. Altadena Dr, Altadena, CA", IsSeller = false},
            new Users {Id = 4, FirebaseKey = "key4", UserName = "donnaloves2shop", FirstName = "Donna", LastName = "Martin", Email = "dmartin@gmail.com", Address = "1060 Brooklawn Dr., Bel Air CA", IsSeller = false},
        });
    }
}