using System.ComponentModel.Design;

namespace CustomerBilling_class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            List<CustomerPackage> All_users = new List<CustomerPackage>();
            bool user_continue = true;
            while(user_continue){ user_continue = BillingMenu(All_users,user_continue); }

            //CustomerPackage CustomerA_ = new CustomerPackage();
            //CustomerA_.DisplayTotalInfo();


        }

        static private bool BillingMenu(List<CustomerPackage> list_all_users,bool user_flag)
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
                    //new_consumer_.DisplayAllInfo();
                    list_all_users.Last().DisplayAllInfo();
                    break;
                case '2':
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string user_id = Console.ReadLine();
                    int i = 0;
                    while (list_all_users[i].GetCustomerID() != user_id) { i++; }
                    if (i == list_all_users.Count) { break; }
                    list_all_users[i].AddNewSmartMeterToCustomer();
                    list_all_users[i].DisplayAllInfo();

                    break;
                case '3':
                    Console.Write("ENTER_METER_ID");
                    string meter_id = Console.ReadLine();
                    int j = 0;
                    while (list_all_users[j].GetMeterID() != meter_id) { j++; }
                    if (j == list_all_users.Count) { break; }
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
                    int k = 0;
                    while (list_all_users[k].GetCustomerID() != billing_user_id) { k++; }
                    if (k == list_all_users.Count) { break; }
                    list_all_users[k].UpdateReading();
                    break;
                case '5':
                    Console.WriteLine("<========USER_BILLING==========>");
                    Console.Write("ENTER_CUSTOMER_ID: ");
                    string bill_user_id = Console.ReadLine();
                    int m = 0;
                    while (list_all_users[m].GetCustomerID() != bill_user_id) { m++; }
                    if (m == list_all_users.Count) { break; }
                    list_all_users[m].DisplayTotalBill();
                    break;
                case '6':
                    return false;



            }

            Console.WriteLine("DO_YOU_WANT_TO_CONTINUE [Y/N]?");
            user_flag = (Console.ReadLine().ToUpper() == "Y") ? true : false;
            return user_flag;

        }
    }
}
