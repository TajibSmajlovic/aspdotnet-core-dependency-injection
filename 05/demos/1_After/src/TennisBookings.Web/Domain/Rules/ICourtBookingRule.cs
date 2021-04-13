using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public interface ICourtBookingRule
    {
        Task<bool> CompliesWithRuleAsync(CourtBooking booking);

        string ErrorMessage { get; }
    }
}
