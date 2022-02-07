using Automate.Data.Model;
using Automate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class SonsController
    {
        public static void PostSon(List<Son> MesureSons, List<string> dates)
        {
            string requete = "INSERT INTO `afpa_sons`(`ValeurSon`, `DateSon`) VALUES ";
            for (int i = 0; i < MesureSons.Count(); i++)
            {
                requete += "( " + MesureSons[i].ValSon.Replace(",", ".") + " , '" + dates[i] + "') , ";
            }
            requete = requete.Substring(0, requete.Length - 2)+" ;";
            DAO.Post(requete);
        }
    }
}
