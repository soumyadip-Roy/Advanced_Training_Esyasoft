using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateExceptionDemo_exception
{
    internal class AgeNotSufficientException: Exception
    {
        int exception_code = 100;
        DateTime datetime_created;
        string error_message;
        int age;
        string error_descp;


        public AgeNotSufficientException(string message,int age_user):base(message)
        {
            error_message = message;
            datetime_created = DateTime.Now;
            age = age_user;
            error_descp = $"Please return in {18-age_user} years!";


        }

        public string error_display_array()
        {
            string[] user_arr = new string[4];
            user_arr[0] = Convert.ToString(datetime_created);
            user_arr[1] = $"Error Code: {exception_code}";
            user_arr[2] = error_message;
            user_arr[3] = error_descp;

            return String.Join(" ",user_arr);
        }
    }
}
