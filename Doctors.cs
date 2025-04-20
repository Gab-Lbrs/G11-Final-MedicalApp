namespace G11_Final_MedicalApp
{
    internal class Doctors : HospitalMember
    {

        public List<Consultation> Consultations { get; } = new();




        public void ConsignerConsultation(Consultation consultation)
        {
            consultation.Rdv.Doctors = this;
            Consultations.Add(consultation);
        }

        public void Status()
        {
            Console.WriteLine($"Docteur assignier :{Name} ");
            Console.WriteLine($"ID : {ID}");
        }

        

       


    }
}
