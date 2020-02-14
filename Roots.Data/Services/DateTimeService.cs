using Roots.Business.Interfaces;
using System;

namespace Roots.Data.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
