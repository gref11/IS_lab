using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierStorageDecoratorFilterFile : SupplierStorageDecorator
    {
        public string? Id { get; init; }
        public string? Inn { get; init; }
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public bool? Order { get; init; }
        public bool? Reverse { get; init; }

        public SupplierStorageDecoratorFilterFile(ISupplierStorage storage)
        {
            this.storage = storage;
        }

        public override Supplier[] GetN(int ind, int count)
        {
            List<Supplier> resSuppliers = new List<Supplier>();

            //for (int i = 0; count * i < storage.Count() && resSuppliers.Count < (ind + 1) * count; ++i)
            //foreach (Supplier supplier in storage.GetN(i, count))
            foreach (Supplier supplier in storage.GetAll())
                if (CheckSupplier(supplier))
                    resSuppliers.Add(supplier);

            if (Order is not null && (bool)Order)
                resSuppliers.Sort((a, b) => a.Name.CompareTo(b.Name));
            if (Reverse is not null && (bool)Reverse)
                resSuppliers.Reverse();


            return resSuppliers.Skip(ind * count).Take(count).ToArray();
        }

        private bool CheckSupplier(Supplier supplier)
        {
            if (Id is not null)
                if (supplier.Id != Id)
                    return false;
            if (Inn is not null)
                if (supplier.Inn != Inn)
                    return false;
            if (Name is not null)
                if (supplier.Name != Name)
                    return false;
            if (Address is not null)
                if (supplier.Address != Address)
                    return false;
            if (Email is not null)
                if (supplier.Email != Email)
                    return false;
            if (Phone is not null)
                if (supplier.Phone != Phone)
                    return false;

            return true;
        }
    }
}
