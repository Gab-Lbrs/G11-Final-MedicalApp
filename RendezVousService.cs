using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public class RendezVousService : IRendezVousService
    {
        public RendezVousService() {}

        private IList<RendezVous> store = new List<RendezVous>();
        public List<RendezVous> ListerPourMedecin(int ID)
        {
            //liste pour medecin a inmplementer
            List<RendezVous> RdvList = new List<RendezVous>();
            return RdvList;
        }


        public List<RendezVous> ListerTousLesRdv()
        {
            List<RendezVous> RdvList = new List<RendezVous>();
            return RdvList;
        }

        public RendezVous PrendreRdv(Patient patient, Medecin medecin, DateTime dateDebut, TimeSpan duree)
        {
            var rv = new RendezVous { Patient = patient, DateDeRdv = dateDebut, Duree = duree };
            
           
            store.Add(rv);
            return rv;
        }

        public IEnumerable<RendezVous> ListerTouslesRendezVous() => store;
        public IEnumerable<RendezVous> ListeMedecin(int medecinID)
        {
            return store.Where(rv => rv.Medecin.ID == medecinID );
        }
    }
}
