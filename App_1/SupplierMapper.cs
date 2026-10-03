using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierMapper
    {
        public static ShortSupplier ToShortSupplier(Supplier supplier)
        {
            ShortSupplier shortSupplier;

            string shortName;
            string[] splittedName = supplier.Name.Split(' ');
            shortName = splittedName[0];

            for (int i = 1; i < splittedName.Length; ++i)
                shortName += " " + splittedName[i].Substring(0, 1) + ".";

            shortSupplier = new ShortSupplier(supplier.Id, supplier.Inn, shortName);

            return shortSupplier;
        }

        public static ShortSupplier[] ToShortSuppliers(Supplier[] suppliers)
        {
            ShortSupplier[] shortSuppliers = new ShortSupplier[suppliers.Length];

            for (int i = 0; i < suppliers.Length; ++i)
            {
                shortSuppliers[i] = ToShortSupplier(suppliers[i]);
            }

            return shortSuppliers;
        }
    }
}
