using Automate.Data.Model;
using Automate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class LumieresController
    {
        public static void PostLumiere(List<Lumiere> MesureLumieres, List<string> dates)
        {
            string requete = "INSERT INTO `afpa_lumieres`(`ValeurLumiere`, `DateLumiere`) VALUES ";
            for (int i = 0; i < MesureLumieres.Count(); i++)
            {
                requete += "( " + MesureLumieres[i].ValLumiere.Replace(",", ".") + " , '" + dates[i] + "') , ";
            }
            requete = requete.Substring(0, requete.Length - 2) + " ;";
            DAO.Post(requete);
        }
    }
}
