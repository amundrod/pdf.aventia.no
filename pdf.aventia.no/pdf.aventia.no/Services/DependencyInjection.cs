using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pdf.aventia.no.Database;
using pdf.aventia.no.Interfaces;
using MySql.EntityFrameworkCore.Extensions;
using System;

namespace pdf.aventia.no.Services
{
    // This static class is responsible for the Dependency Injection setup.
    public static class DependencyInjection
    {
        // This method is used to add and configure services.
        public static IServiceCollection AddServices(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            // Retrieve the connection string from the app's configuration.
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add the PdfDbContext to the services and configure it.
            services.AddDbContext<PdfDbContext>(options => options
                .UseMySQL(connectionString)
                .EnableSensitiveDataLogging(environment.IsDevelopment())
                .EnableDetailedErrors(environment.IsDevelopment()));

            // Add the PdfService as a transient service to the services.
            services.AddTransient<IPdfService, PdfService>();

            // Add and configure Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
                {
                    QueuePollInterval = TimeSpan.FromSeconds(10),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 25000,
                    TransactionTimeout = TimeSpan.FromMinutes(1),
                    TablesPrefix = "hangfire_",
                })));

            // Add the Hangfire server to the services.
            services.AddHangfireServer();

            // Return the services collection to allow for chaining.
            return services;
        }
    }
}
