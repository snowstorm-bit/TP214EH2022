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
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Projections.xaml
    /// </summary>
    public partial class FProjections : Page
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dalFilm;
        private Film _film;

        public FProjections(IMongoClient pClient, IMongoDatabase pDb, Film pFilm)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _film = pFilm;
        }
    }
}
