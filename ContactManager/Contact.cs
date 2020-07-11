using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    class Contact
    {
        private String Name; // contact first name
        private String LastName; // contact last name;
        private String PhoneNumber; // contact phone number
        private String Address; // contact address

        public Contact(String Name, String LastName, String PhoneNumber, String Address)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            if (Address != null)
            {
                this.Address = Address;
            }
            else
            {
                this.Address = "-";
            }
        }
        public String getName()
        {
            return Name;
        }
        public String getLastName()
        {
            return LastName;
        }
        public String getPhoneNumber()
        {
            return PhoneNumber;
        }
        public String getAddress()
        {
            return Address;
        }
        public void setName(String name)
        {
            Name = name;
        }
        public void setLastName(String lastName)
        {
            LastName = lastName;
        }
        public void setPhoneNumber(String phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void setAddress(String address)
        {
            Address = address;
        }
        /// <summary>
        /// Formatted string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("| {0,-15} | {1,-15} | {2,12} | {3} ",
                Name, LastName, PhoneNumber, Address);
        }
    }
}
