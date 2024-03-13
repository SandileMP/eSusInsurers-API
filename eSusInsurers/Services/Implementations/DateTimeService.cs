using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
