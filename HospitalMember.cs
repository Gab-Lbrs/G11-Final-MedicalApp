using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    // Mother Class
    public abstract class HospitalMember
    {
        //Initial variable
        private string name;
        private string lastName;
        private int age;
        private int id;
        //Constructer

        public HospitalMember(string name, string lastName, int age, int id)
        {
            this.name = name;
            this.lastName = lastName;
            this.age = age;
            this.id = id;
        }
        // Setter and getters for variables
        public string Name
        {
            get { return name; }
            set { name = value; } 
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }




    }


}
