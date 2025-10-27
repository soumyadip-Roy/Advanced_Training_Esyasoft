using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBilling_class
{
    class UnitBillingException:Exception
    {

        int exception_code;
        DateTime datetime_created;
        string error_message;
        string error_descp;
        public UnitBillingException(double meter_reading):base() {

            if (meter_reading < 0)
            {
                datetime_created = DateTime.Now;
                exception_code = 101;
                error_message = "Negative Meter Reading";
                error_descp = $"There is More Export than Import...The Meter is Faulty";

            }
            else if (meter_reading == 0)
            {
                datetime_created = DateTime.Now;
                exception_code = 102;
                error_message = "Zero Meter Reading";
                error_descp = $"There is no data to calculate billing amount";

            }
            else if (meter_reading > 700)
            {
                datetime_created = DateTime.Now;
                exception_code = 103;
                error_message = "High Voltage Reading";
                error_descp = "There is Excessive Use of the Meter, ALERT!";

            }

        }

        public string error_display()
        {
            string[] user_string = new string[4];
            user_string[0] = Convert.ToString(datetime_created);
            user_string[1] = $"Exception Code: {exception_code}";
            user_string[2] = error_message;
            user_string[3] = error_descp;

            return String.Join(" ", user_string);
        }
    }
}
