using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1 {
    public class Account {
        public string username { get; set; }
        public string password { get; set; }
        public int priority { get; set; }

        public Account(string username, string password, int priority)
        {
            this.username = username;
            this.password = password;
            this.priority = priority;
        }
    }
}
