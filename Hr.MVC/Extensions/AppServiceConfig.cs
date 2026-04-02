using Hr.Infrastructure.Data;
using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hr.MVC.Extensions;

public static class AppServiceConfig
{
      public static IServiceCollection AddConnectionString(this IServiceCollection services, WebApplicationBuilder builder)
      {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();//عشان افلتر المشكلة واظهرها بوضوح فى صفحة الاكسبشن اللى بتظهر بس ف مرحلة التطوير

            return services;
      }

      public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
      {
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                  options =>
                  {
                        // Password settings
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 6;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequiredUniqueChars = 1;

                        // User settings
                        options.User.RequireUniqueEmail = true;
                        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";

                        // SignIn settings
                        options.SignIn.RequireConfirmedAccount = false;
                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                  }
            )
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddSignInManager<SignInManager<ApplicationUser>>();

            services.ConfigureApplicationCookie(options =>
            {
                  options.LoginPath = "/Common/Account/Login";
                  options.LogoutPath = "/Common/Account/Logout";
                  options.AccessDeniedPath = "/Common/Errors/AccessDenied";
                  options.ExpireTimeSpan = TimeSpan.FromDays(7);
                  options.SlidingExpiration = true;
            });
            return services;
      }

      public static async Task AddScopeToUserAndRole(this IServiceProvider appServices)
      {
            using var scope = appServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                  await SeedData.Initialize(services);
            }
            catch (Exception ex)
            {
                  var logger = services.GetRequiredService<ILogger<Program>>();
                  logger.LogError(ex, "An error occurred while seeding the database.");
            }
      }
}
