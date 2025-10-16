using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomerBilling_class
{
    internal class SmartMeter
    {
        string meter_id;
        string meter_model;
        string meter_location;

        public string ID
        {
            get
            {
                return meter_id;
            }
            set { meter_id = value; }
        }

        public string Model
        {
            get { return meter_model; }
            set { meter_model = value; }
        }
        public string Location
        {
            get { return meter_location; }
            set { meter_location = value; }
        }

        public SmartMeter(string location)
        {
            Console.WriteLine("CONFIRM_IF_NEW_METER_ALLOTED");
            Console.Write("[Y/N] ?: ");
            char meter_installed = Convert.ToChar((Console.ReadLine()).ToUpper());
            if(meter_installed=='Y')
            {
                Console.WriteLine("<===========ENTERING_NEW_METER_DETAILS==========>");
                Console.Write("ENTER_ID: ");
                ID = Console.ReadLine();
                //Console.Write("ENTER_LOCATION: ");
                Location = location;
                Console.Write("ENTER_MODEL: ");
                Model = Console.ReadLine();
                Console.WriteLine("<==========WELCOME_TO_ESYASOFT_ENERGY==========>");
            }
            else {
                ID = "";
                Location = "";
                Model = "";
                Console.WriteLine("<==========WELCOME_TO_ESYASOFT_ENERGY==========>"); }
        }

        public void DisplayMeterInfo()
        {
            Console.WriteLine("SMART_METER_ID: " + ID);
            Console.WriteLine("SMART_METER_MODEL: " + Model);
            Console.WriteLine("SMART_METER_LOCATION: " + Location);
        }

    }
}
