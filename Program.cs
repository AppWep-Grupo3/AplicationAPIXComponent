using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Mapping;
using BackendXComponent.ComponentX.Persistence.Repositories;
using BackendXComponent.ComponentX.Services;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add database connection
var connectionString =
builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine,LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());


//Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Dependency Injection Configuration
builder.Services.AddScoped<ImplOrderRepository, OrderRepository>();
builder.Services.AddScoped<ImplOrderService, OrderService>();
builder.Services.AddScoped<ImplOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<ImpOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<ImplProductRepository, ProductRepository>();
builder.Services.AddScoped<ImplProductService, ProductService>();
builder.Services.AddScoped<ImplUserService, UserService>();
builder.Services.AddScoped<ImplUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ImplUserRespository, UserRepository>();
builder.Services.AddScoped<ImplSubProductRepository, SubProductRepository>();
builder.Services.AddScoped<ImplSubProductService, SubProductService>();
builder.Services.AddScoped<ImplCartRepository, CartRepository>();
builder.Services.AddScoped<ImplCartService, CartService>();





//AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));


//Habilitar cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()//Esto permite que cualquier origen se conecte a nuestra API
            .AllowAnyMethod()//Esto permite que cualquier metodo se conecte a nuestra API
            .AllowAnyHeader());//Esto permite que cualquier header se conecte a nuestra API
});


var app = builder.Build();


//Validation for ensuring Databse objetc are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Autorizmaos el uso de cors
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();