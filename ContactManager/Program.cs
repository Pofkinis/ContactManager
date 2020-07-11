using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ContactManager
{

    class Program
    {
        const String File = "contacts.txt"; // name of a file where contacts are stored
        
        static void Main(string[] args)
        {
            List<Contact> contacts = new List<Contact>(); // contacts list
            ReadDataFromFile(contacts); // reads data from previous program launch
            bool on = true;
            String command = "/commands"; // on program launch instantly shows all commands
            while (on)
            {
                switch (command)
                {
                    case "/commands": // to show all commands
                        PrintCommands();
                        break;
                    case "/add": // command to add a new contact
                        Contact contact = AddContact(contacts);
                        if (contact != null)
                        {
                            contacts.Add(contact);
                            Console.WriteLine("Contact successfully added");
                        }
                        break;
                    case "/view": // command to view all contacts
                        PrintAllContacts(contacts);
                        break;
                    case "/update": // command to update particuar contact
                        Console.WriteLine("Please select contacts ID to update it");
                        String id = Console.ReadLine();
                        if(CheckIfContactExists(id, contacts))
                        {
                            UpdateContact(Convert.ToInt32(id), contacts);
                        }
                        break;
                    case "/delete": // command to delete particular contact
                        Console.WriteLine("Please select contacts ID to delete it");
                        String id1 = Console.ReadLine();
                        if (CheckIfContactExists(id1, contacts))
                        {
                            DeleteContact(Convert.ToInt32(id1), contacts);
                            Console.WriteLine("Contact successfully deleted");
                        }
                        break;
                    case "/exit": // to stop the program
                        on = false;
                        Environment.Exit(0);
                        break;
                    default: // in case user put invalid command
                        Console.WriteLine("Command doesn't exist. Type /commands to view all comannds");
                        break;
                }
                WriteDataToFile(contacts);
                command = Console.ReadLine();
            }
        }
        /// <summary>
        /// method which print all commands to console
        /// </summary>
        static void PrintCommands()
        {
            String[] commands = new String[6] { "/commands - shows a list of all commands",
                "/add - adds a new contact",
                "/update - lets you to choose particular contact and then update it",
                "/delete - lets you to choose particular contact and then deletes it",
                "/view - shows a list of all contacts",
                "/exit - exits program"};
            foreach(String command in commands)
            {
                Console.WriteLine(command);
            }
        }
        /// <summary>
        /// methos which adds a new contact
        /// </summary>
        /// <param name="contacts">contacts list</param>
        /// <returns>new contact</returns>
        static Contact AddContact(List<Contact> contacts)
        {
            Console.WriteLine("Enter contact name:");
            String name = Console.ReadLine().Trim();
            if (CheckWord(name)==false) { return null; } // checks if name was put valid
            Console.WriteLine("Enter contact last name:");
            String lastNanme = Console.ReadLine().Trim();
            if (CheckWord(lastNanme) == false) { return null; } // checks if last name was put valid
            Console.WriteLine("Enter contact phone number:");
            String phoneNumber = Console.ReadLine().Trim();
            if (CheckNumber(phoneNumber) == false || CheckIfNumberExists(contacts, phoneNumber) == false) { return null; } // checks if phone number was put valid
            Console.WriteLine("Enter contact address (optional, you can leave this field empty):");
            String address = Console.ReadLine().Trim();
            if (CheckAddress(address) == false) { return null; } // checks if address was put valid
            Contact contact = new Contact(name, lastNanme, phoneNumber, address);
            return contact;
        }
        /// <summary>
        /// Reads data from file
        /// </summary>
        /// <param name="contacts">list of contacts</param>
        static void ReadDataFromFile(List<Contact> contacts)
        {
            using (StreamReader reader = new StreamReader(File))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] value = line.Split('|');
                    string trash = value[0]; // empty space in the beginning
                    string name = value[1].Trim();
                    string lastName = value[2].Trim();
                    string phoneNumber = value[3].Trim();
                    string address = value[4].Trim();
                    Contact contact = new Contact(name, lastName, phoneNumber, address);
                    contacts.Add(contact);
                }
            }
            WriteDataToFile(contacts);
        }
        /// <summary>
        /// Write data to file
        /// </summary>
        /// <param name="contacts">list of contacts</param>
        static void WriteDataToFile(List<Contact> contacts)
        {            
            using (StreamWriter writer = new StreamWriter(File))
            {
                foreach (Contact contact in contacts)
                {
                    writer.WriteLine(String.Format("| {0,-15} | {1,-15} | {2,12} | {3}",
                         contact.getName(), contact.getLastName(), contact.getPhoneNumber(), contact.getAddress()));
                }
            }
        }
        /// <summary>
        /// Prints all contacts into the console
        /// </summary>
        /// <param name="contacts">list of contacts</param>
        static void PrintAllContacts(List<Contact> contacts)
        {
            int count = 1;
            Console.WriteLine(String.Format("| {0,2} | {1,-15} | {2,-15} | {3,12} | {4,-15}",
                "ID", "Name", "Last name", "Phone number", "Address")); 
            foreach (Contact contact in contacts)
            {
                Console.WriteLine("| {0, 2} {1, 1}", count, contact);
                count++;
            }
        }
        /// <summary>
        /// methos which updates particular contact
        /// </summary>
        /// <param name="id">updated contact id</param>
        /// <param name="contacts">list of contacts</param>
        static void UpdateContact(int id, List<Contact> contacts)
        {

            Console.WriteLine("Contact name is "+ contacts.ElementAt(id-1).getName() + " . If you want to change it - write new name," +
                " if you want to leave old name - leave empty space");
            String newName = Console.ReadLine().Trim();
            if (CheckWord(newName)) // checks if updated name is valid
                contacts.ElementAt(id - 1).setName(newName);
            Console.WriteLine("Contact last name is " + contacts.ElementAt(id - 1).getLastName() + " . If you want to change it - write new name," +
                " if you want to leave old name - leave empty space");
            String newLastName = Console.ReadLine().Trim();

            if (CheckWord(newLastName)) // checks if updated last name is valid
                contacts.ElementAt(id - 1).setLastName(newLastName);

            Console.WriteLine("Contact phone number is " + contacts.ElementAt(id - 1).getPhoneNumber() + " . If you want to change it - write new name," +
                 " if you want to leave old name - leave empty space");
            String newPhoneNumber = Console.ReadLine().Trim();
                if (CheckNumber(newPhoneNumber) ||
                    CheckIfNumberExists(contacts, newPhoneNumber))  // checks if updated phone number is valid
                contacts.ElementAt(id - 1).setPhoneNumber(newPhoneNumber);
            Console.WriteLine("Contact address is " + contacts.ElementAt(id - 1).getAddress() + " . If you want to change it - write new name," +
                " if you want to leave old name - leave empty space");
            String newAddress = Console.ReadLine().Trim();

            if (CheckAddress(newAddress)) // checks if updated address if 
                contacts.ElementAt(id - 1).setAddress(newAddress);
            Console.WriteLine("Contact successfully updated");
        }
        /// <summary>
        /// removes particular contact
        /// </summary>
        /// <param name="id">id of contact which user wants to delete</param>
        /// <param name="contacts">list of all contacts</param>
        static void DeleteContact(int id, List<Contact> contacts)
        {
            contacts.RemoveAt(id - 1);
        }
        /// <summary>
        /// checks if there is a contact with such an id
        /// </summary>
        /// <param name="id">id which we check if it is in a list</param>
        /// <param name="contacts">list of contacts</param>
        /// <returns>true if contact like this exists</returns>
        static bool CheckIfContactExists(String id, List<Contact> contacts)
        {
            if(Regex.IsMatch(id, @"^\d+$"))
            {
                if (contacts.Count > 0 && Convert.ToInt32(id) <= contacts.Count)
                {
                    return true;
                }
            }
            
            Console.WriteLine("Contact with this ID doesn't exist");
            return false;
        }
        /// <summary>
        /// checks if word is valid
        /// </summary>
        /// <param name="word">word which should be checked</param>
        /// <returns>true if word is valid</returns>
        static bool CheckWord(String word)
        {
            if(Regex.IsMatch(word, @"^[a-zA-Z]+$") && word.Length > 1 && word.Length < 16){
                return true;
            }
            Console.WriteLine("Input is invalid");
            return false;
        }
        /// <summary>
        /// checks if number is valid
        /// </summary>
        /// <param name="number">number which should be checked</param>
        /// <returns>true if number is valid</returns>
        static bool CheckNumber(String number)
        {
            if (Regex.IsMatch(number, @"^[0-9]+$") && number.Length > 7 && number.Length < 15)
            {
                return true;
            }
            Console.WriteLine("Phone number is invalid");
            return false;
        }
        /// <summary>
        /// checks if address is valid
        /// </summary>
        /// <param name="address">address which should be checked</param>
        /// <returns>true if address is valid</returns>
        static bool CheckAddress(String address)
        {
            if(address.Length < 255)
            {
                return true;
            }
            Console.WriteLine("Address is too long");
            return false;
        }
        /// <summary>
        /// checks if there is a contact with the same phone number which user wants to create 
        /// </summary>
        /// <param name="contacts">list of contacts</param>
        /// <param name="number">phone number which should be checked</param>
        /// <returns>return false if number like this already exists</returns>
        static bool CheckIfNumberExists(List<Contact> contacts, String number)
        {
            foreach (Contact contact in contacts)
            {
                if(contact.getPhoneNumber() == number)
                {
                    Console.WriteLine("Number like this already exists");
                    return false;
                }
            }
            return true;
        }

    }
}
