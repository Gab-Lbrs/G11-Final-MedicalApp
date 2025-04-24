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

        public enum Role
        {
            Patient,
            Medecin,
            Administratif
        }
        //Initial variable
        private string name;
        private string lastName;
        private int age;
        private int id;

        private string username;
        private string passwordHash;
        public Role role { get; set; }

        //Constructer


        public HospitalMember ()
        {

        }
        public HospitalMember(string name, string lastName, int age, int id, string username,string passwordHash)
        {
            this.name = name;
            this.lastName = lastName;
            this.age = age;
            this.id = id;
            this.username = username;
            this.passwordHash = passwordHash; 
            
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

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; }
        }



    }


}
