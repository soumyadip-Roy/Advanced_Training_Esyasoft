using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace CustomerBilling_class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CustomerPackage> All_users = new List<CustomerPackage>();
            bool user_continue;
            Console.Write("Are you a user? [y/n]: ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                user_continue = false;
                while (!user_continue) { user_continue = BillingMenuUser(All_users,user_continue)}
            }
            else
            {
                user_continue = true;
                while(user_continue){ user_continue = BillingMenu(All_users,user_continue); }
            }
            
        }

        static private bool BillingMenuUser(List<CustomerPackage> all_users, bool user_continue) {

            Console.WriteLine("<==========WELCOME_TO_ESYASOFT==========>");
            if(UserPasswordValidation())
            {
                Console.WriteLine("CHOOSE_FROM_THE_OPTIONS_GIVEN_BELOW");
                Console.WriteLine("1. View Biling Data\n2. Payment History\n3. Exit\n");
                Console.Write("YOUR_CHOICE [1/2/3]?: ");
                char user_option = Convert.ToChar(Console.ReadLine());
                switch (user_option)
                {
                    case '1':
                        Console.WriteLine("<========USER_BILLING==========>");
                        Console.Write("ENTER_CUSTOMER_ID: ");
                        string bill_user_id = Console.ReadLine();

                        (int m, bool flag_user_op5) = ValidateCustomer(all_users, bill_user_id);
                        if (flag_user_op5) { break; }
                        var curr_reading = all_users[m].ValidateCurrentReading();
                        try
                        {
                            throw new UnitBillingException(curr_reading);
                        }
                        catch (UnitBillingException e)
                        {
                            Console.WriteLine(e.error_display());
                            Console.WriteLine("EXITING_FUNCTION");
                        }

                        break;
                    case '2':
                        Console.WriteLine("<========USER_BILLING_HISTORY==========>");
                        //Console.Write("ENTER_CUSTOMER_ID: ");
                        //string bill_user = Console.ReadLine();
                        Console.WriteLine("<++++++++++PAYMENT_HISTORY_WILL_BE_SENT_ON_EMSIL++++++++++>");
                        break;
                    case '3':
                        user_continue = true;
                        break;
                }
            }
            else
            {
                return true;
            }
            return user_continue;
        }

        static private bool UserPasswordValidation()
        {
            bool user_val;
            try
            {
                Console.WriteLine("<==========USER_VALIDATION==========>");
                Console.Write("ENTER_USERNAME: ");
                string username = Console.ReadLine();
                Console.Write("ENTER_PASSWORD: ");
                string password = Console.ReadLine();
                if (!validate_username_and_password(username, password))
                {
                    
                    throw new UnitLoginUser();


                }
                user_val = true;
                return user_val;

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:Details_incorrect_Exiting.....");
                return false;
            }
            
            
        }
        static private bool validate_username_and_password(string username, string password)
        {
            if (username == "S_ROY")
            {
                if (password == "12345")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("PASSWORD_WRONG");
                    Console.WriteLine("<===========INVALID_USER_INPUT============>");
                    return false;
                }

            }
            else
            {
                Console.WriteLine("USERNAME_NOT_FOUND");
                Console.WriteLine("<===========INVALID_USER_INPUT============>");
                return false;
            }
        }
        private bool BillingMenu(List<CustomerPackage> list_all_users,bool user_flag)
        {
            Console.WriteLine("<==========WELCOME_TO_ESYASOFT==========>");
            Console.WriteLine("CHOOSE_FROM_THE_OPTIONS_GIVEN_BELOW");
            Console.WriteLine("1. Add Customer\n2. Add Smart Meter\n3. Map Meter\n4. Add Reading\n5. Generate Bill\n6. Exit\n");
            Console.Write("YOUR_CHOICE [1/2/3/4/5/6]?: ");
            char user_option = Convert.ToChar(Console.ReadLine());

            switch (user_option)
            {
                case '1':
                    Console.WriteLine("CREATING_NEW_CONSUMER_RECORD_RUNNING");

                    CustomerPackage new_consumer_ = new CustomerPackage();

                    list_all_users.Add(new_consumer_);
                    list_all_users.Last().DisplayAllInfo();
                    
                    break;
                case '2':
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string user_id = Console.ReadLine();
                    
                    (int i, bool flag_user_op2) = ValidateCustomer(list_all_users, user_id);
                    
                    if (flag_user_op2) { break; }
                    
                    list_all_users[i].AddNewSmartMeterToCustomer();
                    
                    list_all_users[i].DisplayAllInfo();
                    
                    break;
                case '3':
                    
                    Console.Write("ENTER_METER_ID");
                    string meter_id = Console.ReadLine();
                    
                    (int j, bool flag_meter_op3) = ValidateSmartMeter(list_all_users, meter_id);
                    if (!flag_meter_op3) { break; }
                    
                    if (MultipleEntryCheck(list_all_users,meter_id)) { break; }
                    
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string cust_id = Console.ReadLine();
                    
                    if (cust_id == list_all_users[j].GetCustomerID())
                    {
                        Console.WriteLine("METER_MAPPED");
                        break;
                    }
                    
                    else
                    {
                        Console.WriteLine("METER_MAPPING_REQUIRED");
                        break;
                    }
                case '4':
                    Console.WriteLine("<========METER_ADD_READING==========>");
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string billing_user_id = Console.ReadLine();
                    
                    (int k, bool flag_user_op4) = ValidateCustomer(list_all_users, billing_user_id);
                    if (flag_user_op4) {
                        Console.WriteLine(k);
                        break; }
                    
                    list_all_users[k].UpdateReading();
                    
                    break;
                case '5':
                    Console.WriteLine("<========USER_BILLING==========>");
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string bill_user_id = Console.ReadLine();
                    
                    (int m, bool flag_user_op5) = ValidateCustomer(list_all_users, bill_user_id);
                    if (flag_user_op5) { break; }
                    var curr_reading = list_all_users[m].ValidateCurrentReading();
                    try{
                        throw new UnitBillingException(curr_reading);
                    }
                    catch(UnitBillingException e)
                    {
                        Console.WriteLine( e.error_display() );
                        Console.WriteLine("EXITING_FUNCTION");
                    }

                    break;
                case '6':

                    return false;



            }

            Console.WriteLine("DO_YOU_WANT_TO_CONTINUE [Y/N]?");
            user_flag = (Console.ReadLine().ToUpper() == "Y") ? true : false;
            return user_flag;

        }

        static private (int,bool) ValidateSmartMeter(List<CustomerPackage> list_all_users, string meter_id)
        {
            int j = 0;
            while (list_all_users[j].GetMeterID() != meter_id) { ++j; }
            return (j, (j >= 1) ? true : false);

        }
        static private (int, bool) ValidateCustomer(List<CustomerPackage> list_all_users, string customer_id)
        {
            int j = 0;
            while (list_all_users[j].GetCustomerID() != customer_id) { 
                ++j; }
            return (j, (j >= 1) ? true : false);
        }

        static private bool MultipleEntryCheck(List<CustomerPackage> list_all_users, string meter_id)
        {
            int count = 0;
            for (int a = 0; a < list_all_users.Count; a++)
            {
                if (list_all_users[a].GetMeterID() == meter_id) { count++; }
                else continue;
            }
            if (count > 1) { Console.WriteLine("Multiple Entries Found, Not Valid"); return true; }
            else { return false; }

        }
    }
}
