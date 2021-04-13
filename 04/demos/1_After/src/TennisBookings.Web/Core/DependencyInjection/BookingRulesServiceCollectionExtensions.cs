using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Domain.Rules;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class BookingRulesServiceCollectionExtensions
    {
        public static IServiceCollection AddBookingRules(this IServiceCollection services)
        {
            services.TryAddEnumerable(new []
            {
                ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>(),
                ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>(),
                ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>(),
                ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>(),
                ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>(),
            });
            
            services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            return services;
        }
    }
}
