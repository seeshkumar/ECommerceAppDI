using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services
{
    class ShopService : IShopService
    {
        public string AddProduct(List<Product> products, List<CartProduct> cartProducts,IdQuantityPair idQuantityPair)
        {

            Product selectedProduct = products.FirstOrDefault(product => product.pId == idQuantityPair.id);

            //check if product exists in menu
            if (selectedProduct == null)
            {
                return "Unable to add product\n\n";
            }
            //check if selected product already exists in cart
            CartProduct productInCart = cartProducts.FirstOrDefault(cp => cp.id == idQuantityPair.id);
            int qunatityAlreadyInCart = productInCart == null ? 0 : productInCart.quantity;

            //check if selected quantity is available
            if (idQuantityPair.quantity > selectedProduct.quantityAvailable - qunatityAlreadyInCart)
            {
                return $"Selected quantity({idQuantityPair.quantity}) exceeds available quantity({selectedProduct.quantityAvailable - qunatityAlreadyInCart})\n\n";
            }
            //add product to cart
            //check if product already in cart
            if (productInCart == null)
            {
                cartProducts.Add(new CartProduct(selectedProduct, idQuantityPair.quantity));
                return $"Added {idQuantityPair.quantity} units of {selectedProduct.name} to cart";
            }
            else
            {
                productInCart.quantity += idQuantityPair.quantity;
                return $"{productInCart.quantity} units of {selectedProduct.name} in cart";
            }


        }
    
        public string DeleteProduct(List<CartProduct> cartProducts, IdQuantityPair idQuantityPair)
        {
            CartProduct deleteProduct = cartProducts.SingleOrDefault(cartProduct => cartProduct.id == idQuantityPair.id);
            if (DeleteProduct == null)
            {
                return "Invalid Input";
            }
            if (idQuantityPair.quantity < 0) //negative qunatity is invalid
            {
                return "Invalid input"; 
            }
            if (idQuantityPair.quantity > deleteProduct.quantity)
            {
                return $"Quantity to delete({idQuantityPair.quantity}) is greater than quantity in cart({deleteProduct.quantity})";
            }
            deleteProduct.quantity -= idQuantityPair.quantity;
            if (deleteProduct.quantity == 0)
            {
                cartProducts.Remove(deleteProduct);
                return $"Removed {deleteProduct.brand} {deleteProduct.name} from cart";
            }

            return $"Removed {idQuantityPair.quantity} units of {deleteProduct.brand} {deleteProduct.name} from cart";
        }
    }
}
