using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public interface IPrescriptionService
    {

        //Ajouter une ordonance au dossier

        void AjouterPres(Prescription prescriptions);


        //Recherche des presciptions de patients
        IEnumerable<Prescription> GetPatPresc(int id);



    }
}
