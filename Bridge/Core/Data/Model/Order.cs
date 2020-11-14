using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppZseroEF6.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String BuyerId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public int CurrentStatus { get; set; }
        [ForeignKey("BuyerId")]
       
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        
    }
}
