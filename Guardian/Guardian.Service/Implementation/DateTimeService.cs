using Guardian.Service.Contract;
using System;

namespace Guardian.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}