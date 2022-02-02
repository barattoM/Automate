using Interface_Carte_dAquisition_PicoDrDAQ;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automate
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean AutoScroll = true;

        short handle;
        uint echantillonnage = 500;
        ushort overflow;
        short son;
        short temp;
        short lum;
        int tempsAcquisition=3000; //En ms

        List<string> mesureTemperature = new List<string>();
        List<string> mesureSon = new List<string>();
        List<string> mesureLumiere = new List<string>();
        List<string> tabDate = new List<string>();
        int TailleEnvoi = 2;
        string conString = "Server=localhost;Database=automate;port=3306;UserId=root;password=";
        MySqlConnection con; 

        public MainWindow()
        {

            InitializeComponent();
            //Connexion à la BDD
            con = new MySqlConnection(this.conString);

            //Ouverture des acquisitions
            Imports.OpenUnit(out handle);
            Imports.Run(handle, echantillonnage, Imports._BLOCK_METHOD.BM_STREAM);
            /* Son */

            System.Windows.Threading.DispatcherTimer Son = new System.Windows.Threading.DispatcherTimer();
            Son.Tick += AcquisitionSon;
            Son.Interval = TimeSpan.FromMilliseconds(tempsAcquisition);
            Son.Start();

            /*Temperature*/

            System.Windows.Threading.DispatcherTimer Temperature = new System.Windows.Threading.DispatcherTimer();
            Temperature.Tick += AcquisitionTemperature;
            Temperature.Interval = TimeSpan.FromMilliseconds(tempsAcquisition);
            Temperature.Start();

            /*Lumiere*/

            System.Windows.Threading.DispatcherTimer Lumiere = new System.Windows.Threading.DispatcherTimer();
            Lumiere.Tick += AcquisitionLumiere;
            Lumiere.Interval = TimeSpan.FromMilliseconds(tempsAcquisition);
            Lumiere.Start();

        }

        public void AcquisitionSon(object sender, EventArgs e)
        {
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_MIC_LEVEL, out son, out overflow);
            mesureSon.Add(Convert.ToString(Convert.ToDouble(son) / 10));
        }

        public void AcquisitionTemperature(object sender, EventArgs e)
        {
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_TEMP, out temp, out overflow);
            mesureTemperature.Add(Convert.ToString((temp * 0.088)));
        }

        public void AcquisitionLumiere(object sender, EventArgs e)
        {
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_LIGHT, out lum, out overflow);
            mesureLumiere.Add(Convert.ToString(Convert.ToDouble(lum) / 10));
            //Création de la date du jour (on le gère ici seulement car les 3 relevées sont simultanée)
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tabDate.Add(date);
            //On affiche les logs sur la fenetre
            Log.Text += date
                + "\nRelevé température : " + Convert.ToString((temp * 0.088))
                + "\nRelevé lumière : " + Convert.ToString(Convert.ToDouble(lum) / 10)
                + "\nRelevé son : " + Convert.ToString(Convert.ToDouble(son) / 10)
                + "\n\n";
            //On lance l'envoie à la base
            EnvoieBase();
        }

        public void EnvoieBase()
        {
            if (mesureSon.Count()==TailleEnvoi)
            {
                //Création des requêtes
                string requeteSon = "INSERT INTO `afpa_sons`(`ValeurSon`, `DateSon`) VALUES ";
                string requeteLumiere = "INSERT INTO `afpa_lumieres`(`ValeurLumiere`, `DateLumiere`) VALUES ";
                string requeteTemperature = "INSERT INTO `afpa_temperatures`(`ValeurTemperature`, `DateTemperature`) VALUES ";
                for (int i=0;i<TailleEnvoi;i++)
                {
                    requeteSon += "( "+mesureSon[i].Replace(",",".") + " , '"+tabDate[i]+ "') , ";
                    requeteLumiere += "( " + mesureLumiere[i].Replace(",", ".") + " , '" + tabDate[i] + "') , ";
                    requeteTemperature += "( " + mesureTemperature[i].Replace(",", ".") + " , '" + tabDate[i] + "') , ";
                }
                requeteSon = requeteSon.Substring(0,requeteSon.Length-2);
                requeteLumiere = requeteLumiere.Substring(0, requeteLumiere.Length - 2);
                requeteTemperature = requeteTemperature.Substring(0, requeteTemperature.Length - 2);
                requeteSon += ";";
                requeteLumiere += ";";
                requeteTemperature += ";";

                //On remet les tableaux de données à zéro
                mesureSon.Clear();
                mesureLumiere.Clear();
                mesureTemperature.Clear();

                //On crée la requête lié à la connexion, puis on ouvre la connexion à la base de données et on exécute la requêtes avant de fermer la connexion à la BDD pour laisser la place autre requête de se faire
                /*Son*/
                MySqlCommand comSon = new MySqlCommand(requeteSon, con); // Association de la connexion avec la requete
                con.Open(); // Ouverture de la connexion
                comSon.ExecuteReader(); //Envoie de la requête
                con.Close(); // Fermeture de la connexion

                /*Lumiere*/
                MySqlCommand comLum = new MySqlCommand(requeteLumiere, con);
                con.Open();
                comLum.ExecuteReader();
                con.Close();

                /*Temperature*/
                MySqlCommand comTem = new MySqlCommand(requeteTemperature, con);
                con.Open();
                comTem.ExecuteReader();
                con.Close();

                Log.Text += "\n*******************************" +
                    "\n*         Envoi en Base ...         *" +
                    "\n*******************************\n\n";
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (ScrollBar.VerticalOffset == ScrollBar.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                ScrollBar.ScrollToVerticalOffset(ScrollBar.ExtentHeight);
            }
        }
    }
}
