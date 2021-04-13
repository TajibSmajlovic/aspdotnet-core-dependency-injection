using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class CourtServiceCollectionExtensions
    {
        public static IServiceCollection AddCourtServices(this IServiceCollection services)
        {
            services.TryAddScoped<ICourtMaintenanceService, CourtMaintenanceService>();
            
            return services;
        }
    }
}
