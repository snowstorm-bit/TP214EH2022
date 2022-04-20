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
using System.Data;
using System.Windows.Navigation;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private List<Abonne> _abonnes;
        private DALAbonne _dalAbonne;
        public FAbonnes(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalAbonne = new DALAbonne(_client, _db);
            _abonnes = _dalAbonne.ObtenirAbonnes();
            DataGridAbonnes.DataContext = _abonnes;
        }
    }
}

