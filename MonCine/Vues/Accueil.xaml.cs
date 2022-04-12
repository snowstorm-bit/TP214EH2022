using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MonCine.Data;
using MonCine.Data.Classes;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        private DAL _dal;
        private Cinematheque _cinematheque;

        public Accueil()
        {
            InitializeComponent();
            _dal = new DAL();

            try
            {
                _cinematheque = _dal.ObtenirCinematheque();

                if (_cinematheque.UtilisateurCourant is Administrateur admin)
                {
                    // TODO : Affichage pour un utilisateur admin
                }
            }
            catch (Exception e)
            {
                AfficherMsgErreur(e.Message);
            }
        }

        /// <summary>
        /// Permet d'afficher le message reçu en paramètre dans un dialogue pour afficher ce dernier.
        /// </summary>
        /// <param name="pMsg">Message d'erreur à afficher</param>
        private void AfficherMsgErreur(string pMsg)
        {
            MessageBox.Show("Une erreur s'est produite !!\n\n" + pMsg, "Erreur", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FAbonnes frmAbonnes = new FAbonnes(dal);

            //this.NavigationService.Navigate(frmAbonnes);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FProjections frmProjections = new FProjections();

            this.NavigationService.Navigate(frmProjections);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FFilms frmFilms = new FFilms();

            this.NavigationService.Navigate(frmFilms);
        }
    }
}
