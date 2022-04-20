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
using System.Windows.Input;
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

        #endregion

        #region CONSTRUCTEURS

        public FFilms(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalFilm = new DALFilm(_client, _db);
            _films = _dalFilm.ObtenirFilms();
        }

        #endregion

        #region MÉTHODES

        private void RadioTousLesFilm_Checked(object sender, RoutedEventArgs e)
            => AfficherTousLesFilms();

        private void RadioEstAffiche_Checked(object sender, RoutedEventArgs e)
            => AfficherLesFilmsALaffiche();

        private void ButtonAjouterFilmClick(object sender, RoutedEventArgs e)
        {
            AjouterFilm ajouter = new AjouterFilm(_client, _db);
            ajouter.Show();
        }

        private void BtnRetourAcceuil_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retour acceuil non implémenté", "Information!",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemFilm = ((FrameworkElement)e.OriginalSource).DataContext;
            if (itemFilm != null)
            {
                ModifierFilm modifierFilm = new ModifierFilm((Film)itemFilm, _client, _db);
                modifierFilm.Show();
            }
        }

        private void AfficherLesFilmsALaffiche()
        {
            lstFilms.Items.Clear();
            _films
                .Where(film => film.EstAffiche)
                .ToList()
                .ForEach(film => lstFilms.Items.Add(film));
        }

        private void AfficherTousLesFilms()
        {
            lstFilms.Items.Clear();
            _films
                .Where(film => film.EstAffiche)
                .ToList()
                .ForEach(film => lstFilms.Items.Add(film));
        }

        #endregion
    }
}