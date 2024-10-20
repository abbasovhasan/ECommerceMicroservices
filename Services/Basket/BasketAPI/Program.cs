using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Controllers ve FluentValidation eklenmesi
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BasketDtoValidator>()) // FluentValidation
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // JSON serileştirme ayarları
    });

// Swagger/OpenAPI eklenmesi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext eklenmesi (SQL Server bağlantısı)
builder.Services.AddDbContext<BasketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository ve Service katmanlarının eklenmesi (Dependency Injection)
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();

// AutoMapper eklenmesi
builder.Services.AddAutoMapper(typeof(MappingProfile));

// CORS eklenmesi (Opsiyonel, Cross-Domain istekleri açmak için)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS etkinleştirme (Gerekirse)
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
