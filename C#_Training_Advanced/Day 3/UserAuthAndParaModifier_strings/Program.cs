using System.Globalization;

namespace UserAuthAndParaModifier_strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ModuleAssignment1();
            ModuleAssignment2();
        }

        private static void ModuleAssignment2() {
            Console.WriteLine(" <----------DATA_ENTRY_INCOMING---------->");
            bool data_entry_open = true;
            string meter_id, meter_location;
            string current_timestamp = "";
            string prev_timestamp=null;
            string meter_reading_prev = "0";
            string meter_reading_current;
            double units_consumed = 0;


            while (data_entry_open)
            {
                string[] user_data = GetDataUser();
                if (!user_data[0].StartsWith("SM"))
                {
                    Console.WriteLine("<----------INVALID_DATA_ENTRY---------->");
                    data_entry_open = DataEntryContinue();
                    continue;
                }

                (meter_id, meter_location) = GetMeterInfo(user_data);
                (current_timestamp, meter_reading_current) = GetMeterMetric(user_data);

                if (prev_timestamp == null)
                {
                    units_consumed += Convert.ToDouble(meter_reading_current);
                }
                else
                {
                    units_consumed += Convert.ToDouble(meter_reading_current) - Convert.ToDouble(meter_reading_prev);
                }

                DisplayCustomerBill(units_consumed, current_timestamp, meter_id);

                prev_timestamp = current_timestamp;
                data_entry_open = DataEntryContinue();
            }
        }

        private static void DisplayCustomerBill(double units, string timestamp, string meter_id)
        {
            Console.WriteLine($"<----------CUSTOMER_{meter_id}_BILL---------->");
            string symbol = "\u20B91";
            Console.WriteLine($"_BILL_PAYABLE_{symbol}_{units * 5}_AT_{timestamp}_");
        }

        private static (string, string) GetMeterInfo(string[] user_data)
        {
            return (user_data[0], user_data[3].Split(':')[1]);
        }
        private static (string, string) GetMeterMetric(string[] user_data)
        {

            return (user_data[1], user_data[2].Split(' ')[1]);
        }

        private static bool DataEntryContinue()
        {
            bool data_entry_open=true;
            Console.WriteLine("<----------DATA_ENTRY_CONTINUE?[Y/N]---------->");
            char user_desc = Convert.ToChar(Console.ReadLine().ToUpper());
            if (user_desc == 'N')
            {
                data_entry_open = false;
            }
            return data_entry_open;
        }

        private static string[] GetDataUser()
        {
            Console.WriteLine("<----------DATA_ENTRY---------->");
            string[] data_string = Console.ReadLine().Split('|');
            return data_string;
        }

        private static void ModuleAssignment1()
        {
            if (VerifyUserData())
            {
                ParagraphModify();
            }
        }

        private static bool VerifyUserData()
        {
            Console.WriteLine("Hello, PLease enter your username and password!");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            if (username.Length < 8)
            {
                Console.WriteLine("The Username entered is too short, please try again with proper username later!");
                return false;
            }
            Console.Write("Password: ");
            string pswd = Console.ReadLine();
            if (username == "sroy_123")
            {
                if (pswd == "123456")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong Password... Exiting...");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("No matching User found... Exiting...");
                return false;
            }

        }

        private static void ParagraphModify()
        {
            Console.WriteLine($"Please enter the reason why you want to enter the driving test. Enter your student id as \"Student:<your id no>\" in the beginning!");
            string yes_no = "N";
            string user_para = "";
            string display_conf="";
            while(yes_no != "Y")
            {
                user_para += Console.ReadLine();
                Console.WriteLine("Did you finish writing the paragraph?  [Y/N] ");
                yes_no = (Console.ReadLine()).ToUpper();
                if (yes_no == "N")
                {
                    Console.WriteLine("Please continue writing...");
                }
                else
                {
                    Console.WriteLine("Thank you your result will be processed...Do you want to see the data? [Y/N]:");
                    display_conf = (Console.ReadLine()).ToUpper();
                }
            }

            if(display_conf=="Y")
            {
                int length_user_string = user_para.Length;
                int num_words_user_para = (user_para.Split([' ', '.', '\n'])).Length;
                int num_spaces_user_para = (user_para.Split(' ')).Length-1;
                Console.WriteLine($"The User has input {num_words_user_para} words with {num_spaces_user_para} spaces");

                string[] user_para_sents = user_para.Split('.');
                int user_para_a_pos = user_para.IndexOf("a");
                Console.WriteLine($"Index of the first \" a\" is {user_para_a_pos}");

                string user_highlight = "Student";
                DisplayHighlightedWordInUpperCase(user_para, user_highlight);

                string user_para_copy_2 = user_para;
                DisplayCamelCase(user_para_copy_2);
                DisplayCountNumberOfSubstring(user_para_copy_2, ['a', 'A']);
                DisplayStateFromNumPlate();
            }
            else
            {
                DisplayStateFromNumPlate();
            }



        }
        static private void DisplayStateFromNumPlate()
        {
            Console.WriteLine("Please enter the NUMBER PLATE you registered with your vehicle: ");
            string user_plate = Console.ReadLine();
            string user_state = user_plate.Split(' ')[0];
            switch (user_state)
            {
                case "KA":
                    Console.WriteLine("Your Vehicle is registered in KARNATAKA");
                    break;
                case "WB":
                    Console.WriteLine("Your Vehicle is registered in WEST BENGAL");
                    break;
                default:
                    Console.WriteLine("Unregistered");
                    break;
            }
        }

        static private void DisplayCamelCase(string user)
        {
            string user_copy = user;
            char[] delimiter_array = [' ', '.', ',', '\n'];
            for(int j = 0;j<delimiter_array.Length;j++)
            { 
                string[] user_repl = user_copy.Split(delimiter_array[j]);

                for (int i = 0; i < user_repl.Length; i++)
                {
                    if (user_repl[i] != "")
                    {
                        char substring = user_repl[i][0];
                        char substring_rep = user_repl[i].ToUpper()[0];

                        user_repl[i] = user_repl[i].Replace(substring, substring_rep);
                    }
                }
                user_copy = String.Join(delimiter_array[j],user_repl);
            }
            Console.WriteLine(user_copy);
        }

        static private void DisplayCountNumberOfSubstring(string user, char[] substring_arr)
        {
            int count_num_of_inst = (user.Split(substring_arr)).Length - 1;
            Console.WriteLine($"There are {count_num_of_inst} instances of {substring_arr} in the user para");
        }
        static private void DisplayHighlightedWordInUpperCase(string user_para, string user_highlight)
        {
            string user_para_copy = user_para;
            user_para_copy = user_para_copy.Replace(user_highlight, user_highlight.ToUpper());
            Console.WriteLine(user_para_copy);
        }
    }
}
