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
        string inn;
        string name;
        string address;
        string email;
        string phone;

        public Supplier(string id, string inn, string name, string address, string email, string phone) 
        {
            Id = id;
            Inn = inn;
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
                Inn = root.GetProperty("inn").GetString();
                Name = root.GetProperty("name").GetString();
                Address = root.GetProperty("address").GetString();
                Email = root.GetProperty("email").GetString();
                Phone = root.GetProperty("phone").GetString();
            }
            else
            {
                string[] splittedString = stringSup.Split(';');

                if (splittedString.Length != 6)
                    throw new ArgumentException("Invalid string format");

                Id = splittedString[0];
                Inn = splittedString[1];
                Name = splittedString[2];
                Address = splittedString[3];
                Email = splittedString[4];
                Phone = splittedString[5];
            }
        }

        public Supplier(Supplier other, string id)
        {
            Id = id;
            Inn = other.Inn;
            Name = other.Name;
            Address = other.Address;
            Email = other.Email;
            Phone = other.Phone;
        }

        public Supplier() {}

        public string Id
        {
            get { return id; }
            set {
                if (ValidateId(value))
                    id = value;
                else
                    throw GetInvalidFieldException("id");
            }
        }

        public string Inn
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

        public string Name
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

        public string Address
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

        public string Email
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

        public string Phone
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
            string pattern = @"^[a-zA-Z]+[a-zA-Z0-9\ \.]*@[a-z]{3,}\.[a-z]{2,}$";
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
            return String.Format("Supplier object -- id: {0}; inn: {1}; name: {2}; address: {3}; email: {4}; phone: {5}\n", id, inn, name, address, email, phone);
        }

        public string ToShortString()
        {
            return String.Format("Supplier object -- id: {0}; inn: {1}; name: {2};", id, inn, name);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Supplier sup)
                return false;

            if (inn != sup.inn)
                return false;
            return true;
        }
    }
}
