using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonCine.Data;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dal;
        private List<Film> _filmList;

        public FFilms(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dal = new DALFilm(_client, _db);
            _filmList = _dal.ObtenirFilms();
        }

        private void radioEstPasAffiche_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();

            _filmList
                .Where(film => film.EstAffiche == false).ToList()
                .ForEach(film => lstFilms.Items.Add(film));
        }

        private void radioTousLesFilm_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();

            _filmList.ForEach(film => lstFilms.Items.Add(film));
        }
        private void radioEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();
            _filmList
                .Where(film => film.EstAffiche == false).ToList()
                .ForEach(film => lstFilms.Items.Add(film));
        }

        private void ButtonAjouterFilmClick(object sender, RoutedEventArgs e)
        {
            AjouterFilm ajouter = new AjouterFilm();
            ajouter.Show();
        }

        private void btnRetourAcceuil_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retour acceuil non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                ModifierFilm modifierFilm = new ModifierFilm((Film)item, _client, _db);
                modifierFilm.Show();
            }
        }

    }
}
