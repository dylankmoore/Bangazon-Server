using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using System.Runtime.CompilerServices;

public class BangazonDbContext : DbContext
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentType> PaymentType { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed data with CATEGORIES
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
        new Category {Id = 1, Name = "Books"},
        new Category {Id = 2, Name = "Music"},
        new Category {Id = 3, Name = "Games"},
        new Category {Id = 4, Name = "Home"},
        new Category {Id = 5, Name = "Movies & TV"}
        });

        // seed data with ORDERS
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order {Id = 1, CustomerId = 1, IsOpen = true, PaymentTypeId = 1, DateCreated = DateTime.Now},
            new Order {Id = 2, CustomerId = 2, IsOpen = true, PaymentTypeId = 2, DateCreated = DateTime.Now},
            new Order {Id = 3, CustomerId = 3, IsOpen = false, PaymentTypeId = 3, DateCreated = DateTime.Now},
            new Order {Id = 4, CustomerId = 4, IsOpen = true, PaymentTypeId = 4, DateCreated = DateTime.Now},
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
        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product {Id = 1, SellerId = 3, CategoryId = 1, Name = "The White Album by Joan Didion", Description = "Book of personal essays", ImageURL = "https://m.media-amazon.com/images/I/41fm2uwWJUL._SY445_SX342_.jpg", Price = 13},
            new Product {Id = 2, SellerId = 2, CategoryId = 2, Name = "Badmotorfinger by Soundgarden", Description = "Audio CD", ImageURL = "https://m.media-amazon.com/images/I/71rRNAnVW6L._SL1400_.jpg", Price = 10},
            new Product {Id = 3, SellerId = 3, CategoryId = 3, Name = "Taboo", Description = "Guessing Game", ImageURL = "https://m.media-amazon.com/images/I/81MBgtB-Y8L._AC_SL1500_.jpg", Price = 14},
            new Product {Id = 4, SellerId = 2, CategoryId = 4, Name = "Cereal Bowl Candle", Description = "Froot Loop cereal bowl as a fun candle!", ImageURL = "https://m.media-amazon.com/images/I/71kVczcRfdL._AC_SL1500_.jpg", Price = 35},
            new Product {Id = 5, SellerId = 2, CategoryId = 5, Name = "Beverly Hills 90210: The Complete Series", Description = "DVD", ImageURL = "https://m.media-amazon.com/images/I/91ULKyUQWlL._SX425_.jpg", Price = 100},
            new Product {Id = 6, SellerId = 3, CategoryId = 2, Name = "Now That's What I Call Music", Description = "Audio CD", ImageURL = "https://m.media-amazon.com/images/I/51G92BBD5EL.jpg", Price = 20},
            new Product {Id = 7, SellerId = 2, CategoryId = 3, Name = "Scrabble", Description = "Board Game", ImageURL = "https://m.media-amazon.com/images/I/71Jr69W+MpL._AC_SL1500_.jpg", Price = 30},
            new Product {Id = 8, SellerId = 3, CategoryId = 4, Name = "Pilea Plant", Description = "Potted Plant", ImageURL = "https://m.media-amazon.com/images/I/61PUzl5Z1BL._AC_SL1500_.jpg", Price = 25}
        });

        // seed data with USERS
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Uid = "npAVsfejgPZyg1q0OEKHq6l9zur2", UserName = "branman", FirstName = "Brandon", LastName = "Walsh", Email = "brandonwalsh74@gmail.com", Address = "1675 E Altadena Dr, Altadena, CA", IsSeller = false},
            new User {Id = 2, Uid = "fbkey2", UserName = "kells90210", FirstName = "Kelly", LastName = "Taylor", Email = "kelltaylor@hotmail.com", Address = "3959 Longridge Ave, Sherman Oaks, CA", IsSeller = true},
            new User {Id = 3, Uid = "fbkey3", UserName = "dmckay", FirstName = "Dylan", LastName = "McKay", Email = "dmckay74@aol.com", Address = "1605 E. Altadena Dr, Altadena, CA", IsSeller = true},
            new User {Id = 4, Uid = "fbkey4", UserName = "donnaloves2shop", FirstName = "Donna", LastName = "Martin", Email = "dmartin@gmail.com", Address = "1060 Brooklawn Dr., Bel Air, CA", IsSeller = false},
        });
    }
}