using DuckDB.NET.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierStorageDecoratorFilterDuckDB : SupplierStorageDecorator, IStorageDuckDB
    {
        public string? Id { get; init; }
        public string? Inn { get; init; }
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public bool? Order { get; init; }
        public bool? Reverse { get; init; }
        DuckDB.NET.Data.DuckDBCommand? command;
        DuckDB.NET.Data.DuckDBConnection connection;


        public SupplierStorageDecoratorFilterDuckDB(ISupplierStorage storage)
        {
            this.storage = storage;

            if (storage is not IStorageDuckDB storageDuckDB)
                throw new ArgumentNullException("Storage must be DuckDB storage");
            connection = storageDuckDB.Connection;
        }

        public DuckDBConnection Connection { get { return connection; } }

        public override Supplier[] GetN(int ind, int count)
        {
            if (command is null)
                InitCommand();

            if (Id is not null)
                command.Parameters.Add(new DuckDBParameter("id", int.Parse(Id)));
            if (Inn is not null)
                command.Parameters.Add(new DuckDBParameter("inn", Inn));
            if (Name is not null)
                command.Parameters.Add(new DuckDBParameter("name", Name));
            if (Address is not null)
                command.Parameters.Add(new DuckDBParameter("address", Address));
            if (Email is not null)
                command.Parameters.Add(new DuckDBParameter("email", Email));
            if (Phone is not null)
                command.Parameters.Add(new DuckDBParameter("phone", Phone));
            command.Parameters.Add(new DuckDBParameter("n", count));
            command.Parameters.Add(new DuckDBParameter("k", ind * count));

            var reader = command.ExecuteReader();

            List<Supplier> suppliers = new List<Supplier>();

            while (reader.Read())
            {
                Supplier supplier = new Supplier(
                        Convert.ToString(reader["id"]),
                        Convert.ToString(reader["inn"]),
                        Convert.ToString(reader["name"]),
                        Convert.ToString(reader["address"]),
                        Convert.ToString(reader["email"]),
                        Convert.ToString(reader["phone"])
                    );

                suppliers.Add(supplier);
            }

            command.Parameters.Clear();

            return suppliers.ToArray();
        }

        private void InitCommand()
        {
            command = ((IStorageDuckDB)storage).Connection.CreateCommand();

            string commandText = """
                SELECT * FROM suppliers
                WHERE 1 = 1
                """;

            if (Id is not null)
                commandText += "AND id = $id\n";
            if (Inn is not null)
                commandText += "AND inn = $inn\n";
            if (Name is not null)
                commandText += "AND name = $name\n";
            if (Address is not null)
                commandText += "AND address = $address\n";
            if (Email is not null)
                commandText += "AND email = $email\n";
            if (Phone is not null)
                commandText += "AND phone = $phone\n";
            if (Order is not null && (bool)Order)
            {
                commandText += "ORDER BY name\n";
                if (Reverse is not null && (bool)Reverse)
                    commandText += "DESC\n";
            } else
            {
                commandText += "ORDER BY id\n";
                if (Reverse is not null && (bool)Reverse)
                    commandText += "DESC\n";
            }
            commandText += "LIMIT $n OFFSET $k";
            command.CommandText = commandText;

        }
    }
}
