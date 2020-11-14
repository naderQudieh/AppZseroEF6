using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppZseroEF6.ViewModels
{
    public class ProductVM
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public String BannerPath { get; set; }
        public double Star { get; set; }
        public long CategoryId { get; set; }
        public long GenderId { get; set; }
    }

    class SmellVM
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int O { get; set; }
    }

    public class ProductCM
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public List<long> SmellIds { get; set; }
        public long GenderId { get; set; }
        public long CategoryId { get; set; }
        public DateTime DateSale { get; set; }
    }

    public class ProductUM : ProductCM
    {
        public long Id { get; set; }
    }

    public class ProductRatingCM
    {
        public long OrderDetailId { get; set; }
        public float  Rate { get; set; }
        public String Comment { get; set; }
    }

    public class ProductRatingVM
    {
        public String FullName { get; set; }
        public float Rate { get; set; }
        public String Comment { get; set; }
    }
}
