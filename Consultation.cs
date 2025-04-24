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
        private string diagnostic;
        public  RendezVous Rdv {  get; set; }
        public  string Diagnostic { get; set; }
        public IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();


        public Consultation() { }
        public Consultation(RendezVous rdv, string diag)
        {
            Rdv = rdv;
            this.diagnostic = diag;
        }

      

        
        public void AJouterPresciption(Prescription p)
        {
            if(p == null) throw new ArgumentNullException(nameof(p));
            Prescriptions.Add(p);
        }



    }
}
