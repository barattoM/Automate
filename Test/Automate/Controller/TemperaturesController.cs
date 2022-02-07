using Automate.Data.Model;
using Automate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class TemperaturesController
    {
        public static void PostTemperature(List<Temperature> MesureTemperatures, List<string> dates)
        {
            string requete = "INSERT INTO `afpa_temperatures`(`ValeurTemperature`, `DateTemperature`) VALUES ";
            for (int i = 0; i < MesureTemperatures.Count(); i++)
            {
                requete += "( " + MesureTemperatures[i].ValTemperature.Replace(",", ".") + " , '" + dates[i] + "') , ";
            }
            requete = requete.Substring(0, requete.Length - 2) + " ;";
            DAO.Post(requete);
        }
    }
}
