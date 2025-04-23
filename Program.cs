using G11_Final_MedicalApp;
using System.Runtime.Intrinsics.Arm;
using System.Collections.Generic;

namespace G11_Final_MedicalApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            

            Patient patient1 = new Patient
            {Name = "Dupont",LastName = "Martin",Age = 30,ID = 555,Username = "DMartin", PasswordHash = "123"};

            Medecin doc1 = new Medecin("bhaal", "fourire", 56, 222, "Bfourire", " 321");

            Staff admin1 = new Staff { Name = "Sophie", LastName = "Durand", Age = 34, ID = 112, Post = "Reception" };


            var users = new List<HospitalMember> {patient1, doc1, admin1 };

            users.Add(patient1 );
            users.Add(doc1);
            users.Add(admin1);

            var authService = new AuthService(users);

            Console.WriteLine("~~~~~~~~~~~~~~~ Programme  Medical ~~~~~~~~~~~~~~~" );

            // Interface de Connexion
            Console.Write("Login : ");
            var userName = Console.ReadLine() ?? "";

            Console.Write("Password : ");
            var password = Console.ReadLine() ?? "";

            var currentUser = authService.Login(userName, password);

            if (currentUser == null)
            {
                Console.WriteLine("Identifiant Incorrect. ");
                return;
            }

            //Message de confirmation de login

            Console.WriteLine($"Bienvenue {currentUser.Name} ({currentUser.role})\n");


            //Affiche le menu selon le "role" du user
            if (currentUser is Patient patientX)
            {
                ShowPatientMenu(patientX);
            }

            ShowPatientMenu(patient1);

            patient1.PrendreRendezVous(
                DateTime.Parse("2025-06-05"),
                TimeSpan.FromHours(2.5)
                );

            var rdv = patient1.Agenda[0];

             
            Console.WriteLine($"\nRendez-vous dans l'Agenda : {patient1.Agenda.Count}");
            Console.WriteLine($"Statut  du RDV : {rdv.Status}");
        }

        static void ShowPatientMenu(Patient pat)
        {
            string choix;
            do
            {
                Console.WriteLine("\n--- MENU PATIENT ---");
                Console.WriteLine("1 : Prendre un rendez-vous");
                Console.WriteLine("2 : Lister mes rendez-vous");
                Console.WriteLine("0 : Quitter");
                Console.Write("Votre choix : ");
                choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        // Demander la date et la durée
                        Console.Write("Date (yyyy-MM-dd HH:mm) : ");
                        if (DateTime.TryParse(Console.ReadLine(), out var date))
                        {
                            Console.Write("Durée en heures (ex. 1.5) : ");
                            if (double.TryParse(Console.ReadLine(), out var heures))
                            {
                                pat.PrendreRendezVous(date, TimeSpan.FromHours(heures));
                                Console.WriteLine("RDV ajouté !");
                            }
                            else
                            {
                                Console.WriteLine("Durée invalide.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Date invalide.");
                        }
                        break;

                    case "2":
                        Console.WriteLine($"\nVous avez {pat.Agenda.Count} RDV :");
                        foreach (var rdv in pat.Agenda)
                            Console.WriteLine($"- {rdv.DateDeRdv:yyyy-MM-dd HH:mm} " +
                                              $"({rdv.Duree.TotalHours}h) statut : {rdv.Status}");
                        break;

                    case "0":
                        Console.WriteLine("Au revoir !");
                        break;

                    default:
                        Console.WriteLine("Choix non reconnu, réessayez.");
                        break;
                }

            } while (choix != "0");
        }
    }

}

    



