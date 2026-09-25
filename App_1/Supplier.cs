using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_1
{
    internal class Supplier : ShortSupplier
    {
        string address;
        string email;
        string phone;

        public Supplier(string id, string name, string address, string email, string phone) : base(id, name)
        {
            Address = address;
            Email = email;
            Phone = phone;
        }

        private Supplier((string id, string name, string address, string email, string phone) data) : this(data.id, data.name, data.address, data.email, data.phone)
        {
        }

        public Supplier(string stringSup, bool isJson=false) : this(ParseString(stringSup, isJson))
        {
        }

        string Address
        {
            get { return address; }
            set
            {
                if (ValidateAddress(value))
                    address = value;
                else
                    throw GetInvalidFieldException("address");
            }
        }

        string Email
        {
            get { return email; }
            set
            {
                if (ValidateEmail(value))
                    email = value;
                else
                    throw GetInvalidFieldException("email");
            }
        }

        string Phone
        {
            get { return phone; }
            set
            {
                if (ValidatePhone(value))
                    phone = value;
                else
                    throw GetInvalidFieldException("phone");
            }
        }

        static bool ValidateAddress(string address)
        {
            string pattern = @"^[a-zA-Z\ 0-9]{3,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(address))
                return true;

            return false;
        }

        static bool ValidateEmail(string email)
        {
            string pattern = @"[a-zA-Z]+[a-zA-Z0-9\ \.]*@[a-z]{3,}\.[a-z]{2,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(email))
                return true;

            return false;
        }

        static bool ValidatePhone(string phone)
        {
            string pattern = @"^\+?[0-9]{11}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(phone))
                return true;

            return false;
        }

        public override string ToString()
        {
            return String.Format("{0}address: {1}\nemail: {2}\nphone: {3}\n", base.ToString(), address, email, phone);
        }

        public string ToShortString()
        {
            return String.Format("Supplier object. id: {0}; name: {1};", id, name);
        }

        public bool Equals(object? obj)
        {

            if (obj is not Supplier sup)
                return false;

            if (!base.Equals(obj))
                return false;
            if (address != sup.address)
                return false;
            if (email != sup.email)
                return false;
            if (phone != sup.phone)
                return false;
            return true;
        }

        private static (string id, string name, string address, string email, string phone) ParseString(string stringSup, bool isJson = false)
        {
            if (isJson)
            {
                using var doc = JsonDocument.Parse(stringSup);
                var root = doc.RootElement;

                return (
                    root.GetProperty("id").GetString(),
                    root.GetProperty("name").GetString(),
                    root.GetProperty("address").GetString(),
                    root.GetProperty("email").GetString(),
                    root.GetProperty("phone").GetString()
                    );
            }

            string[] splittedString = stringSup.Split(';');

            if (splittedString.Length != 5)
                throw new ArgumentException("Invalid string format");

            return (
                splittedString[0],
                splittedString[1],
                splittedString[2],
                splittedString[3],
                splittedString[4]
                );
        }
    }
}
