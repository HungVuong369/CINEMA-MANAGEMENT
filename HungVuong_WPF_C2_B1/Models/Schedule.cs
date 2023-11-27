using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class Schedule
    {
        public DateTime ReleaseDate { get; set; }
        public List<TimeSpan> ShowtimeList { get; set; }
        public string CinemaType { get; set; }

        public Schedule(DateTime releaseDate, List<TimeSpan> showTimeList, string cinemaType)
        {
            this.ReleaseDate = releaseDate;
            this.ShowtimeList = showTimeList;
            this.CinemaType = cinemaType;
        }
    }
}
