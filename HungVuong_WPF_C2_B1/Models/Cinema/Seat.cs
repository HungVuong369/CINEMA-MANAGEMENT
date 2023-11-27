using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1 {
    public class Seat {
        public bool Status { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public Seat(string Id, string name, bool status) {
            this.Status = status;
            this.Id = Id;
            this.Name = name;
        }
    }
}
