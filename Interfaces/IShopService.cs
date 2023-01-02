using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    interface IShopService
    {
        public string AddProduct(List<Product> products, List<CartProduct> cartProducts, IdQuantityPair idQuantityPair);

        public string DeleteProduct(List<CartProduct> cartProducts, IdQuantityPair idQuantityPair);
    }
}
