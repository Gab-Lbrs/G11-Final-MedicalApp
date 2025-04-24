using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public class Staff : HospitalMember
    {
        private string post;
        public string Post { get; set; }
        public Staff()
        {

        }
        public Staff(string name, string lastName, int age, int id,string username, string passwordHash, string post) : base(name, lastName, age, id, username, passwordHash)
        {
           post = Post;
        }

        public enum RendezVousStatus
        {
            EnAttente,
            Valide,
            Annule
        }

        /// Valide un rendez‑vous, en passant son statut à « Valide ».  
        public void ValiderRendezVous(RendezVous rdv)
        {
            if (rdv == null) throw new ArgumentNullException(nameof(rdv));
            rdv.Status = RendezVousStatus.Valide;
        }
        


        /// Annule un rendez‑vous, en passant son statut à « Annule ».
        public void AnnulerRendezVous(RendezVous rdv)
        {
            if (rdv == null) throw new ArgumentNullException(nameof(rdv));
            rdv.Status = RendezVousStatus.Annule;
        }

        public override string ToString()
            => $"Administratif : {Name} {LastName} (ID : {ID})";

        
    } 

        
    
}
