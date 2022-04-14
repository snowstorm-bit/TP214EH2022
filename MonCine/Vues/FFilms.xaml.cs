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

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        private DAL _dal;
        private Cinematheque _cinematheque;
        private List<Film> _filmList;

        public FFilms(DAL pDal, Cinematheque pCinematheque)
        {
            _dal = pDal;
            _cinematheque = pCinematheque;
            _filmList = new();
            InitializeComponent();
        }

        private void radioEstPasAffiche_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();
            _filmList.Clear();
            _cinematheque.Films
                .Where(film => film.Etat == false).ToList()
                .ForEach(film => _filmList.Add(film));

            foreach (Film f in _filmList)
            {
                lstFilms.Items.Add(f);
            }
        }

        private void radioTousLesFilm_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();
            _filmList.Clear();
            _cinematheque.Films.ForEach(film => _filmList.Add(film));

            foreach (Film f in _filmList)
            {
                lstFilms.Items.Add(f);
            }
        }
        private void radioEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            lstFilms.Items.Clear();
            _filmList.Clear();
            _cinematheque.Films
                .Where(film => film.Etat == true).ToList()
                .ForEach(film => _filmList.Add(film));

            foreach (Film f in _filmList)
            {
                lstFilms.Items.Add(f);
            }
        }

        private void ButtonAjouterFilmClick(object sender, RoutedEventArgs e)
        {
            AjouterFilm ajouter = new AjouterFilm(_dal, _cinematheque);
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
                ModifierFilm modifierFilm = new ModifierFilm(_dal, _cinematheque, (Film)item);
                modifierFilm.Show();
            }
        }

    }
}
