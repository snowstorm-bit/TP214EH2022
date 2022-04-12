#region MÉTADONNÉES

// Nom du fichier : MainWindow.xaml.cs
// Date de création : 2022-04-06
// Date de modification : 2022-04-10

#endregion

#region USING

using System.Windows;
using MonCine.Data;
using MonCine.Data.Classes;
using MonCine.Vues;

#endregion

namespace MonCine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DAL _dal;
        private Cinematheque _cinematheque;

        #region CONSTRUCTEURS

        public MainWindow()
        {
            InitializeComponent();
            _dal = new DAL();
            _cinematheque = _dal.ObtenirCinematheque();

            if (_cinematheque.UtilisateurCourant is Administrateur admin)
            {

                // TODO : Affichage pour un utilisateur admin
                _NavigationFrame.Navigate(new Accueil(_dal, _cinematheque));
            }
            else
            {
                AfficherMsgErreur("Vous n'êtes pas connecté en tant qu'administrateur");
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

        #endregion
    }
}