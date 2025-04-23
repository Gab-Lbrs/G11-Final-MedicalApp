using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static G11_Final_MedicalApp.Staff;

namespace G11_Final_MedicalApp
{
    // Rendez vous des patients
    public class RendezVous
    {
         public DateTime DateDeRdv { get; set; }
         public TimeSpan Duree {  get; set; }

         public Patient Patient { get; set; }

        public Medecin Medecin { get; set; }



        // Statut courant (par défaut EnAttente)

        public RendezVousStatus Status { get; set; } = RendezVousStatus.EnAttente;


       
       


    }

}
