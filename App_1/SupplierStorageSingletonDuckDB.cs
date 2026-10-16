using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierStorageSingletonDuckDB : ISupplierStorageSingleton
    {
        private static Dictionary<string, ISupplierStorage> Storages = new Dictionary<string, ISupplierStorage>();

        public static ISupplierStorage GetStorage(string filename)
        {
            if (Storages.ContainsKey(filename))
                return Storages[filename];

            Storages[filename] = new SupplierStorageDuckDB(filename);
            return Storages[filename];
        }
    }
}
