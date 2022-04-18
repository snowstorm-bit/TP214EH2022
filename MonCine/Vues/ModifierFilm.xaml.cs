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
        private DAL _dal;
        private Cinematheque _cinematheque;
        private Film _film;

        public ModifierFilm(DAL pDal, Cinematheque pCinematheque, Film pFilm)
        {
            _dal = pDal;
            _cinematheque = pCinematheque;
            _film = pFilm;
            InitializeComponent();
            AfficherInformationDuFilm();
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AfficherInformationDuFilm()
        {
            this.txtNomFilm.Text = _film.Nom.ToString();

            _cinematheque.Categories.ForEach(c =>
            {
                this.dropDownCategories.Items.Add(c.Nom);
                if (c.Id == _film.CategorieId)
                {
                    int index = this.dropDownCategories.Items.IndexOf(c.Nom);
                    this.dropDownCategories.SelectedIndex = index;
                }
            });
            this.calendrierDate.SelectedDate = _film.DateSortieInternationnale;
            this.calendrierDate.DisplayDate = _film.DateSortieInternationnale;

            _cinematheque.Acteurs.ForEach(a => this.lstActeursComplet.Items.Add(a.Nom));
            if (_film.Acteurs != null)
            {
                foreach (Acteur acteur in _film.Acteurs)
                {
                    this.lstActeursChoisi.Items.Add(acteur.Nom);
                }
            }

            _cinematheque.Realisateurs.ForEach(r => this.lstRealisateurComplet.Items.Add(r.Nom));
            if (_film.Realisateurs != null)
            {
                foreach (Realisateur realisateur in _film.Realisateurs)
                {
                    this.lstRealisateurChoisi.Items.Add(realisateur.Nom);
                }
            }
        }   

        private void btnRetirerActeur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retiré un acteur non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAjouterActeur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouté un acteur non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnRetirerRealisateur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Retiré un réalisateur non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAjouterRealisateur_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ajouter un realisateur non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnSupprimerFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Supprimer film non implémenté", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnModifierFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Modifier un nouveau film, non implémenté!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
