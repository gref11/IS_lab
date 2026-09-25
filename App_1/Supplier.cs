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
    internal class Supplier
    {
        string id;
        string name;
        string address;
        string email;
        string phone;

        public Supplier(string id, string name, string address, string email, string phone) 
        {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public Supplier(string stringSup, bool isJson=false)
        {
            if (isJson)
            {
                using var doc = JsonDocument.Parse(stringSup);
                var root = doc.RootElement;

                Id = root.GetProperty("id").GetString();
                Name = root.GetProperty("name").GetString();
                Address = root.GetProperty("address").GetString();
                Email = root.GetProperty("email").GetString();
                Phone = root.GetProperty("phone").GetString();
            }
            else
            {
                string[] splittedString = stringSup.Split(';');

                if (splittedString.Length != 5)
                    throw new ArgumentException("Invalid string format");

                Id = splittedString[0];
                Name = splittedString[1];
                Address = splittedString[2];
                Email = splittedString[3];
                Phone = splittedString[4];
            }
        }

        string Id
        {
            get { return id; }
            set {
                if (ValidateId(value))
                    id = value;
                else
                    throw GetInvalidFieldException("id");
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

        static Exception GetInvalidFieldException(string fieldName)
        {
            return new ArgumentException("Invalid supplier " + fieldName);
        }

        static bool ValidateId(string id)
        {
            string pattern = @"^[0-9]{6}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(id)) 
                return true;

            return false;
        }

        static bool ValidateName(string name)
        {
            string pattern = @"^[a-zA-Z\ ]{3,}$";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(name))
                return true;

            return false;
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
            return String.Format("Supplier object\nid: {0}\nname: {1}\naddress: {2}\nemail: {3}\nphone: {4}\n", id, name, address, email, phone);
        }

        public string ToShortString()
        {
            return String.Format("Supplier object. id: {0}; name: {1};", id, name);
        }

        public bool Equals(object? obj)
        {
            if (obj is not Supplier sup)
                return false;

            if (id != sup.id)
                return false;
            if (name != sup.name)
                return false;
            if (address != sup.address)
                return false;
            if (email != sup.email)
                return false;
            if (phone != sup.phone)
                return false;
            return true;
        }
    }
}
