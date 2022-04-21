#region MÉTADONNÉES

// Nom du fichier : ModifierFilm.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System.Collections.Generic;
using System.Windows;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    public partial class ModifierFilm : Window
    {
        #region ATTRIBUTS

        private DALCategorie _dalCategorie;
        private DALActeur _dalActeur;
        private DALRealisateur _dalRealisateur;
        private DALFilm _dalFilm;
        private List<Categorie> _categories;
        private List<Acteur> _acteurs;
        private List<Realisateur> _realisateurs;
        private Film _film;

        #endregion

        #region CONSTRUCTEURS

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

        #endregion

        #region MÉTHODES

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AfficherInformationDuFilm()
        {
            TxtNomFilm.Text = _film.Nom;
            AfficherCategories();
            AfficherActeurs();
            AfficherRealisateurs();
        }

        private void AfficherCategories()
        {
            _categories.ForEach(cat =>
            {
                DropDownCategories.Items.Add(cat.Nom);
                if (cat.Id == _film.CategorieId)
                {
                    int index = DropDownCategories.Items.IndexOf(cat.Nom);
                    DropDownCategories.SelectedIndex = index;
                    _categories.ForEach(c =>
                    {
                        DropDownCategories.Items.Add(c.Nom);
                        if (c.Id == _film.CategorieId)
                        {
                            DropDownCategories.SelectedIndex = DropDownCategories.Items.IndexOf(c.Nom);
                        }
                    });

                    CalendrierDate.SelectedDate = _film.DateSortie;
                    CalendrierDate.DisplayDate = _film.DateSortie;

                    _acteurs.ForEach(a => LstActeursComplet.Items.Add(a.Nom));
                }
            });
        }

        private void AfficherActeurs()
        {
            _acteurs.ForEach(act => LstActeursComplet.Items.Add(act.Nom));
            if (_film.Acteurs != null)
            {
                foreach (Acteur acteur in _film.Acteurs)
                {
                    LstActeursChoisi.Items.Add(acteur.Nom);
                }
            }
        }

        private void AfficherRealisateurs()
        {
            _realisateurs.ForEach(rea => LstRealisateurComplet.Items.Add(rea.Nom));
            _realisateurs.ForEach(r => LstRealisateurComplet.Items.Add(r.Nom));
            if (_film.Realisateurs != null)
            {
                foreach (Realisateur realisateur in _film.Realisateurs)
                {
                    LstRealisateurChoisi.Items.Add(realisateur.Nom);
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

        #endregion
    }
}