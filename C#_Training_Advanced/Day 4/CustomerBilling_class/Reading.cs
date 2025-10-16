using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBilling_class
{
    internal class Reading
    {
        string current_timestamp;
        double current_meter_reading;

        public string TimeStamp
        {
            get { return current_timestamp; }
            set { current_timestamp = value; }
        }
        public double MeterReading
        {
            get { return current_meter_reading; }
            set { current_meter_reading = value; }
        }

        public Reading()
        {
            Console.Write("Please Provide Timestamp:");
            TimeStamp = Console.ReadLine();
            Console.Write("Please Provide MeterReading:");
            MeterReading = Convert.ToDouble(Console.ReadLine());
        }
    }
}
