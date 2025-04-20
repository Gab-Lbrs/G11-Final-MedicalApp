namespace G11_Final_MedicalApp
{
   public class Program
   {
        static void Main(string[] args)
        {

            var Dossier1 = new DossierMedicale<Consultation>();
        }

        public enum RendezVousStatus
        {
            EnAttente,
            Valide,
            Annule
        }

        public class RendezVous
        {
            public DateTime DateDebut { get; set; }
            public TimeSpan Duree { get; set; }
            public Patients Patient { get; set; } = null!;
            public Doctors Doctor { get; set; } = null!;
            public RendezVousStatus Status { get; set; } = RendezVousStatus.EnAttente;
        }


        public class ConflitRendezVousException : Exception
{
    public ConflitRendezVousException(string message) : base(message) { }
}

public class UtilisateurNonAutoriseException : Exception
{
    public UtilisateurNonAutoriseException(string message) : base(message) { }
}


    }

    

}

