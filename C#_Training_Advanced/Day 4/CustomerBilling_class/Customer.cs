using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBilling_class
{
    internal class Customer
    {
        string customer_id;
        string customer_name;
        string customer_location;
        string customer_rate_plan;

        public string ID
        {
            get { return customer_id; }
            set { customer_id = value; }
        }
        public string Name
        {
            get { return customer_name; }
            set { customer_name = value; }
        }
        public string Location
        {
            get { return customer_location; }
            set { customer_location = value; }
        }
        public string EnergyRate
        {
            get { return customer_rate_plan; }
            set { customer_rate_plan = value; }
        }

        public Customer()
        {
            Console.WriteLine("<===========ENTERING_NEW_CONSUMER_DETAILS==========>");

            Console.Write("ENTER_NAME: ");
            Name = Console.ReadLine();
            Console.Write("ENTER_ID: ");
            ID = Console.ReadLine();
            Console.Write("ENTER_LOCATION: ");
            Location = Console.ReadLine();
            Console.Write("ENTER_ENERGY_RATE: ");
            EnergyRate = Console.ReadLine();

            Console.WriteLine("<==========WELCOME_TO_ESYASOFT_ENERGY==========>");
        }

        public void GetCustomerInfo()
        {
            Console.WriteLine("CUSTOMER_NAME: " + Name);
            Console.WriteLine("CUSTOMER_ID: " + ID);
            Console.WriteLine("CUSTOMER_LOCATION: " + Location);
            Console.WriteLine("CUSTOMER_ENERGY_RATE: " + EnergyRate);

        }
    }
}
