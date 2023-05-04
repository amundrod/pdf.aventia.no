using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Database;
using pdf.aventia.no.Interfaces;

namespace pdf.aventia.no.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<PdfDbContext>(
                options => options
                    .UseMySql(
                        connectionString,
                        serverVersion,
                        b => b.MigrationsAssembly(typeof(PdfDbContext).Assembly.FullName))
                    .EnableSensitiveDataLogging(environment.IsDevelopment())
                    .EnableDetailedErrors(environment.IsDevelopment())
            );

            services.AddTransient<IPdfService, PdfService>();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(
                new MySqlStorage(
                    connectionString,
                    new MySqlStorageOptions
                    {
                        QueuePollInterval = TimeSpan.FromSeconds(10),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 25000,
                        TransactionTimeout = TimeSpan.FromMinutes(1),
                        TablesPrefix = "hangfire_",
                    }
                )
            ));
            services.AddHangfireServer();

            return services;
        }
    }
}
