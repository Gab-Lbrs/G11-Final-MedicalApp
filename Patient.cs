using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G11_Final_MedicalApp;
using static G11_Final_MedicalApp.Staff;


namespace G11_Final_MedicalApp
{
    public class Patient : HospitalMember
    {
        //Ancien constructeur specifique


        // Liste pour les redez vous pris par les patient dans le Main programme
        public List<RendezVous> Agenda { get; set; } = new List<RendezVous>();

        // Construteur vide pour instanciation
        public Patient() : base("", "", 0, 0,"","") { }


        public DossierMedicale<Consultation> DossierConsultations { get; }
        = new DossierMedicale<Consultation>();


        // Methode utiliser par les patient pour ajouter leur rendez-vous
        public void PrendreRendezVous(DateTime dateDeRdv, TimeSpan duree)
        {
            var rdv = new RendezVous
            {
                DateDeRdv = dateDeRdv,
                Duree = duree,
                Patient = this,
                Status = RendezVousStatus.EnAttente //initialisation

            };
            // ajout du rendez-vous a l'agenda .
            Agenda.Add(rdv);
        }
        
    }



}
