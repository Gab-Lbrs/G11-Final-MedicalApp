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

        public void PrendreRdv(Patient patient, Medecin medecin, DateTime dateDebut, TimeSpan duree)
        {
            throw new NotImplementedException();
        }
    }
}
