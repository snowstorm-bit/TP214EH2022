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
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

namespace MonCine.Vues
{
    public partial class ModifierFilm : Window
    {
        private DALCategorie _dalCategorie;
        private DALActeur _dalActeur;
        private DALRealisateur _dalRealisateur;
        private DALFilm _dalFilm;
        private List<Categorie> _categories;
        private List<Acteur> _acteurs;
        private List<Realisateur> _realisateurs;
        private Film _film;

        public ModifierFilm(Film pFilm, IMongoClient pClient, IMongoDatabase pDb)
        {
            _dalCategorie = new DALCategorie(pClient, pDb);
            _dalActeur = new DALActeur(pClient, pDb);
            _dalRealisateur = new DALRealisateur(pClient, pDb);
            _dalFilm = new DALFilm(_dalCategorie, _dalActeur, _dalRealisateur, pClient, pDb);
            _categories = _dalCategorie.ObtenirCategories();
            _acteurs = _dalActeur.ObtenirActeurs();
            _realisateurs = _dalRealisateur.ObtenirRealisateurs();
            _film = pFilm;

            InitializeComponent();
            AfficherInformationDuFilm();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AfficherInformationDuFilm()
        {
            txtNomFilm.Text = _film.Nom;
            AfficherCategories();
            AfficherActeurs();
            AfficherRealisateurs();
        }

        private void AfficherCategories()
        {
            _categories.ForEach(cat =>
            {
                dropDownCategories.Items.Add(cat.Nom);
                if (cat.Id == _film.CategorieId)
                {
                    int index = dropDownCategories.Items.IndexOf(cat.Nom);
                    dropDownCategories.SelectedIndex = index;
                    _categories.ForEach(c =>
                    {
                        dropDownCategories.Items.Add(c.Nom);
                        if (c.Id == _film.CategorieId)
                        {
                            dropDownCategories.SelectedIndex = dropDownCategories.Items.IndexOf(c.Nom);
                        }
                    });

                    calendrierDate.SelectedDate = _film.DateSortie;
                    calendrierDate.DisplayDate = _film.DateSortie;

                    _acteurs.ForEach(a => lstActeursComplet.Items.Add(a.Nom));
                }
            });
        }

        private void AfficherActeurs()
        {
            _acteurs.ForEach(act => lstActeursComplet.Items.Add(act.Nom));
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
            _realisateurs.ForEach(rea => lstRealisateurComplet.Items.Add(rea.Nom));
            _realisateurs.ForEach(r => lstRealisateurComplet.Items.Add(r.Nom));
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