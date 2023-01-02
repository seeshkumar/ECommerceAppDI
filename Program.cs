
using ECommerceApp.Injectors;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using ECommerceApp.Services;
using System.Collections.Generic;

class Program
{
    public static void Main(String[] args)
    {

        UserService userService = new UserService();
        FileService fileService = new FileService();
        ShopService shopService = new ShopService();

        Injector injector = new Injector(fileService, userService, shopService);

        User user = new User();
        
        Console.Write("Enter Username : ");
        user.username = Console.ReadLine();

        Console.Write("Enter Password : ");
        user.password = Console.ReadLine();


        List<User> users = injector.ReadJsonFile("Users.json");

        bool login = injector.ValidDetails(users, user);//return true if valid

        if (!login)
        {
            Console.WriteLine("Unable to login");
            return;
        }

        Console.WriteLine($"Loged in as {user.username}");

        List<Product> products = injector.ReadJsonFile("Products.json");

        List<CartProduct> cartProducts = new List<CartProduct>();




        AddProductsLoop(products, cartProducts, injector, user);

    }


    public static IdQuantityPair PrintMenu(List<Product> products, List<CartProduct> cartProducts)
    {

        IdQuantityPair idQuantityPair = new IdQuantityPair();

        Console.WriteLine("Id\tBrand\tName\t\tPrice\tQuantity Available");
        Console.WriteLine("-------------------------------------------------------");
        for(int i=0;i<products.Count();i++)
        {
            CartProduct productInCart = cartProducts.FirstOrDefault(cp => cp.id == products[i].pId);
            int qunatityAlreadyInCart = productInCart == null ? 0 : productInCart.quantity;

            Console.WriteLine($"{i + 1}\t{products[i].brand}\t{products[i].name}\t{products[i].price}\t\t{products[i].quantityAvailable - qunatityAlreadyInCart}");
        }
        Console.WriteLine("Enter -1 for checkout.\n");

        Console.Write("\nEnter Product Id, -1 for checkout :");
        int uId = Convert.ToInt32(Console.ReadLine());
        if (uId == -1)
        {
            idQuantityPair.id = -1;
            return idQuantityPair;
        }

        idQuantityPair.id = products[uId-1].pId;
        //-1 for checkout

        Console.Write("Enter Quantity :");
        idQuantityPair.quantity = Convert.ToInt32(Console.ReadLine());

        return idQuantityPair;
    }

    public static int PrintCartProducts(List<CartProduct> cartProducts)
    {
        Console.WriteLine("Id\tBrand\tName\t\tPrice\tQuantity");
        Console.WriteLine("--------------------------------------------");
        int sum = 0;
        for(int i =0;i<cartProducts.Count();i++)
        {
            Console.WriteLine($"{i + 1}\t{cartProducts[i].brand}\t{cartProducts[i].name}\t{cartProducts[i].price}\t{cartProducts[i].quantity}");
            sum += cartProducts[i].price * cartProducts[i].quantity;
        }
        Console.WriteLine("----------------------------------------------");

        return sum;

    }

    public static void PrintBill(List<CartProduct> cartProducts)
    {
        Console.WriteLine("\n\n------------------BILL---------------------------");
        int total = PrintCartProducts(cartProducts);
        Console.WriteLine($"Total : {total}\n");
    }

    static void AddProductsLoop(List<Product> products, List<CartProduct> cartProducts, Injector injector, User user)
    {
        IdQuantityPair idQuantityPair = new IdQuantityPair();

        while (idQuantityPair.id != -1)
        {
            idQuantityPair = PrintMenu(products, cartProducts);
            if (idQuantityPair.id == -1) break;

            string resultMsg = injector.AddProduct(products, cartProducts, idQuantityPair);

            Console.WriteLine("\n"+resultMsg+"\n\n");
        }

        CheckOutPage(products, cartProducts, injector, user);

    }


    static void DeleteProductsLoop(List<Product> products, List<CartProduct> cartProducts, Injector injector, User user)
    {
        IdQuantityPair deleteIdQuantityPair = new IdQuantityPair();
        deleteIdQuantityPair.id = 0;
        deleteIdQuantityPair.quantity = 0;

        while (deleteIdQuantityPair.id != -1)
        {
            Console.WriteLine("\n\n\"---------------------PRODUCTS IN CART--------------------\"");
            PrintCartProducts(cartProducts);
            Console.Write("Select Id to delete,-1 to checkout :");
            int uId = Convert.ToInt32(Console.ReadLine());
            if (uId == -1) 
            {
                Console.WriteLine("\n\n");
                break; 
            }
            deleteIdQuantityPair.id = cartProducts[uId - 1].id;
            

            //check if Id exitst in cart
            Console.Write("How many units to delete:");
            deleteIdQuantityPair.quantity = Convert.ToInt32(Console.ReadLine());

            injector.DeleteProduct(cartProducts, deleteIdQuantityPair);
        }

        CheckOutPage(products, cartProducts, injector, user);
    }

    static void CheckOutPage(List<Product> products, List<CartProduct> cartProducts, Injector injector, User user)
    {
        PrintBill(cartProducts);
        Console.Write("1.Add products, 2.Delete products, 3.exit : ");
        int op = Convert.ToInt32(Console.ReadLine());
        switch (op)
        {
            case 1:
                AddProductsLoop(products, cartProducts, injector, user);
                break;
            case 2:
                DeleteProductsLoop(products, cartProducts, injector, user);
                //cartProducts = deleteProducts(CartProducts, displayService);
                //goto Checkout;
                break;
            case 3:
                SaveData(products, cartProducts, user, injector);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }

    }

    static void SaveData(List<Product> products, List<CartProduct> cartProducts, User user, Injector injector)
    {
        foreach (var cartProduct in cartProducts)
        {
            products.Find(product => product.pId == cartProduct.id).quantityAvailable -= cartProduct.quantity;
        }

        bool saved = injector.SaveJson("products.json", products);
        if (saved)
        {
            Console.WriteLine("Transaction complete.\n\n");
        }
    }



}