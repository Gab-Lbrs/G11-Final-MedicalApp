using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public class DossierMedicale<T>
    {

        private  List<T> dossiers = new();
        public void Ajouter(T dossierT) => dossiers.Add(dossierT);

    }

    
}
