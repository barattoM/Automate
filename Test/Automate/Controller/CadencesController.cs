using Automate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class CadencesController
    {
        public static void PostCadence(int cadence, string date)
        {
            string requete = "INSERT INTO `afpa_cadences`( `NbProduit`, `DateCadence`) VALUES(" + cadence + ",'" + date + "')";
            DAO.Post(requete);
        }
    }
}
