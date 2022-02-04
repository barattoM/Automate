using Automate.Data.Model;
using Automate.Data.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class CouleursController
    {
        
        public static List<Couleur> getCouleurs()
        {
            string requete = "SELECT * FROM `afpa_couleurs`";
            MySqlDataReader donnees = DAO.Get(requete);
            List<Couleur> Couleurs = new List<Couleur>();
            donnees.Read();
            Couleur Bas = new Couleur();
            Bas.Red=donnees.GetInt32("Red");
            Bas.Green = donnees.GetInt32("Green");
            Bas.Blue = donnees.GetInt32("Blue");
            donnees.Read();
            Couleur OK = new Couleur();
            OK.Red = donnees.GetInt32("Red");
            OK.Green = donnees.GetInt32("Green");
            OK.Blue = donnees.GetInt32("Blue");
            donnees.Read();
            Couleur Haut = new Couleur();
            Haut.Red = donnees.GetInt32("Red");
            Haut.Green = donnees.GetInt32("Green");
            Haut.Blue = donnees.GetInt32("Blue");
            Couleurs.Add(Bas);
            Couleurs.Add(OK);
            Couleurs.Add(Haut);
            return Couleurs;
        }
    }
}
