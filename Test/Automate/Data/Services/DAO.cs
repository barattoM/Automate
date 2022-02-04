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

        static public MySqlDataReader Get(string requete)
        {
            MySqlCommand com = new MySqlCommand(requete, con);
            Connexion();
            MySqlDataReader reader = com.ExecuteReader();
            Deconnexion();
            return reader;
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
