using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class CinemaVip : Cinema
    {
        public double surchargeValue { get; set; }

        public CinemaVip(string Id, string name, string type, double priceCenter, double percentOff, double reductionAmount, double surchargeValue) : base(Id, name, type, priceCenter, percentOff, reductionAmount)
        {
            this.surchargeValue = surchargeValue;
        }
    }
}
