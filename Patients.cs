using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    internal class Patients : HospitalMember
    {


        public List<RendezVous> Agenda { get; } = new();

        public void PrendreRendezVous(DateTime dateDebut, TimeSpan duree)
        {
            var rdv = new RendezVous
            {
                DateDebut = dateDebut,
                Duree = duree,
                Patient = this
            };
            Agenda.Add(rdv);

        }
    }



}
