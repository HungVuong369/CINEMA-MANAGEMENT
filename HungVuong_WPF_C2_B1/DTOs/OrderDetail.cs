using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class OrderDetail
    {
        public int SeatNumber { get; set; }
        public double Price { get; set; }
        public string CustomerType { get; set; }

        public OrderDetail(int seatNumber, double price, string customerType)
        {
            this.SeatNumber = seatNumber;
            this.Price = price;
            this.CustomerType = customerType;
        }
    }
}
