namespace FindThe_Larget_Smallest_conditional
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Please Enter Numbers within 1000 and 100");

            //MethodAssignment1();
            //MethodAssignment2();
     
            
        }

        private static void MethodAssignment2()
        {
            int num, num_max, num_min, index;
            num_max = 100;
            num_min = 1000;
            index = 0;
            string flag="";

            int num1 = GetNumber(++index);
            (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num1);
            flag = CheckNumberRangeFlag(num1,index,flag);

            int num2 = GetNumber(++index);
            (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num2);
            flag = CheckNumberRangeFlag(num2,index,flag);

            int num3 = GetNumber(++index);
            (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num3);
            flag = CheckNumberRangeFlag(num3,index,flag);

            DisplayErrorMessage(flag, num1, num2, num3, num_max, num_min);
            
        }

        private static void MethodAssignment1()
        {
            int num, num_max, num_min;
            num_max = 100;
            num_min = 1000;
            int index = 0;
            int num1 = GetNumber(++index);
            if (CheckNumberRange(num1))
            {
                (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num1);
                int num2 = GetNumber(++index);
                if (CheckNumberRange(num2))
                {
                    (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num2);
                    int num3 = GetNumber(++index);
                    if (CheckNumberRange(num3))
                    {
                        (num_max, num_min) = CheckGreatestAndLeast(num_max, num_min, num3);

                        Console.WriteLine($"ALL_VALUES: {num1}, {num2}, {num3}\n MAX_VALUE: {num_max}, MIN_VALUE: {num_min}");
                    }
                }
            }
        }
        private static int GetNumber(int index)
        {
            Console.WriteLine($"Please Enter The #{index} Number: ");
            int userInpt = Convert.ToInt32(Console.ReadLine());
            return userInpt;
        }
        static private bool CheckNumberRange(int num)
        {
            if (num > 1000 || num < 100)
            {
                Console.WriteLine("The value entered is not within the desired range!");
                return false;
            }
            else
            {
                return true;
            }
        }
        static private string CheckNumberRangeFlag(int num, int index, string flag)
        {
            string temp="";
            switch (index)
            {
                case 1:
                    temp = "First";
                    break;
                case 2:
                    temp = "Second";
                    break;
                case 3:
                    temp = "Third";
                    break;
                default:
                    break;
            }
            if (num > 1000 || num < 100)
            {
                flag += $"...The {temp} Number is not valid...\n";
                return flag;
            }
            return flag;
            
        }

        static private void DisplayErrorMessage(string flag, int num1, int num2, int num3, int num_max, int num_min)
        {
            if (flag != "")
            {
                Console.WriteLine(flag);
                Console.WriteLine("Terminating Program ....");
            }
            else
            {
                Console.WriteLine($"ALL_VALUES: {num1}, {num2}, {num3}\n MAX_VALUE: {num_max}, MIN_VALUE: {num_min}");
            }
        }

        static private (int num_max, int num_min) CheckGreatestAndLeast(int num_max, int num_min, int num)
        {
            if (num_max < num)
            {
                num_max = num;
            }
            if (num_min > num)
            {
                num_min = num;
            }
            return (num_max, num_min);
        }
    }
}
