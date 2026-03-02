using MinimalProductApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using MinimalProductApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Entity Framework configuration
builder.Services.AddDbContext<ProductDbContext>(options
    => options.UseInMemoryDatabase("ProductsDatabase"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/products",
    async (ProductDto productDto, IProductRepository productRepo) =>
    {
        await productRepo.SaveProductAsync(productDto);
        return Results.Created($"/products/{productDto.Id}", productDto);
    })
    .WithDescription("Create a new product")
    .Produces(201)
    .Produces(500);

app.MapGet("/products", 
    async (IProductRepository productRepo) =>
    {
        return await productRepo.GetProductsAsync();
    })
    .WithDescription("Get all products")
    .Produces(200)
    .Produces(500);

app.MapGet("/products/{productId:int}", 
    async (int productId, IProductRepository productRepo) =>
    {
        return await productRepo.GetProductByIdAsync(productId);
    })
    .WithDescription("Get a product by Id")
    .Produces(200)
    .Produces(404);

app.MapPut("/products/{productId:int}",     
    async (int productId, ProductDto productDto, IProductRepository productRepo) =>
    {
        await productRepo.UpdateProductAsync(productId, productDto);
        return Results.Ok();
    })
    .WithDescription("Update a product")
    .Produces(200)
    .Produces(404);

app.MapDelete("/products/{productId:int}",     
    async (int productId, IProductRepository productRepo) =>
    {
        await productRepo.DeleteProductAsync(productId);
        return Results.Ok();
    })
    .WithDescription("Update a product")
    .Produces(200)
    .Produces(404);

app.Run();

