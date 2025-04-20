using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    internal class Staff : HospitalMember
    {
        private string post;
        public Staff(string name, string lastName, int age, int id, string post) : base(name, lastName, age, id)
        {
            this.post = post;
        }

    
        
    } 

        
    
}
