using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.DataAccess;
using CodeSchool.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSchool.Web.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddCodeSchool(this IServiceCollection services, IConfigurationRoot configuration)
        {
            ConfigureDb(services, configuration);
            ConfigureSecurity(services, configuration);
            ConfigureBusinessLogic(services);
        }

        private static void ConfigureDb(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddTransient<IGenericRepository, EntityFrameworkRepository>();
            services.AddScoped<DbContext>((provider) => new CodeSchoolDbContext(configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureBusinessLogic(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<IChapterService, ChapterService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
        }

        private static void ConfigureSecurity(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddTransient<JWTTokenProvider>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult("User Unauthorized");
                    }
                };
            });
        }
    }
}
