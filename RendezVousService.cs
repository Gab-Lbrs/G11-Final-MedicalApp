using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public class RendezVousService : IRendezVousService
    {
        // ta storage interne
        private readonly List<RendezVous> store = new List<RendezVous>();

        //  Signature et type de retour conforme à l'interface
        public RendezVous PrendreRdv(Patient patient,Medecin medecin,DateTime dateDebut,TimeSpan duree)
        {
            //if (store.Any(rv => rv.Patient.ID == patient.ID && rv.Medecin.ID == medecin.ID &&
            //   rv.DateDeRdv == dateDebut))
            //{
            //    throw new ConflitRendezVousException(
            //        $"Un RDV existe déjà le {dateDebut:yyyy-MM-dd HH:mm}.");     
            //}


            var rdv = new RendezVous
            {
                Patient = patient,
                Medecin = medecin,
                DateDeRdv = dateDebut,
                Duree = duree
                // Status = EnAttente par défaut
            };
            rdv.Patient.Agenda.Add( rdv ); 
            store.Add(rdv);
            return rdv;
        }

        //  Retourne LE store, pas une nouvelle liste vide
        public List<RendezVous> ListerTousLesRdv() => store;

        //  Filtre et retourne une List<RendezVous>
        public List<RendezVous> ListerPourMedecin(int ID)=> store
               .Where(rv => rv.Medecin.ID == ID).ToList();
    }
}
