using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierStorageJson : SupplierStorageFile
    {

        public SupplierStorageJson(string filename)
        {
            this.filename = filename;
            suppliers = new HashSet<Supplier>();
            suppliersIdDict = new Dictionary<string, Supplier>();
        }

        public override void Save()
        {
            string json = JsonSerializer.Serialize(suppliers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filename, json);
        }

        public override void Load()
        {
            string json = File.ReadAllText(filename);

            suppliers = JsonSerializer.Deserialize<HashSet<Supplier>>(json) ?? new HashSet<Supplier>();
            BuildIdDict();
        }
    }
}
