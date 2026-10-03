using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace App_1
{
    internal class SupplierStorageYaml : SupplierStorageFile
    {
        public SupplierStorageYaml(string filename)
        {
            this.filename = filename;
            suppliers = new HashSet<Supplier>();
            suppliersIdDict = new Dictionary<string, Supplier>();
        }

        public override void Save()
        {
            ISerializer serializer = new SerializerBuilder().Build();

            string yaml = serializer.Serialize(suppliers);

            File.WriteAllText(filename, yaml);
        }

        public override void Load()
        {
            string yaml = File.ReadAllText(filename);

            IDeserializer serializer = new DeserializerBuilder().Build();

            suppliers = serializer.Deserialize<HashSet<Supplier>>(yaml);

            BuildIdDict();
        }
    }
}
