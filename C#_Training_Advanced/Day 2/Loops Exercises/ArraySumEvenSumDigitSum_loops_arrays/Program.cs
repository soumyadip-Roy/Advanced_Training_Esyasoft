namespace ArraySumEvenSumDigitSum_loops_arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assignment4();
        }

        static public void Assignment4()
        {
            // Create a space counting module
            Console.Write("Please enter a string of characters: ");
            string user = Console.ReadLine();
            string[] user_arr = user.Split(" ");
            Console.WriteLine($"The user defined string has {user_arr.Length - 1} spaces");
        }
        private static void Assignment3()
        {
            // Reverses a user defined array
            int[] array_2_rev = FillArray();
            int[] rev_array = ReverseArray(array_2_rev);

            for(int i = 0; i < rev_array.Length; i++)
            {
                Console.Write($"{rev_array[i]} ");
            }
        }

        
        private static void Assignment2()
        {
            //gets 2 arrays from user-split into four arrays containing only odd or even nums
            //find sum of elements of individual arrays-calc sum of digits of the sums of the 4 array elements
            (int[] array_1_even, int[] array_1_odd) = FillArraySeperated();
            (int[] array_2_even, int[] array_2_odd) = FillArraySeperated();

            int array_1_even_sum = SumArrayElems(array_1_even);
            int array_1_odd_sum = SumArrayElems(array_1_odd);
            int array_2_even_sum = SumArrayElems(array_2_even);
            int array_2_odd_sum = SumArrayElems(array_2_odd);

            int array_1_even_digit_sum = CalcDigitSum(array_1_even_sum);
            int array_1_odd_digit_sum = CalcDigitSum(array_1_odd_sum);
            int array_2_even_digit_sum = CalcDigitSum(array_2_even_sum);
            int array_2_odd_digit_sum = CalcDigitSum(array_2_odd_sum);

            Console.WriteLine($"First Array Even Sum: {array_1_even_sum} | Even Array Sum of Digits: {array_1_even_digit_sum} ");
            Console.WriteLine($"First Array Odd Sum: {array_1_odd_sum} | Odd Array Sum of Digits: {array_1_odd_digit_sum} ");
            Console.WriteLine($"Second Array Even Sum: {array_2_even_sum} | Even Array Sum of Digits: {array_2_even_digit_sum} ");
            Console.WriteLine($"Second Array Even Sum: {array_2_odd_sum} | Odd Array Sum of Digits: {array_2_odd_digit_sum} ");

        }

        private static void Assignment1()
        {
            //Gets 2 arrays from user - Adds Corresponding elements - Finds sum of even elements in sum array - Finds sum of Digit
            int[] array_1 = FillArray();
            int[] array_2 = FillArray();

            (bool result, int[] sum_array) = CalcSumArray(array_1, array_2);
            if (result)
            {
                int even_sum = CalcEvenSum(sum_array);
                int digit_sum = CalcDigitSum(even_sum);
                Console.WriteLine($"Even Sum is {even_sum} and Sum of Digits are {digit_sum}");
            }
        }
        private static int[] ReverseArray(int[] array_2b_rev)
        {
            int temp = 0;
            for (int i = 0; i < array_2b_rev.Length; i++)
            {
                int j = array_2b_rev.Length - i - 1;
                if (i < j)
                {
                    temp = array_2b_rev[j];
                    array_2b_rev[j] = array_2b_rev[i];
                    array_2b_rev[i] = temp;
                }
                else
                {
                    break;
                }
            }
            return array_2b_rev;
        }
        private static int SumArrayElems(int[] array)
        {
            int sum_elems = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum_elems += array[i];
            }
            return sum_elems;
        }
        private static (int[], int[]) FillArraySeperated() {

            Console.Write("Please Enter The Number of Array Elements: ");
            int length_arr = Convert.ToInt16(Console.ReadLine());
            int even_count = 0;
            int odd_count = 0;
            int[] return_arr = new int[length_arr];
            for (int i = 0; i < length_arr; i++)
            {
                Console.Write($"Please Enter #{i + 1} Array Element: ");
                return_arr[i] = Convert.ToInt16(Console.ReadLine());
                if (return_arr[i] % 2 == 0) { even_count++; }
                else { odd_count++; }
            }

            int[] array_even = new int[even_count];
            int[] array_odd = new int[odd_count];
            even_count--;
            odd_count--;
            for(int j = 0; j < length_arr; j++)
            {
                if (return_arr[j] % 2 == 0) { 
                    array_even[even_count] = return_arr[j];
                    even_count--;
                }
                else { 
                    array_odd[odd_count] = return_arr[j];
                    odd_count--;
                }
            }

            return (array_even,array_odd);
        }
        private static int CalcDigitSum(int sum)
        {
            int sum_digit = 0;
            int index = 0;

            while(sum > 0)
            {
                int divisor = 1;
                for (int i = 0; i < index + 1; i++) { divisor *= 10; }
                sum_digit += sum % divisor;
                sum = sum / divisor;
                index++;
            }
            return sum_digit;
        }

        private static int CalcEvenSum(int[] array_user)
        {
            int sum_even = 0;
            for(int i = 0; i < array_user.Length; i++)
            {
                if (array_user[i] % 2 == 0)
                {
                    sum_even += array_user[i];
                }
            }
            return sum_even;
        }


        private static (bool, int[]) CalcSumArray(int[] array_1, int[] array_2)
        {
            if (array_1.Length == array_2.Length)
            {
                int[] sum_arr = new int[array_1.Length];
                for (int i = 0; i < array_1.Length; i++)
                {
                    sum_arr[i] = array_1[i] + array_2[i];
                }
                return (true, sum_arr);
            }
            else
            {
                Console.WriteLine("Please Rerun program with equal arrays");
                return (false, [0]);
            }
        }

        private static int[] FillArray()
        {
            
            Console.Write("Please Enter The Number of Array Elements: ");
            int length_arr = Convert.ToInt16(Console.ReadLine());
            int[] return_arr=new int[length_arr];
            for (int i = 0; i < length_arr; i++)
            {
                Console.Write($"Please Enter #{i+1} Array Element: ");
                return_arr[i] = Convert.ToInt16(Console.ReadLine());
            }

            return return_arr;
        }
    }
}
