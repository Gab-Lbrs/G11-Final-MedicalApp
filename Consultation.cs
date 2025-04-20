using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace G11_Final_MedicalApp
{
    //Consultation 
    internal interface Consultation
    {
        public RendezVous Rdv { get; set; } = null!;
        public string Diagnostic { get; set; } = string.Empty;
        public IList<Prescription> Prescriptions { get; } = new List<Prescription>();



    }
}
