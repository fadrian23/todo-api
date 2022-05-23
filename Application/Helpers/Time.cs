using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public enum TimePeriod
    {
        Today,
        Tomorrow,
        CurrentWeek,
    }

    public static class TimeHelper
    {
        public static Dictionary<TimePeriod, DateTimeOffset> LastTicksOfTimePeriod = new Dictionary<
            TimePeriod,
            DateTimeOffset
        >
        {
            { TimePeriod.Today, DateTime.Today.AddDays(1).AddTicks(-1) },
            { TimePeriod.Tomorrow, DateTime.Today.AddDays(2).AddTicks(-1) },
            {
                TimePeriod.CurrentWeek,
                DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek).AddDays(1).AddTicks(-1)
            }
        };
    }
}
