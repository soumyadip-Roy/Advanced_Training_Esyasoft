namespace PrinterManuf_interfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            EpsonPrinter printer1 = new EpsonPrinter();
            HPPrinter printer2 = new HPPrinter();
            printer1.Print();
            printer2.Print();

            HashSet<int> int_set = new HashSet<int> { 1, 2, 3, 4, 5 };
            List<int> int_list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> int_list_2 = new List<int>();

            int_set.Add(5);
            //int_list_2=int_list.Append(2);

            Console.Write(int_set.Count);
            Console.WriteLine(int_list.Count);
            foreach (int element in int_set) { Console.Write(element); }
            foreach (int element in int_list) { Console.Write(element); }
            //Console.WriteLine(int_list);
        }
    }
}
