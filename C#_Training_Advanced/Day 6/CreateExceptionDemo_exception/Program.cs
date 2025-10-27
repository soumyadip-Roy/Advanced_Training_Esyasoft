using System.Diagnostics;

namespace CreateExceptionDemo_exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Console.WriteLine("Please enter your age:");
            int user_age = Convert.ToInt16(Console.ReadLine());
            //string[] print_arr = new string[4];
            try
            {
                if (user_age < 18)
                {
                    throw new AgeNotSufficientException("Age not sufficient",user_age);
                }
                

            }
            catch(AgeNotSufficientException e)
            {
                //print_arr[0] = Convert.ToString(DateTime.Now);
                //(int exception_code, print_arr[2]) = e.errordisplay();  
                //print_arr[1] = $"Exception Code: {exception_code}";
                //print_arr[3] = $"Please try after {18 - user_age} years!";
                

                //string user_string = String.Join(" ", print_arr);
                Console.WriteLine(e.error_display_array());
            }
            finally
            {
                Console.WriteLine("End_program");
            }

        }
    }
}
