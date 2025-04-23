using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    // Donner par Doctor et voyable par Staff et Patient
    public class Prescription
    {
        /// <summary>
        /// Indiquation de la medication.
        /// </summary>
        public required string Medicament { get; set; }

        /// <summary>
        /// 'x' de comprimer par jours 'x' par jours
        /// </summary>
        public  required int Posologie { get; set; }

        /// <summary>
        /// Duree de tritement en JOURS 
        /// </summary>
        public required int DureeTraitement { get; set; }
    

        public override string ToString()
        {
            return ($"{Medicament} – {Posologie} pendant" +
                $" {DureeTraitement} jour(s)");
        }

    }
}

