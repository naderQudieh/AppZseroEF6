using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bridge.Model
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public String Size { get; set; }
        public String Smell { get; set; }
        public int Quantity { get; set; }
        public String Comment { get; set; }
        public float?  Star { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
