using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Net.Mail;
using Bangazon.DTOs;

var builder = WebApplication.CreateBuilder(args);


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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET CATEGORIES
app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

// GET PAYMENT TYPES
app.MapGet("/api/paymenttypes", (BangazonDbContext db) =>
{
    return db.PaymentType.ToList();
});

// GET USERS
app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

// GET USERS BY ID
app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    var user = db.Users.SingleOrDefault(u => u.Id == id);

    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

// GET SELLERS
app.MapGet("/api/users/sellers", (BangazonDbContext db) =>
{
    return db.Users.Where(u => u.IsSeller == true).ToList();
});

// CREATE USERS
app.MapPost("/api/users", (BangazonDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/user/{user.Id}", user);
});

// UPDATE USERS
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

// DELETE USERS
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

// GET PRODUCTS
app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

// GET PRODUCTS BY ID
app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var product = db.Products.SingleOrDefault(u => u.Id == id);

    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

// CREATE PRODUCT
app.MapPost("/api/products", (BangazonDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/api/products/{product.Id}", product);
});

// UPDATE PRODUCT
app.MapPut("/api/products/{id}", (BangazonDbContext db, Product product, int id) =>
{
    var updateProduct = db.Products.SingleOrDefault(u => u.Id == id);
    if (updateProduct == null)
    {
        return Results.NotFound();
    }
    updateProduct.Name = product.Name;
    updateProduct.Description = product.Description;
    updateProduct.ImageURL = product.ImageURL;
    updateProduct.Price = product.Price;
    db.SaveChanges();
    return Results.Ok(updateProduct);

});

// DELETE PRODUCT
app.MapDelete("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var deleteProduct = db.Products.SingleOrDefault(u => u.Id == id);
    if (deleteProduct == null)
    {
        return Results.NotFound();
    }
    db.Products.Remove(deleteProduct);
    db.SaveChanges();
    return Results.Ok(deleteProduct);
});

// GET ORDERS
app.MapGet("/api/orders", (BangazonDbContext db) =>
{
    return db.Orders.ToList();
});

// GET ORDER BY ID
app.MapGet("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    var order = db.Orders.SingleOrDefault(u => u.Id == id);

    if (order == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(order);
});

// CREATE ORDER
app.MapPost("/api/orders", (BangazonDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/orders/{order.Id}", order);
});

// UPDATE ORDER
app.MapPut("/api/orders/{id}", (BangazonDbContext db, Order order, int id) =>
{
    var updateOrder = db.Orders.SingleOrDefault(u => u.Id == id);
    if (updateOrder == null)
    {
        return Results.NotFound();
    }
    updateOrder.PaymentTypeId = order.PaymentTypeId;
    updateOrder.IsOpen = order.IsOpen;
    updateOrder.DateCreated = order.DateCreated;
    db.SaveChanges();
    return Results.Ok(updateOrder);

});

// DELETE ORDER
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

// ADD PRODUCTS TO ORDER
app.MapPost("/api/orders/addingProduct", (BangazonDbContext db, productOrderDTO newProduct) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == newProduct.OrderId);
    if (order == null)
    {
        return Results.NotFound();
    }

    var product = db.Products.Find(newProduct.ProductId);
    if (product == null)
    {
        return Results.NotFound();
    }

    order.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/orders/addingProduct", newProduct);
});

// DELETE PRODUCTS FROM ORDER
app.MapDelete("/orders/{id}/products/{pId}", (BangazonDbContext db, int id, int pId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    var product = db.Products.Find(pId);
    if (product == null)
    {
        return Results.NotFound();
    }

    order.Products.Remove(product);
    db.SaveChanges();
    return Results.Ok();
});

app.Run();