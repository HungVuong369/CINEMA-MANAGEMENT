using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1 {
    public class AccountList
    {
        public List<Account> lstAccount { get; set; }

        public AccountList() {
            lstAccount = SeedData.getListAccount(true);
        }
    }
}
