using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppZseroEF6.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            OrderDetailVMs = new List<OrderDetailVM>();
            StatusVMs = new List<OrderStatusVM>();
        }
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double TotalAmount { get; set; }
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public long CurrentStatus { get; set; }
        public List<OrderDetailVM> OrderDetailVMs { get; set; }
        public List<OrderStatusVM> StatusVMs { get; set; }

    }

    public class OrderCM
    {
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public List<OrderDetailVM> OrderDetailCMs { get; set; }

    }
    public class OrderDetailVM
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public String ProductName { get; set; }
        public String Size { get; set; }
        public String Smell { get; set; }
        public int Quantity { get; set; }
        public String Image { get; set; }
    }

    public class OrderStatusVM
    {
        public DateTime DateCreated { get; set; }
        public String Name { get; set; }
        public int Priority { get; set; }
    }

    public class OrderStatusCM
    {
        public long OrderId { get; set; }
        public long StatusId { get; set; }
    }
   
}
