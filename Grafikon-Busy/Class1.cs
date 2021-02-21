using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafikon_Busy
{
    static class TimeConverter
    {
        /// <summary>
        /// Converts string in format hh:mm to minutes
        /// </summary>
        /// <param name="hoursMins">String in format number:number. Each part (separated by :) might contain leading/trailing spaces and leading zeros</param>
        /// <param name="totalMinutes">If the input is in valid format, returns the hours and minutes converted to minutes only.</param>
        /// <returns>True if conversion was valid</returns>
        public static bool HoursMinsToMins(string hoursMins, out int totalMinutes)
        {
            int hours;
            int minutes;
            totalMinutes = 0;

            string[] timeString = hoursMins.Split(':');
            if (!(
                int.TryParse(timeString[0], out hours)
                && int.TryParse(timeString[1], out minutes)
                ))
                return false;
            if (!(
                0 <= hours && hours < 24 && 0 <= minutes && minutes < 60)
                )
                return false;

            totalMinutes = 60 * hours + minutes;
            return true;
        }
    }
}
