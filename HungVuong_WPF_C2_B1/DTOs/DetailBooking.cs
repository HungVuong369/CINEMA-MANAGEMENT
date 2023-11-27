using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HungVuong_WPF_C2_B1
{
    public class DetailBooking
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CinemaName { get; set; }
        public string Date { get; set; }
        public string Showtime { get; set; }
        public List<double> LstPrice { get; set; }
        public List<string> Types { get; set; }
        public List<Button> SelectedButton { get; set; }

        public DetailBooking(string customerName, string customerPhone, string cinemaType, string date, string showtime, List<double> lstPrice, List<string> types, List<Button> selectedButton)
        {
            this.CustomerName = customerName;
            this.CustomerPhone = customerPhone;
            this.CinemaName = cinemaType;
            this.Date = date;
            this.Showtime = showtime;
            this.LstPrice = lstPrice;
            this.Types = types;
            this.SelectedButton = selectedButton;
        }
    }
}
