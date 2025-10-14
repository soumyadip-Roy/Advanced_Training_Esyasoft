namespace billingApp_conditional
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeUser();
            string[] userData1 = GetUserData();
            string[] userBill1 = GetUserBill(userData1);
            DisplayWelcomeUser();
            DisplayUserBill(userBill1, userData1);
        }

        static private void DisplayWelcomeUser()
        {
            Console.WriteLine("<---------- Welcome to MESCOM ---------->");
        }

        static private void DisplayUserBill(string[] userBill_data_arr, string[] userData_arr)
        {
            Console.WriteLine("<---------- Monthly Bill ---------->");
            Console.WriteLine($"|{"ZONE"}|{"Tarif Plan"}|{"Total Units Consumed"}|{"Total Payable"}|{"Slab 1 (Rs.1/unit)"}|{"Slab 2 (Rs.2/unit)"}|{"Slab 3 (Rs.3/unit)"}|");
            Console.WriteLine($"|{userData_arr[0].PadRight(4)}|{userData_arr[1].PadRight(10)}|{userData_arr[2].PadRight(20)}|{userBill_data_arr[0].PadRight(13)}|{userBill_data_arr[1].PadRight(18)}|{userBill_data_arr[2].PadRight(18)}|{userBill_data_arr[3].PadRight(18)}|");

        }

        static private string[] GetUserData()
        {
            string[] userData_arr = new string[3];
            
            Console.Write("Please Enter Your Zone as  Indicated [ Z1 / Z2 / Z3 ]: ");
            userData_arr[0] = Console.ReadLine();

            Console.Write("Please Enter Your Tarif as  Indicated [ LT1 / LT2 / LT3 ]: ");
            userData_arr[1] = Console.ReadLine();

            Console.Write("Please Enter Your Units Consumed: ");
            userData_arr[2] = Console.ReadLine();

            return userData_arr;
        }

        static private string[] GetUserBill(string[] userData_arr)
        {
            string[] userBill_arr = new string[4];    
                switch (userData_arr[0])
            {
                case "Z1":
                    userBill_arr = BillingZ1(userData_arr);
                    break;
                case "Z2":
                    userBill_arr = BillingZ1(userData_arr);
                    break;
                case "Z3":
                    userBill_arr = BillingZ1(userData_arr);
                    break;
                default:
                    Console.WriteLine("Not A Registered Zone, Exiting...");
                    return ["0","0","0","0"];
                    
            }

            return userBill_arr;
        }

        static private string[] BillingZ1(string[] userData_arr)
        {
            string[] bill_arr = new string[4];
            switch (userData_arr[1])
            {
                case "LT1":
                    bill_arr = BillingZ1LT1(userData_arr[2]);
                    break;
                case "LT2":
                    bill_arr = BillingZ1LT1(userData_arr[2]);
                    break;
                case "LT3":
                    bill_arr = BillingZ1LT1(userData_arr[2]);
                    break;
                default:
                    Console.WriteLine("Not A Registered Tarif plan, Exiting...");
                    return ["0", "0", "0", "0"];
                    
            }
            return bill_arr;
        }

        static private string[] BillingZ1LT1(string user_energy)
        {
            int total = 0;
            int units_consumed = Convert.ToInt32(user_energy);
            string[] bill_breakdown_arr = new string[4];
            if (units_consumed > 50)
            {
                units_consumed -= 50;
                total += 50 * 1;
                bill_breakdown_arr[1] = Convert.ToString(50*1);
                if (units_consumed > 50)
                {
                    units_consumed -= 50;
                    total += 50 * 2;
                    bill_breakdown_arr[2] = Convert.ToString(50 * 2);
                    if (units_consumed > 0)
                    {
                        total += 3 * units_consumed;
                        bill_breakdown_arr[3] = Convert.ToString((3 * units_consumed));
                        bill_breakdown_arr[0] = Convert.ToString(total);
                    }
                    else
                    {
                        bill_breakdown_arr[3] = "0";
                        bill_breakdown_arr[0] = Convert.ToString(total);
                    }
                }
                else
                {
                    total += 2 * units_consumed;
                    bill_breakdown_arr[2] = Convert.ToString(2 * units_consumed);
                    bill_breakdown_arr[3] = "0";
                    bill_breakdown_arr[0] = Convert.ToString(total);
                }
            }
            else
            {
                total += 1 * units_consumed;
                bill_breakdown_arr[1] = Convert.ToString(2 * units_consumed);
                bill_breakdown_arr[2] = "0";
                bill_breakdown_arr[3] = "0";
                bill_breakdown_arr[0] = Convert.ToString(total);
            }


            return bill_breakdown_arr;
        }
    }
}
