using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public interface IRendezVousService
    {
        // <summary>
        /// Prend un nouveau rendez-vous pour un patient et un médecin,  
        /// ou lève ConflitRendezVousException en cas de chevauchement.
        /// </summary>
        void PrendreRdv(Patient patient, Medecin medecin, DateTime dateDebut, TimeSpan duree);

        /// <summary>
        /// Liste tous les rendez-vous (tous statuts confondus).
        /// </summary>
        List<RendezVous> ListerTousLesRdv();


        /// <summary>
        /// Liste les rendez-vous d’un ID donné.
        /// </summary>
        List<RendezVous> ListerPourMedecin(int ID);
    }
}
