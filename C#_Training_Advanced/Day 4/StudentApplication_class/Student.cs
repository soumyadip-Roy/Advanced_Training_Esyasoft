using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication_class
{
    internal class Student
    {
        string std_name;
        public string Name
        {
            get
            {
                return std_name;
            }
            set
            {
                std_name = value;
            }
        }

        short std_class;
        short std_maths_marks;
        short std_science_marks;
        short std_social_science_marks;
        short std_english_marks;
        short std_phy_edu_marks;
        double std_avg_marks;
        short std_total_marks;
        char std_grade;
        static int student_count;

        public Student()
        {
            Console.WriteLine("Please enter the student's details when prompted");

            Console.Write("Name:");
            Name= Console.ReadLine();

            Console.Write("Class:");
            std_class = Convert.ToInt16(Console.ReadLine());

            Console.Write("Maths Marks:");
            std_maths_marks = Convert.ToInt16(Console.ReadLine());
            std_total_marks += std_maths_marks;

            Console.Write("Science Marks:");
            std_science_marks = Convert.ToInt16(Console.ReadLine());
            std_total_marks += std_science_marks;

            Console.Write("Social Science Marks:");
            std_social_science_marks = Convert.ToInt16(Console.ReadLine());
            std_total_marks += std_social_science_marks;
            
            Console.Write("English Marks:");
            std_english_marks = Convert.ToInt16(Console.ReadLine());
            std_total_marks += std_english_marks;
            
            Console.Write("Physical Education Marks:");
            std_phy_edu_marks = Convert.ToInt16(Console.ReadLine());
            std_total_marks += std_phy_edu_marks;

            std_avg_marks = Convert.ToDouble(std_total_marks)/5.00;

            std_grade = 'N';

            calculate_student_grade();
            student_count++;
        }

        private void calculate_student_grade()
        {
            if (std_avg_marks < 30)
            {
                std_grade = 'F';
            }
            else
            {
                if (std_avg_marks > 30)
                {
                    if (std_avg_marks > 40)
                    {
                        if (std_avg_marks > 50)
                        {
                            if (std_avg_marks > 60)
                            {
                                if (std_avg_marks > 70)
                                {
                                    if (std_avg_marks > 80)
                                    {
                                        if (std_avg_marks > 90)
                                        {
                                            std_grade = 'S';
                                        }
                                        else std_grade = 'A';
                                    }
                                    else std_grade = 'B';
                                }
                                else std_grade = 'C';
                            }
                            else std_grade = 'D';
                        }
                        else std_grade = 'E';
                    }
                    else std_grade = 'P';
                }
                else std_grade = 'R';
            }
        }

        public void DisplayStudentData()
        {
            //Console.WriteLine("Name: " + userArr[0] + " Class: " + userArr[1] + " Maths Marks: " + userArr[2] + " Science Marks: " + userArr[3] + " SST Marks: " + userArr[4] + " Total: " + userArr[5] + " Average: " + userArr[6] + " Grades: " + userArr[7]);
            Console.WriteLine("Displaying Students Data:");

            Console.WriteLine($"|Name:{Name.PadRight(10)}|Class:{std_class}|Mt:{std_maths_marks}|Sc:{std_science_marks}|SSt:{std_social_science_marks}|En:{std_english_marks}|PE:{std_phy_edu_marks}|Total:{std_total_marks}|Avg:{std_avg_marks}|Grd:{std_grade}|");
        }

        static public void DisplayStudentCount()
        {
            Console.WriteLine("Total Number Of students in Class: "+student_count);
        }
    }
}
