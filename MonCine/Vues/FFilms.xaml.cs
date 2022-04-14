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

        public FFilms(DAL pDal, Cinematheque pCinematheque)
        {
            _dal = pDal;
            _cinematheque = pCinematheque;
            InitializeComponent();
        }

        private void radioEstPasAffiche_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            //POUR FILM À L'AFFICHE DEPUIS CINEMATHEQUE
            //List<Film> filmsAffiche = new List<Film>();
            //_cinematheque.Films
            //    .Where(film => film.Etat).ToList()
            //    .ForEach(film => filmsAffiche.Add(film));
        }

        private void ButtonAjouterFilmClick(object sender, RoutedEventArgs e)
        {
            //List<Film> films = _dal.DbContext.ObtenirCollectionListe<Film>();

            //foreach (Film f in films)
            //{
            //    lstFilms.Items.Add(f);
            //}
        }
    }
}
