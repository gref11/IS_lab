using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal interface ISupplierStorageSingleton
    {
        public static abstract ISupplierStorage GetStorage(string filename);
    }
}
