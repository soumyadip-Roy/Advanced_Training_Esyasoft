using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemo_class
{
    internal class Vet
    {
        string vet_id { get; set; }
        string vet_name { get; set; }
        string vet_contact_number { get; set; }
        string vet_address { get; set; }
        int vet_age { get; set; }
        string[] vet_animal_speciality { get; set; }
        string vet_salary_per_session { get; set; }
        static private void SignInVet() { }
        static private void SignOutVet() { }
        static private void CheckLocationVet() { }
        static private void CreditVetSalary() { }
    }
}
