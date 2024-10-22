using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


/*var requiredAuthorizationPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
*/

/*builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requiredAuthorizationPolicy));
});*/


//Authorization
builder.Services.AddHttpContextAccessor();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("roles");

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ImageAPIFullAccess";
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();