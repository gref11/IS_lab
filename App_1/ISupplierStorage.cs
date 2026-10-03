using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal interface ISupplierStorage // CRUD supplier storage
    {
        public void Load();
        public void Save();

        public string Add(Supplier supplier);
        public Supplier[] GetAll();
        public Supplier GetById(string id);
        public Supplier[] GetN(int ind, int count);
        public void Update(string id, Supplier supplier);
        public void Delete(string id);

        //public void SortByName(bool reverse=false);
        public int Count();
    }
}
