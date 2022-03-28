

using CustomerOnb.Data.Entities;
using CustomerOnb.Shared.Helpers;
using CustomerOnb.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOnb.Shared.UtilityService
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureAppSettingsSerivice(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<CustomerOnbdbContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("BillConnection")));

            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            //var IpSettingsSection = configuration.GetSection("ClientChannels");
            //services.Configure<ClientChannels>(IpSettingsSection);
            //var AuthSettingsSection = configuration.GetSection("AuthTokens");
            //services.Configure<AuthTokens>(AuthSettingsSection);

        }  

        public static void ConfigureScopedService(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IEncryptionManager, EncryptionManager>();
            services.AddScoped<ILgaService, LgaService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IRapidAPI, RapidAPI>();
            
        }
        public static void ConfigureHttpClientService(this IServiceCollection services)
        {
             services.AddHttpClient<RapidHttpClient>();
        }
        public static void ConfigureSingletonService(this IServiceCollection services)
        {
            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
