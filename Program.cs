using G11_Final_MedicalApp;
using System.Runtime.Intrinsics.Arm;
using System.Collections.Generic;
using static G11_Final_MedicalApp.Staff;
using static G11_Final_MedicalApp.Consultation;
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


           

            var authService = new AuthService(users);


            var rappelService = new RappelService();
            rappelService.RdvEnApproche += (s, rdv) =>
            {
                Console.WriteLine($"\n[ RAPPEL ] RDV imminent le {rdv.DateDeRdv:yyyy-MM-dd HH:mm} " +
                                  $"pour {rdv.Patient.Name} {rdv.Patient.LastName}");
                Console.Write("Votre choix : ");
            };



            while (true)
            {

                Console.WriteLine("~~~~~~~~~~~~~~~ Programme  Medical ~~~~~~~~~~~~~~~");

                // Interface de Connexion
                Console.Write("Login : ");
                var userName = Console.ReadLine() ?? "";

                Console.Write("Password : ");
                var password = Console.ReadLine() ?? "";

                var currentUser = authService.Login(userName, password);

                if (currentUser == null || userName != currentUser.Username && password != currentUser.PasswordHash)
                {
                    Console.WriteLine("Identifiant Incorrect. ");
                    Console.ReadKey();
                    continue;
                }

                //Message de confirmation de login

                Console.WriteLine($"Bienvenue {currentUser.Name}\n");


                //Affiche le menu selon le "role" du user
                if (currentUser is Patient patientX)
                {
                    ShowPatientMenu(patientX, med1, rdvService, rappelService);
                }
                else if (currentUser is Medecin MedecinX)
                {
                    ShowMedecinMenu(MedecinX, rdvService);
                } 
                else if (currentUser is Staff adminX)
                {
                    ShowStaffMenu(adminX, rdvService);
                }


                Console.Write("\nSe déconnecter et revenir au login ? (O/N) : ");
                var key = Console.ReadLine() ?? "";
                if (!key.Equals("O", StringComparison.OrdinalIgnoreCase))
                    break;  // exit outer loop - end program
            }


            }

        static void ShowPatientMenu(Patient pat,Medecin med , IRendezVousService rendezVousService, IRappelService rappelService)
        {
            string choix;
            do
            {
                Console.WriteLine("\n--- MENU PATIENT ---");
                Console.WriteLine("1 : Prendre un rendez-vous");
                Console.WriteLine("2 : Lister mes rendez-vous");
                Console.WriteLine("0 : Se déconnecter");
                Console.Write("Votre choix : ");
                choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        // 1) Saisie date & durée
                        Console.Write("Date (yyyy-MM-dd HH:mm) : ");
                        if (!DateTime.TryParse(Console.ReadLine(), out var date)) { Console.WriteLine("Date invalide."); break; }
                        Console.Write("Durée (heures) : ");
                        if (!double.TryParse(Console.ReadLine(), out var h)) { Console.WriteLine("Durée invalide."); break; }

                        // 2) Création via le service on récupère le RendezVous
                        
                        var nouveauRdv = rendezVousService.PrendreRdv(pat, med, date, TimeSpan.FromHours(h));
                        // 3) On le met aussi dans l'agenda du patient
                        if (nouveauRdv == null)
                        {
                            Console.WriteLine("Erreur interne : impossible de créer le RDV.");
                            break;
                        }
                        Console.WriteLine($"RDV ajouté pour {nouveauRdv.DateDeRdv}.");

                        //pat.Agenda.Add(nouveauRdv);

                        rappelService.PlanifierRappel(nouveauRdv, TimeSpan.FromMinutes(30));

                        Console.WriteLine($"RDV ajouté pour {nouveauRdv.DateDeRdv:yyyy-MM-dd HH:mm}. " +
                                          $"Rappel 30 min avant programmé.");
                        break;

                    case "2":
                        Console.WriteLine($"\nVous avez {pat.Agenda.Count} RDV :");
                        foreach (var rv in pat.Agenda)
                            Console.WriteLine($"- {rv.DateDeRdv:yyyy-MM-dd HH:mm} (statut : {rv.Status})");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Choix non reconnu.");
                        break;
                }
            } while (true);
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
                        var rdvs = rdvService.ListerPourMedecin(med.ID).Where(rv => rv.Status == RendezVousStatus.Valide || rv.Status == RendezVousStatus.EnAttente)
                    .ToList();
                        if (!rdvs.Any())
                        {
                            Console.WriteLine("Vous n'avez aucun rendez-vous à consigner.");
                            break;
                        }

                        // → Affiche numéroté
                        Console.WriteLine("Liste des RDV du médecin :");
                        for (int i = 0; i < rdvs.Count; i++)
                        {
                            var r = rdvs[i];
                            Console.WriteLine(
                                $"{i + 1}. {r.DateDeRdv:yyyy-MM-dd HH:mm} — Statut : {r.Status}"
                            );
                        }

                        Console.WriteLine("Sélectionnez le numéro du RDV à consigner :");
                        for (int i = 0; i < rdvs.Count; i++)
                        {
                            var r = rdvs[i];
                            Console.WriteLine($"{i + 1}. {r.DateDeRdv:yyyy-MM-dd HH:mm} " +
                                              $"Patient: {r.Patient.Name} {r.Patient.LastName} (Statut: {r.Status})");
                        }

                        // 3) Lit le choix et vérifie
                        if (!int.TryParse(Console.ReadLine(), out int idx)
                            || idx < 1 || idx > rdvs.Count)
                        {
                            Console.WriteLine("Choix invalide.");
                            break;
                        }
                        // On crée un rendez-vous factice (en vrai, il faudrait le prendre depuis l'agenda)

                        var rdvChoisi = rdvs[idx - 1];


                   

                        Console.Write("Diagnostic : ");
                        var diag = Console.ReadLine() ?? "";


                        var consult = new Consultation(rdvChoisi, diag);

                        med.ConsignerConsultation(consult);

                        rdvChoisi.Patient.DossierConsultations.Ajouter(consult);

                        Console.WriteLine("Consultation consignée.");


                        break;

                    case "2":
                        Console.WriteLine($"\nVous avez {med.Consultations.Count} consultations :");
                        foreach (var c in med.Consultations)
                        {
                            Console.WriteLine($"- {c.Rdv.DateDeRdv:yyyy-MM-dd HH:mm} : {c.Diagnostic}");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Retour au menu principal.");
                        return;


                    default:
                        Console.WriteLine("Choix non reconnu, réessayez.");
                        break;
                }

            } while (true);
        }

        static void ShowStaffMenu(Staff adm, IRendezVousService rdvService)
        {
            
            string choix;
            var enAttente = rdvService.ListerTousLesRdv()
                           .Where(rv => rv.Status == RendezVousStatus.EnAttente)
                           .ToList();

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
                        Console.WriteLine($"\nRDV en attente ({enAttente.Count}) :");
                        for (int i = 0; i < enAttente.Count; i++)
                        {
                            var r = enAttente[i];
                            Console.WriteLine(
                                $"{i + 1}. {r.DateDeRdv:yyyy-MM-dd HH:mm}  " +
                                $"Patient: {r.Patient.Name} {r.Patient.LastName}"
                            );
                        }
                        break;

                    case "2":
                        if (!enAttente.Any())
                        {
                            Console.WriteLine("Aucun RDV en attente.");
                            break;
                        }

                        Console.Write("Numéro du RDV à valider : ");
                        if (int.TryParse(Console.ReadLine(), out int numVal)
                            && numVal >= 1 && numVal <= enAttente.Count)
                        {
                            var rdvToValider = enAttente[numVal - 1];
                            adm.ValiderRendezVous(rdvToValider);
                            Console.WriteLine($"RDV #{numVal} validé (statut : {rdvToValider.Status}).");
                        }
                        else
                        {
                            Console.WriteLine("Numéro invalide.");
                        }
                        break;

                    case "3":
                        if (!enAttente.Any())
                        {
                            Console.WriteLine("Aucun RDV en attente à annuler.");
                            break;
                        }

                        Console.Write("Numéro du RDV à annuler : ");
                        if (int.TryParse(Console.ReadLine(), out int numAnn)
                            && numAnn >= 1 && numAnn <= enAttente.Count)
                        {
                            var rdvToAnnuler = enAttente[numAnn - 1];
                            adm.AnnulerRendezVous(rdvToAnnuler);
                            Console.WriteLine($"RDV #{numAnn} annulé (statut : {rdvToAnnuler.Status}).");
                        }
                        else
                        {
                            Console.WriteLine("Numéro invalide.");
                        }
                        break;

                    case "0":
                        // on sort et on revient au login/menu principal
                        return;

                    default:
                        Console.WriteLine("Choix non reconnu, réessayez.");
                        break;
                }

            } while (true);
        }
    }

}

    



