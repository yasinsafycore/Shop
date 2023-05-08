using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Extensions
{
    public static class DataExtensions
    {
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return $"{dateTime.Month.ToString("00")} / {dateTime.Day.ToString("00")} / {dateTime.Year.ToString("0000")}";
        }
    }
}
