using G11_Final_MedicalApp;
using System.Runtime.Intrinsics.Arm;
using System.Collections.Generic;
using static G11_Final_MedicalApp.Staff;
using System.Linq;

namespace G11_Final_MedicalApp
{
    public class Program
    {

        private static IRendezVousService rdvService = new RendezVousService();
        static void Main(string[] args)
        {

            

            Patient patient1 = new Patient
            {Name = "Dupont",LastName = "Martin",Age = 30,ID = 555,Username = "DMartin", PasswordHash = "123"};

           Medecin med1 = new Medecin
           {Name = "Bhaal", LastName = "Ball", Age = 30, ID = 222, Username ="Bball", PasswordHash="321"};

            Staff admin1 = new Staff
            { Name = "sophie", LastName = "fire", Age = 22, ID = 666, Username = "Fsophie", PasswordHash = "666" } ;
                    
            var users = new List<HospitalMember> {patient1, med1, admin1 };

            users.Add(patient1 );
            users.Add(med1);
            users.Add(admin1);


            IRendezVousService rdvService = new RendezVousService();
            var authService = new AuthService(users);
            

            Console.WriteLine("~~~~~~~~~~~~~~~ Programme  Medical ~~~~~~~~~~~~~~~" );

            // Interface de Connexion
            Console.Write("Login : ");
            var userName = Console.ReadLine() ?? "";

            Console.Write("Password : ");
            var password = Console.ReadLine() ?? "";

            var currentUser = authService.Login(userName, password);

            if (currentUser == null || userName != currentUser.Username && password != currentUser.PasswordHash)
            {
                Console.WriteLine("Identifiant Incorrect. ");
                return;
            }

            //Message de confirmation de login

            Console.WriteLine($"Bienvenue {currentUser.Name}\n");


            //Affiche le menu selon le "role" du user
            if (currentUser is Patient patientX)
            {
                ShowPatientMenu(patientX , rdvService);
            }
            else if (currentUser is Medecin MedecinX)
            {
                ShowMedecinMenu(MedecinX, rdvService);
            } else if (currentUser is Staff adminX)
            {
                ShowStaffMenu(adminX, rdvService);
            }



            //patient1.PrendreRendezVous(
            //    DateTime.Parse("2025-06-05"),
            //    TimeSpan.FromHours(2.5)
            //    );

            //var rdv = patient1.Agenda[0];


            //Console.WriteLine($"\nRendez-vous dans l'Agenda : {patient1.Agenda.Count}");
            //Console.WriteLine($"Statut  du RDV : {rdv.Status}");
        }

        static void ShowPatientMenu(Patient pat, IRendezVousService rendezVousService)
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
                                Console.WriteLine("Rendez-Vous ajouté !");

                                
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

        static void ShowMedecinMenu(Medecin med, IRendezVousService rendezVousService)
        {
            string choix;
            do
            {
                Console.WriteLine("\n--- MENU MÉDECIN ---");
                Console.WriteLine("1 : Consigner une consultation");
                Console.WriteLine("2 : Lister mes consultations");
                Console.WriteLine("0 : Retour");
                Console.Write("Votre choix : ");
                choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        // Consigner une nouvelle consultation
                        if (med.Consultations == null)
                        {
                            Console.WriteLine("Erreur : liste de consultations non initialisée.");
                            break;
                        }

                        // On demande le RDV associé
                        Console.Write("Date du RDV (yyyy-MM-dd HH:mm) : ");
                        if (!DateTime.TryParse(Console.ReadLine(), out var date))
                        {
                            Console.WriteLine("Date invalide.");
                            break;
                        }
                        // On crée un rendez-vous factice (en vrai, il faudrait le prendre depuis l'agenda)
                        var rdv = new RendezVous
                        {
                            DateDeRdv = date,
                            Duree = TimeSpan.FromHours(1),
                            Medecin = med,
                            Patient = null!   // a adapter : recuperer un patient existant
                        };

                        Console.Write("Diagnostic : ");
                        var diag = Console.ReadLine() ?? "";


                        var consult = new Consultation
                        {
                            Rdv = rdv,
                            Diagnostic = diag
                        };

                        med.ConsignerConsultation(consult);
                        Console.WriteLine("Consultation consignée.");
                        break;

                    case "2":
                        Console.WriteLine($"\nVous avez {med.Consultations.Count} consultations :");
                        foreach (var c in med.Consultations)
                        {
                            Console.WriteLine($"- {c}");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Retour au menu principal.");
                        break;

                    default:
                        Console.WriteLine("Choix non reconnu, réessayez.");
                        break;
                }

            } while (choix != "0");
        }

        static void ShowStaffMenu(Staff adm, IRendezVousService rendezVousService)
        {
            
            string choix;
            do
            {
                Console.WriteLine("\n--- MENU ADMINISTRATIF ---");
                Console.WriteLine("1 : Lister tous les RDV en attente");
                Console.WriteLine("2 : Valider un RDV");
                Console.WriteLine("3 : Annuler un RDV");
                Console.WriteLine("0 : Retour");
                Console.Write("Votre choix : ");
                choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        
                        var listRdv = rendezVousService.ListerTousLesRdv();
                        var enAttente = listRdv
                            .Where(rv => rv.Status == RendezVousStatus.EnAttente)
                            .ToList();
                        Console.WriteLine($"\nRDV en attente ({enAttente.Count}) :");
                        foreach (var rv in enAttente)
                        {
                            Console.WriteLine($"- {rv.DateDeRdv:yyyy-MM-dd HH:mm} " +
                                              $"Patient: {rv.Patient.Name} {rv.Patient.LastName}");
                        }
                        break;

                    case "2":
                        Console.Write("Date du RDV à valider (yyyy-MM-dd HH:mm) : ");
                        if (DateTime.TryParse(Console.ReadLine(), out var dateVal))
                        {
                            var rdv = rdvService.ListerTousLesRdv()
                                        .FirstOrDefault(rv => rv.DateDeRdv == dateVal);
                            if (rdv != null)
                            {
                                adm.ValiderRendezVous(rdv);
                                Console.WriteLine("RDV validé.");
                            }
                            else
                            {
                                Console.WriteLine("RDV introuvable.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Date invalide.");
                        }
                        break;

                    case "3":
                        Console.Write("Date du RDV à annuler (yyyy-MM-dd HH:mm) : ");
                        if (DateTime.TryParse(Console.ReadLine(), out var dateAnn))
                        {
                            var allRdv = rdvService.ListerTousLesRdv();
                            var targetRdv = allRdv.FirstOrDefault(rv => rv.DateDeRdv == dateAnn);
                            if (targetRdv != null)
                            {
                                adm.AnnulerRendezVous(targetRdv);
                                Console.WriteLine("RDV annulé.");
                            }
                            else
                            {
                                Console.WriteLine("RDV introuvable.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Date invalide.");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Retour au menu principal.");
                        break;

                    default:
                        Console.WriteLine("Choix non reconnu, réessayez.");
                        break;
                }

            } while (choix != "0");
        }
    }

}

    



