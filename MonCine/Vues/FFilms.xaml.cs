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
        private Cinematheque _cinematheque;
        private List<Film> _films;

        public FFilms(Cinematheque pCinematheque)
        {
            _cinematheque = pCinematheque;
            _films = new();
            InitializeComponent();
        }

        private void RadioTousLesFilm_Checked(object sender, RoutedEventArgs e)
            => AfficherTousLesFilms();

        private void RadioEstAffiche_Checked(object sender, RoutedEventArgs e)
            => AfficherLesFilmsALaffiche();

        private void ButtonAjouterFilmClick(object sender, RoutedEventArgs e)
        {
            AjouterFilm ajouter = new AjouterFilm(_cinematheque);
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
                ModifierFilm modifierFilm = new ModifierFilm(_cinematheque, (Film)itemFilm);
                modifierFilm.Show();
            }
        }

        private void AfficherLesFilmsALaffiche()
        {
            lstFilms.Items.Clear();
            _films.Clear();
            _cinematheque.Films.Where(film => film.Etat == true).ToList()
                .ForEach(film =>
                {
                    _films.Add(film);
                    lstFilms.Items.Add(film);
                });
        }

        private void AfficherTousLesFilms()
        {
            lstFilms.Items.Clear();
            _films.Clear();
            _cinematheque.Films.ForEach(film =>
            {
                _films.Add(film);
                lstFilms.Items.Add(film);
            });
        }
    }
}
