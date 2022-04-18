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
    public partial class ModifierFilm : Window
    {
        private Cinematheque _cinematheque;
        private Film _film;

        public ModifierFilm(Cinematheque pCinematheque, Film pFilm)
        {
            _cinematheque = pCinematheque;
            _film = pFilm;
            InitializeComponent();
            AfficherInformationDuFilm();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e) => Close();

        private void AfficherInformationDuFilm()
        {
            txtNomFilm.Text = _film.Nom;
            AfficherCategories();
            AfficherActeurs();
            AfficherRealisateurs();
        }

        private void AfficherCategories()
        {
            _cinematheque.Categories.ForEach(cat =>
            {
                dropDownCategories.Items.Add(cat.Nom);
                if (cat.Id == _film.CategorieId)
                {
                    int index = dropDownCategories.Items.IndexOf(cat.Nom);
                    dropDownCategories.SelectedIndex = index;
                }
            });
            calendrierDate.SelectedDate = _film.DateSortieInternationnale;
            calendrierDate.DisplayDate = _film.DateSortieInternationnale;
        }

        private void AfficherActeurs()
        {
            _cinematheque.Acteurs.ForEach(act => lstActeursComplet.Items.Add(act.Nom));
            if (_film.Acteurs != null)
            {
                foreach (Acteur acteur in _film.Acteurs)
                {
                    lstActeursChoisi.Items.Add(acteur.Nom);
                }
            }
        }

        private void AfficherRealisateurs()
        {
            _cinematheque.Realisateurs.ForEach(rea => lstRealisateurComplet.Items.Add(rea.Nom));
            if (_film.Realisateurs != null)
            {
                foreach (Realisateur realisateur in _film.Realisateurs)
                {
                    lstRealisateurChoisi.Items.Add(realisateur.Nom);
                }
            }
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

        private void BtnSupprimerFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Supprimer film non implémenté", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnModifierFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Modifier un nouveau film, non implémenté!", "Information!", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
