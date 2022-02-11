using Automate.Controller;
using Automate.Data.Model;
using Automate.Data.Services;
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

        /* SEUILS */
        Seuil SeuilTemperature;
        Seuil SeuilSon;
        Seuil SeuilLumiere;

        /* COULEURS */
        Couleur seuilBasCouleur;
        Couleur seuilOKCouleur;
        Couleur seuilHautCouleur;

        /* Arret */
        bool arret = false;

        List<Temperature> mesureTemperature = new List<Temperature>();
        List<Son> mesureSon = new List<Son>();
        List<Lumiere> mesureLumiere = new List<Lumiere>();
        List<string> tabDate = new List<string>();
        List<List<string>> tabAnomalies = new List<List<string>>();

        /* Paramètre */
        int TailleEnvoi = 2;

        public MainWindow()
        {

            InitializeComponent();

            // Récupération des couleurs de seuils et des valeurs des seuils
            RecupCouleur();
            RecupSeuil();
            //Ouverture des acquisitions
            Imports.OpenUnit(out handle);
            Imports.Run(handle, echantillonnage, Imports._BLOCK_METHOD.BM_STREAM);
            /* Acquisition périodique */
            System.Windows.Threading.DispatcherTimer Aquisition = new System.Windows.Threading.DispatcherTimer();
            Aquisition.Tick += Acquisitions; //Fonction à lancer à chaque interval
            Aquisition.Interval = TimeSpan.FromMilliseconds(tempsAcquisition); //Definition de l'Interval 
            Aquisition.Start();
        }

        /// <summary>
        /// Gère l'acquisition des données relevées grâce à la carte d'aquisition.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Acquisitions(object sender, EventArgs e)
        {
            //Création de la date du jour
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tabDate.Add(date);


            /* Son */
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_MIC_LEVEL, out son, out overflow);
            string stringSon = Convert.ToString(Math.Round(Convert.ToDouble(son) / 10,2));
            float floatSon = (float) Math.Round(Convert.ToDouble(son) / 10,2);
            int resultSeuilSon = VerifSeuil(floatSon,"son"); // On vérifie que la valeur du son est OK

            /* Temperature */
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_TEMP, out temp, out overflow);
            string stringTemp = Convert.ToString(Math.Round(temp * 0.088,2));
            float floatTemp = (float) Math.Round(temp * 0.088,2);
            int resultSeuilTemp = VerifSeuil(floatTemp, "temp"); // On vérifie que la valeur de la température est OK


            /* Lumière */
            Imports.GetSingle(handle, Imports.Inputs.USB_DRDAQ_CHANNEL_LIGHT, out lum, out overflow);
            string stringLum = Convert.ToString(Math.Round(Convert.ToDouble(lum) / 10,2));
            float floatLum = (float) Math.Round(Convert.ToDouble(lum) / 10,2); 
            int resultSeuilLum = VerifSeuil(floatLum, "lum"); // On vérifie que la valeur de la lumière est OK



            /* Ajout des valeurs dans le tableau */

            if (arret) // Si on est en arret alors on ajoute des valeurs à 0 (machine non opérationnelle)
            {
                mesureSon.Add(new Son("0"));
                mesureTemperature.Add(new Temperature("0"));
                mesureLumiere.Add(new Lumiere("0"));
            } else // Sinon on ajoute les valeurs relevées par les capteurs
            {
                mesureSon.Add(new Son(stringSon));
                mesureTemperature.Add(new Temperature(stringTemp));
                mesureLumiere.Add(new Lumiere(stringLum));
            }
            


            /* Gestion des anomalies */
            CheckAnomalies(resultSeuilTemp,"temp",date); // Ajoute l'anomalie de température, si il y en a une, aux tableaux d'anomalie 
            CheckAnomalies(resultSeuilSon, "son", date); // Ajoute l'anomalie de son, si il y en a une, aux tableaux d'anomalie 
            CheckAnomalies(resultSeuilLum, "lum", date); // Ajoute l'anomalie de lumière, si il y en a une, aux tableaux d'anomalie 


            /* Gestion de l'affichage */
            //On affiche les logs sur la fenetre AVEC LES COULEURS
            if (!arret) { // On affiche pas les valeurs relevées si on est en arret

                Log.Inlines.Add(new Run(date) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) });
                //Log.Text += date;
                //Changement de la couleur pour la ligne de température
                switch (resultSeuilTemp)
                {
                    case 1: // Valeur au dessus du seuil haut
                        Log.Inlines.Add(new Run("\nRelevé température : " + stringTemp) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilHautCouleur.Red, (byte)seuilHautCouleur.Green, (byte)seuilHautCouleur.Blue)) });
                        break;
                    case -1: // Valeur en dessous du seuil bas
                        Log.Inlines.Add(new Run("\nRelevé température : " + stringTemp) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilBasCouleur.Red, (byte)seuilBasCouleur.Green, (byte)seuilBasCouleur.Blue)) });
                        break;
                    default: // Valeur OK
                        Log.Inlines.Add(new Run("\nRelevé température : " + stringTemp) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilOKCouleur.Red, (byte)seuilOKCouleur.Green, (byte)seuilOKCouleur.Blue)) });
                        break;
                }
                //Changement de la couleur pour la ligne de lumière
                switch (resultSeuilLum)
                {
                    case 1: // Valeur au dessus du seuil haut
                        Log.Inlines.Add(new Run("\nRelevé lumière : " + stringLum) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilHautCouleur.Red, (byte)seuilHautCouleur.Green, (byte)seuilHautCouleur.Blue)) });
                        break;
                    case -1: // Valeur en dessous du seuil bas
                        Log.Inlines.Add(new Run("\nRelevé lumière : " + stringLum) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilBasCouleur.Red, (byte)seuilBasCouleur.Green, (byte)seuilBasCouleur.Blue)) });
                        break;
                    default: // Valeur ok
                        Log.Inlines.Add(new Run("\nRelevé lumière : " + stringLum) { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilOKCouleur.Red, (byte)seuilOKCouleur.Green, (byte)seuilOKCouleur.Blue)) });
                        break;
                }
                //Changement de la couleur pour la ligne du son                
                switch (resultSeuilSon)
                {
                    case 1: // Valeur au dessus du seuil haut
                        Log.Inlines.Add(new Run("\nRelevé son : " + stringSon + "\n\n") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilHautCouleur.Red, (byte)seuilHautCouleur.Green, (byte)seuilHautCouleur.Blue)) });
                        break;
                    case -1: // Valeur en dessous du seuil bas
                        Log.Inlines.Add(new Run("\nRelevé son : " + stringSon + "\n\n") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilBasCouleur.Red, (byte)seuilBasCouleur.Green, (byte)seuilBasCouleur.Blue)) });
                        break;
                    default: // Valeur OK
                        Log.Inlines.Add(new Run("\nRelevé son : " + stringSon + "\n\n") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)seuilOKCouleur.Red, (byte)seuilOKCouleur.Green, (byte)seuilOKCouleur.Blue)) });
                        break;
                }
            }
            /* Gestion de l'envoi en base de doonées */
           
            //On lance l'envoie à la base si la taille de l'envoi est atteint
            if (mesureSon.Count() == TailleEnvoi)
            {
                EnvoieBase();
            }

            //Si il y a eu une anomalie sur le son ou sur la température alors on arrete la production (si elle n'est pas déja arreté) et on envoi en base les données déja récolté avant l'anomalie
            if (resultSeuilSon != 0 || resultSeuilTemp != 0 && !arret)
            {
                Log.Inlines.Add(new Run("*** PAUSE ***") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))});
                //Log.Text += "*** PAUSE ***"; // Affichage de l'arret de production
                arret = true; // Production arreté
                EnvoieBase();

                // On fait une pause en fonction du temps d'arret proposé dans le seuil.
                if (resultSeuilSon != 0 && resultSeuilTemp != 0) // Si il ya deux anomalies en même temps
                {
                    if (resultSeuilSon > resultSeuilTemp) // On prend le temps de pause le plus grand
                    {
                        pause(SeuilSon.Temps* 60000);
                    } else
                    {
                        pause(SeuilTemperature.Temps * 60000);
                    }
                } else // s'il n'y a qu'une anomalie
                {
                    if (resultSeuilSon != 0) // On prends le temps de pause de l'anomalie concernée.
                    {
                        pause(SeuilSon.Temps * 60000);
                    }
                    else
                    {
                        pause(SeuilTemperature.Temps * 60000);
                    }
                }
            }

        }

        /// <summary>
        /// Gère l'envoie des données en base.
        /// </summary>
        private void EnvoieBase()
        {
            /* Gestion de la cadence */
            int cadence = 0; // Initialisation de la cadence à 0
            Random rand = new Random();
            // Crée une cadence aléatoire si on est pas en arret
            if (!arret)
            {
                cadence = rand.Next(100, 300); 
            }

            //On crée la date pour la cadence
            string date = "";
            if (tabDate.Count != 0) // Il y a eu des relevés effectués, alors on prend la date du dernier relevé des données à envoyer
            {
                date = tabDate[tabDate.Count() - 1];
            }
            else//Si il n'y a pas encore eu de relevé alors on met la date d'aujourd'hui (arret dès le 1er relevé)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
            }

            CadencesController.PostCadence(cadence, date); // Envoie de la cadence en base

            /* Gestion des relevés */
            if (mesureSon.Count != 0) //Si il y a des relevés alors on les envoies en base
            {
                SonsController.PostSon(mesureSon,tabDate);
                LumieresController.PostLumiere(mesureLumiere, tabDate);
                TemperaturesController.PostTemperature(mesureTemperature, tabDate);
                //Affichage de l'envoie en base à l'écran
                Log.Inlines.Add(new Run("\n*******************************" +
                            "\n*         Envoi en Base ...         *" +
                            "\n*******************************\n\n") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) });
                //Log.Text += "\n*******************************" +
                //            "\n*         Envoi en Base ...         *" +
                //            "\n*******************************\n\n";
            }
            /* Gestion des anomalies */
            if (tabAnomalies.Count() != 0) // Si il y a des anomalies alors on les envoies en base
            {
                AnomaliesController.PostAnomalies(tabAnomalies,cadence);
            }

            /* Gestion des tableaux de données */
            //On remet les tableaux de données à zéro pour pouvoir reprendre l'aquisition des données pour une nouvelle tranche
            mesureSon.Clear();
            mesureLumiere.Clear();
            mesureTemperature.Clear();
            tabDate.Clear();
            tabAnomalies.Clear();

        }

        /// <summary>
        /// Auto scroll de la fenêtre de log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Gère la récupération des seuils pour la température, le son et la lumière . Affiche également ces valeurs, au lancement, dans les logs
        /// </summary>
        private void RecupSeuil()
        {
            SeuilTemperature= SeuilsController.getSeuils(1);
            SeuilSon = SeuilsController.getSeuils(2);
            SeuilLumiere = SeuilsController.getSeuils(3);

            Log.Text += "Seuil bas temperature :" + SeuilTemperature.SeuilBas
                        +"\nSeuil haut temperature :" + SeuilTemperature.SeuilHaut
                        + "\nSeuil bas son :" + SeuilSon.SeuilBas
                        + "\nSeuil haut son :" + SeuilSon.SeuilHaut
                        + "\nSeuil bas lumiere :" + SeuilLumiere.SeuilBas
                        + "\nSeuil haut lumiere :" + SeuilLumiere.SeuilHaut + "\n\n";
        }
        
        /// <summary>
        /// Vérifie si les valeurs relevées sont dans les seuils.
        /// </summary>
        /// <param name="value"> La valeur à tester </param>
        /// <param name="type"> Le type de relevé (temp, son , lum) </param>
        /// <returns>
        /// 1 si la valeur est au dessus du seuil haut <br/>
        /// -1 si la valeur est en dessous du seuil bas <br/>
        /// 0 si la valeur est OK
        /// </returns>
        private int VerifSeuil(float value,string type)
        {
            switch (type)
            {
                case "temp":
                    if (value < SeuilTemperature.SeuilBas)
                    {
                        return -1;
                    } else if (value > SeuilTemperature.SeuilHaut)
                    {
                        return 1;
                    }
                    return 0;
                case "son":
                    if (value < SeuilSon.SeuilBas)
                    {
                        return -1;
                    }
                    else if (value > SeuilSon.SeuilHaut)
                    {
                        return 1;
                    }
                    return 0;
                case "lum":
                    if (value < SeuilLumiere.SeuilBas)
                    {
                        return -1;
                    }
                    else if (value > SeuilLumiere.SeuilHaut)
                    {
                        return 1;
                    }
                    return 0;
                default:
                    return 99;
            }

        }

        /// <summary>
        /// Vérifie si il y a une anomalie, si il y en a une, alors l'ajoute au tableau d'anomalies.
        /// </summary>
        /// <param name="value">Valeur retournée par la fonction VerifSeuil (1,0,-1)</param>
        /// <param name="type">Le type de relevé (temp, son , lum)</param>
        /// <param name="date">Date du relevé de l'anomalie</param>
        private void CheckAnomalies(int value, string type, string date)
        {
          
            switch (type)
            {
                case "temp":

                    if (value == 1)
                    {
                        tabAnomalies.Add(new List<string> { date, "temperature", "0", "5" });
                    } else if (value == -1)
                    {
                        tabAnomalies.Add(new List<string> { date, "temperature", "0", "6" });
                    }
                    break;
                case "son":
                    if (value == 1)
                    {
                        tabAnomalies.Add(new List<string> { date, "son", "0", "2" });
                    }
                    else if (value == -1)
                    {
                        tabAnomalies.Add(new List<string> { date, "son", "0", "4" });
                    }
                    break;
                case "lum":
                    if (value == 1)
                    {
                        tabAnomalies.Add(new List<string> { date, "lumiere", "0", "1" });
                    }
                    else if (value == -1)
                    {
                        tabAnomalies.Add(new List<string> { date, "lumiere", "0", "3" });
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Récupère les couleurs des différents seuils
        /// </summary>
        private void RecupCouleur()
        {
            List<Couleur> c=CouleursController.getCouleurs();
            seuilBasCouleur = c[0];
            seuilOKCouleur = c[1];
            seuilHautCouleur = c[2];
        }

        /// <summary>
        /// Fonction qui permet de mettre en pause la production pour un temps donnée.
        /// </summary>
        /// <param name="temps">Temps de la pause en ms</param>
        private async void pause(int temps)
        {
            await Task.Delay(temps);
            Log.Inlines.Add(new Run("FIN PAUSE\n") { Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) });
            //Log.Text += "FIN PAUSE\n";
            arret = false;
        }
    }
   
}
