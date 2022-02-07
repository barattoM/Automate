using Automate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Controller
{
    class AnomaliesController
    {
        public static void PostAnomalies(List<List<string>> tabAnomalies, int cadence)
        {
            string requete = "INSERT INTO `afpa_anomalies`(`DateAnomalie`, `TypeAnomalie`, `NbDeclasses`, `IdErreur`) VALUES ";
            Random rand = new Random();
            foreach (List<string> anomalie in tabAnomalies)
            {
                if (anomalie[1] == "lumiere")
                {
                    // Si la cadence est a 0 soit la production est en pause, on met le nombre de declassé a 0 puisque le rand commence a 1 et cadence = 0; 
                    if (cadence == 0)
                    {
                        requete += " ( '" + anomalie[0] + "' , '" + anomalie[1] + "' , '0' , '" + anomalie[3] + "') , ";
                    }
                    else
                    {
                        requete += " ( '" + anomalie[0] + "' , '" + anomalie[1] + "' , '" + rand.Next(1, (cadence / 2)) + "' , '" + anomalie[3] + "') , ";
                    }


                }
                else
                {
                    requete += " ( '" + anomalie[0] + "' , '" + anomalie[1] + "' , '" + anomalie[2] + "' , '" + anomalie[3] + "') , ";
                }
            }
            requete = requete.Substring(0, requete.Length - 2)+ ";";
            DAO.Post(requete);
        }
        
    }
}
