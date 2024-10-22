using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

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

//Authorization
builder.Services.AddHttpContextAccessor();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("roles");

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "BasketAPIFullAccess";
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "roles",
        NameClaimType = "sub"
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WriteOrder", policy => policy.RequireRole("superadmin", "admin", "director", "waiter"));
    options.AddPolicy("ReadOrder", policy => policy.RequireRole("superadmin", "admin", "director"));
});

var requiredAuthorizationPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requiredAuthorizationPolicy));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
