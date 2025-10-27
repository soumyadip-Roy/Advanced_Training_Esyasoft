using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceDemo_inheritance
{
    internal abstract class Card
    {
        string card_num;
        string card_user_name;
        string card_cvv;
        string card_exp_date;
        string card_issue_date;
        string card_bank_name;
        string card_type;
        string card_desc;

        //public string ID { get; set; }
        //public string Name { get; set; }
        //public string Cvv { get; set; }
        //public string ExpiryDate { get; set; }
        //public string IssueDate { get; set; }
        //public string Type { get; set; }
        //public string Desc { get; set; }

        public void GetCardInfo()
        {
            card_num = Console.ReadLine();
            card_user_name = Console.ReadLine();
            card_cvv = Console.ReadLine();
            card_exp_date = Console.ReadLine();
            card_issue_date = Console.ReadLine();
            card_bank_name = Console.ReadLine();
            card_type = Console.ReadLine();
            card_desc = Console.ReadLine();
        }

        public Card(string card_num_new)
        {
            card_num = card_num_new;
            card_user_name = Console.ReadLine();
            card_cvv = Console.ReadLine();
            card_exp_date = Console.ReadLine();
            card_issue_date = Console.ReadLine();
            card_bank_name = Console.ReadLine();
            card_type = Console.ReadLine();
            card_desc = Console.ReadLine();
        }

        public Card()
        {
            card_num = Console.ReadLine();
            card_user_name = Console.ReadLine();
            card_cvv = Console.ReadLine();
            card_exp_date = Console.ReadLine();
            card_issue_date = Console.ReadLine();
            card_bank_name = Console.ReadLine();
            card_type = Console.ReadLine();
            card_desc = Console.ReadLine();
        }

        public virtual int DisplayCardInfo()
        {
            Console.WriteLine("card_num:" + card_num);
            Console.WriteLine("card_user_name:" + card_num);
            Console.WriteLine("card_cvv:" + card_num);
            Console.WriteLine("card_exp_date:" + card_num);
            Console.WriteLine("card_issue_date:" + card_num);
            Console.WriteLine("card_type:" + card_num);
            Console.WriteLine("card_desc:" + card_num);
            return 0;
        }

        public abstract void CheckStatus();
    }
}
