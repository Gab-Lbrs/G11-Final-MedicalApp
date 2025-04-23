namespace G11_Final_MedicalApp
{
    public class Medecin : HospitalMember
    {

        public IList<Consultation> Consultations { get; set; } =
            new List<Consultation>();
      
        public Medecin(string nom, string lastname, int age, int id, string username, string passwordHash)
           : base(nom, lastname, age, id, username, passwordHash)
        { 
           
        }

      public Medecin() : base("","", 0,0,"","") { 
        }


        public void ConsignerConsultation(Consultation consultation)
        {
            if (consultation == null)
                throw new ArgumentNullException(nameof(consultation));

            consultation.Rdv.Medecin = this;
            Consultations.Add(consultation);
        }


        // Retourne une description simple du médecin.
        public override string ToString()
            => $"{Name} {LastName} (ID: {ID} , {Role.Medecin})";



    }
}
