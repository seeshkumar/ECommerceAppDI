using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    class CartProduct
    {
        public int id { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public CartProduct(Product product, int quantity)
        {
            id = product.pId;
            brand = product.brand;
            name = product.name;
            price = product.price;
            this.quantity = quantity;
        }
    }
}
