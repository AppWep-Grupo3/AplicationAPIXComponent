using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;

using BackendXComponent.ComponentX.Persistence.Repositories;
using BackendXComponent.ComponentX.Services;
using BackendXComponent.Security.Authorization.Handlers.Implementations;
using BackendXComponent.Security.Authorization.Handlers.Interfaces;
using BackendXComponent.Security.Authorization.Middleware;
using BackendXComponent.Security.Authorization.Settings;
using BackendXComponent.Security.Domain.Services;
using BackendXComponent.Security.Domain.Repositories;
using BackendXComponent.Security.Persistence.Repositories;
using BackendXComponent.Security.Services;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
// Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type =
                    ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});


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


builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<ImplUserRepository, UserRepository>();
builder.Services.AddScoped<ImplUserService, UserService>();



builder.Services.AddScoped<ImplUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ImplSubProductRepository, SubProductRepository>();
builder.Services.AddScoped<ImplSubProductService, SubProductService>();
builder.Services.AddScoped<ImplCartRepository, CartRepository>();
builder.Services.AddScoped<ImplCartService, CartService>();


// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


//AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(BackendXComponent.ComponentX.Mapping.ModelToResourceProfile),
    typeof(BackendXComponent.Security.Mapping.ModelToResourceProfile),
    typeof(BackendXComponent.ComponentX.Mapping.ResourceToModelProfile),
    typeof(BackendXComponent.Security.Mapping.ResourceToModelProfile)    
    );


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
// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });



app.UseHttpsRedirection();

//Autorizmaos el uso de cors
app.UseCors("CorsPolicy");


// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();
// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
