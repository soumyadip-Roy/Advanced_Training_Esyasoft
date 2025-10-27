using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceDemo_inheritance
{
    internal class CreditCard:Card
    {
        string credit_card_limit;
        string credit_card_tap_limit;
        string credit_card_online_limit;
        string credit_card_status;

        public void GetCreditCardInfo()
        { 
            Console.WriteLine("Enter Credit Card Details:");
            //GetCardInfo();
            
            credit_card_limit = Console.ReadLine();
            credit_card_tap_limit = Console.ReadLine();
            credit_card_online_limit = Console.ReadLine();
            credit_card_status = Console.ReadLine();
                      
            
        }
        
        public CreditCard(string a):base(a)
        {
            //Card card = new Card();
            GetCreditCardInfo();
        }
        public CreditCard()
        {
            //Card card = new Card();
            GetCreditCardInfo();
        }

        public override int DisplayCardInfo()
        {
            
            Console.WriteLine("card_limit:" + credit_card_limit);
            Console.WriteLine("credit_card_tap_limit:" + credit_card_tap_limit);
            Console.WriteLine("credit_card_online_limit:" + credit_card_online_limit);
            Console.WriteLine("credit_card_status:" + credit_card_status);
            return 0;

        }

        public override void CheckStatus()
        {
            Console.Write("credit_card_status: OKAY!");
        }




    }
}
