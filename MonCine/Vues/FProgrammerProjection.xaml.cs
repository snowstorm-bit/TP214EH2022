#region MÉTADONNÉES

// Nom du fichier : FProgrammerProjection.xaml.cs
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
    public partial class FProgrammerProjection : Window
    {
        #region ATTRIBUTS
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly Film _film;

        #endregion

        #region CONSTRUCTEURS

        public FProgrammerProjection(Film pFilm, IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _film = pFilm;
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
        }


        #endregion

        private void BtnModifierFilm_OnClick(object pSender, RoutedEventArgs pE)
        {
            throw new System.NotImplementedException();
        }
    }
}