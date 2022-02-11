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
            List<List<object>> donnees =DAO.Get(requete,6);
            //donnees[0][0] : seuilBas ; donnees[0][1] : SeuilHaut ; donnees[0][3] : Temps
            Seuil seuil = new Seuil((float)donnees[0][0], (float)donnees[0][1], (int)donnees[0][3]);
            return seuil;
        }
    }
}
