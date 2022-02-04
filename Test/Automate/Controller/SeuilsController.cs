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
    static class  SeuilsController
    {
        
        // Nature 1 : Temperature ; 2 : Son ; 3 : Lumiere
        public static Seuil getSeuils(int nature)
        {
            string requete = "SELECT * FROM `afpa_seuils` WHERE nature = "+nature+" ORDER BY `DateSeuil` DESC LIMIT 1;";
            MySqlDataReader donnees =DAO.Get(requete);
            Seuil seuil = new Seuil();
            donnees.Read();
            seuil.SeuilBas = donnees.GetFloat("SeuilBas");
            seuil.SeuilHaut = donnees.GetFloat("SeuilHaut");
            seuil.Temps = donnees.GetInt32("Temps");
            return seuil;
        }
    }
}
