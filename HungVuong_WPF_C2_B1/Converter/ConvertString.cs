using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class ConvertString
    {
        // converted: 18-4-2005
        public static string ConvertDateToStringOne(DateTime date)
        {
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            return $"{day}-{month}-{year}";
        }

        // converted: 18/4/2005
        public static string ConvertDateToStringTwo(DateTime date)
        {
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            return $"{day}/{month}/{year}";
        }

        // Converted: 08:20
        public static string ConvertHourAndMinuteToStringOne(int hour, int minute)
        {
            string hours = hour.ToString();
            string minutes = minute.ToString();

            if (hours.Length == 1)
                hours = "0" + hours;
            if (minutes.Length == 1)
                minutes = "0" + minutes;

            return $"{hours}:{minutes}";
        }

        // Converted: 08-20
        public static string ConvertHourAndMinuteToStringTwo(int hour, int minute)
        {
            string hours = hour.ToString();
            string minutes = minute.ToString();

            if (hours.Length == 1)
                hours = "0" + hours;
            if (minutes.Length == 1)
                minutes = "0" + minutes;

            return $"{hours}-{minutes}";
        }
    }
}
