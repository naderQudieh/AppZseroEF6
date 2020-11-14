using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppZseroEF6.ViewModels
{
    public class CategoryVM
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Logo { get; set; }
    }
    public class CategoryCM
    {
        public String Name { get; set; }
    }
}
