using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using ECommerceApp.Models;
using ECommerceApp.Interfaces;

namespace ECommerceApp.Services
{
    class FileService : IFileService
    {
        public dynamic? ReadJsonFile(string filename)
        {
            string fullFilename = "./assets/" + filename;

            string json = File.ReadAllText(fullFilename);
            if (filename == "Users.json")
                return JsonConvert.DeserializeObject<List<User>>(json);
            else if (filename == "Products.json")
                return JsonConvert.DeserializeObject<List<Product>>(json);
            else return null;
        }
        public bool SaveJson(string filename, dynamic objs)
        {

            try
            {
                filename = "./assets/" + filename;
                File.WriteAllText(filename, JsonConvert.SerializeObject(objs, Newtonsoft.Json.Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
