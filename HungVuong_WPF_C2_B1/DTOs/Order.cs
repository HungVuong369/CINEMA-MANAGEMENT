using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class Order
    {
        public CustomerInfo customerInfo { get; set; }
        public string CinemaType { get; set; }
        public string Date { get; set; }
        public string Showtime { get; set; }
        public double TotalPrice { get; set; }
        public int TotalSeat { get; set; }

        public Order(CustomerInfo customerInfo, string cinemaType, string date, string showtime, double totalPrice, int totalSeat)
        {
            this.customerInfo = customerInfo;
            this.CinemaType = cinemaType;
            this.Date = date;
            this.Showtime = showtime;
            this.TotalPrice = totalPrice;
            this.TotalSeat = totalSeat;
        }
    }
}
