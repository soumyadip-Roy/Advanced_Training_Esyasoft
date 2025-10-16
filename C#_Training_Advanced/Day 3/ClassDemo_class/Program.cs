namespace ClassDemo_class
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Vet vet_1 = new Vet(); // Object Instantiation for our classes
            Vet vet_2 = new Vet();
            Caretaker caretaker_1 = new Caretaker();
            Caretaker caretaker_2 = new Caretaker();
            Animal animal_1 = new Animal();
            Animal animal_2 = new Animal();

            animal_1.GetAnimalData();
            Animal.update_zoo_population();
            animal_1.DisplayAnimalStats();

        }
    }
}
