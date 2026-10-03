using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App_1
{
    internal class ShortSupplier
    {
        string id;
        string inn;
        string name;

        public ShortSupplier(string id, string inn, string name)
        {
            Id = id;
            Inn = inn;
            Name = name;
        }

        string Id
        {
            get { return id; }
            set
            {
                if (ValidateId(value))
                    id = value;
                else
                    throw GetInvalidFieldException("id");
            }
        }

        string Inn
        {
            get { return inn; }
            set
            {
                if (ValidateInn(value))
                    inn = value;
                else
                    throw GetInvalidFieldException("inn");
            }
        }

        string Name
        {
            get { return name; }
            set
            {
                if (ValidateName(value))
                    name = value;
                else
                    throw GetInvalidFieldException("name");
            }
        }

        static Exception GetInvalidFieldException(string fieldName)
        {
            return new ArgumentException("Invalid supplier " + fieldName);
        }

        static bool ValidateId(string id)
        {
            string pattern = @"^[0-9]{6,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(id))
                return true;

            return false;
        }

        static bool ValidateInn(string inn)
        {
            string pattern = @"^[0-9]{10,12}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(inn))
                return true;

            return false;
        }

        static bool ValidateName(string name)
        {
            string pattern = @"^[a-zA-Z\ а-яА-Я.]{3,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(name))
                return true;

            return false;
        }

        public override string ToString()
        {
            return String.Format("Supplier object -- id: {0}; inn: {1}; name: {2}\n", id, inn, name);
        }

        public bool Equals(object? obj)
        {
            if (obj is not ShortSupplier sup)
                return false;

            if (inn != sup.inn)
                return false;
            return true;
        }
    }
}
