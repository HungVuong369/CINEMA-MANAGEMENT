﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1 {
    public class CustomerInfo {
        public string Name { get; set; }
        public string Phone { get; set; }

        public CustomerInfo(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;
        }
    }
}
