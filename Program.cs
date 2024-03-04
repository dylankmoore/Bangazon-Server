using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Net.Mail;
using Bangazon.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET CATEGORIES //WORKING
app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    var categoriesWithProducts = db.Categories
        .Include(c => c.Products)
        .ToList();

    return categoriesWithProducts;
});

// GET PAYMENT TYPES //WORKING
app.MapGet("/api/paymenttypes", (BangazonDbContext db) =>
{
    return db.PaymentType.ToList();
});

// GET USERS //WORKING
app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

// GET USERS BY UID //WORKING
app.MapGet("/api/users/{uid}", (BangazonDbContext db, string uid) =>
{
    var user = db.Users.SingleOrDefault(u => u.Uid == uid);

    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

//GET USERS BY ID //WORKING
app.MapGet("/api/users/ids/{id}", (BangazonDbContext db, int id) =>
{
    var user = db.Users.SingleOrDefault(u => u.Id == id);

    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

// GET SELLERS //WORKING
app.MapGet("/api/users/sellers", (BangazonDbContext db) =>
{
    return db.Users.Where(u => u.IsSeller == true).ToList();
});

// REGISTER NEW USER //WORKING
app.MapPost("/api/register", async (BangazonDbContext db, User user) =>
{
    var existingUser = await db.Users.SingleOrDefaultAsync(u => u.UserName == user.UserName);
    if (existingUser != null)
    {
        return Results.Conflict("User already exists!");
    }

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/api/users/{user.Uid}", user); 
});

// CREATE USERS //WORKING
app.MapPost("/api/users", (BangazonDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/user/{user.Id}", user);
});

// UPDATE USERS //WORKING
app.MapPut("/api/user/{id}", (BangazonDbContext db, User user, int id) =>
{
    var updateUser = db.Users.SingleOrDefault(u => u.Id == id);
    if (updateUser == null)
    {
        return Results.NotFound();
    }
    updateUser.UserName = user.UserName;
    updateUser.FirstName = user.FirstName;
    updateUser.LastName = user.LastName;
    updateUser.Email = user.Email;
    updateUser.Address = user.Address;
    updateUser.IsSeller = user.IsSeller;
    db.SaveChanges();
    return Results.Ok(updateUser);

});

// DELETE USERS //WORKING
app.MapDelete("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    var deleteUser = db.Users.SingleOrDefault(u => u.Id == id);
    if (deleteUser == null)
    {
        return Results.NotFound();
    }
    db.Users.Remove(deleteUser);
    db.SaveChanges();
    return Results.Ok(deleteUser);
});

// GET PRODUCTS //WORKING
app.MapGet("/api/products", (BangazonDbContext db, bool latest = false) =>
{
    IQueryable<Product> query = db.Products
        .Include(p => p.Seller)
        .Include(p => p.Category);

    if (latest)
    {
        query = query.OrderByDescending(p => p.Id).Take(20);
    }

    var products = query.ToList().Select(p => new
    {
        p.Id,
        p.Name,
        p.Description,
        p.ImageURL,
        p.Price,
        Category = new { p.Category.Id, p.Category.Name },
        Seller = new
        {
            p.Seller.Id,
            p.Seller.UserName,
            p.Seller.FirstName,
            p.Seller.LastName,
            p.Seller.Email,
            p.Seller.Address,
            p.Seller.IsSeller
        }
    }).ToList();

    return Results.Ok(products);
});


// GET PRODUCTS BY ID //WORKING
app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var product = db.Products
                    .Include(p => p.Seller)
                    .Include(p => p.Category)
                    .SingleOrDefault(u => u.Id == id);

    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

// GET ORDERS //WORKING
app.MapGet("/api/orders", (BangazonDbContext db, bool? completed, int? customerId) =>
{
    IQueryable<Order> query = db.Orders.Include(o => o.Products).ThenInclude(p => p.Seller);

    if (customerId.HasValue)
    {
        query = query.Where(o => o.CustomerId == customerId.Value);
    }

    if (completed.HasValue)
    {
        query = query.Where(o => o.IsOpen != completed.Value);
    }

    var orders = query.ToList();
    return Results.Ok(orders);
});


// GET ORDER BY ID //WORKING
app.MapGet("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    var order = db.Orders
      .Include(o => o.Products)
          .ThenInclude(p => p.Seller)
      .FirstOrDefault(o => o.Id == id);

    if (order == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(order);
});

// COMPLETE ORDER //WORKING
app.MapPut("/api/orders/{id}/complete", async (BangazonDbContext db, int id, int paymentTypeId) =>
{
    var orderToUpdate = await db.Orders.FindAsync(id);

    if (orderToUpdate == null || !orderToUpdate.IsOpen)
    {
        return Results.NotFound("Order not found or already finalized.");
    }

    // Assign the payment type and close the order
    orderToUpdate.PaymentTypeId = paymentTypeId;
    orderToUpdate.IsOpen = false;
    await db.SaveChangesAsync();

    return Results.Ok(orderToUpdate);
});

// DELETE ORDER //WORKING
app.MapDelete("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    var deleteOrder = db.Orders.SingleOrDefault(u => u.Id == id);
    if (deleteOrder == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(deleteOrder);
    db.SaveChanges();
    return Results.Ok(deleteOrder);
});

//REMOVE ITEM FROM ORDER/CART //WORKING
app.MapDelete("/api/cart/{customerId}/products/{productId}", async (BangazonDbContext db, int customerId, int productId) =>
{
    var order = await db.Orders
        .Where(o => o.CustomerId == customerId && o.IsOpen)
        .Include(o => o.Products)
        .FirstOrDefaultAsync();

    if (order == null)
    {
        return Results.NotFound("No open cart found for this customer.");
    }

    var productToRemove = order.Products.FirstOrDefault(p => p.Id == productId);
    if (productToRemove == null)
    {
        return Results.NotFound("Product not found in the cart!");
    }

    order.Products.Remove(productToRemove);
    await db.SaveChangesAsync();

    return Results.Ok("Product removed from cart.");
});

// ADD PRODUCTS TO ORDER/CART //WORKING
app.MapPost("/api/cart", (BangazonDbContext db, productOrderDTO newProductOrder) =>
{
   var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == newProductOrder.OrderId && o.CustomerId == newProductOrder.CustomerId && o.IsOpen);

    if (order == null)
    {
        return Results.NotFound($"Order not found for CustomerId: {newProductOrder.CustomerId}");
    }

    var product = db.Products.Find(newProductOrder.ProductId);
    if (product == null)
    {
        return Results.NotFound("Product not found!");
    }

    order.Products.Add(product);
    db.SaveChanges();

    return Results.Created($"/api/cart?customerId={newProductOrder.CustomerId}", new { order.Id, product.Name });
});

// GET PRODUCTS BY SELLER //WORKING
app.MapGet("/api/sellers/{sellerId}/products", (BangazonDbContext db, int sellerId) =>
{
    var sellerProducts = db.Products.Where(p => p.SellerId == sellerId).ToList();
    return sellerProducts;
});

// SEARCH PRODUCTS //WORKING
app.MapGet("/api/products/search", (BangazonDbContext db, string searchQuery = null) =>
{
    var query = db.Products
              .Include(p => p.Seller)
              .Include(p => p.Category)
              .AsQueryable();

    if (!string.IsNullOrEmpty(searchQuery))
    {
        query = query.Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()) || p.Description.ToLower().Contains(searchQuery.ToLower()));
    }

    var matchingProducts = query.ToList();
    return Results.Ok(matchingProducts);
});

// GET ORDERS WITH SELLER'S PRODUCTS //WORKING
app.MapGet("/api/order/history/seller/{sellerId}", async (BangazonDbContext db, int sellerId, bool? completed) =>
{
    var query = db.Orders
        .Where(o => o.Products.Any(p => p.SellerId == sellerId))
        .AsQueryable();

    if (completed.HasValue)
    {
        query = query.Where(o => o.IsOpen != completed.Value);
    }

    var orders = await query
        .Include(o => o.Products.Where(p => p.SellerId == sellerId))
        .ToListAsync();

    return Results.Ok(orders);
});

// VIEW SHOPPING CART DETAILS //WORKING
app.MapGet("/api/cart", (BangazonDbContext db, int customerId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.CustomerId == customerId && o.IsOpen);

    if (order == null)
    {
        return Results.NotFound("Cart not found!");
    }

    var productsInCart = order.Products.ToList();
    decimal totalAmount = productsInCart.Sum(p => p.Price);

    var cartDetails = new
    {
        Products = productsInCart,
        TotalAmount = totalAmount
    };

    return Results.Ok(cartDetails);
});

// SEARCH FOR SELLERS //WORKING
app.MapGet("/api/sellers/search", (BangazonDbContext db, string searchQuery = null) =>
{
    var query = db.Users.Where(u => u.IsSeller).AsQueryable();

    if (!string.IsNullOrEmpty(searchQuery))
    {
        query = query.Where(u => u.FirstName.ToLower().Contains(searchQuery.ToLower()) || u.LastName.ToLower().Contains(searchQuery.ToLower()));
    }

    var matchingSellers = query.ToList();
    return Results.Ok(matchingSellers);
});


app.Run();