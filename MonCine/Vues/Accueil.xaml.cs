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

        public Accueil(DAL pDal, Cinematheque pCinematheque)
        {
            InitializeComponent();
            _dal = pDal;
            _cinematheque = pCinematheque;

            try
            {
                _cinematheque = _dal.ObtenirCinematheque();

                if (_cinematheque.UtilisateurCourant is Administrateur admin)
                {
                    //List<Categorie> cat = _dal.DbContext.ObtenirCollectionListe<Categorie>();
                    //List<Categorie> nouvCat = new List<Categorie>();

                    //foreach (var item in collection)
                    //{

                    //}
                    //_cinematheque.Abonnes[0].Preference.Acteurs
                    //    _dal.DbContext.MAJUn<Film, object>(
                    //        x => x.Id == _cinematheque.Films[3].Id,

                    //        new List<(System.Linq.Expressions.Expression<Func<Film, object>> field, object value)>
                    //        {
                    //            (x => x.Notes, new List<Note>()),
                    //            (x => x.NoteMoy, 10)
                    //        });
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
            FAbonnes frmAbonnes = new FAbonnes(_dal, _cinematheque);

            this.NavigationService.Navigate(frmAbonnes);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //FProjections frmProjections = new FProjections(_dal, _cinematheque);

            //this.NavigationService.Navigate(frmProjections);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //FFilms frmFilms = new FFilms(_dal, _cinematheque);

            //this.NavigationService.Navigate(frmFilms);
        }
    }
}
