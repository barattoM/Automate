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

        /**
         * param name="requete" La requete SQL
         * param name="nbColonnes" le nombre de colonnes dans la table (id inclue)
         *  returns Tableau contenant les données pour chaque colonnes (id exclue)
         */
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

        static public void Set(string requete)
        {
            MySqlCommand com = new MySqlCommand(requete, con);
            Connexion();
            com.ExecuteReader();
            Deconnexion();
        }
    }
}
