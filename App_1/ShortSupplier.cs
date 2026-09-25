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
        protected string id;
        protected string name;

        public ShortSupplier(string id, string name)
        {
            Id = id;
            Name = name;
        }

        protected string Id
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

        protected string Name
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

        protected static Exception GetInvalidFieldException(string fieldName)
        {
            return new ArgumentException("Invalid supplier " + fieldName);
        }

        protected static bool ValidateId(string id)
        {
            string pattern = @"^[0-9]{6}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(id))
                return true;

            return false;
        }

        protected static bool ValidateName(string name)
        {
            string pattern = @"^[a-zA-Z\ ]{3,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(name))
                return true;

            return false;
        }

        public override string ToString()
        {
            return String.Format("Supplier object\nid: {0}\nname: {1}\n", id, name);
        }

        public bool Equals(object? obj)
        {
            if (obj is not ShortSupplier sup)
                return false;

            if (id != sup.id)
                return false;
            if (name != sup.name)
                return false;
            return true;
        }
    }
}
