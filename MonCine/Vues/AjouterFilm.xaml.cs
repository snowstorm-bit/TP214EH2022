using MonCine.Data.Classes;
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

namespace MonCine.Vues
{
    public partial class AjouterFilm : Window
    {
        private DAL _dal;
        private Cinematheque _cinematheque;

        public AjouterFilm(DAL pDal, Cinematheque pCinematheque)
        {
            _dal = pDal;
            _cinematheque = pCinematheque;
            InitializeComponent();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnAjouterFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouter un nouveau film, non implémenté!", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRetirerActeur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retiré un acteur non implémenté", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAjouterActeur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouté un acteur non implémenté", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRetirerRealisateur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retiré un réalisateur non implémenté", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAjouterRealisateur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouter un realisateur non implémenté", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
