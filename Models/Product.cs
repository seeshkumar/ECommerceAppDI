using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    class Product
    {
        public int pId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantityAvailable { get; set; }
        public string brand { get; set; }
    }
}
