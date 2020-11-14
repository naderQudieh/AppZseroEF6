using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppZseroEF6.ViewModels
{
    public class UserAddressVM
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
    }

    public class UserAddressCM
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
    }
}
