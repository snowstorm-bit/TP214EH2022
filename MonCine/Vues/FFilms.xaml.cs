#region MÉTADONNÉES

// Nom du fichier : FFilms.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-20

#endregion

#region USING

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dalFilm;
        private List<Film> _films;
        private Film _filmSelectionne;

        #endregion

        #region CONSTRUCTEURS

        public FFilms(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalFilm = new DALFilm(_client, _db);
            _films = _dalFilm.ObtenirFilms();
            RbTousLesFilms.IsChecked = true;
        }

        #endregion

        #region MÉTHODES

        private void RbTousLesFilm_Checked(object sender, RoutedEventArgs e)
        {
            LstFilms.Items.Clear();
            _films.ForEach(film => LstFilms.Items.Add(film));
            AfficherBtnPourFilmEstAffiche(false);
        }

        private void RbEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            LstFilms.Items.Clear();
            _films
                .Where(film => film.EstAffiche)
                .ToList()
                .ForEach(film => LstFilms.Items.Add(film));
            AfficherBtnPourFilmEstAffiche(true);
        }

        private void BtnAjouterFilm_Click(object sender, RoutedEventArgs e)
        {
            GererFilm ajouterFilm = new GererFilm(_client, _db);
            ajouterFilm.Show();
        }

        private void BtnModifierFilm_Click(object pSender, RoutedEventArgs pE)
        {
            GererFilm modifierFilm = new GererFilm(_client, _db, _filmSelectionne);
            modifierFilm.Show();
        }

        private void BtnVoirProjections_Click(object pSender, RoutedEventArgs pE)
        {
            NavigationService.Navigate(new FProjections(_client, _db, _filmSelectionne));
        }

        private void BtnRetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retour acceuil non implémenté", "Information!",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LstFilms_OnSelectionChanged(object pSender, SelectionChangedEventArgs pE)
        {
            AfficherBtnsPourFilmSelectionne();
        }

        private void AfficherBtnPourFilmEstAffiche(bool afficher)
        {
            BtnAjouterFilm.Visibility = ObtenirVisibilite(afficher);
            AfficherBtnsPourFilmSelectionne();
        }

        private void AfficherBtnsPourFilmSelectionne()
        {
            bool itemIsSelected = LstFilms.SelectedIndex > -1;

            _filmSelectionne = itemIsSelected
                ? (Film)LstFilms.SelectedItem
                : null;

            bool btnsSontVisibles = itemIsSelected;

            if (_filmSelectionne != null)
                btnsSontVisibles &= _filmSelectionne.EstAffiche;

            Visibility visibilite = ObtenirVisibilite(btnsSontVisibles);

            BtnModifierFilm.Visibility = visibilite;
            BtnVoirProjection.Visibility = visibilite;
        }

        private Visibility ObtenirVisibilite(bool pEstVisible)
        {
            return pEstVisible ? Visibility.Visible : Visibility.Hidden;
        }

        #endregion
    }
}