using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace G11_Final_MedicalApp
{
    //Consultation 
    public class Consultation
    {
        private string diag;

        public Consultation(RendezVous rdv, string diag)
        {
            Rdv = rdv;
            this.diag = diag;
        }

        public Consultation() { }

        public required RendezVous Rdv {  get; set; }
        public required string Diagnostic { get; set; }
        public IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        public void AJouterPresciption(Prescription p)
        {
            if(p == null) throw new ArgumentNullException(nameof(p));
            Prescriptions.Add(p);
        }



    }
}
