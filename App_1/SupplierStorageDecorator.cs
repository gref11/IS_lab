using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal abstract class SupplierStorageDecorator : ISupplierStorage
    {
        protected ISupplierStorage storage;

        public virtual string Add(Supplier supplier)
        {
            return storage.Add(supplier);
        }

        public virtual int Count()
        {
            return storage.Count();
        }

        public virtual void Delete(string id)
        {
            storage.Delete(id);
        }

        public virtual Supplier[] GetAll()
        {
            return storage.GetAll();
        }

        public virtual Supplier GetById(string id)
        {
            return storage.GetById(id);
        }

        public virtual Supplier[] GetN(int ind, int count)
        {
            return storage.GetN(ind, count);
        }

        public virtual void Load()
        {
            storage.Load();
        }

        public virtual void Save()
        {
            storage.Save();
        }

        public virtual void Update(string id, Supplier supplier)
        {
            storage.Update(id, supplier);
        }
    }
}
