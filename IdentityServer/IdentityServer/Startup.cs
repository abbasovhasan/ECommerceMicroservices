using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Veritabanı bağlantısı için DbContext'i ekle
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // ASP.NET Identity'yi ekle ve IdentityServer ile entegre et
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // IdentityServer yapılandırması
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true; // Static audience claim ekle
            })
                .AddInMemoryIdentityResources(Config.IdentityResources) // Bellekte kimlik kaynakları
                .AddInMemoryApiResources(Config.ApiResources)           // Bellekte API kaynakları
                .AddInMemoryApiScopes(Config.ApiScopes)                 // Bellekte API kapsamları
                .AddInMemoryClients(Config.Clients)                     // Bellekte istemciler
                .AddAspNetIdentity<ApplicationUser>();                  // IdentityServer ile ASP.NET Identity entegrasyonu


            //services.AddScoped<IdentityServer4.Services.IProfileService, ProfileService>();

            // Geliştirme için geliştirici imzalama anahtarını kullan
            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential(); // Üretim dışı ortamlarda kullanılır
            }

            // Kaynak sahibi parola doğrulayıcı ekleme
           // builder.AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();

            // Local API Authentication ekle
            services.AddLocalApiAuthentication();

            // MVC ve kontrolcüleri ekle
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles(); // Statik dosyaları etkinleştir (wwwroot vs.)

            app.UseRouting(); // Routing'i etkinleştir

            // IdentityServer'ı etkinleştir
            app.UseIdentityServer();

            // Authentication ve Authorization middleware'lerini ekle
            app.UseAuthentication();
            app.UseAuthorization();

            // MVC rotalarını tanımla
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute(); // Varsayılan MVC rotası
            });
        }
    }
}