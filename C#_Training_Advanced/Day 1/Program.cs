namespace small_Datatypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte varByte = 120;
            sbyte varSByte = -28;
            short varShort = 26;
            int varInt = -90;
            uint varUInt = 1000000;
            long varLong = -10000000000;
            ulong varULong = 1020000000000;

            float varFloat = 10.9865F;
            double varDouble = -2.0900;
            decimal varDecimal = 98.0001234M;


            //Console.WriteLine("Byte:" + varByte);
            //Console.WriteLine("SByte:" + varSByte);
            //Console.WriteLine("Short:" + varShort);
            //Console.WriteLine("Int:" + varInt);
            //Console.WriteLine("UInt:" + varUInt);
            //Console.WriteLine("Long:" + varLong);
            //Console.WriteLine("ULong:" + varULong);
            //Console.WriteLine("Float:" + varFloat);
            //Console.WriteLine("Double:" + varDouble);
            //Console.WriteLine("Decimal:" + varDecimal);
            ////Console.WriteLine("Hello, World!");

            //Console.WriteLine("The sum of 10,20.19 is: " + addition(10, 20.19));
            //List<float> numList = new List<float> { 1, 2, 3, 4, 5 };
            //Console.WriteLine("The sum of numList is: " + addition(numList, 5));



            //Console.WriteLine("Please enter the array values as following: ");
            //int[] userArray = get_array_data();

            //dataDisplay(userArray);
            //int num = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("The result is: " + (((num % 2) == 1 ? true : false) ? "odd" : "even"));

            //Console.WriteLine("Pre Increment " + ++num+" "+num);

            //Console.WriteLine("Post Increment " + num++ + " " + num);

            //Console.WriteLine("Pre Decrement " + --num + " " + num);
            //Console.WriteLine("Post Decrement " +  num-- + " " + num);

            // Lets create a basic str
            //1. We welcome the user
            //------Welcome to School-------
            //-> So we create a function welcomeUser()
            //2. We prompt to enter the info
            // Name:
            //Class:
            //3. We prompt the subject marks
            //Maths:
            //Science:
            //Social Science Marks:
            //4. We store the student's info in an array of strings
            //->->-> We make a single function where we return an array of strings to the user
            //->->-> This array will contain all the info
            //->->-> We will store 3 such arrays for each student
            //5. We Display the info of the kids in a tabular format,
            // Name:  __  Total Marks:  __  Average Marks:  __  Grades:  __
            //-> We calculate avegare and sum using two diff functions calcAverage() and calcTotal()
            //

            welcomeUser();
            string[] student1 = get_student_data_arr();
            string[] student2 = get_student_data_arr();
            string[] student3 = get_student_data_arr();

            student1 = get_stat(student1);
            student2 = get_stat(student2);
            student3 = get_stat(student3);

            display_student_data_heading();
            display_student_data(student1);
            display_student_data(student2);
            display_student_data(student3);

        }

        static public void welcomeUser()
        {
            Console.WriteLine("<----------Welcome to School---------->");
        }

        static public string[] get_student_data_arr()
        {
            Console.WriteLine("Please enter the student's details when prompted");

            string[] student_info_arr = new string[8];

            Console.Write("Name:");
            student_info_arr[0] = Console.ReadLine();

            Console.Write("Class:");
            student_info_arr[1] = Console.ReadLine();

            Console.Write("Maths Marks:");
            student_info_arr[2] = Console.ReadLine();

            Console.Write("Science Marks:");
            student_info_arr[3] = Console.ReadLine();

            Console.Write("Social Science Marks:");
            student_info_arr[4] = Console.ReadLine();

            student_info_arr[5] = "0";

            student_info_arr[6] = "0";

            student_info_arr[7] = "NG";

            return student_info_arr;
        }

        static public string[] get_stat(string[] userArr)
        {
            userArr[5] = Convert.ToString(Convert.ToInt32(userArr[2]) + Convert.ToInt32(userArr[3]) + Convert.ToInt32(userArr[4]));
            userArr[6] = Convert.ToString(Math.Round(Convert.ToDouble(userArr[5]) / 3.00,2));

            return userArr;
        }

        static public void display_student_data_heading()
        {
            Console.WriteLine($"|{"Name".PadRight(10)}|{"Class".PadRight(0)}|{"Maths Marks"}|{"Science Marks".PadRight(0)}|{"SST Marks".PadRight(0)}|{"Total".PadRight(0)}|{"Average".PadRight(0)}|{"Grade".PadRight(0)}|");
        }
        static public void display_student_data(string[] userArr)
        {
            //Console.WriteLine("Name: " + userArr[0] + " Class: " + userArr[1] + " Maths Marks: " + userArr[2] + " Science Marks: " + userArr[3] + " SST Marks: " + userArr[4] + " Total: " + userArr[5] + " Average: " + userArr[6] + " Grades: " + userArr[7]);
            Console.WriteLine($"|{userArr[0].PadRight(10)}|{userArr[1].PadRight(5)}|{userArr[2].PadRight(11)}|{userArr[3].PadRight(13)}|{userArr[4].PadRight(9)}|{userArr[5].PadRight(5)}|{userArr[6].PadRight(7)}|{userArr[7].PadRight(5)}|");
        }

        static public double addition(int first, double second)
        {
            return (double)first + second;
        }
        static public float addition(List<float> sumList,short size)
        {
            if (size >5) return 0;
            else
            {
                return sumList[0] + sumList[1] + sumList[2] + sumList[3] + sumList[4];
            }
        }

        static public int[] dataCollection( int[] inpArray, byte ind)
        {
            inpArray[ind] = Convert.ToInt16(Console.ReadLine());

            return inpArray;    
        }

        static public int[] get_array_data()
        {
            int[] arrayFill = new int[5];
            dataCollection(arrayFill, 0);
            dataCollection(arrayFill, 1);
            dataCollection(arrayFill, 2);
            dataCollection(arrayFill, 3);
            dataCollection(arrayFill, 4);

            return arrayFill;
        }

        static public void dataDisplay(int[] userArray)
        {
            Console.WriteLine("The Following are the elements of the user array:");
            Console.Write(userArray[0] + " " + userArray[1] + " " + userArray[2] + " " + userArray[3] + " " + userArray[4] + "\n");
        }
    }
}
