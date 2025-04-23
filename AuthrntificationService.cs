using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_Final_MedicalApp
{
    public class AuthService
    {
        // Stockage en mémoire pour l’exemple
        private readonly List<HospitalMember> users;

        public AuthService(IEnumerable<HospitalMember> users)
        {
            users = users.ToList() ??
                throw new ArgumentNullException(nameof(users));
        }

        /// <summary>Retourne l’utilisateur si les infos sont bonnes, sinon null.</summary>
        public HospitalMember? Login(string username, string password)
        {
            // Recherche l’utilisateur
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // Compare le mot de passe (clair ici)
             return users
           .FirstOrDefault(u =>
               u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
               && u.PasswordHash.Equals(password, StringComparison.OrdinalIgnoreCase));
        }
    }
}
