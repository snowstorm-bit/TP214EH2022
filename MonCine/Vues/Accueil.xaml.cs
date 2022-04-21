#region MÉTADONNÉES

// Nom du fichier : Accueil.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;

        #endregion

        #region CONSTRUCTEURS

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
                //foreach (Abonne abonne in _cinematheque.Abonnes)
                //{
                //    Preference preference = abonne.Preference;
                //    preference.Acteurs = _dal.DbContext.ObtenirDocumentsFiltres<Acteur, ObjectId>(x => x.Id, preference.ActeursId);
                //    preference.Categories = _dal.DbContext.ObtenirDocumentsFiltres<Categorie, ObjectId>(x => x.Id, preference.CategoriesId);
                //    preference.Realisateurs = _dal.DbContext.ObtenirDocumentsFiltres<Realisateur, ObjectId>(x => x.Id, preference.RealisateursId);
                //}

                //DALAbonne dalAbonne = new DALAbonne();

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
                //}
            }
            catch (Exception e)
            {
                AfficherMsgErreur(e.Message);
            }
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'afficher le message reçu en paramètre dans un dialogue pour afficher ce dernier.
        /// </summary>
        /// <param name="pMsg">Message d'erreur à afficher</param>
        private void AfficherMsgErreur(string pMsg)
        {
            MessageBox.Show("Une erreur s'est produite !!\n\n" + pMsg, "Erreur", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void BtnConsulterAbonne_Click(object sender, RoutedEventArgs e)
        {
            FAbonnes frmAbonnes = new FAbonnes(_client, _db);

            NavigationService.Navigate(frmAbonnes);
        }

        private void BtnConsulterFilms_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FFilms(_client, _db));
        }

        #endregion
    }
}