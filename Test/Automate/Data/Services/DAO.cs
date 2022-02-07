using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Data.Services
{
    static class DAO
    {
        static string conString = "Server=localhost;Database=automate;port=3306;UserId=root;password=";
        static MySqlConnection con = new MySqlConnection(conString);

        static public void Connexion()
        {
            con.Open();
        }

        static public void Deconnexion()
        {
            con.Close();
        }

        
        /// <summary>
        /// Recupère les données en base de données grâce à la requête en paramètre.
        /// </summary>
        /// <param name="requete">La requête à envoyer en base</param>
        /// <param name="nbColonnes">Le nombre de colonnes dans la tables (avec id)</param>
        /// <returns>Tableau contenant les données (sauf id) dans le même ordre que dans la base</returns>
        static public List<List<object>> Get(string requete, int nbColonnes)
        {
            MySqlCommand com = new MySqlCommand(requete, con);
            Connexion();
            MySqlDataReader reader = com.ExecuteReader();
            List<List<object>> tab = new List<List<object>>();
            while (reader.Read())
            {
                List<object> donnees = new List<object>();
                for (int i=1;i<nbColonnes;i++)
                {
                    donnees.Add(reader.GetValue(i));
                }
                tab.Add(donnees);
            }
            Deconnexion();
            return tab;
        }

        /// <summary>
        /// Envoi les données en base.
        /// </summary>
        /// <param name="requete">La requête à envoyer en base</param>
        static public void Post(string requete)
        {
            MySqlCommand com = new MySqlCommand(requete, con);
            Connexion();
            com.ExecuteReader();
            Deconnexion();
        }
    }
}
