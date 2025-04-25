
namespace G11_Final_MedicalApp
{
    [Serializable]
    internal class ConflitRendezVousException : Exception
    {
        public ConflitRendezVousException()
            : base("Conflit de rendez-vous : ce créneau est déjà réservé.")
        {
        }

        public ConflitRendezVousException(string? message) : base(message)
        {
        }

        public ConflitRendezVousException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}