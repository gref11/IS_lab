using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
