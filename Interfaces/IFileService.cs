using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    interface IFileService
    {
        public dynamic ReadJsonFile(string filename);
        public bool SaveJson(string filename, dynamic objs);
    }
}
