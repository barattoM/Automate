using Automate.Data.Model;
using Automate.Data.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            List<List<object>> donnees = DAO.Get(requete,4);
            List<Couleur> Couleurs = new List<Couleur>();
            for (int i=0;i<donnees.Count;i++)
            {
                Couleurs.Add(new Couleur((int)donnees[i][0],(int)donnees[i][1],(int)donnees[i][2])) ;
            }
            return Couleurs;
        }
    }
}
