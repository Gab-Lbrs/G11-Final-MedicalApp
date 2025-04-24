using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public interface IRappelService
    {
        /// <summary>
        /// Service d’événements de rappel pour les rendez-vous.
        /// </summary>
        public interface IRappelService
        {
            /// <summary>
            /// Événement déclenché 24 h avant un rendez‑vous.
            /// </summary>
            event EventHandler<RendezVous> RdvEnApproche;

            /// <summary>Vérifie la date du rdv et déclenche l’événement si proche.</summary>
            void VerifierEtNotifier(RendezVous rendezVous);
        }
    }
}
