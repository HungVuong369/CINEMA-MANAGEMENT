using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class Cinema
    {
        public double priceCenter { get; set; }
        public double percentOff { get; set; }
        public double reductionAmount { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Seat[][] seats { get; set; }

        public Cinema(string Id, string name, string type, double priceCenter, double percentOff, double reductionAmount)
        {
            this.Id = Id;
            this.Name = name;
            this.Type = type;

            this.priceCenter = priceCenter;
            this.percentOff = percentOff;
            this.reductionAmount = reductionAmount;

            this.seats = SeedData.getListSeatByCinemaId(this.Id);
        }

        public Cinema() { }
    }
}
