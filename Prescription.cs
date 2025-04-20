using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    // Donner par doctor et voyable par staff
    internal interface Prescription
    {
        public string Medicament { get; set; } = string.Empty;
        public int DureeJours { get; set; }
    }
}
