namespace ConsoleAppPrep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] my_arr = new int[10];

            //Console.WriteLine("Hello, World!");
            //try{// read 2 data from keyboard
            //    //int first = 20;
            //    //int sec = 0;
            //    //int result = first / sec;
            //    //// Built in exceptions: To handle frequent exceptions, we have defined classes
            //    /////system has same pre defined classes whih help in defininhwhat the porhrmson moyivepaf, like divide by zero, out of bounds.
            //    //Console.WriteLine(result);
            //    //Console.WriteLine("programTerminating...");
            //    try{ my_arr[11] = 20; }
            //    catch(IndexOutOfRangeException e)
            //    {
            //        Console.WriteLine("Error caught in inner try block!");
            //    }
            //}

            //catch(DivideByZeroException e)
            //{
            //    Console.WriteLine("Error Out: " + e + " as Divison by Zero has taken place");
            //}
            //catch(IndexOutOfRangeException ex)
            //{
            //    Console.WriteLine("Error Out: " + ex + " as Out Of Range of the designated array");
            //}
            //finally
            //{
            //    Console.WriteLine("Finally Block is Called!");
            //}

            int user_inp1, user_inp2, user_inp3;

            Console.WriteLine("Please enter the numbers in order! ");

            Console.Write("First Num");
            user_inp1 = Convert.ToInt16(Console.ReadLine());
            Console.Write("Second Num");
            user_inp2 = Convert.ToInt16(Console.ReadLine());
            Console.Write("Third Num");
            user_inp3 = Convert.ToInt16(Console.ReadLine());
            int result;
            try
            {
                //try { result = 1 / user_inp1; }
                //catch (DivideByZeroException e) {
                //    Console.WriteLine("The First number is Zero Thus Processing Halted!");
                //}
                //try { result = 1/ user_inp2; }
                //catch (DivideByZeroException e) {
                //    Console.WriteLine("The Second number is Zero Thus Processing Halted!");
                //}
                //try { result = 1 / user_inp3; }
                //catch (DivideByZeroException e) {
                //    Console.WriteLine("The Third number is Zero Thus Processing Halted!");
                //}
                if(user_inp1<5){ throw new ArgumentException("Please Enter The First Number again with value more than 5"); }
                if(user_inp2<5){ throw new ArgumentException("Please Enter The Second Number again with value more than 5"); }
                if(user_inp3<5){ throw new ArgumentException("Please Enter The Third Number again with value more than 5"); }
                


            }
            catch(Exception e)
            {
                Console.WriteLine("Error Detected.."+e+"..Exiting Validation");
            }
            finally
            {
                Console.WriteLine("We Have Thus verified the Data!");
            }
        }
    }
}
