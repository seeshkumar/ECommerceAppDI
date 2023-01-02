using ECommerceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Injectors
{
    class Injector
    {
        private IFileService fileService;
        private IUserService userService;
        private IShopService shopService;

        public Injector(IFileService fileService, IUserService userService, IShopService shopService)
        {
            this.fileService = fileService;
            this.userService = userService;
            this.shopService = shopService;
        }

        //fileService methods
        public dynamic ReadJsonFile(string filename)
        {
            return fileService.ReadJsonFile(filename);
        }
        public bool SaveJson(string filename, dynamic objs)
        {
            return fileService.SaveJson(filename, objs);
        }


        //userService methods
        public bool ValidDetails(List<User> users, User user)
        {
            return userService.ValidDetails(users, user); 
        }

        //shopService methods
        public string AddProduct(List<Product> products, List<CartProduct> cartProducts, IdQuantityPair idQuantityPair)
        {
            return shopService.AddProduct(products, cartProducts, idQuantityPair);
        }

        public string DeleteProduct(List<CartProduct> cartProducts, IdQuantityPair idQuantityPair)
        {
            return shopService.DeleteProduct(cartProducts, idQuantityPair);
        }
    }
}
