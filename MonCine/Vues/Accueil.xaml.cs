using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
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
using MonCine.Data.Classes.DAL;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        private IMongoClient _client;
        private IMongoDatabase _db;

        public Accueil(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();

            _client = pClient;
            _db = pDb;
            try
            {
                //_cinematheque = _dal.ObtenirCinematheque();

                //// POUR FILM À L'AFFICHE DEPUIS CINEMATHEQUE
                ////List<Film> filmsAffiche = new List<Film>();
                ////_cinematheque.Films
                ////    .Where(film => film.EtatAffiche).ToList()
                ////    .ForEach(film => filmsAffiche.Add(film));

                //// POUR FILM À L'AFFICHE DEPUIS BD
                ////List<Film> filmsAffiche = _dal.DbContext.ObtenirDocumentsFiltres<Film>(x => x.EtatAffiche);

                //// POUR OBTENIR TOUS LES OBJETS DEPUIS LA LISTE DES IDENTIFIANTS DES OBJETS D'UN TDOCUMENT
                //foreach (Abonne abonne in _cinematheque.Abonnes)
                //{
                //    Preference preference = abonne.Preference;

                //    preference.Categories =
                //        _dal.DbContext.ObtenirDocumentsFiltres<Categorie>(x => x.Id, preference.CategoriesId);

                //    preference.Acteurs =
                //        _dal.DbContext.ObtenirDocumentsFiltres<Acteur>(x => x.Id, preference.ActeursId);
                //    preference.Realisateurs =
                //        _dal.DbContext.ObtenirDocumentsFiltres<Realisateur>(x => x.Id, preference.RealisateursId);
                //}

                //DALAbonne dalAbonne = new DALAbonne();

                //if (_cinematheque.UtilisateurCourant is Administrateur admin)
                //{
                //    //List<Categorie> cat = _dal.DbContext.ObtenirCollectionListe<Categorie>();
                //    //List<Categorie> nouvCat = new List<Categorie>();

                //    //foreach (var item in collection)
                //    //{

                //    //}
                //    //_cinematheque.Abonnes[0].Preference.Acteurs
                //    //    _dal.DbContext.MAJUn<Film, object>(
                //    //        x => x.Id == _cinematheque.Films[3].Id,

                //    //        new List<(System.Linq.Expressions.Expression<Func<Film, object>> field, object value)>
                //    //        {
                //    //            (x => x.Notes, new List<Note>()),
                //    //            (x => x.NoteMoy, 10)
                //    //        });
                //    // TODO : Affichage pour un utilisateur admin
                //}
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
            FAbonnes frmAbonnes = new FAbonnes(_client, _db);

            NavigationService.Navigate(frmAbonnes);
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