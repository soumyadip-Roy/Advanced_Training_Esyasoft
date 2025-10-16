using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemo_class
{
    internal class Animal
    {
        string zoo_id { get; set; }
        string animal_name { get; set; }
        string animal_scientific_name { get; set; }
        static int zoo_population { get; set; }
        string animal_diet { get; set; }
        string zoo_inventory_animal_id { get; set; }
        int zoo_inventory_animal_feed { get; set; }
        int zoo_expense_animal { get; set; }
        string zoo_caretaker_id { get; set; }
        string zoo_vet_id { get; set; }

        public void CheckAnimalHealth() {
            Console.WriteLine("Checking Animal Health:");
            //Check the health of the animal function()
            Console.WriteLine("The Animal is healthy!");

        }

        public void GetAnimalData()
        {
            Console.WriteLine("<===========WELCOME_TO_ZOO_TERMINAL==========>");
            Console.WriteLine("<===========ENTERING_NEW_ANIMAL_INFO==========>");
            
            Console.Write("ENTER NAME: ");
            animal_name = Console.ReadLine();
            Console.Write("ENTER SPECIES: ");
            animal_scientific_name = Console.ReadLine();
            Console.Write("ENTER DIET: ");
            animal_diet = Console.ReadLine();

            Console.WriteLine("<===============================>");
            Console.WriteLine("<===========GENERATING_RANDOM_ID_FOR_ANIMAL==========>");
            zoo_inventory_animal_id = Convert.ToString(RandomNumberGenerator.GetInt32(20000))+"FEED"+ Convert.ToString(RandomNumberGenerator.GetInt32(200));
            zoo_id= Convert.ToString(RandomNumberGenerator.GetInt32(20000))+"ANIMAL" + Convert.ToString(RandomNumberGenerator.GetInt32(50))+"ID";

            Console.WriteLine("<===========ASSIGNING_CARETAKER_AND_VET==========>");
            zoo_caretaker_id = Convert.ToString(RandomNumberGenerator.GetInt32(100)) + "CARETAKER" + Convert.ToString(RandomNumberGenerator.GetInt32(200));
            zoo_vet_id = Convert.ToString(RandomNumberGenerator.GetInt32(50)) + "VET" + Convert.ToString(RandomNumberGenerator.GetInt32(200));

            Console.WriteLine($"<============WELCOME_{animal_name.ToUpper()}_TO_THE_ZOO============>");


        }
        static public void update_zoo_population()
        {
            zoo_population++;
        }
        private void CheckAnimalLocation() { }
        private void CheckAnimalFood() { }
        private void RemoveAnimalForCheckup() { }
        public void DisplayAnimalStats()
        {
            Console.WriteLine("<===========WELCOME_TO_ZOO_TERMINAL==========>");
            Console.WriteLine("<===========INFO_OF_DESIGNATED_ANIMAL==========>");
            Console.WriteLine($"<============HERE_IS_{animal_name.ToUpper()}_FROM_THE_ZOO============>");
            Console.WriteLine("ANIMAL_NAME: "+animal_name);
            Console.WriteLine("ANIMAL_SPECIES: "+ animal_scientific_name);
            Console.WriteLine("TOTAL_ANIMAL_POPULATION: "+zoo_population);
            Console.WriteLine("ANIMAL_DIET: "+animal_diet);
            Console.WriteLine("ANIMAL_INVENTORY_ID: "+zoo_inventory_animal_id);
            Console.WriteLine("ANIMAL_CARETAKER_ID: "+zoo_caretaker_id);
            Console.WriteLine("ANIMAL_VET_ID: "+zoo_vet_id);
        }


        
    }
}
