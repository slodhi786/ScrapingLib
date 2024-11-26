using Microsoft.Extensions.DependencyInjection;

using ScrapingLib.Interfaces;
using ScrapingLib.Services;

namespace ScrapingLib.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring dependency injection services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the application's services and their dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services and their implementations.
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IParser, Parser>();
            services.AddSingleton<IScraperService, ScraperService>();
            services.AddSingleton<ITransactionService, TransactionService>();

            return services;
        }
    }
}
