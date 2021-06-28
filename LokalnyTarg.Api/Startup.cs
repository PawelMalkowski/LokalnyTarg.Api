using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.Middlewares;
using LokalnyTarg.Data.Identity;
using LokalnyTarg.IServices.User;
using LokalnyTarg.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LokalnyTarg.Data.Sql;
using LokalnyTarg.Data.Sql.Announcement;
using LokalnyTarg.Data.Sql.Category;
using LokalnyTarg.Data.Sql.UserProfile;
using LokalnyTarg.IData.Announcement;
using LokalnyTarg.IData.Category;
using LokalnyTarg.IData.UserProfile;
using LokalnyTarg.IServices.Announcement;
using LokalnyTarg.IServices.Category;
using LokalnyTarg.IServices.UserProfile;
using LokalnyTarg.Services.Announcement;
using LokalnyTarg.Services.Category;
using LokalnyTarg.Services.UserProfile;

namespace LokalnyTarg.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost", "null", "http://lokalny.targ.pl:8081", "http://lokalny.targ.pl:8080", "http://localhost:8080");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();

                    });
            });
            services.AddTransient<Data.Sql.Migrations.DateBaseSeed>();
            services.AddControllers();
            services.AddDbContextPool<LokalnyTargIdentityContext>(options => options.UseMySql(Configuration.GetConnectionString("IdentityDbContext"), ServerVersion.AutoDetect(Configuration.GetConnectionString("IdentityDbContext")),b => b.MigrationsAssembly("LokalnyTarg.Api")));
            services.AddDbContext<LokalnyTargDBContext>(options => options.UseMySql(Configuration.GetConnectionString("LokalnyTargDbContext"), ServerVersion.AutoDetect(Configuration.GetConnectionString("LokalnyTargDbContext")),b => b.MigrationsAssembly("LokalnyTarg.Api")));
            services.AddTransient<Data.Identity.Migrations.DataBaseSeed>();
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.UseApiBehavior = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "Token";
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 2;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.User.AllowedUserNameCharacters = "a�bc�de�fghijkl�mn�o�pqrs�tuvwxyz��A�BC�DE�FGHIJKL�MN�O�PRS�TUWYZ��1234567890-_$.";
                })
                .AddEntityFrameworkStores<LokalnyTargIdentityContext>().AddDefaultTokenProviders();

            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserService, UserService>();
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            { 
                var context = serviceScope.ServiceProvider.GetRequiredService<LokalnyTargIdentityContext>();
                var databaseSeed = serviceScope.ServiceProvider.GetRequiredService<Data.Identity.Migrations.DataBaseSeed>();
               // context.Database.EnsureDeleted();
               // context.Database.EnsureCreated();
                context.Database.Migrate();
                var a = databaseSeed.Seed();
                a.Wait();
            }
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<LokalnyTargDBContext>();
                var databaseSeed = serviceScope.ServiceProvider.GetRequiredService<Data.Sql.Migrations.DateBaseSeed>();
               // context.Database.EnsureDeleted();
              //  context.Database.EnsureCreated();
                context.Database.Migrate();
                databaseSeed.Seed();
                
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors();
            app.UseAuthorization();
            app.UseApiVersioning();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
