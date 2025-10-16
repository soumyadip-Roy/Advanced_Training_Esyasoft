using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBilling_class
{
    internal class Bill
    {
        string bill_customer_id;
        string bill_meter_id;
        string bill_previous_timestamp;
        string bill_current_timestamp;
        double bill_previous_reading;
        double bill_current_reading;
        double bill_current_amount;
        double bill_customer_energy_rate;

        public string CustomerID
        {
            get { return bill_customer_id; }
            set { bill_customer_id = value; }
        }
        public string MeterID
        {
            get { return bill_meter_id; }
            set { bill_meter_id = value; }
        }
        public string CurrentTimeStamp
        {
            get { return bill_current_timestamp; }
            set {
                bill_previous_timestamp = bill_current_timestamp?? "";
                bill_current_timestamp= value; 
            }

        }
        public double CurrentReading
        {
            get { return bill_current_reading; }
            set
            {
                bill_previous_reading = bill_current_reading;
                bill_current_reading = value;
            }
        }

        public double EnergyRate
        {
            get { return bill_customer_energy_rate; }
            set { bill_customer_energy_rate = (value>0)?value:2; }
        }

        public double BillCurrentAmount
        {
            get {
                bill_current_amount = (bill_current_reading - bill_previous_reading) * bill_customer_energy_rate;
                return bill_current_amount; 
            }
            
        }

        public void Bill_update(string customer_id, string meter_id, string current_timestamp, double current_reading,double customer_energy_rate )
        {
            CustomerID = customer_id;
            MeterID = meter_id;
            CurrentTimeStamp = current_timestamp;
            CurrentReading = current_reading;
            EnergyRate = customer_energy_rate;
        }

        public void BillDisplay()
        {
            if (BillCurrentAmount != 0)
            {
                Console.WriteLine("<==========AUTOMATED_BILL_GENERATED==========>");
                Console.WriteLine("CUSTOMER_ID: " + CustomerID);
                Console.WriteLine("METER_ID: " + MeterID);
                Console.WriteLine("CURRENT_TIMESTAMP: " + CurrentTimeStamp);
                Console.WriteLine("CURRENT_READING: " + CurrentReading);
                Console.WriteLine("CURRENT_BILLING_AMOUNT: " + BillCurrentAmount);
            }
            else
            {
                Console.WriteLine("<===========NO_BILL_TO_GENERATE_NO_USAGE==========>");
            }
        }
    }
}
