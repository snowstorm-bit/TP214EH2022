#region MÉTADONNÉES

// Nom du fichier : FProjections.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System.Windows.Controls;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Projections.xaml
    /// </summary>
    public partial class FProjections : Page
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dalFilm;
        private Film _film;

        #endregion

        #region CONSTRUCTEURS

        public FProjections(IMongoClient pClient, IMongoDatabase pDb, Film pFilm)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _film = pFilm;
        }

        #endregion
    }
}