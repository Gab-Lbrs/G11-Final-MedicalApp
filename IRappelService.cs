using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public interface IRappelService
    {
       
        /// Service d’événements de rappel pour les rendez-vous.
        


        /// <summary>
        /// Levé lorsqu’un rappel arrive à échéance.
        /// </summary>
        event EventHandler<RendezVous> RdvEnApproche;

        /// <summary>
        /// Planifie un rappel pour le RDV donné, X temps avant la dateDebut.
        /// </summary>
        void PlanifierRappel(RendezVous rdv, TimeSpan avant);
    }
}
