using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBilling_class
{
    internal class CustomerPackage
    {

        Customer customer_;
        SmartMeter smart_meter_;
        Bill bill_;

        public CustomerPackage()
        {
            customer_ = new Customer();
            smart_meter_ = new SmartMeter(customer_.Location);
            bill_ = new Bill();
        }

        public void UpdateBill(Customer customer, SmartMeter smart_meter,Reading reading)
        {
            bill_.Bill_update(customer.ID, smart_meter.ID, reading.TimeStamp, reading.MeterReading, Convert.ToDouble(customer.EnergyRate));
        }

        public void UpdateReading()
        {
            Reading reading_ = new Reading();
            UpdateBill(customer_, smart_meter_, reading_);
        }

        public void AddNewSmartMeterToCustomer()
        {
            Console.WriteLine("<==========ENTER_DETAILS_OF_NEW_METER==========>");
            Console.Write("METER_ID: ");
            smart_meter_.ID = Console.ReadLine();
            //Console.Write("METER_LOCATION: ");
            smart_meter_.Location = customer_.Location;
            Console.WriteLine("METER_MODEL: ");
            smart_meter_.Model = Console.ReadLine();
            Console.WriteLine("<==========OPERATION_FINISHED==========>");

        }

        public void DisplayTotalBill()
        {
            bill_.BillDisplay();
        }

        public void DisplayCustomerInfo()
        {
            customer_.GetCustomerInfo();
        }

        public void DisplaySmartMeterInfo()
        {
            smart_meter_.DisplayMeterInfo();
        }

        public void DisplayAllInfo()
        {
            DisplayCustomerInfo();
            DisplaySmartMeterInfo();
        }

        public string GetCustomerID()
        {
            return customer_.ID;
        }

        public string GetMeterID()
        {
            return smart_meter_.ID;
        }
        

    }
}
