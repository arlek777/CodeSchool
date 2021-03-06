﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PassJs.Core;
using PassJs.Core.Interfaces;
using PassJs.Core.Services;
using PassJs.DataAccess;
using PassJs.Web.Infrastructure.AppSettings;
using PassJs.Web.Infrastructure.Services;

namespace PassJs.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddCodeSchool(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            ConfigureDb(services, configuration, env);
            ConfigureSecurity(services, configuration);
            ConfigureBusinessLogic(services);
        }

        private static void ConfigureDb(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
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
            services.AddTransient<PassJs.Core.Interfaces.ILogService, PassJs.Web.Infrastructure.Services.FileLogService>();
            services.AddTransient<ISimpleCRUDService, SimpleCRUDService>();
            services.AddTransient<IAnswerSubTaskOptionService, AnswerSubTaskOptionService>();
        }

        private static void ConfigureSecurity(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddTransient<JWTTokenProvider>();
        }
    }
}
