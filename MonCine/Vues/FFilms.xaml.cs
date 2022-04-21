#region MÉTADONNÉES

// Nom du fichier : FFilms.xaml.cs
// Date de création : 2022-04-21
// Date de modification : 2022-04-21

#endregion

#region USING

using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            Loaded += OnLoaded;
        }

        #endregion

        #region MÉTHODES

        private void OnLoaded(object pSender, RoutedEventArgs pE)
        {
            ObtenirFilms();
        }

        private void ObtenirFilms()
        {
            _films = _dalFilm.ObtenirFilms();
            if (LstFilms.Items.Count != 0)
            {
                ChargerLstFilms(false);
            }
            else
            {
                RbTousLesFilms.IsChecked = true;
            }
        }

        private void RbTousLesFilm_Checked(object sender, RoutedEventArgs e)
        {
            ChargerLstFilms(false);
        }

        private void RbEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            ChargerLstFilms(true);
        }

        private void BtnAjouterFilm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GererFilm(_client, _db));
        }

        private void BtnModifierFilm_Click(object pSender, RoutedEventArgs pE)
        {
            NavigationService.Navigate(new GererFilm(_client, _db, _filmSelectionne));
        }

        private void BtnVoirProjections_Click(object pSender, RoutedEventArgs pE)
        {
            NavigationService.Navigate(new FProjections(_client, _db, _filmSelectionne));
        }

        private void BtnRetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void LstFilms_OnSelectionChanged(object pSender, SelectionChangedEventArgs pE)
        {
            ActiverBtnsPourFilmSelectionneEstAffiche();
        }

        private void ActiverBtnPourFilmEstAffiche(bool afficher)
        {
            BtnAjouterFilm.IsEnabled = afficher;
            ActiverBtnsPourFilmSelectionneEstAffiche();
        }

        private void ActiverBtnsPourFilmSelectionneEstAffiche()
        {
            bool itemIsSelected = LstFilms.SelectedIndex > -1;
            _filmSelectionne = itemIsSelected
                ? (Film)LstFilms.SelectedItem
                : null;

            bool btnsSontActifs = itemIsSelected;

            if (_filmSelectionne != null)
            {
                btnsSontActifs &= _filmSelectionne.EstAffiche;
            }

            BtnModifierFilm.IsEnabled = btnsSontActifs;
            BtnVoirProjection.IsEnabled = btnsSontActifs;
            BtnRetirerDeAffiche.IsEnabled = btnsSontActifs;
        }

        private void BtnRetirerDeAffiche_OnClick(object pSender, RoutedEventArgs pE)
        {
            if (_filmSelectionne != null)
            {
                if (_filmSelectionne.EstAffiche && _filmSelectionne.RetirerFilmEstAffiche())
                {
                    _dalFilm.MAJProjectionsFilm(_filmSelectionne);
                    _films[_films.FindIndex(x => x.Id == _filmSelectionne.Id)] = _filmSelectionne;
                    ChargerLstFilms((bool)RbTousLesFilms.IsChecked);
                }
                else
                {
                    AfficherMsgErreur("Il a été impossible de retiré le film sélectionné des films à l'affiche.");
                }
            }
        }

        private void ChargerLstFilms(bool affichagePourRbEstAffiche)
        {
            LstFilms.Items.Clear();

            if (affichagePourRbEstAffiche)
            {
                _films
                    .Where(film => film.EstAffiche)
                    .ToList()
                    .ForEach(film => LstFilms.Items.Add(film));
            }
            else
            {
                _films.ForEach(film => LstFilms.Items.Add(film));
            }

            ActiverBtnPourFilmEstAffiche(affichagePourRbEstAffiche);
        }

        /// <summary>
        /// Permet d'afficher le message reçu en paramètre dans un dialogue pour afficher ce dernier.
        /// </summary>
        /// <param name="pMsg">Message d'erreur à afficher</param>
        private void AfficherMsgErreur(string pMsg)
        {
            MessageBox.Show(
                "Une erreur s'est produite !!\n\n" + pMsg, "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        #endregion
    }
}