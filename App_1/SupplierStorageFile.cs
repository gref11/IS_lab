using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App_1
{
    internal abstract class SupplierStorageFile : ISupplierStorage
    {
        protected string filename;
        protected HashSet<Supplier> suppliers;
        protected Dictionary<string, Supplier> suppliersIdDict;

        public string Filename
        {
            get { return filename; }
        }

        public abstract void Save();

        public abstract void Load();

        public string Add(Supplier supplier)
        {
            if (IsInnExists(supplier.Inn))
                throw new ArgumentException("Supplier with given INN already exists");

            Supplier newSupplier = new Supplier(supplier, GetNextId());
            suppliers.Add(newSupplier);
            suppliersIdDict.Add(newSupplier.Id, newSupplier);

            return newSupplier.Id;
        }

        public Supplier[] GetAll()
        {
            return suppliers.ToArray();
        }

        public Supplier GetById(string id)
        {
            if (!suppliersIdDict.ContainsKey(id))
                throw new ArgumentException("Supplier with given id does not exist");

            return suppliersIdDict[id];
        }

        public Supplier[] GetN(int ind, int count)
        {
            if (ind < 0 || ind + count > Count())
                throw new ArgumentException("Index out of range");

            return suppliers.Skip(ind * count).Take(count).ToArray();
        }

        public void Update(string id, Supplier supplier)
        {
            if (!suppliersIdDict.ContainsKey(id))
                throw new ArgumentException("Supplier with given id does not exist");

            Supplier oldSupplier = suppliersIdDict[id];
            Supplier newSupplier = new Supplier(supplier, id);
            suppliers.Remove(oldSupplier);


            if (IsInnExists(supplier.Inn))
            {
                suppliers.Add(oldSupplier);
                throw new ArgumentException("Supplier with given INN already exists");
            }


            suppliers.Add(newSupplier);
            suppliersIdDict[id] = newSupplier;
        }

        public void Delete(string id)
        {
            if (!suppliersIdDict.ContainsKey(id))
                throw new ArgumentException("Supplier with given id does not exist");

            Supplier oldSupplier = suppliersIdDict[id];
            suppliers.Remove(oldSupplier);
            suppliersIdDict.Remove(id);
        }

        public int Count()
        {
            return suppliers.Count;
        }

        // вспомогательные

        protected string GetNextId()
        {
            return GetStringId(GetMaxId() + 1);
        }

        protected int GetMaxId()
        {
            int maxId = 0;

            foreach (Supplier supplier in suppliers)
            {
                if (maxId < int.Parse(supplier.Id))
                    maxId = int.Parse(supplier.Id);
            }

            return maxId;
        }

        protected static string GetStringId(int id)
        {
            return id.ToString("D7");
        }

        protected void BuildIdDict()
        {
            suppliersIdDict.Clear();

            foreach (Supplier supplier in suppliers)
            {
                suppliersIdDict[supplier.Id] = supplier;
            }
        }

        protected bool IsInnExists(string inn)
        {
            foreach (Supplier supplier in suppliers)
            {
                if (inn.Equals(supplier.Inn))
                    return true;
            }

            return false;
        }
    }
}
