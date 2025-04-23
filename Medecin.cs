namespace G11_Final_MedicalApp
{
    public class Medecin : HospitalMember
    {

        public IList<Consultation> Consultations { get; set; } =
            new List<Consultation>();
      
      

        public Medecin(string name, string lastName, int age, int id, string username, string passwordHash) : base(name, lastName, age, id, username, passwordHash)
        {
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
