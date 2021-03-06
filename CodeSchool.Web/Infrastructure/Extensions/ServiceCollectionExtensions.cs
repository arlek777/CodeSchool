﻿using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.DataAccess;
using CodeSchool.Web.Infrastructure.AppSettings;
using CodeSchool.Web.Infrastructure.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSchool.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddCodeSchool(this IServiceCollection services, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            ConfigureDb(services, configuration, env);
            ConfigureSecurity(services, configuration);
            ConfigureBusinessLogic(services);
        }

        private static void ConfigureDb(IServiceCollection services, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            var connStringName = env.IsDevelopment() ? "LocalConnection" : "RemoteConnection";
            var connString = configuration.GetConnectionString(connStringName);

            services.AddTransient<IGenericRepository, EntityFrameworkRepository>();
            services.AddScoped<DbContext>((provider) => new CodeSchoolDbContext(connString));
        }

        private static void ConfigureBusinessLogic(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISubTaskService, SubTaskService>();
            services.AddTransient<ITaskHeadService, TaskHeadService>();
            services.AddTransient<IUserSubTaskService, UserSubTaskService>();
            services.AddTransient<IUserTaskHeadService, UserTaskHeadService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ILogService, FileLogService>();
            services.AddTransient<ISimpleCRUDService, SimpleCRUDService>();
            services.AddTransient<IAnswerSubTaskOptionService, AnswerSubTaskOptionService>();
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
