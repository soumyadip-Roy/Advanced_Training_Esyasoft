namespace LoopBasic_loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MethodAssignment1();
        }

        static private void MethodAssignment1()
        {
            int num_user_col = 2;
            int num_user_row = 5;
            for(int i = 0; i < 5; i++)
            {
                int num_spec = i + 1;
                for(int j=0; j<num_spec; j++)
                {
                    if (j == num_user_col - 1 && i== num_user_row-1)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.WriteLine("\n");
            }
        }

        static private void MethodAssignment2()
        {
            int num_user = 4;
            int grid_num = 2 * num_user - 1;
            for(int i=0; i<grid_num; i++)
            {
                for(int j=0; j<grid_num; j++)
                {
                    if(j==num_user-1 || j == grid_num - num_user )
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                --num_user;
                Console.WriteLine("");
            }
            
        }
    }
}
