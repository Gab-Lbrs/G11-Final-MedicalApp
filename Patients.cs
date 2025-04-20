using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    internal class Patients : HospitalMember
    {
        public Patients(string name, string lastName, int age, int id) : base(name, lastName, age, id)
        {
        }

        public void PrendreRendezVous(DateTime dateDebut, TimeSpan duree)
        {
          

        }
    }



}
