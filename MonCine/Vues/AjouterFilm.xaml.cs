#region MÉTADONNÉES

// Nom du fichier : AjouterFilm.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-20

#endregion

#region USING

using System.Windows;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    public partial class AjouterFilm : Window
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dalFilm;

        #endregion

        #region CONSTRUCTEURS

        public AjouterFilm(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalFilm = new DALFilm(_client, _db);
        }

        #endregion

        #region MÉTHODES

        private void BtnAnnuler_Click(object pSender, RoutedEventArgs pE)
        {
            Close();
        }

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

        #endregion
    }
}