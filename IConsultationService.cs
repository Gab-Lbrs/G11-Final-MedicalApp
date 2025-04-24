using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public interface IConsultationService
    {

        /// <summary>Ajoute une nouvelle consultation.</summary>
        void Consigner(Consultation consultations);

        /// <summary>Récupère les consultations d’un patient.</summary>
        IEnumerable<Consultation> RechercherParPatient(int ID);
    }
}
