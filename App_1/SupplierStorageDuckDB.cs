using DuckDB.NET.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal class SupplierStorageDuckDB : ISupplierStorage, IStorageDuckDB
    {
        DuckDB.NET.Data.DuckDBConnection connection;
        DuckDB.NET.Data.DuckDBCommand commandAdd;
        DuckDB.NET.Data.DuckDBCommand commandCount;
        DuckDB.NET.Data.DuckDBCommand commandDelete;
        DuckDB.NET.Data.DuckDBCommand commandGetAll;
        DuckDB.NET.Data.DuckDBCommand commandGetById;
        DuckDB.NET.Data.DuckDBCommand commandGetN;
        DuckDB.NET.Data.DuckDBCommand commandUpdate;
        public SupplierStorageDuckDB(string fileName)
        {
            connection = new DuckDBConnection(String.Format("Data Source={0}", fileName));
            connection.Open();

            commandAdd = connection.CreateCommand();
            commandAdd.CommandText = """
                INSERT INTO suppliers (inn, name, address, email, phone)
                VALUES ($inn, $name, $address, $email, $phone)
                RETURNING id
                """;

            commandCount = connection.CreateCommand();
            commandCount.CommandText = """
                SELECT count(1)
                FROM suppliers
                """;

            commandDelete = connection.CreateCommand();
            commandDelete.CommandText = """
                DELETE FROM suppliers
                WHERE id = $id
                """;

            commandGetAll = connection.CreateCommand();
            commandGetAll.CommandText = """
                SELECT * FROM suppliers
                """;

            commandGetById = connection.CreateCommand();
            commandGetById.CommandText = """
                SELECT * 
                FROM SUPPLIERS
                WHERE id = $id
                """;

            commandGetN = connection.CreateCommand();
            commandGetN.CommandText = """
                SELECT * FROM suppliers
                ORDER BY id
                LIMIT $n OFFSET $k
                """;

            commandUpdate = connection.CreateCommand();
            commandUpdate.CommandText = """
                UPDATE suppliers
                SET inn = $inn,
                    name = $name,
                    address = $address,
                    email = $email,
                    phone = $phone
                WHERE id = $id
                """;
        }

        public DuckDBConnection Connection { get { return connection; } }

        public string Add(Supplier supplier)
        {
            commandAdd.Parameters.Add(new DuckDBParameter("inn", supplier.Inn));
            commandAdd.Parameters.Add(new DuckDBParameter("name", supplier.Name));
            commandAdd.Parameters.Add(new DuckDBParameter("address", supplier.Address));
            commandAdd.Parameters.Add(new DuckDBParameter("email", supplier.Email));
            commandAdd.Parameters.Add(new DuckDBParameter("phone", supplier.Phone));

            string newId = Convert.ToString(commandAdd.ExecuteScalar());

            commandAdd.Parameters.Clear();

            return newId;
        }

        public int Count()
        {
            int count = Convert.ToInt32(commandCount.ExecuteScalar());

            return count;
        }

        public void Delete(string id)
        {
            commandDelete.Parameters.Add(new DuckDBParameter("id", int.Parse(id)));

            commandDelete.ExecuteNonQuery();

            commandDelete.Parameters.Clear();
        }

        public Supplier[] GetAll()
        {
            var reader = commandGetAll.ExecuteReader();

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

            return suppliers.ToArray();
        }

        public Supplier GetById(string id)
        {
            commandGetById.Parameters.Add(new DuckDBParameter("id", int.Parse(id)));

            var reader = commandGetById.ExecuteReader();

            if (!reader.Read())
                throw new ArgumentException("Supplier with given id does not exist");

            var supplier = new Supplier(
                    Convert.ToString(reader["id"]),
                    Convert.ToString(reader["inn"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToString(reader["address"]),
                    Convert.ToString(reader["email"]),
                    Convert.ToString(reader["phone"])
                );

            commandGetById.Parameters.Clear();

            return supplier;
        }

        public Supplier[] GetN(int ind, int count)
        {
            commandGetN.Parameters.Add(new DuckDBParameter("n", count));
            commandGetN.Parameters.Add(new DuckDBParameter("k", ind * count));

            var reader = commandGetN.ExecuteReader();

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

            commandGetN.Parameters.Clear();

            return suppliers.ToArray();
        }

        public void Load()
        {
        }

        public void Save()
        {
        }

        public void Update(string id, Supplier supplier)
        {
            commandUpdate.Parameters.Add(new DuckDBParameter("id", int.Parse(id)));
            commandUpdate.Parameters.Add(new DuckDBParameter("inn", supplier.Inn));
            commandUpdate.Parameters.Add(new DuckDBParameter("name", supplier.Name));
            commandUpdate.Parameters.Add(new DuckDBParameter("address", supplier.Address));
            commandUpdate.Parameters.Add(new DuckDBParameter("email", supplier.Email));
            commandUpdate.Parameters.Add(new DuckDBParameter("phone", supplier.Phone));

            commandUpdate.ExecuteNonQuery();

            commandUpdate.Parameters.Clear();
        }
    }
}
